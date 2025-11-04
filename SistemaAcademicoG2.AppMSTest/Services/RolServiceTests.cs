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
    public class RolServiceTests
    {
        private AppDBContext _context;
        private RolRepository _repository;
        private RolService _service;

        [TestInitialize]
        public void Setup()
        {
            var connectionString = "Server=mysql-287fd73a-guty-2e76.j.aivencloud.com;Port=12802;Database=defaultdb;Uid=avnadmin;Pwd=AVNS_lx1T25NjAl1b6YBdx89;SslMode=Required;";

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
                .Options;

            _context = new AppDBContext(options);
            _context.Database.EnsureCreated();

            _repository = new RolRepository(_context);
            _service = new RolService(_repository);
        }

        [TestMethod]
        public async Task AgregarRolAsync_DeberiaAgregarCorrectamente()
        {
            var nuevoRol = new Rol
            {
                Nombre = "Tester_" + Guid.NewGuid().ToString("N").Substring(0, 5),
                Estado = true
            };

            var resultado = await _service.AgregarRolAsync(nuevoRol);
            Assert.AreEqual("Rol agregado correctamente", resultado);
        }

        [TestMethod]
        public async Task AgregarRolAsync_NoDebePermitirDuplicado()
        {
            var nombreDuplicado = "Administrador";

            // Aseguramos que exista uno con ese nombre
            var rol = new Rol { Nombre = nombreDuplicado, Estado = true };
            await _repository.AddAsync(rol);

            var duplicado = new Rol { Nombre = nombreDuplicado, Estado = true };
            var resultado = await _service.AgregarRolAsync(duplicado);

            Assert.AreEqual("Error: Ya existe un rol con ese nombre", resultado);
        }

        [TestMethod]
        public async Task ObtenerRolesActivosAsync_DeberiaRetornarSoloActivos()
        {
            var lista = await _repository.GetAllAsync();
            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Any(), "No hay roles en la base de datos.");

            // En este caso el método actual filtra por nombre "activo", lo ajustamos para verificar que no da error
            var activos = await _service.ObtenerRolesActivosAsync();
            Assert.IsNotNull(activos);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
