using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WepApi_1.Models
{
    public class User
    {


        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int online { get; set; }
    }
}