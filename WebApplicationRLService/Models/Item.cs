using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//не работает
namespace WebApplicationRLService.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Article { get; set; }
        public string Guid { get; set; }
        public double Stock { get; set; }
    }
}