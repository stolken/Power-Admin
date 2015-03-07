using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerAdmin.Models;
using System.Management.Automation;

namespace PowerAdmin.Logic
{
    public static class Database
    {
        public static void Reinit()
        {
            using (var db = new ScriptContext())
            {
                try
                {
                    bool b = db.Database.Delete();
                }
                catch
                {
                }
                db.Database.Initialize(true);
            }
        }



        public static void ResetQueryScript(string Query)
        {
        }

        public static void Seed(string PsQuery, bool Overwrite)
        {
            string[] TextBoxArray = { "System.String", "System.String[]" };
            string[] CheckBoxArray = { "System.Management.Automation.SwitchParameter" };
            ScriptContext context = new ScriptContext();
            int ParameterSetID;
            LogEvent.AddEvent(0, "Database", "Information", "Seed", "PowerAdmin");
            var myPowershell = PowerShell.Create();
            myPowershell.Commands.AddScript("$modules = get-module -ListAvailable; foreach($module in $modules){import-module $module}");
            myPowershell.Commands.AddScript("Get-command -CommandType Cmdlet " + PsQuery);
            var PSObjects = myPowershell.Invoke();
            foreach (PSObject PSObject in PSObjects)
            {
                int ModuleID;
                int ScriptID;
                CommandInfo CommandInfoObject = (CommandInfo)PSObject.BaseObject;
                CmdletInfo CmdletInfoObject = (CmdletInfo)CommandInfoObject;
                ScriptID = (from entry in context.Scripts where entry.Name.Equals(CmdletInfoObject.Name) select entry.ID).FirstOrDefault();
                if ((ScriptID != 0) && (Overwrite = true)) //if exist and Overwrite is true
                { //delete the script from db
                    PowerAdmin.Models.Script ScriptToDelete = context.Scripts.First(i => i.Name == CmdletInfoObject.Name);
                    context.Scripts.Remove(ScriptToDelete);
                    context.SaveChanges();
                }

                if ((Overwrite == true) || (ScriptID == 0)) //if overwrite is true OR the script is new, add the script to the DB
                {
                    ModuleID = (from entry in context.Categories where entry.Name.Equals(CmdletInfoObject.ModuleName) select entry.ID).FirstOrDefault();
                    if (ModuleID == 0) //if not exist then create category
                    {
                        Category category = new Category { Name = CmdletInfoObject.ModuleName };
                        context.Categories.Add(category);
                        context.SaveChanges();
                        ModuleID = category.ID;
                        LogEvent.AddEvent(ModuleID, "Category", "Information", "Add", "PowerAdmin");
                    }

                    Script script = new Script { Name = CmdletInfoObject.Name, CategoryID = ModuleID, Verb = CmdletInfoObject.Verb };
                    context.Scripts.Add(script);
                    context.SaveChanges();
                    ScriptID = script.ID;
                    LogEvent.AddEvent(ScriptID, "Script", "Information", "Add", "PowerAdmin");
                    foreach (CommandParameterSetInfo CommandParameterSetInfoObject in CommandInfoObject.ParameterSets)
                    {
                        ParameterSet parameterset = new ParameterSet { Name = CommandParameterSetInfoObject.Name, ScriptID = ScriptID };
                        context.ParameterSets.Add(parameterset);
                        context.SaveChanges();
                        ParameterSetID = parameterset.ID;
                        LogEvent.AddEvent(ParameterSetID, "ParameterSet", "Information", "Add", "PowerAdmin");
                        int Position = 0;
                        foreach (CommandParameterInfo CommandParameterInfoObject in CommandParameterSetInfoObject.Parameters)
                        {
                            string InputControl;
                            InputControl =  "none";
                            if (Array.Exists(TextBoxArray, element => element == CommandParameterInfoObject.ParameterType.ToString()))
                            {
                                InputControl = "TextBox";
                            }
                            if (Array.Exists(CheckBoxArray, element => element == CommandParameterInfoObject.ParameterType.ToString()))
                            {
                                InputControl = "CheckBox";
                            }

                            Parameter parameter = new Parameter { Name = CommandParameterInfoObject.Name, Type = CommandParameterInfoObject.ParameterType.ToString(), InputControl = InputControl, ParameterSetID = ParameterSetID, Position = Position };
                            context.Parameters.Add(parameter);
                            context.SaveChanges();
                            int ParameterID = parameter.ID;
                            LogEvent.AddEvent(ParameterID, "Parameter", "Information", "Add", "PowerAdmin");
                            Position = Position + 1;

                        }
                    }

                }





            }



        }




    }


}
