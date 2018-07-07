using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyOrderWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EasyOrderWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {       
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET: api/Login/5
        //[HttpGet("{id}")]
        //[Route("Login")]
        //public string Login(int id)
        //{
        //    return id.ToString();
        //}
        
        // POST: api/User/Login
        [HttpPost]
        [Route("Login")]
        public Response Post([FromBody]Credential credential)
        {
            return new Response
            {
                Allowed = true,
                Message = "All Ok"
            };
        }        
    }
}
