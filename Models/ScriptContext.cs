using PowerAdmin.Models;
using System.Data.Entity;
namespace PowerAdmin.Models
{
    public class ScriptContext : DbContext
    {
        public ScriptContext() : base("PowerAdmin")
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ParameterSet> ParameterSets { get; set; }
    }
}