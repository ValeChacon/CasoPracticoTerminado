namespace CasoPracticoDAL.Entidades
{
    public class Cita
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = "Ingresada";
        public string Observaciones { get; set; } = "";

        public Cliente Cliente { get; set; } = null!;
        public Vehiculo Vehiculo { get; set; } = null!;
    }
}
