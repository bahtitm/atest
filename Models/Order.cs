using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAtest.Models
{
    public class Order
    {


        public string  UserName { get; set; }
        public string Password   { get; set; }
        public int  Amount { get; set; }

        public string OrderNumber { get; set; }

        public string FailURL { get; set; }
        public string ReturnURL { get; set; }
        public string Descriptioin { get; set; }

        
    }
}
