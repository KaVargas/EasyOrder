using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyOrderWeb.Model
{
    public class ProductInfo
    {
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productPrice { get; set; }
        public string productAvailability { get; set; } 
        /*
         json
         {
            "productName":"example",
            "productDescription":"example",
            "productprice":0.00
         }
         */
    }
}
