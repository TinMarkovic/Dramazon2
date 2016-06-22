using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dramazon2.Web.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Fullname { get; set; }
    }
}