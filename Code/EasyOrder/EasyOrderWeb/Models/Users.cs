﻿using System;
using System.Collections.Generic;

namespace EasyOrderWeb.Models
{
    public partial class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
