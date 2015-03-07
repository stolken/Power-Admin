using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PowerAdmin.Models
{
    public class Event
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        public string Message { get; set; }

        public int ObjectID { get; set; }

        public string ObjectType { get; set; }

        public System.DateTime datetime { get; set; }

        public string Username { get; set; }

        public string MessageType { get; set; }
    }
}