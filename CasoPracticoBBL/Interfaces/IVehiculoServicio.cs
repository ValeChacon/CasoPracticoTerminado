using CasoPracticoBLL.Dtos;

namespace CasoPracticoBLL.Interfaces
{
    public interface IVehiculoServicio
    {
        Task<CustomResponse<List<VehiculoDto>>> ObtenerVehiculosAsync();
        Task<CustomResponse<VehiculoDto>> ObtenerVehiculoPorIdAsync(int id);
        Task<CustomResponse<VehiculoDto>> AgregarVehiculoAsync(VehiculoDto vehiculoDto);
        Task<CustomResponse<VehiculoDto>> ActualizarVehiculoAsync(VehiculoDto vehiculoDto);
        Task<CustomResponse<bool>> EliminarVehiculoAsync(int id);
    }
}
