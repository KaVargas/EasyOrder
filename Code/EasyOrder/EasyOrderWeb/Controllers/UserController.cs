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
    //The following class has the objective to crontroll everything that's user related in the web page
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly EasyorderContext _context;
        public UserController(EasyorderContext context)
        {
            _context = context;
        }

        //search the credentials in the database and gran access to the services if the credentials are found
        //if the are not found access will be denied
        #region login
            [HttpPost]
            [Route("Login")]
            public Response login([FromBody]Credential credential)
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
        #endregion

        //save the user related info in the DB for other services like login
        #region Register
            [HttpPost]
            [Route("Register")]
            public Response Register([FromBody]RegisterCredential newUser)
            {
                if(validatefullName(newUser.FullName)) return new Response { Allowed = false, Message = "Los nombres no pueden contener números" };
                //validate the citizens CI
                if (!validateCI(newUser.CI)) return new Response { Allowed = false, Message = "Cédula incorrecta"};
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
        #endregion

        //search the DB for an user with the same username
        #region registeredUser
            private bool registeredUser(RegisterCredential user)
            {
                //retrieve the username from the database
                var iduser = _context.Empleado.FirstOrDefault(x => x.Username == user.UserName);
                //if it's found return true
                if (iduser != null) return true;
                //else return false
                else return false;
            }
        #endregion

        //add a new register to 'Persona' table
        #region addPerson
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
        #endregion

        //add a new register to 'Empleado' table
        #region addEmployee
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
        #endregion

        //function for validating an Ecuadors citizen ID
        #region validateCI
            public bool validateCI(string CI)
            {
                int isNumeric;
                var total = 0;
                const int sizeofCI = 10;
                int[] coeficients = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
                const int provinces = 24;
                const int thirdDigit = 6;
                if(int.TryParse(CI, out isNumeric) && CI.Length == sizeofCI)
                {
                    var province = Convert.ToInt32(string.Concat(CI[0], CI[1], string.Empty));
                    var digit = Convert.ToInt32(CI[2] + string.Empty);
                    if((province > 0 && province <= provinces) && digit < thirdDigit)
                    {
                        var valDigit = Convert.ToInt32(CI[9] + string.Empty);
                        for(var k = 0; k < coeficients.Length; k++)
                        {
                            var value = Convert.ToInt32(coeficients[k] + string.Empty) 
                                * Convert.ToInt32(CI[k] + string.Empty);
                            total = value >= 10 ? total + (value - 9) : total + value;
                        }
                        var valDigitobtained = total >= 10 ? (total % 10) != 0 ? 
                            10 - (total % 10) : (total % 10) : total;
                        if (valDigit == valDigitobtained) return true;
                        else return false;
                    }
                }
                return false;
            }
        #endregion

        //function for validating a name, it can't contain any number at all
        #region validatefullName
            public bool validatefullName(string FullName)
            {
                string[] names = FullName.Split(" ");
                int result;
                foreach (var name in names)
                {
                    for(int i = 0; i< name.Length; i++)
                        if (Int32.TryParse(name[i] + string.Empty, out result)) return true;
                
                }
                return false;
            }
        #endregion
    }
}
