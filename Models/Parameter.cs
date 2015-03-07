using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PowerAdmin.Models
{
    public class Parameter
    {
        public Parameter()
        {
            Hidden = false;
        }

        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [StringLength(1000)]
        public string Type { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; }

        public int ParameterSetID { get; set; }
        public virtual ParameterSet ParameterSet { get; set; }

        [StringLength(1000), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public bool? Required { get; set; }

        public bool? Hidden { get; set; }

        public string InputControl { get; set; }



        public int? Position { get; set; }



    }
}