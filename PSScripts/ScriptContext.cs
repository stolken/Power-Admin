using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Power_Admin.PSScripts
{
    public class ScriptContext : DbContext
    {
        public ScriptContext() : base("Power-Admin") 
        {
        }
  
        public DbSet<Script> Scripts { get; set; }
    }
}