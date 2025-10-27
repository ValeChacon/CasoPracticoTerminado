using CasoPracticoDAL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Casopractico.DAL.Data
{
    public class LavadoAutosContexto : DbContext
    {
        public LavadoAutosContexto(DbContextOptions<LavadoAutosContexto> options)
            : base(options)
        {
        }

        // Tablas
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===========================
            // CONFIGURACIONES CLIENTE
            // ===========================
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Identificacion)
                      .IsRequired()
                      .HasMaxLength(50);

                // Relación Cliente -> Vehículos (1:N)
                entity.HasMany(c => c.Vehiculos)
                      .WithOne(v => v.Cliente)
                      .HasForeignKey(v => v.ClienteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ===========================
            // CONFIGURACIONES VEHÍCULO
            // ===========================
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.Marca).HasMaxLength(50);
                entity.Property(v => v.Modelo).HasMaxLength(50);
                entity.Property(v => v.Color).HasMaxLength(30);
                entity.Property(v => v.Placa)
                      .IsRequired()
                      .HasMaxLength(20);

                // Relación Vehículo -> Citas (1:N)
                entity.HasMany(v => v.Citas)
                      .WithOne(c => c.Vehiculo)
                      .HasForeignKey(c => c.VehiculoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ===========================
            // CONFIGURACIONES CITA
            // ===========================
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Fecha)
                      .IsRequired();

                entity.Property(c => c.Estado)
                      .HasMaxLength(30)
                      .HasDefaultValue("Ingresada");

                entity.Property(c => c.Observaciones)
                      .HasMaxLength(250);

                // Relación Cita -> Cliente (N:1)
                entity.HasOne(c => c.Cliente)
                      .WithMany()
                      .HasForeignKey(c => c.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación Cita -> Vehículo (N:1)
                entity.HasOne(c => c.Vehiculo)
                      .WithMany(v => v.Citas)
                      .HasForeignKey(c => c.VehiculoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
