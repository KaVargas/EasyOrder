﻿using System;
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

        // POST: api/User/Login
        [HttpPost]
        [Route("Login")]
        public Response Post([FromBody]Credential credential)
        {
            try
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
            catch
            {
                return new Response
                {
                    Allowed = false,
                    Message = "Internal server error"
                
                };
            }
        }

        [HttpPost]
        [Route("Register")]
        public Response Register([FromBody]RegisterCredential newUser)
        {
            //it's only possible to have one unique username per employee
            //validate if the username has been occupied by other employee, if true, ask for another username
            if (registeredUser(newUser)) return new Response { Allowed = false, Message = "The Username is already in use" };
            //search in the DB if the person has been registered already, if not, he or she will be added 
            var existingPerson =
                _context.Persona.FirstOrDefault(x => x.Cedulapersona == newUser.CI && x.Nombrepersona == newUser.FullName);
            if (existingPerson == null)
            {
                //if the person doesn't exists, it's registered in the DB
                addPerson(newUser);
                //and an employee is added too
                addEmployee(newUser);
            }
            else
            {
                //if the person has been already registered, it will be only necesary to add the employee
                addEmployee(newUser);
            }
            return new Response
            {
                Allowed = true,
                Message = "User added successfully."
            };
        }

        //search the DB for an user with the same username
        private bool registeredUser(RegisterCredential user)
        {
            var iduser = _context.Empleado.FirstOrDefault(x => x.Username == user.UserName);
            if (iduser != null) return true;
            else return false;
        }

        //add a new register to 'Persona' table
        private void addPerson(RegisterCredential newUser)
        {
            _context.Persona.Add(
                   new Persona
                   {
                       Nombrepersona = newUser.FullName,
                       Cedulapersona = newUser.CI,
                       Telefonopersona = newUser.PhoneNumber,
                       Idpersona = Guid.NewGuid()
                   });

            _context.SaveChanges();
        }

        //add a new register to 'Empleado' table
        private void addEmployee(RegisterCredential newUser)
        {

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
        }
    }
}
