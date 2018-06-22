using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyOrderWeb.Model
{
    public class Employee
    {
        //atributes, declare private atributes with underscore
        private String _name, _lastname, _userName, _password, _email, _phoneNumber, _type;

        //getters and setters
        public string Name { get => _name; set => _name = value; }
        public string Lastname { get => _lastname; set => _lastname = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public string Email { get => _email; set => _email = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Type { get => _type; set => _type = value; }

        //Constructors
        public Employee()
        {
            Name = "";
            Lastname = "";
            UserName = "";
            Password = "";
            Email = "";
            PhoneNumber = "";
            Type = "";
        }

        public Employee(String name, String lastname, String username, String password, String email, String phonenumber, String type)
        {
            Name = name;
            Lastname = lastname;
            UserName = username;
            Password = password;
            Email = email;
            PhoneNumber = phonenumber;
            Type = type;
        }
    }
}
