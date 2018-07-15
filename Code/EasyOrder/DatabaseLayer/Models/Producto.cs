using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Detalledeorden = new HashSet<Detalledeorden>();
        }

        public Guid Idproducto { get; set; }
        public string Nombreproducto { get; set; }
        public string Descripcionproducto { get; set; }
        public double Precioproducto { get; set; }
        public int? Disponibilidadproducto { get; set; }

        public ICollection<Detalledeorden> Detalledeorden { get; set; }
    }
}
