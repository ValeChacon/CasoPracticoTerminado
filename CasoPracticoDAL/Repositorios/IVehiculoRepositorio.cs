using CasoPracticoDAL.Entidades;

namespace CasoPracticoDAL.Repositorios
{
    public interface IVehiculoRepositorio
    {
        Task<IEnumerable<Vehiculo>> ListarAsync();
        Task<Vehiculo> ObtenerPorIdAsync(int id);
        Task<bool> RegistrarAsync(Vehiculo vehiculo);
        Task<bool> EditarAsync(Vehiculo vehiculo);
        Task<bool> EliminarAsync(int id);
    }
}
