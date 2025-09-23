using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class AsistenciaService
    {
        private readonly IAsistenciaRepository _repository;

        public AsistenciaService(IAsistenciaRepository repository)
        {
            _repository = repository;
        }

        // Caso de uso: Obtener asistencias por fecha
        public async Task<IEnumerable<Asistencia>> ObtenerPorFechaAsync(DateTime fecha)
        {
            return await _repository.GetByFechaAsync(fecha);
        }

        // Caso de uso: Agregar asistencia (evitar duplicados por nombre y fecha)
        public async Task<string> AgregarAsistenciaAsync(Asistencia nuevaAsistencia)
        {
            try
            {
                var existe = await _repository.AsistenciaExistsAsync(nuevaAsistencia.Id);
                if (existe)
                    return "Error: Ya existe una asistencia registrada para ese estudiante en esa fecha";

                await _repository.AddAsync(nuevaAsistencia);
                return "Asistencia registrada correctamente";
            }
            catch (Exception ex)
            {
                return "Error de servidor: " + ex.Message;
            }
        }
    }
}
