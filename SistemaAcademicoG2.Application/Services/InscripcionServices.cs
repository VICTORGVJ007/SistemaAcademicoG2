//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using SistemaAcademico.Domain.Entities;
//using SistemaAcademicoG2.Domain.Repositories;
//using SistemaAcademicoG2.DTOs;

//namespace SistemaAcademicoG2.Application.Services
//{
//    public class InscripcionService
//    {
//        private readonly IInscripcionRepository _repository;

//        public InscripcionService(IInscripcionRepository repository)
//        {
//            _repository = repository;
//        }

//        // 🔹 Obtener todas las inscripciones (DTO)
//        public async Task<IEnumerable<InscripcionDTO>> ObtenerTodasAsync()
//        {
//            var data = await _repository.GetAllAsync();

//            return data.Select(i => new InscripcionDTO
//            {
//                IdInscripcion = i.IdInscripcion,
//                IdUsuario = i.IdUsuario,
//                NombreUsuario = i.Usuario?.Nombre ?? string.Empty,
//                IdGrado = i.IdGrado,
//                NombreGrado = i.Grado?.Nombre ?? string.Empty,
//                FechaIngreso = i.FechaIngreso,
//                AnioLectivo = i.AnioLectivo,
//                Estado = i.Estado
//            }).ToList();
//        }

//        // 🔹 Obtener por Id (DTO)
//        public async Task<InscripcionDTO?> ObtenerPorIdAsync(int idInscripcion)
//        {
//            var i = await _repository.GetByIdAsync(idInscripcion);
//            if (i == null) return null;

//            return new InscripcionDTO
//            {
//                IdInscripcion = i.IdInscripcion,
//                IdUsuario = i.IdUsuario,
//                NombreUsuario = i.Usuario?.Nombre ?? string.Empty,
//                IdGrado = i.IdGrado,
//                NombreGrado = i.Grado?.Nombre ?? string.Empty,
//                FechaIngreso = i.FechaIngreso,
//                AnioLectivo = i.AnioLectivo,
//                Estado = i.Estado
//            };
//        }

//        // 🔹 Agregar (recibe DTO, convierte a entidad)
//        public async Task<string> AgregarInscripcionAsync(InscripcionDTO dto)
//        {
//            try
//            {
//                var nuevaInscripcion = new Inscripcion
//                {
//                    IdInscripcion = dto.IdInscripcion,
//                    IdUsuario = dto.IdUsuario,
//                    IdGrado = dto.IdGrado,
//                    FechaIngreso = dto.FechaIngreso,
//                    AnioLectivo = dto.AnioLectivo,
//                    Estado = dto.Estado
//                };

//                var existe = await _repository.InscripcionExistsAsync(dto.IdInscripcion);
//                if (existe)
//                    return "Error: Ya existe una inscripción con ese Id";

//                await _repository.AddAsync(nuevaInscripcion);
//                return "Inscripción registrada correctamente";
//            }
//            catch (Exception ex)
//            {
//                return "Error de servidor: " + ex.Message;
//            }
//        }

//        // 🔹 Actualizar (DTO → Entidad)
//        public async Task<string> ActualizarInscripcionAsync(InscripcionDTO dto)
//        {
//            try
//            {
//                var existe = await _repository.InscriptionExistsAsync(dto.IdInscripcion);
//                if (!existe)
//                    return "Error: No se encontró la inscripción";

//                var inscripcion = new Inscripcion
//                {
//                    IdInscripcion = dto.IdInscripcion,
//                    IdUsuario = dto.IdUsuario,
//                    IdGrado = dto.IdGrado,
//                    FechaIngreso = dto.FechaIngreso,
//                    AnioLectivo = dto.AnioLectivo,
//                    Estado = dto.Estado
//                };

//                await _repository.UpdateAsync(inscripcion);
//                return "Inscripción actualizada correctamente";
//            }
//            catch (Exception ex)
//            {
//                return "Error de servidor: " + ex.Message;
//            }
//        }

//        // 🔹 Cambiar estado sin modificar DTO
//        public async Task<string> CambiarEstadoAsync(int id)
//        {
//            try
//            {
//                var existe = await _repository.InscripcionExistsAsync(id);
//                if (!existe)
//                    return "Error: No se encontró la inscripción";

//                await _repository.CambiarEstadoAsync(id);
//                return "Estado cambiado correctamente";
//            }
//            catch (Exception ex)
//            {
//                return "Error de servidor: " + ex.Message;
//            }
//        }

//        // 🔹 Get por grado (DTO)
//        public async Task<IEnumerable<InscripcionDTO>> ObtenerPorGradoAsync(int idGrado)
//        {
//            var data = await _repository.GetByGradoIdAsync(idGrado);

//            return data.Select(i => new InscripcionDTO
//            {
//                IdInscripcion = i.IdInscripcion,
//                IdUsuario = i.IdUsuario,
//                NombreUsuario = i.Usuario?.Nombre ?? string.Empty,
//                IdGrado = i.IdGrado,
//                NombreGrado = i.Grado?.Nombre ?? string.Empty,
//                FechaIngreso = i.FechaIngreso,
//                AnioLectivo = i.AnioLectivo,
//                Estado = i.Estado
//            }).ToList();
//        }

//        // 🔹 Get por usuario (DTO)
//        public async Task<IEnumerable<InscripcionDTO>> ObtenerPorUsuarioAsync(int idUsuario)
//        {
//            var data = await _repository.GetByUsuarioIdAsync(idUsuario);

//            return data.Select(i => new InscripcionDTO
//            {
//                IdInscripcion = i.IdInscripcion,
//                IdUsuario = i.IdUsuario,
//                NombreUsuario = i.Usuario?.Nombre ?? string.Empty,
//                IdGrado = i.IdGrado,
//                NombreGrado = i.Grado?.Nombre ?? string.Empty,
//                FechaIngreso = i.FechaIngreso,
//                AnioLectivo = i.AnioLectivo,
//                Estado = i.Estado
//            }).ToList();
//        }
//    }
//}
