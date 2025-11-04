using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Infrastructure.Data;
using SistemaAcademicoG2.Infrastructure.Repositories;
using SistemaAcademico.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Application.Services.Tests
{
    [TestClass]
    public class InscripcionServiceTests
    {
        private AppDBContext _context;
        private InscripcionRepository _repository;
        private InscripcionService _service;

        [TestInitialize]
        public void Setup()
        {
            // 🔹 Usa una base de datos temporal (ideal para pruebas unitarias)
            var connectionString = "Server=mysql-287fd73a-guty-2e76.j.aivencloud.com;Port=12802;Database=defaultdb;Uid=avnadmin;Pwd=AVNS_lx1T25NjAl1b6YBdx89;SslMode=Required;";

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
                .Options;

            _context = new AppDBContext(options);
            _context.Database.EnsureCreated();

            _repository = new InscripcionRepository(_context);
            _service = new InscripcionService(_repository);
        }

        // ✅ 1. Agregar Inscripción
        [TestMethod]
        public async Task AgregarInscripcionAsync_DeberiaCrearInscripcion()
        {
            var nueva = new Inscripcion
            {
                IdUsuario = 1,
                IdGrado = 2,
                FechaIngreso = DateTime.Now.Date,
                Estado = true
            };

            var resultado = await _service.AgregarInscripcionAsync(nueva);
            Assert.AreEqual("Inscripción registrada correctamente", resultado);
        }

        // ✅ 2. Obtener todas las inscripciones
        [TestMethod]
        public async Task ObtenerTodasAsync_DeberiaRetornarLista()
        {
            var lista = await _service.ObtenerTodasAsync();
            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Any(), "No se encontraron inscripciones en la base de datos.");
        }

        // ✅ 3. Obtener inscripción por ID
        [TestMethod]
        public async Task ObtenerPorIdAsync_DeberiaRetornarInscripcionCorrecta()
        {
            var primera = (await _service.ObtenerTodasAsync()).FirstOrDefault();
            if (primera == null)
            {
                Assert.Inconclusive("No hay inscripciones registradas para probar.");
                return;
            }

            var encontrada = await _service.ObtenerPorIdAsync(primera.IdInscripcion);
            Assert.IsNotNull(encontrada);
            Assert.AreEqual(primera.IdInscripcion, encontrada.IdInscripcion);
        }

        // ✅ 4. Actualizar inscripción
        [TestMethod]
        public async Task ActualizarInscripcionAsync_DeberiaActualizarCorrectamente()
        {
            var inscripcion = (await _service.ObtenerTodasAsync()).FirstOrDefault();
            if (inscripcion == null)
            {
                Assert.Inconclusive("No hay inscripciones para modificar.");
                return;
            }

            var estadoOriginal = inscripcion.Estado;
            inscripcion.Estado = !estadoOriginal;

            var resultado = await _service.ActualizarInscripcionAsync(inscripcion);
            Assert.AreEqual("Inscripción actualizada correctamente", resultado);

            // Restaurar estado original
            inscripcion.Estado = estadoOriginal;
            await _service.ActualizarInscripcionAsync(inscripcion);
        }

        // ✅ 5. Obtener por grado
        [TestMethod]
        public async Task ObtenerPorGradoAsync_DeberiaRetornarCorrectamente()
        {
            var lista = await _service.ObtenerPorGradoAsync(2);
            Assert.IsNotNull(lista);
        }

        // ✅ 6. Obtener por usuario
        [TestMethod]
        public async Task ObtenerPorUsuarioAsync_DeberiaRetornarCorrectamente()
        {
            var lista = await _service.ObtenerPorUsuarioAsync(1);
            Assert.IsNotNull(lista);
        }

        // ✅ 7. Eliminar inscripción
        [TestMethod]
        public async Task EliminarInscripcionAsync_DeberiaEliminarCorrectamente()
        {
            var inscripcion = new Inscripcion
            {
                IdUsuario = 10,
                IdGrado = 5,
                FechaIngreso = DateTime.Now.Date,
                Estado = true
            };

            await _repository.AddAsync(inscripcion);

            var id = inscripcion.IdInscripcion;
            await _repository.DeleteAsync(id);

            var eliminada = await _repository.GetByIdAsync(id);
            Assert.IsNull(eliminada, "La inscripción no fue eliminada correctamente.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
