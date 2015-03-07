using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PowerAdmin.Models
{
    public class Category
    {

        public Category()
        {
            Hidden = false;
        }

        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public bool? Hidden { get; set; }

        public int? Position { get; set; }

        public virtual ICollection<Script> Scripts { get; set; }
    }
}