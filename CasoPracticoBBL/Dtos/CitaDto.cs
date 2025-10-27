namespace CasoPracticoBLL.Dtos
{
    public class CitaDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int VehiculoId { get; set; }
        public string PlacaVehiculo { get; set; } = string.Empty;
        public string Observaciones { get; set; }

        // Fecha unificada
        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = "Ingresada";
    }
}
