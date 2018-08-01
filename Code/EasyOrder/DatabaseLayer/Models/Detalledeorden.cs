using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Detalledeorden
    {
        public Guid Idorden { get; set; }
        public Guid Idproducto { get; set; }
        public string Nombreproducto { get; set; }
        public Guid Iddetalle { get; set; }
        public decimal? Precioparcial { get; set; }
        public int? Cantproducto { get; set; }

        public Orden IdordenNavigation { get; set; }
        public Producto IdproductoNavigation { get; set; }
    }
}
