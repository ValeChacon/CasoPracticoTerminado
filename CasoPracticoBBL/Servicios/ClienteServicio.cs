using Casopractico.DAL.Interfaces;
using CasoPracticoBLL.Dtos;
using CasoPracticoBLL.Interfaces;
using CasoPracticoDAL.Entidades;

namespace Casopractico.BLL.Servicios
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _repositorio;

        public ClienteServicio(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<CustomResponse<List<ClienteDto>>> ObtenerClientesAsync()
        {
            var clientes = await _repositorio.ListarAsync();

            var dtos = clientes.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Identificacion = c.Identificacion,
                FechaNacimiento = c.FechaNacimiento,
                Edad = CalcularEdad(c.FechaNacimiento)
            }).ToList();

            return new CustomResponse<List<ClienteDto>>(dtos);
        }

        public async Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(int id)
        {
            var cliente = await _repositorio.ObtenerPorIdAsync(id);
            if (cliente == null)
                return new CustomResponse<ClienteDto> { EsError = true, Mensaje = "Cliente no encontrado" };

            var dto = new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Identificacion = cliente.Identificacion,
                FechaNacimiento = cliente.FechaNacimiento,
                Edad = CalcularEdad(cliente.FechaNacimiento)
            };

            return new CustomResponse<ClienteDto>(dto);
        }

        public async Task<CustomResponse<string>> AgregarClienteAsync(ClienteDto dto)
        {
            // Validar edad mínima
            if (CalcularEdad(dto.FechaNacimiento) < 18)
                return new CustomResponse<string> { EsError = true, Mensaje = "El cliente debe ser mayor de edad" };

            // Validar identificación única
            var existe = await _repositorio.BuscarPorIdentificacionAsync(dto.Identificacion);
            if (existe != null)
                return new CustomResponse<string> { EsError = true, Mensaje = "Ya existe un cliente con esa identificación" };

            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Identificacion = dto.Identificacion,
                FechaNacimiento = dto.FechaNacimiento
            };

            await _repositorio.AgregarAsync(cliente);
            return new CustomResponse<string>("Cliente agregado correctamente");
        }

        public async Task<CustomResponse<string>> ActualizarClienteAsync(ClienteDto dto)
        {
            var cliente = await _repositorio.ObtenerPorIdAsync(dto.Id);
            if (cliente == null)
                return new CustomResponse<string> { EsError = true, Mensaje = "Cliente no encontrado" };

            cliente.Nombre = dto.Nombre;
            cliente.Identificacion = dto.Identificacion;
            cliente.FechaNacimiento = dto.FechaNacimiento;

            await _repositorio.ActualizarAsync(cliente);
            return new CustomResponse<string>("Cliente actualizado correctamente");
        }

        public async Task<CustomResponse<bool>> EliminarClienteAsync(int id)
        {
            var ok = await _repositorio.EliminarAsync(id);
            return new CustomResponse<bool>
            {
                Data = ok,
                EsError = !ok,
                Mensaje = ok ? "" : "No se pudo eliminar el cliente"
            };
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now.Date < fechaNacimiento.AddYears(edad)) edad--;
            return edad;
        }
    }
}
