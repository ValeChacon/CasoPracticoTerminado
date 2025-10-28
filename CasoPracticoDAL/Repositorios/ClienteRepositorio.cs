using Casopractico.DAL.Data;
using Casopractico.DAL.Interfaces;
using CasoPracticoDAL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CasoPracticoDAL.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly LavadoAutosContexto _context;
        public ClienteRepositorio(LavadoAutosContexto context) => _context = context;
        public async Task<List<Cliente>> ListarAsync() =>
            await _context.Clientes.Include(c => c.Vehiculos).ToListAsync();

        public async Task<Cliente> ObtenerPorIdAsync(int id) =>
            await _context.Clientes.Include(c => c.Vehiculos)
                                   .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<bool> AgregarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Cliente?> BuscarPorIdentificacionAsync(string identificacion)
        {
            return await _context.Clientes
                                 .FirstOrDefaultAsync(c => c.Identificacion == identificacion);
        }
    }
}
