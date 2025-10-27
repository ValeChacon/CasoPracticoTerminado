using Casopractico.DAL.Interfaces;
using CasoPracticoBLL.Dtos;
using CasoPracticoBLL.Interfaces;
using CasoPracticoDAL.Entidades;
using CasoPracticoDAL.Repositorios;

namespace CasoPractico.BLL.Servicios
{
    public class VehiculoServicio : IVehiculoServicio
    {
        private readonly IVehiculoRepositorio _repositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public VehiculoServicio(IVehiculoRepositorio repositorio, IClienteRepositorio clienteRepositorio)
        {
            _repositorio = repositorio;
            _clienteRepositorio = clienteRepositorio;
        }

        // Obtener todos los vehículos
        public async Task<CustomResponse<List<VehiculoDto>>> ObtenerVehiculosAsync()
        {
            var vehiculos = await _repositorio.ListarAsync();
            var dto = new List<VehiculoDto>();

            foreach (var v in vehiculos)
            {
                var cliente = await _clienteRepositorio.ObtenerPorIdAsync(v.ClienteId);
                dto.Add(new VehiculoDto
                {
                    Id = v.Id,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Placa = v.Placa,
                    Color = v.Color,
                    ClienteId = v.ClienteId,
                    NombreCliente = cliente != null ? cliente.Nombre : ""
                });
            }

            return new CustomResponse<List<VehiculoDto>>(dto);
        }

        // Obtener vehículo por Id
        public async Task<CustomResponse<VehiculoDto>> ObtenerVehiculoPorIdAsync(int id)
        {
            var v = await _repositorio.ObtenerPorIdAsync(id);
            if (v == null)
                return new CustomResponse<VehiculoDto> { EsError = true, Mensaje = "Vehículo no encontrado" };

            var cliente = await _clienteRepositorio.ObtenerPorIdAsync(v.ClienteId);

            var dto = new VehiculoDto
            {
                Id = v.Id,
                Marca = v.Marca,
                Modelo = v.Modelo,
                Placa = v.Placa,
                Color = v.Color,
                ClienteId = v.ClienteId,
                NombreCliente = cliente != null ? cliente.Nombre : ""
            };

            return new CustomResponse<VehiculoDto>(dto);
        }

        // Agregar vehículo
        public async Task<CustomResponse<VehiculoDto>> AgregarVehiculoAsync(VehiculoDto dto)
        {
            var vehiculo = new Vehiculo
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                Color = dto.Color,
                ClienteId = dto.ClienteId
            };

            await _repositorio.RegistrarAsync(vehiculo);
            dto.Id = vehiculo.Id;

            return new CustomResponse<VehiculoDto>(dto);
        }

        // Actualizar vehículo (solución al problema de tracking)
        public async Task<CustomResponse<VehiculoDto>> ActualizarVehiculoAsync(VehiculoDto dto)
        {
            // Obtener el vehículo existente desde el repositorio
            var vehiculoExistente = await _repositorio.ObtenerPorIdAsync(dto.Id);
            if (vehiculoExistente == null)
                return new CustomResponse<VehiculoDto> { EsError = true, Mensaje = "Vehículo no encontrado" };

            // Actualizar propiedades
            vehiculoExistente.Marca = dto.Marca;
            vehiculoExistente.Modelo = dto.Modelo;
            vehiculoExistente.Placa = dto.Placa;
            vehiculoExistente.Color = dto.Color;
            vehiculoExistente.ClienteId = dto.ClienteId;

            await _repositorio.EditarAsync(vehiculoExistente);
            return new CustomResponse<VehiculoDto>(dto);
        }

        // Eliminar vehículo
        public async Task<CustomResponse<bool>> EliminarVehiculoAsync(int id)
        {
            var ok = await _repositorio.EliminarAsync(id);
            return new CustomResponse<bool>
            {
                Data = ok,
                EsError = !ok,
                Mensaje = ok ? "" : "No se pudo eliminar el vehículo"
            };
        }
    }
}
