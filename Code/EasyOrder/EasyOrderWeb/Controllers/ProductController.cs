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
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly EasyorderContext _context;
        public ProductController(EasyorderContext context)
        {
            _context = context;
        }
        
        // POST: api/Product
        [HttpPost]
        [Route("newproduct")]
        public Response NewProduct([FromBody]ProductInfo newProductinfo)
        {
            _context.Producto.Add(
                new Producto
                {
                    Descripcionproducto = newProductinfo.productDescription,
                    Nombreproducto = newProductinfo.productName,
                    Precioproducto = newProductinfo.productPrice,
                    Idproducto = Guid.NewGuid(),
                    Disponibilidadproducto = newProductinfo.productAvailability
                });
            _context.SaveChanges();
            return new Response
            {
                Allowed = true,
                Message = "product added succesfully"
            };
        }
        
        
    }
}
