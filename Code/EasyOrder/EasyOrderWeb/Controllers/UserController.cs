using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using EasyOrderWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EasyOrderWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {

        private readonly EasyorderContext _context;
        public UserController(EasyorderContext context)
        {
            _context = context;
        }


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

            var existingUser = _context.Empleado.FirstOrDefault(x => x.Username == credential.UserName);

            if (existingUser != null)
            {
                if (existingUser.Password == credential.Password)
                {
                    return new Response
                    {
                        Allowed = true,
                        Message = "All Ok."
                    };
                }
            }
            return new Response
            {
                Allowed = false,
                Message = "User or Password is wrong."
            };
        }

        [HttpPost]
        [Route("Register")]
        public Response Register([FromBody]RegisterCredential newUser)
        {
            _context.Persona.Add(
                new Persona
                {
                    Nombrepersona = newUser.FullName,
                    Cedulapersona = newUser.CI,
                    Telefonopersona = newUser.PhoneNumber,
                    Cumpleanospersona = null,
                    Idpersona = Guid.NewGuid()
                });
            _context.SaveChanges();
            _context.Empleado.Add(
                new Empleado
                {
                    Username = newUser.UserName,
                    Password = newUser.Password,
                    Idrestaurante = _context.Restaurante.Where(x => x.Nombrerestaurante == "Uchu Manka").Select(x => x.Idrestaurante).FirstOrDefault(),
                    Idpersona = _context.Persona.Where(x => x.Nombrepersona == newUser.FullName && x.Cedulapersona == newUser.CI).Select(x => x.Idpersona).FirstOrDefault(),
                    Idempleado = Guid.NewGuid()
                });
            _context.SaveChanges();
            return new Response
            {
                Allowed = true,
                Message = "User added successfully."
            };
        }
    }
}
