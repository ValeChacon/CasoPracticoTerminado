namespace CasoPracticoDAL.Entidades
{
    public class Cliente
    {
        public object? Telefonos;

        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Identificacion { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    }
}
