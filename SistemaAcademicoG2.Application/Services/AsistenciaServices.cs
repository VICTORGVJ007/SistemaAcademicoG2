using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Application.Services
{
    public class AsistenciaService
    {
        private readonly IAsistenciaRepository _repository;

        public AsistenciaService(IAsistenciaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Asistencia>> ObtenerActivasAsync() =>
            await _repository.GetActivasAsync();

        public async Task<IEnumerable<Asistencia>> ObtenerInactivasAsync() =>
            await _repository.GetInactivasAsync();

        public async Task<IEnumerable<Asistencia>> ObtenerPorFechaAsync(DateTime fecha) =>
            await _repository.GetByFechaAsync(fecha);

        public async Task<IEnumerable<Asistencia>> ObtenerPorUsuarioAsync(int idUsuario) =>
            await _repository.GetByUsuarioAsync(idUsuario);

        public async Task<string> AgregarAsistenciaAsync(Asistencia asistencia)
        {
            await _repository.AddAsync(asistencia);
            return "Asistencia registrada correctamente";
        }

        public async Task<string> ModificarAsync(Asistencia asistencia)
        {
            await _repository.UpdateAsync(asistencia);
            return "Asistencia actualizada correctamente";
        }

        public async Task<string> DesactivarAsync(int id)
        {
            var ok = await _repository.DesactivarAsync(id);
            return ok ? "Asistencia desactivada" : "Error: asistencia no encontrada";
        }

        public async Task<string> ActivarAsync(int id)
        {
            var ok = await _repository.ActivarAsync(id);
            return ok ? "Asistencia activada" : "Error: asistencia no encontrada";
        }
    }
}

