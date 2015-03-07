using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerAdmin.Models;

namespace PowerAdmin.Logic
{
    public class AddScripts
    {
        public bool AddScript(string ScriptName, string ScriptDesc,  string ScriptCategory, string ScriptCode)
        {
            var myScript = new Script();            
            myScript.Description = ScriptDesc;
            myScript.CategoryID = Convert.ToInt32(ScriptCategory);

            // Get DB context.
            ScriptContext _db = new ScriptContext();

            // Add Script to DB.
            _db.Scripts.Add(myScript);
            _db.SaveChanges();

            // Success.
            return true;
        }

















    }
}