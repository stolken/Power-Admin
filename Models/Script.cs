using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PowerAdmin.Models
{
    public class Script
    {

        public Script()
        {
            Hidden = false;
        }

        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(10000), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ParameterSet> ParameterSets { get; set; }

        public bool? Hidden { get; set; }

        public int? Position { get; set; }

        [StringLength(100)]
        public string Verb { get; set; }
    }
}