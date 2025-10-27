using CasoPracticoBLL.Dtos;

namespace CasoPracticoBLL.Interfaces
{
    public interface IClienteServicio
    {
        Task<CustomResponse<List<ClienteDto>>> ObtenerClientesAsync();
        Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(int id);
        Task<CustomResponse<string>> AgregarClienteAsync(ClienteDto clienteDto);
        Task<CustomResponse<string>> ActualizarClienteAsync(ClienteDto clienteDto);
        Task<CustomResponse<bool>> EliminarClienteAsync(int id);
    }
}
