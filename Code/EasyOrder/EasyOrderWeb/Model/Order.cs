using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyOrderWeb.Model
{
    public class Order
    {
        public int NumeroMesa { get; set; }
        public string NombreEmpleado { get; set; }
        public List<Plato> Platos { get; set; }
    }
}
