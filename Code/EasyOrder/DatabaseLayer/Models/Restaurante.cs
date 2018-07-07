using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Restaurante
    {
        public Restaurante()
        {
            Empleado = new HashSet<Empleado>();
        }

        public string Nombrerestaurante { get; set; }
        public string Direccionrestaurante { get; set; }
        public string Rucrestaurante { get; set; }
        public string Fonorestaurante { get; set; }
        public Guid Idrestaurante { get; set; }

        public ICollection<Empleado> Empleado { get; set; }
    }
}
