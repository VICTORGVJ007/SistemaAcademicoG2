using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Infrastructure.Data;
using SistemaAcademicoG2.Infrastructure.Repositories;
using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Application.Services.Tests
{
    [TestClass]
    public class AsignaturaServiceTests
    {
        private AppDBContext _context;
        private AsignaturaRepository _repository;
        private AsignaturaService _service;

        [TestInitialize]
        public void Setup()
        {
            var connectionString = "Server=mysql-287fd73a-guty-2e76.j.aivencloud.com;Port=12802;Database=defaultdb;Uid=avnadmin;Pwd=AVNS_lx1T25NjAl1b6YBdx89;SslMode=Required;";

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
                .Options;

            _context = new AppDBContext(options);

            // Verifica conexión
            _context.Database.EnsureCreated();

            _repository = new AsignaturaRepository(_context);
            _service = new AsignaturaService(_repository);
        }

        [TestMethod]
        public async Task AgregarAsignaturaAsync_DeberiaCrearAsignatura()
        {
            var nueva = new Asignatura
            {
                Nombre = "Algebra",
                Estado = true
            };

            var resultado = await _service.AgregarAsignaturaAsync(nueva);
            Assert.AreEqual("Asignatura agregada correctamente", resultado);
        }

        [TestMethod]
        public async Task ObtenerAsignaturasActivasAsync_DeberiaRetornarSoloActivas()
        {
            var lista = await _service.ObtenerAsignaturasActivasAsync();
            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.All(a => a.Estado), "Se encontró una asignatura inactiva");
        }

        [TestMethod]
        public async Task ObtenerAsignaturaPorIdAsync_DeberiaRetornarAsignaturaActiva()
        {
            var primera = (await _service.ObtenerAsignaturasActivasAsync()).FirstOrDefault();
            if (primera == null)
            {
                Assert.Inconclusive("No hay asignaturas activas en la base de datos para probar.");
                return;
            }

            var encontrada = await _service.ObtenerAsignaturaPorIdAsync(primera.Id);
            Assert.IsNotNull(encontrada);
            Assert.AreEqual(primera.Id, encontrada.Id);
        }

        [TestMethod]
        public async Task ModificarAsignaturaAsync_DeberiaActualizarCorrectamente()
        {
            var asignatura = (await _service.ObtenerAsignaturasActivasAsync()).FirstOrDefault();
            if (asignatura == null)
            {
                Assert.Inconclusive("No hay asignaturas activas en la base de datos para modificar.");
                return;
            }

            var nombreOriginal = asignatura.Nombre;
            asignatura.Nombre = "Matemáticas";

            var resultado = await _service.ModificarAsignaturaAsync(asignatura);
            Assert.AreEqual("Asignatura modificada correctamente", resultado);

            // Restaurar
            asignatura.Nombre = nombreOriginal;
            await _service.ModificarAsignaturaAsync(asignatura);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}

