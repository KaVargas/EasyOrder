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

        public OrderController(EasyorderContext context) { _context = context; }

        #region new order
        [HttpPost]
        [Route("newOrder")]
        public Response NewOrder(OrderInfo orderInfo, Credential credential)
        {
            return new Response
            {
                Allowed = true,
                Message = "Successful"
            };
        }
        #endregion

        #region add Order to DB
        private void AddOrder(OrderInfo orderInfo, Credential credential)
        {
            Guid id = Guid.NewGuid();
            _context.Orden.Add(
                new Orden
                {
                    Numeromesa = orderInfo.numMesa,
                    Idempleado = _context.Empleado.Where(x => x.Username == credential.UserName).Select(x => x.Idempleado).FirstOrDefault(),
                    Idpersona = _context.Empleado.Where(x => x.Username == credential.UserName).Select(x => x.Idpersona).FirstOrDefault(),
                    Idorden = id
                    // = getTotalPrice(orderInfo)
                });
            _context.SaveChanges();
            OrderDetails(orderInfo, id);
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
            string[] orders = orderInfo.platoCantidad.Split(',');
            for (int i = 0; i < orders.Length; i++)
            {
                string[] quantityperplate = orders[i].Split(':');
                _context.Detalledeorden.Add(
                    new Detalledeorden
                    {
                        Iddetalle = Guid.NewGuid(),
                        Idorden = orderID,
                        Idproducto = _context.Producto.Where(x => x.Nombreproducto == quantityperplate[0]).Select(x => x.Idproducto).FirstOrDefault()
                    }
                );
            }
            _context.SaveChanges();
            return true;
        }
        #endregion
    }
}