using System.Collections.Generic;
using System.Data.Entity;
using System.Management.Automation;
using System.Linq;
using PowerAdmin.Logic;

namespace PowerAdmin.Models
{
    public class ScriptDatabaseInitializer : DropCreateDatabaseIfModelChanges<ScriptContext>
    {
        protected override void Seed(ScriptContext context)
        {
            PowerAdmin.Logic.Database.Seed("", true);
        }
    }
}