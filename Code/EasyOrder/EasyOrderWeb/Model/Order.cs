using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyOrderWeb.Model
{
    public class Order
    {
        public Guid OrderNumber { get; set; }
        public int NumeroMesa { get; set; }
        public string NombreEmpleado { get; set; }
        public string EstadoOrden { get; set; }
        public List<Plato> Platos { get; set; }
    }
}
