using Casopractico.DAL.Data;
using CasoPracticoDAL.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasoPracticoDAL.Repositorios
{
    public class VehiculoRepositorio : IVehiculoRepositorio
    {
        private readonly LavadoAutosContexto _context;

        public VehiculoRepositorio(LavadoAutosContexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> ListarAsync()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        public async Task<Vehiculo> ObtenerPorIdAsync(int id)
        {
            return await _context.Vehiculos.FindAsync(id);
        }

        public async Task<bool> RegistrarAsync(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            var cambios = await _context.SaveChangesAsync();
            return cambios > 0;
        }

        public async Task<bool> EditarAsync(Vehiculo vehiculo)
        {
            // Cargar entidad existente
            var vehiculoExistente = await _context.Vehiculos
                .FirstOrDefaultAsync(v => v.Id == vehiculo.Id);

            if (vehiculoExistente == null) return false;

            // Actualizar propiedades
            vehiculoExistente.Marca = vehiculo.Marca;
            vehiculoExistente.Modelo = vehiculo.Modelo;
            vehiculoExistente.Placa = vehiculo.Placa;
            vehiculoExistente.Color = vehiculo.Color;
            vehiculoExistente.ClienteId = vehiculo.ClienteId;

            var cambios = await _context.SaveChangesAsync();
            return cambios > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null) return false;

            _context.Vehiculos.Remove(vehiculo);
            var cambios = await _context.SaveChangesAsync();
            return cambios > 0;
        }
    }
}

