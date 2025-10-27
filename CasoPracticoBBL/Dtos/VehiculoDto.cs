namespace CasoPracticoBLL.Dtos
{
    public class VehiculoDto
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
    }
}
