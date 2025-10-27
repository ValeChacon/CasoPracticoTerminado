using System.Collections.Generic;

namespace CasoPracticoDAL.Entidades
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
