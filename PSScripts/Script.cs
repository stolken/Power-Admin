using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Power_Admin.PSScripts
{
    public class Script
    {

        [ScaffoldColumn(false)]
        public int ScriptID { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string Name { get; set; }

        [Required, StringLength(10000), Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, StringLength(10000), Display(Name = "Code"), DataType(DataType.MultilineText)]
        public string Code { get; set; }

    }
}