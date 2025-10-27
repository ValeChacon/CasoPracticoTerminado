using CasoPracticoDAL.Entidades;

namespace CasoPracticoDAL.Repositorios
{
    public interface ICitaRepositorio
    {
        Task<IEnumerable<Cita>> ListarAsync();
        Task<Cita> ObtenerPorIdAsync(int id);
        Task<bool> RegistrarAsync(Cita cita);
        Task<bool> EditarAsync(Cita cita);
        Task<bool> EliminarAsync(int id);
    }
}
