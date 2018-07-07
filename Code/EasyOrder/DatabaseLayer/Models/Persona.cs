using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Empleado = new HashSet<Empleado>();
        }

        public string Nombrepersona { get; set; }
        public string Cedulapersona { get; set; }
        public string Telefonopersona { get; set; }
        public DateTime? Cumpleanospersona { get; set; }
        public Guid Idpersona { get; set; }

        public ICollection<Empleado> Empleado { get; set; }
    }
}
