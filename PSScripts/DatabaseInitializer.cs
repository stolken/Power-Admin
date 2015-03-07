using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;

namespace Power_Admin.PSScripts
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ScriptContext>
    {
        protected override void Seed(ScriptContext context)
        {
            GetScripts().ForEach(c => context.Scripts.Add(c));           
        }

        private static List<Script> GetScripts()
        {
            var Scripts = new List<Script> {
            new Script
                {
                    ScriptID = 1,
                    Name = "Get old users",
                    Description = "This convertible car is fast! The engine is powered by a neutrino based battery (not included)." + 
                                  "Power it up and let it go!", 
                    Code="get-aduser -identity blala",
                 
               },
               new Script
                {
                    ScriptID = 2,
                    Name = "Get old computers",
                    Description = "This convertible car is fast! The engine is powered by a neutrino based battery (not included)." + 
                                  "Power it up and let it go!", 
                    Code="get-adcomputer -identity blala",
                 
               }
            };

            return Scripts;

        }

    }
}