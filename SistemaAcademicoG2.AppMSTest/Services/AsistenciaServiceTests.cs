using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Application.Services.Tests
{
    [TestClass]
    public class AsistenciaServiceTests
    {
        private AppDBContext _context;
        private AsistenciaRepository _repository;
        private AsistenciaService _service;

        [TestInitialize]
        public void Setup()
        {
            // 🔹 Cadena de conexión
            var connectionString = "Server=mysql-287fd73a-guty-2e76.j.aivencloud.com;Port=12802;Database=defaultdb;Uid=avnadmin;Pwd=AVNS_lx1T25NjAl1b6YBdx89;SslMode=Required;";

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
                .Options;

            _context = new AppDBContext(options);

            _context.Database.EnsureCreated();

            _repository = new AsistenciaRepository(_context);
            _service = new AsistenciaService(_repository);
        }

        [TestMethod]
        public async Task AgregarAsistenciaAsync_DeberiaAgregarCorrectamente()
        {
            var nueva = new Asistencia
            {
                IdUsuario = 1,
                IdGrado = 1,
                Fecha = DateTime.Now.Date,
                Estado = true
            };

            var resultado = await _service.AgregarAsistenciaAsync(nueva);
            Assert.AreEqual("Asistencia registrada correctamente", resultado);
        }

        [TestMethod]
        public async Task ObtenerPorFechaAsync_DeberiaRetornarAsistenciasDeLaFecha()
        {
            var fecha = DateTime.Now.Date;

            // Asegurar que haya al menos una asistencia registrada para la fecha
            await _repository.AddAsync(new Asistencia
            {
                IdUsuario = 2,
                IdGrado = 1,
                Fecha = fecha,
                Estado = true
            });

            var asistencias = await _service.ObtenerPorFechaAsync(fecha);

            Assert.IsNotNull(asistencias);
            Assert.IsTrue(asistencias.All(a => a.Fecha.Date == fecha));
        }

        [TestMethod]
        public async Task ObtenerPorUsuarioAsync_DeberiaRetornarAsistenciasDelUsuario()
        {
            int idUsuario = 3;

            // Crear asistencia si no hay ninguna
            await _repository.AddAsync(new Asistencia
            {
                IdUsuario = idUsuario,
                IdGrado = 2,
                Fecha = DateTime.Now.Date,
                Estado = true
            });

            var lista = await _repository.GetByUsuarioAsync(idUsuario);

            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.All(a => a.IdUsuario == idUsuario));
        }

        [TestMethod]
        public async Task ModificarAsistenciaAsync_DeberiaActualizarCorrectamente()
        {
            var asistencia = (await _repository.GetAllAsync()).FirstOrDefault();

            if (asistencia == null)
            {
                asistencia = new Asistencia
                {
                    IdUsuario = 4,
                    IdGrado = 1,
                    Fecha = DateTime.Now.Date,
                    Estado = true
                };
                await _repository.AddAsync(asistencia);
            }

            asistencia.Estado = !asistencia.Estado;
            await _repository.UpdateAsync(asistencia);

            var actualizada = await _repository.GetByIdAsync(asistencia.IdAsistencia);
            Assert.AreEqual(asistencia.Estado, actualizada.Estado);
        }

        [TestMethod]
        public async Task EliminarAsistenciaAsync_DeberiaEliminarCorrectamente()
        {
            var nueva = new Asistencia
            {
                IdUsuario = 5,
                IdGrado = 1,
                Fecha = DateTime.Now.Date,
                Estado = true
            };

            await _repository.AddAsync(nueva);
            int id = nueva.IdAsistencia;

            await _repository.DeleteAsync(id);
            var eliminada = await _repository.GetByIdAsync(id);

            Assert.IsNull(eliminada);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}


