using Casopractico.DAL.Data;
using CasoPracticoDAL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CasoPracticoDAL.Repositorios
{
    public class CitaRepositorio : ICitaRepositorio
    {
        private readonly LavadoAutosContexto _contexto;

        public CitaRepositorio(LavadoAutosContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Cita>> ListarAsync()
        {
            return await _contexto.Citas
                .Include(c => c.Vehiculo)
                .ThenInclude(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<Cita?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Citas
                .Include(c => c.Vehiculo)
                .ThenInclude(v => v.Cliente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> RegistrarAsync(Cita cita)
        {
            await _contexto.Citas.AddAsync(cita);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditarAsync(Cita cita)
        {
            var existente = await _contexto.Citas.FindAsync(cita.Id);
            if (existente == null)
                return false;

            existente.Fecha = cita.Fecha;
            existente.Estado = cita.Estado;
            existente.Observaciones = cita.Observaciones;
            existente.VehiculoId = cita.VehiculoId;

            _contexto.Citas.Update(existente);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var cita = await _contexto.Citas.FindAsync(id);
            if (cita == null)
                return false;

            _contexto.Citas.Remove(cita);
            return await _contexto.SaveChangesAsync() > 0;
        }
    }
}
