using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Orden
    {
        public Orden()
        {
            Detalledeorden = new HashSet<Detalledeorden>();
        }

        public Guid Idorden { get; set; }
        public Guid Idpersona { get; set; }
        public Guid Idempleado { get; set; }
        public int? Numeromesa { get; set; }
        public double? Preciototal { get; set; }
        public string Estadoorden { get; set; }

        public Empleado Id { get; set; }
        public ICollection<Detalledeorden> Detalledeorden { get; set; }
    }
}
