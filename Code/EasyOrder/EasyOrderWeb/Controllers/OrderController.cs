using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.Models;
using EasyOrderWeb.Model;

namespace EasyOrderWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly EasyorderContext _context;
        private static Guid[] Orderbuffer = new Guid[4];
        private static int OrderCount = 0;
        public OrderController(EasyorderContext context) { _context = context; }

        #region new order
        [HttpPost]
        [Route("add")]
        public Response NewOrder([FromBody]OrderInfo orderInfo)
        {
            try
            {
                AddOrder(orderInfo);
                return new Response
                {
                    Allowed = true,
                    Message = "Successful"
                };
            }
            catch {
                return new Response
                {
                    Allowed = false,
                    Message = "Error"
                };
            }
            
        }
        #endregion

        #region add Order to DB
        private void AddOrder(OrderInfo orderInfo)
        {
            Guid id = Guid.NewGuid();
            int tablenumber;
            int.TryParse(orderInfo.numMesa, out tablenumber);
            OrderBuff(id);
            _context.Orden.Add(
                new Orden
                {
                    Numeromesa = tablenumber, 
                    Idempleado = _context.Empleado.Where(x => x.Username == orderInfo.UserName).Select(x => x.Idempleado).FirstOrDefault(),
                    Idpersona = _context.Empleado.Where(x => x.Username == orderInfo.UserName).Select(x => x.Idpersona).FirstOrDefault(),
                    Idorden = id,
                    Preciototal = GetTotalPrice(orderInfo)
                });
            _context.SaveChanges();
            OrderDetails(orderInfo, id);
        }
        #endregion

        #region establish order buffer
        private void OrderBuff(Guid guid)
        {
            if (OrderCount == 3) OrderCount = 0;
            Orderbuffer[OrderCount] = guid;
            OrderCount++;
        }

        #endregion
        #region calculate total Price
        private double GetTotalPrice(OrderInfo orderInfo)
        {
            double cantTotal = 0.0;
            string[] orders = orderInfo.platoCantidad.Split(',');
            for(int i = 0; i < orders.Length; i++)
            {
                string[] quantityperplate = orders[i].Split(':');
                cantTotal += float.Parse(quantityperplate[1])*(float)_context.Producto.Where(x => x.Nombreproducto == quantityperplate[0]).Select(x => x.Precioproducto).FirstOrDefault();
            }
            return cantTotal;
        }
        #endregion

        #region order detailing
        private bool OrderDetails(OrderInfo orderInfo, Guid orderID)
        {
            int k;
            string[] orders = orderInfo.platoCantidad.Split(',');
            for (int i = 0; i < orders.Length; i++)
            {
                string[] quantityperplate = orders[i].Split(':');
                int.TryParse(quantityperplate[1], out k);
                _context.Detalledeorden.Add(
                    new Detalledeorden
                    {
                        Iddetalle = Guid.NewGuid(),
                        Idorden = orderID,
                        Idproducto = _context.Producto.Where(x => x.Nombreproducto == quantityperplate[0]).Select(x => x.Idproducto).FirstOrDefault(),
                        Cantproducto = k,
                        Nombreproducto = quantityperplate[0],
                        Precioparcial = (decimal)_context.Producto.Where(x => x.Nombreproducto == quantityperplate[0]).Select(x => x.Precioproducto).FirstOrDefault(),
                        
                    }
                );
                _context.SaveChanges();
            }
            
            return true;
        }
        #endregion

        #region
        [HttpPost]
        [Route("Last Orders")]
        public OrderInfo[] GetLast4Orders()
        {
            Guid test = new Guid();
            string platoCantidad = "";
            OrderInfo[] orders = new OrderInfo[4];
            for(int i = 0; i < 4; i++)
            {
                if (Orderbuffer[i] == test) break;
                var mesa = _context.Orden.Where(x => x.Idorden == Orderbuffer[i]).Select(x => x.Numeromesa);
                //retrive all the order detail associated with the ID saved on the buffer
                IEnumerable<Detalledeorden> detalle = _context.Orden.Where(x=>x.Idorden==Orderbuffer[i]).SelectMany(x => x.Detalledeorden);
                //for each detail, retrieve the product name and quantity so a string is constructed to send back to the page
                int k = 0;
                foreach (var item in detalle)
                {
                    if(k!=0) platoCantidad += ",";
                    var productName = item.Nombreproducto;
                    var quantity = item.Cantproducto;
                    string tmp = productName + ":" + quantity;
                    platoCantidad += tmp;
                    k++;
                }
                orders[i] = new OrderInfo { numMesa = mesa+string.Empty, platoCantidad = platoCantidad}; 
            }
            return orders;
        }
        #endregion
    }
}