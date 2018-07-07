using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
