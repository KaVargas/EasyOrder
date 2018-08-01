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
        [Route("add")]
        public Response NewProduct([FromBody]ProductInfo newProductinfo)
        {
            if(AddProduct(newProductinfo))
            return new Response
            {
                Allowed = true,
                Message = "product added succesfully"
            };
            else return new Response
            {
                Allowed = false,
                Message = "error"
            };
        }

        private bool AddProduct(ProductInfo productInfo)
        {
            try
            {
                _context.Producto.Add(
                    new Producto
                    {
                        Descripcionproducto = productInfo.productDescription,
                        Nombreproducto = productInfo.productName,
                        Precioproducto = float.Parse(productInfo.productPrice),
                        Idproducto = Guid.NewGuid(),
                        Disponibilidadproducto = int.Parse(productInfo.productAvailability)
                    });
                _context.SaveChanges();
            }
            catch { return false; }
            return true;
        }
        
        
    }
}
