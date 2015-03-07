using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PowerAdmin.Models
{
    public class ParameterSet
    {
        public ParameterSet()
        {
            Hidden = false;
        }

        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public int ScriptID { get; set; }
        public virtual Script Script { get; set; }

        public bool? Hidden { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }

    }
}