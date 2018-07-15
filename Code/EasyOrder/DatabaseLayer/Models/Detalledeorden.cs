using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Detalledeorden
    {
        public Guid Idorden { get; set; }
        public Guid Idproducto { get; set; }
        public Guid Iddetalle { get; set; }

        public Orden IdordenNavigation { get; set; }
        public Producto IdproductoNavigation { get; set; }
    }
}
