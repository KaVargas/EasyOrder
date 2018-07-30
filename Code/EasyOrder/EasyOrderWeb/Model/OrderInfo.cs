using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyOrderWeb.Model
{
    public class OrderInfo
    {
        public int numMesa { get; set; }
        public string platoCantidad { get; set; }
        ///{
        /// "numMesa":1
        /// "platoCantidad":"tilapia:2,maito:3,tamales:0"
        ///}
    }
}
