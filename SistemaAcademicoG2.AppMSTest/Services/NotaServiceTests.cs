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
    public class NotaServiceTests
    {
        private AppDBContext _context;
        private NotaRepository _repository;
        private NotaService _service;

        [TestInitialize]
        public void Setup()
        {
            var connectionString = "Server=mysql-287fd73a-guty-2e76.j.aivencloud.com;Port=12802;Database=defaultdb;Uid=avnadmin;Pwd=AVNS_lx1T25NjAl1b6YBdx89;SslMode=Required;";

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
                .Options;

            _context = new AppDBContext(options);

            // Verifica conexión o crea si no existe
            _context.Database.EnsureCreated();

            _repository = new NotaRepository(_context);
            _service = new NotaService(_repository);
        }

        [TestMethod]
        public async Task AgregarNotaAsync_DeberiaAgregarNotaCorrectamente()
        {
            // ⚙️ Arrange
            var nueva = new Nota
            {
                IdUsuario = 1,
                IdAsignatura = 1,
                Periodo = $"Primer periodo {DateTime.Now.Year}",
                NotaFinal = 8.5m,
                Estado = true
            };

            // 🧪 Act
            var resultado = await _service.AgregarNotaAsync(nueva);

            // ✅ Assert
            Assert.AreEqual("Nota agregada correctamente", resultado);
        }

        [TestMethod]
        public async Task ObtenerNotasActivasAsync_DeberiaRetornarSoloActivas()
        {
            var lista = await _service.ObtenerNotasActivasAsync();

            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.All(n => n.Estado), "Se encontró una nota inactiva");
        }

        [TestMethod]
        public async Task ObtenerNotaPorIdAsync_DeberiaRetornarSoloActiva()
        {
            var primera = (await _service.ObtenerNotasActivasAsync()).FirstOrDefault();
            if (primera == null)
            {
                Assert.Inconclusive("No hay notas activas en la base de datos para probar.");
                return;
            }

            var encontrada = await _service.ObtenerNotaPorIdAsync(primera.Id);
            Assert.IsNotNull(encontrada);
            Assert.AreEqual(primera.Id, encontrada.Id);
        }

        [TestMethod]
        public async Task ModificarNotaAsync_DeberiaActualizarNotaCorrectamente()
        {
            var nota = (await _service.ObtenerNotasActivasAsync()).FirstOrDefault();
            if (nota == null)
            {
                Assert.Inconclusive("No hay notas activas en la base de datos para modificar.");
                return;
            }

            var notaOriginal = nota.NotaFinal;
            nota.NotaFinal = nota.NotaFinal >= 9 ? 6 : 9; // Cambiar valor

            var resultado = await _service.ModificarNotaAsync(nota);

            Assert.AreEqual("Nota modificada correctamente", resultado);

            // Restaurar valor original
            nota.NotaFinal = notaOriginal;
            await _service.ModificarNotaAsync(nota);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
