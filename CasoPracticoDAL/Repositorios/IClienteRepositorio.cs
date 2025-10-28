using CasoPracticoDAL.Entidades;

namespace Casopractico.DAL.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<Cliente>> ListarAsync();
        Task<Cliente> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(int id);
        Task<Cliente?> BuscarPorIdentificacionAsync(string identificacion);
    }
}
