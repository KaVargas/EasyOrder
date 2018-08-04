using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.Models;
using EasyOrderWeb.Model;
using EasyOrderWeb.Signalr;
using Microsoft.AspNetCore.SignalR;

namespace EasyOrderWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly EasyorderContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        private static Guid[] Orderbuffer = { new Guid(), new Guid(), new Guid(), new Guid()}; 
        private static int OrderCount = 0;

        public OrderController(EasyorderContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
            
        }

        #region new order
        [HttpPost]
        [Route("add")]
        public Response NewOrder([FromBody]Order order)
        {
            try
            {
                AddOrder(order); 
                _hubContext.Clients.All.SendAsync("ReceiveMessage", Newtonsoft.Json.JsonConvert.SerializeObject(order));
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
        private Guid AddOrder(Order orderInfo)
        {
            Guid id = Guid.NewGuid();
            
            OrderBuff(id);
            _context.Orden.Add(
                new Orden
                {
                    Numeromesa = orderInfo.NumeroMesa,
                    Idempleado = _context.Empleado.Where(x => x.Username == orderInfo.NombreEmpleado).Select(x => x.Idempleado).FirstOrDefault(),
                    Idpersona = _context.Empleado.Where(x => x.Username == orderInfo.NombreEmpleado).Select(x => x.Idpersona).FirstOrDefault(),
                    Idorden = id,
                    Preciototal = GetTotalPrice(orderInfo.Platos)
                });
            _context.SaveChanges();
            OrderDetails(orderInfo.Platos, id);
            return id;
        }
        #endregion

        #region establish order buffer
        private void OrderBuff(Guid guid)
        {
            if (OrderCount == 4) OrderCount = 0;
            Orderbuffer[OrderCount] = guid;
            OrderCount++;
        }

        #endregion

        #region calculate total Price
        private double GetTotalPrice(List<Plato> platos)
        {
            double cantTotal = 0.0;
            foreach (var plato in platos)
            {
                if (plato.Cantidad == 0) break;
                cantTotal += 
                    plato.Cantidad * _context.Producto.Where(x => x.Nombreproducto == plato.Nombre).Select(x => x.Precioproducto).FirstOrDefault();
            }
            return cantTotal;
        }
        #endregion

        #region order detailing
        private bool OrderDetails(List<Plato> platos, Guid orderID)
        {
            foreach (var plato in platos)
            {
                if (plato.Cantidad == 0) break;
                _context.Detalledeorden.Add(
                    new Detalledeorden
                    {
                        Iddetalle = Guid.NewGuid(),
                        Idorden = orderID,
                        Idproducto = _context.Producto.Where(x => x.Nombreproducto == plato.Nombre).Select(x => x.Idproducto).FirstOrDefault(),
                        Cantproducto = plato.Cantidad,
                        Nombreproducto = plato.Nombre,
                        Precioparcial = (decimal)_context.Producto.Where(x => x.Nombreproducto == plato.Nombre).Select(x => x.Precioproducto).FirstOrDefault()
                    }
                );
                _context.SaveChanges();
            }
            
            return true;
        }
        #endregion

        #region get the last 4 orders
        [HttpPost]
        [Route("Last Orders")]
        public Order[] GetLast4Orders()
        {
            Guid test = new Guid();
            Order[] orders = new Order[4];
            List<Plato> platos = new List<Plato>();
            for (int i = 0; i < 4; i++)
            {
                platos.Clear();
                if (Orderbuffer[i] == test) break;
                var mesa = _context.Orden.Where(x => x.Idorden == Orderbuffer[i]).Select(x => x.Numeromesa); 
                //retrive all the order detail associated with the ID saved on the buffer
                IEnumerable<Detalledeorden> detalle = _context.Orden.Where(x=>x.Idorden==Orderbuffer[i]).SelectMany(x => x.Detalledeorden);
                //for each detail, retrieve the product name and quantity so it´s possible to create an object to send back information
                foreach (var item in detalle)
                {
                    platos.Add(new Plato { Cantidad = int.Parse(item.Cantproducto.ToString()), Nombre = item.Nombreproducto});
                }
                orders[i] = new Order { NumeroMesa = int.Parse(mesa.ToString()), Platos = platos}; 
            }
            return orders;
        }
        #endregion
    }
}