using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Orden = new HashSet<Orden>();
        }

        public Guid Idpersona { get; set; }
        public Guid Idempleado { get; set; }
        public Guid Idrestaurante { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public Persona IdpersonaNavigation { get; set; }
        public Restaurante IdrestauranteNavigation { get; set; }
        public ICollection<Orden> Orden { get; set; }
    }
}
