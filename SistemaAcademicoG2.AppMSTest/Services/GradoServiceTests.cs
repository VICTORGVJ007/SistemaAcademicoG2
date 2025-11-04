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
    public class GradoServiceTests
    {
        private AppDBContext _context;
        private GradoRepository _repository;
        private GradoService _service;

        [TestInitialize]
        public void Setup()
        {
            // 🔹 Base de datos temporal para pruebas
            var connectionString = "Server=mysql-287fd73a-guty-2e76.j.aivencloud.com;Port=12802;Database=defaultdb;Uid=avnadmin;Pwd=AVNS_lx1T25NjAl1b6YBdx89;SslMode=Required;";

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
                .Options;

            _context = new AppDBContext(options);
            _context.Database.EnsureCreated();

            _repository = new GradoRepository(_context);
            _service = new GradoService(_repository);
        }

        // ✅ 1. Agregar Grado
        [TestMethod]
        public async Task AgregarGradoAsync_DeberiaAgregarCorrectamente()
        {
            var nuevo = new Grado
            {
                Nombre = "Grado de Prueba " + Guid.NewGuid().ToString("N").Substring(0, 5),
                Estado = true
            };

            var resultado = await _service.AgregarGradoAsync(nuevo);
            Assert.AreEqual("Grado registrado correctamente", resultado);
        }

        // ✅ 2. Obtener todos los grados
        [TestMethod]
        public async Task ObtenerTodosAsync_DeberiaRetornarListaDeGrados()
        {
            var lista = await _service.ObtenerTodosAsync();
            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Any(), "No se encontraron grados en la base de datos.");
        }

        // ✅ 3. Obtener grado por ID
        [TestMethod]
        public async Task ObtenerPorIdAsync_DeberiaRetornarGradoCorrecto()
        {
            var primero = (await _service.ObtenerTodosAsync()).FirstOrDefault();
            if (primero == null)
            {
                Assert.Inconclusive("No hay grados disponibles para probar.");
                return;
            }

            var resultado = await _service.ObtenerPorIdAsync(primero.IdGrado);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(primero.IdGrado, resultado.IdGrado);
        }

        // ✅ 4. Actualizar grado
        [TestMethod]
        public async Task ActualizarGradoAsync_DeberiaActualizarCorrectamente()
        {
            var grado = (await _service.ObtenerTodosAsync()).FirstOrDefault();
            if (grado == null)
            {
                Assert.Inconclusive("No hay grados para modificar.");
                return;
            }

            var nombreOriginal = grado.Nombre;
            grado.Nombre = "Grado Actualizado " + Guid.NewGuid().ToString("N").Substring(0, 4);

            var resultado = await _service.ActualizarGradoAsync(grado);
            Assert.AreEqual("Grado actualizado correctamente", resultado);

            // Restaurar
            grado.Nombre = nombreOriginal;
            await _service.ActualizarGradoAsync(grado);
        }

        // ✅ 5. Eliminar grado
        [TestMethod]
        public async Task EliminarGradoAsync_DeberiaEliminarCorrectamente()
        {
            var nuevo = new Grado
            {
                Nombre = "Grado Temporal " + Guid.NewGuid().ToString("N").Substring(0, 5),
                Estado = true
            };

            await _repository.AddAsync(nuevo);
            var id = nuevo.IdGrado;

            await _repository.DeleteAsync(id);

            var eliminado = await _repository.GetByIdAsync(id);
            Assert.IsNull(eliminado, "El grado no fue eliminado correctamente.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
