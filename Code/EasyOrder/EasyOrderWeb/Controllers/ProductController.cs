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
        // GET: api/Product
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
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
                    Idproducto = Guid.NewGuid()
                });
            _context.SaveChanges();
            return new Response
            {
                Allowed = true,
                Message = "product added succesfully"
            };
        }
        
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
