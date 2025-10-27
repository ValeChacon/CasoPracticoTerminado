using CasoPracticoBLL.Dtos;

namespace CasoPracticoBLL.Interfaces
{
    public interface ICitaServicio
    {
        Task<CustomResponse<List<CitaDto>>> ListarDtoAsync();
        Task<CustomResponse<CitaDto>> ObtenerPorIdAsync(int id);
        Task<CustomResponse<string>> RegistrarAsync(CitaDto citaDto);
        Task<CustomResponse<string>> CambiarEstadoAsync(int id, string estado);
        Task<CustomResponse<bool>> EliminarAsync(int id);
    }
}
