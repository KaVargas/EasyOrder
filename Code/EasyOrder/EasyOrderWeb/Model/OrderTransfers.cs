using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.Models; 

namespace EasyOrderWeb.Model
{
    public class OrderTransfers
    {
        public Guid OrderID { get; set; }
        public List<Detalledeorden> Productos { get; set; }

        public OrderTransfers(OrderInfo orderInfo, Guid id)
        {
            OrderID = id;
            Productos = DivideOrder(orderInfo.platoCantidad);
        }

        private List<Detalledeorden> DivideOrder(string order)
        {
            string[] plates = order.Split(",");
            List<Detalledeorden> productos = new List<Detalledeorden>();
            foreach (var item in plates)
            {
                string[] dish_quantity = item.Split(":");
                productos.Add(new Detalledeorden {
                    Nombreproducto = dish_quantity[0],
                    Cantproducto = int.Parse(dish_quantity[1])
                });
            }
            return productos;
        }
    }
}
