namespace CasoPracticoBLL.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }  // <<--- Agregar
        public int Edad { get; set; }                   // <<--- Agregar
    }
}
