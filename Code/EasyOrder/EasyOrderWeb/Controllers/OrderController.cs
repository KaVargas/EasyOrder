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
        private static Guid[] Orderbuffer = { new Guid(), new Guid(), new Guid(), new Guid() };
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
                var orderId = AddOrder(order);
                order.OrderNumber = orderId;
                order.Platos = order.Platos.Where(x => x.Cantidad != 0).ToList();
                _hubContext.Clients.All.SendAsync("ReceiveMessage", Newtonsoft.Json.JsonConvert.SerializeObject(order));
                return new Response
                {
                    Allowed = true,
                    Message = "Successful"
                };
            }
            catch
            {
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
                    Preciototal = GetTotalPrice(orderInfo.Platos),
                    Estadoorden = orderInfo.EstadoOrden
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

        #region get orders
        [HttpGet]
        [Route("GetOrders")]
        public List<Order> GetOrders(int amount)
        {
            return _context.Orden.Where(x => x.Estadoorden == "Espera")
                .Take(amount)
                .Select(x => new Order
                {
                    OrderNumber = x.Idorden,
                    EstadoOrden = x.Estadoorden,
                    NumeroMesa = x.Numeromesa.Value,
                    Platos = _context.Detalledeorden.Where(y => y.Idorden == x.Idorden)
                    .Where(y => y.Cantproducto.Value != 0)
                    .Select(y => new Plato
                    {
                        Nombre = y.Nombreproducto,
                        Cantidad = y.Cantproducto.Value
                    }).ToList()
                }).ToList();
        }

        [HttpGet]
        [Route("DispatchOrder")]
        public bool DispatchOrder(Guid orderId)
        {
            var order = _context.Orden.FirstOrDefault(x => x.Idorden == orderId);
            if (order != null)
            {
                order.Estadoorden = "Terminado";
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
    }
}