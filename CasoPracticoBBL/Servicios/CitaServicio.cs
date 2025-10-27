using Casopractico.DAL.Interfaces;
using CasoPracticoBLL.Dtos;
using CasoPracticoBLL.Interfaces;
using CasoPracticoDAL.Repositorios;
using CasoPracticoDAL.Entidades; // <- AÑADIDO


namespace Casopractico.BLL.Servicios
{
    public class CitaServicio : ICitaServicio
    {
        private readonly ICitaRepositorio _repositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IVehiculoRepositorio _vehiculoRepositorio;

        public CitaServicio(ICitaRepositorio repositorio,
                            IClienteRepositorio clienteRepositorio,
                            IVehiculoRepositorio vehiculoRepositorio)
        {
            _repositorio = repositorio;
            _clienteRepositorio = clienteRepositorio;
            _vehiculoRepositorio = vehiculoRepositorio;
        }

        public async Task<CustomResponse<List<CitaDto>>> ListarDtoAsync()
        {
            var citas = await _repositorio.ListarAsync();
            var dto = new List<CitaDto>();

            foreach (var c in citas)
            {
                var cliente = await _clienteRepositorio.ObtenerPorIdAsync(c.ClienteId);
                var vehiculo = await _vehiculoRepositorio.ObtenerPorIdAsync(c.VehiculoId);

                dto.Add(new CitaDto
                {
                    Id = c.Id,
                    ClienteId = c.ClienteId,
                    VehiculoId = c.VehiculoId,
                    NombreCliente = cliente?.Nombre ?? "",
                    PlacaVehiculo = vehiculo?.Placa ?? "",
                    Fecha = c.Fecha, // ✅ unificado
                    Estado = c.Estado
                });
            }

            return new CustomResponse<List<CitaDto>>(dto);
        }

        public async Task<CustomResponse<CitaDto>> ObtenerPorIdAsync(int id)
        {
            var c = await _repositorio.ObtenerPorIdAsync(id);
            if (c == null)
                return new CustomResponse<CitaDto> { EsError = true, Mensaje = "Cita no encontrada" };

            var cliente = await _clienteRepositorio.ObtenerPorIdAsync(c.ClienteId);
            var vehiculo = await _vehiculoRepositorio.ObtenerPorIdAsync(c.VehiculoId);

            var dto = new CitaDto
            {
                Id = c.Id,
                ClienteId = c.ClienteId,
                VehiculoId = c.VehiculoId,
                NombreCliente = cliente?.Nombre ?? "",
                PlacaVehiculo = vehiculo?.Placa ?? "",
                Fecha = c.Fecha, // ✅ unificado
                Estado = c.Estado
            };

            return new CustomResponse<CitaDto>(dto);
        }

        public async Task<CustomResponse<string>> RegistrarAsync(CitaDto dto)
        {
            var c = new Cita
            {
                ClienteId = dto.ClienteId,
                VehiculoId = dto.VehiculoId,
                Fecha = dto.Fecha, // ✅ unificado
                Estado = dto.Estado ?? "Ingresada"
            };

            await _repositorio.RegistrarAsync(c);
            return new CustomResponse<string>("Cita registrada correctamente");
        }

        public async Task<CustomResponse<string>> CambiarEstadoAsync(int id, string estado)
        {
            var c = await _repositorio.ObtenerPorIdAsync(id);
            if (c == null)
                return new CustomResponse<string> { EsError = true, Mensaje = "Cita no encontrada" };

            c.Estado = estado;
            await _repositorio.EditarAsync(c);

            return new CustomResponse<string>("Estado actualizado");
        }

        public async Task<CustomResponse<bool>> EliminarAsync(int id)
        {
            var ok = await _repositorio.EliminarAsync(id);
            return new CustomResponse<bool> { Data = ok, EsError = !ok, Mensaje = ok ? "" : "No se pudo eliminar la cita" };
        }
    }
}
