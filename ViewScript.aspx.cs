﻿using PowerAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management.Automation;
using PowerAdmin.Logic;
using System.Text;
using System.Data;

namespace PowerAdmin
{
    public partial class ViewScript : System.Web.UI.Page
    {
        PowerAdmin.Models.Script script;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["MoveUpParameter"] != null)
            {
                MoveUpParameterPosition(int.Parse(Request.QueryString["MoveUpParameter"]));
            }

            if (Request.QueryString["MoveDownParameter"] != null)
            {
                MoveDownParameterPosition(int.Parse(Request.QueryString["MoveDownParameter"]));
            }

            if (Request.QueryString["HideParameter"] != null)
            {
                HideParameter(int.Parse(Request.QueryString["HideParameter"]));
            }

            if (Request.QueryString["UnhideParameter"] != null)
            {
                UnhideParameter(int.Parse(Request.QueryString["UnhideParameter"]));
            }


            int ScriptID;
            if (Request.QueryString["scriptID"] == null)
            {
                ScriptID = 31;
            }
            else
            {
                ScriptID = int.Parse(Request.QueryString["scriptID"]);
            }
            script = GetScriptByID(ScriptID);
            var parameters = GetParameterByScriptID(ScriptID);
            var _db = new PowerAdmin.Models.ScriptContext();
            TextBox ArgumentTextBox = null;
            CheckBox ArgumentCheckBox = null;
            Label ArgumentLabel = null;
            Label ArgumentTypeUnknowLabel = null;
            TableRow ArgumentTableRow = null;
            TableCell ArgumentTableCell1, ArgumentTableCell2, ArgumentTableCell3;
            HyperLink ArgumentUpHyperLink = null;
            HyperLink ArgumentDownHyperLink = null;
            HyperLink ArgumentHideHyperLink = null;
            HyperLink ArgumentUnhideHyperLink = null;


            foreach (var parameter in parameters)
            {

                ArgumentLabel = new Label();
                ArgumentTypeUnknowLabel = new Label();
                ArgumentTableRow = new TableRow();
                ArgumentTableCell1 = new TableCell();
                ArgumentTableCell2 = new TableCell();
                ArgumentTableCell3 = new TableCell();
                ArgumentDownHyperLink = new HyperLink();
                ArgumentUpHyperLink = new HyperLink();
                ArgumentDownHyperLink = new HyperLink();
                ArgumentHideHyperLink = new HyperLink();
                ArgumentUnhideHyperLink = new HyperLink();

                ArgumentUpHyperLink.ImageUrl = "~/Images/up.gif";
                ArgumentUpHyperLink.ImageHeight = 8;
                ArgumentUpHyperLink.ImageWidth = 8;
                ArgumentUpHyperLink.NavigateUrl = "~/ViewScript.aspx?scriptID=" + ScriptID + "&MoveUpParameter=" + parameter.ID;

                ArgumentDownHyperLink.ImageUrl = "~/Images/down.gif";
                ArgumentDownHyperLink.ImageHeight = 8;
                ArgumentDownHyperLink.ImageWidth = 8;
                ArgumentDownHyperLink.NavigateUrl = "~/ViewScript.aspx?scriptID=" + ScriptID + "&MoveDownParameter=" + parameter.ID;

                ArgumentHideHyperLink.ImageUrl = "~/Images/hide.gif";
                ArgumentHideHyperLink.ImageHeight = 8;
                ArgumentHideHyperLink.ImageWidth = 8;
                ArgumentHideHyperLink.NavigateUrl = "~/ViewScript.aspx?scriptID=" + ScriptID + "&HideParameter=" + parameter.ID;

                ArgumentUnhideHyperLink.ImageUrl = "~/Images/Unhide.gif";
                ArgumentUnhideHyperLink.ImageHeight = 8;
                ArgumentUnhideHyperLink.ImageWidth = 8;
                ArgumentUnhideHyperLink.NavigateUrl = "~/ViewScript.aspx?scriptID=" + ScriptID + "&UnhideParameter=" + parameter.ID;

                ArgumentLabel.Text = parameter.Name;

                ArgumentTableCell1.Controls.Add(ArgumentLabel);



                switch (parameter.Type)
                {
                    case "System.String":
                        ArgumentTextBox = new TextBox();
                        ArgumentTextBox.ID = parameter.Name;
                        ArgumentTableCell2.Controls.Add(ArgumentTextBox);
                        parameter.Managed = true;
                        break;
                    case "System.String[]":
                        ArgumentTextBox = new TextBox();
                        ArgumentTextBox.ID = parameter.Name;
                        ArgumentTableCell2.Controls.Add(ArgumentTextBox);
                        parameter.Managed = true;
                        break;
                    case "System.Management.Automation.SwitchParameter":
                        ArgumentCheckBox = new CheckBox();
                        ArgumentCheckBox.ID = parameter.Name;
                        ArgumentTableCell2.Controls.Add(ArgumentCheckBox);
                        parameter.Managed = true;
                        break;
                    default:
                        SetParameterManagedFalse(parameter.ID);
                        parameter.Managed = false;
                        ArgumentTypeUnknowLabel.Text = "Type d'argument non géré (" + parameter.Type + ")";
                        ArgumentTableCell2.Controls.Add(ArgumentTypeUnknowLabel);
                        break;

                }



                ArgumentTableRow.Cells.Add(ArgumentTableCell1);
                ArgumentTableRow.Cells.Add(ArgumentTableCell2);

                if (parameter.Hidden == false && parameter.Managed == true)
                {
                    ArgumentTableCell3.Controls.Add(ArgumentUpHyperLink);
                    ArgumentTableCell3.Controls.Add(ArgumentDownHyperLink);
                    ArgumentTableCell3.Controls.Add(ArgumentHideHyperLink);
                    ArgumentTableRow.Cells.Add(ArgumentTableCell3);
                    ArgumentsTable.Rows.Add(ArgumentTableRow);
                }

                if (parameter.Hidden == true)
                {
                    ArgumentTableCell3.Controls.Add(ArgumentUnhideHyperLink);
                    ArgumentTableRow.Cells.Add(ArgumentTableCell3);
                    HiddenArgumentsTable.Rows.Add(ArgumentTableRow);

                }

                if (parameter.Managed == false)
                {
                    UnmanagedArgumentsTable.Rows.Add(ArgumentTableRow);
                }



            }
        }


        protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            List<string> Parameters = GetParametersValue();
            string command = BuildCommand(Parameters);
            ExecuteScript(command);
        }

        private void ExecuteScript(string command)
        {
            LogEvent.AddEvent(script.ID, "Script", "Execution", command, Page.User.Identity.Name);
            var myPowershell = PowerShell.Create();
            var builder = new StringBuilder();
            GridViewResult.DataSource = null;
            GridViewResult.DataBind();
            GridViewError.DataSource = null;
            GridViewError.DataBind();
            //OutputTextBox.Text = null;
            myPowershell.Commands.AddScript(command);
            var PSObjects = myPowershell.Invoke();
            if (myPowershell.HadErrors)
            {
                GridViewError.DataSource = myPowershell.Streams.Error;
                GridViewError.DataBind();
                foreach (var err in myPowershell.Streams.Error)
                {
                    LogEvent.AddEvent(script.ID, "Script", "Error", err.Exception.Message, Page.User.Identity.Name);
                }
            }
            else
            {
                //if (PSObjects.Count == 999)
                //{
                //    foreach (PSObject PSObject in PSObjects)
                //    {
                //        builder.Append(PSObject.BaseObject.ToString() + "\r\n");
                //    }
                //    OutputTextBox.Text = builder.ToString();
                //}
                //else //some objects to gridview
                //{
                bool IsFirst = true;
                DataTable datatable = new DataTable();
                foreach (PSObject PSObject in PSObjects)
                {
                    if (IsFirst)
                    {
                        IsFirst = false;
                        foreach (System.Management.Automation.PSPropertyInfo property in PSObject.Properties)
                        {
                            datatable.Columns.Add(property.Name);
                        }
                    }
                    DataRow datarow = datatable.NewRow();
                    foreach (System.Management.Automation.PSPropertyInfo property in PSObject.Properties)
                    {
                        datarow[property.Name] = property.Value;
                    }
                    datatable.Rows.Add(datarow);
                }
                GridViewResult.DataSource = datatable;
                GridViewResult.DataBind();
                //}
            }
        }
        private string BuildCommand(List<string> Parameters)
        {
            string command = script.Name;
            Parameters = GetParametersValue();
            foreach (string parameter in Parameters)
            {
                command = command + parameter;
            }
            return command;
        }

        public List<string> GetParametersValue()
        {
            List<string> parameters = new List<string>();
            foreach (TableRow tablerow in ArgumentsTable.Rows)
            {
                foreach (TableCell tablecell in tablerow.Cells)
                {

                    foreach (Control controle in tablecell.Controls)
                    {
                        if (controle.ID != null)
                        {
                            if (controle is TextBox)
                            {
                                TextBox textbox = (TextBox)controle;
                                if (textbox.Text != "")
                                {
                                    parameters.Add(" -" + textbox.ID + " " + textbox.Text);
                                }
                            }
                            if (controle is CheckBox)
                            {
                                CheckBox checkbox = (CheckBox)controle;
                                if (checkbox.Checked == true)
                                {
                                    parameters.Add(" -" + checkbox.ID + " true");
                                }
                            }
                        }
                    }
                }
            }

            return parameters;
        }

        public Script GetScriptByID(int scriptID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            Script script = _db.Scripts.First(p => p.ID == scriptID);
            return script;
        }

        public IQueryable<PowerAdmin.Models.Parameter> GetParameterByScriptID(int scriptID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<PowerAdmin.Models.Parameter> query = _db.Parameters;
            query = query.Where(p => p.ParameterSetID == scriptID).OrderBy(p => p.Position);
            return query;
        }

        public IQueryable<PowerAdmin.Models.Parameter> GetValidsParametersByScriptID(int scriptID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<PowerAdmin.Models.Parameter> query = _db.Parameters;
            query = query.Where(p => p.ParameterSetID == scriptID).OrderBy(p => p.Position);
            return query;
        }

        public void MoveUpParameterPosition(int parameterID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            PowerAdmin.Models.Parameter parameter = _db.Parameters.First(i => i.ID == parameterID);
            int ScriptID = parameter.ParameterSetID;

            IQueryable<PowerAdmin.Models.Parameter> query = _db.Parameters;
            query = query.Where(p => p.ParameterSetID == ScriptID && p.Managed == true && p.Hidden == false).OrderBy(p => p.Position);

            int? parameterAOldPosition = 0;
            int parameterBID = 0;

            PowerAdmin.Models.Parameter previousParameter = null;
            foreach (PowerAdmin.Models.Parameter paramameterA in query)
            {
                if (paramameterA.ID == parameterID && previousParameter != null)
                {
                    parameterAOldPosition = paramameterA.Position;
                    parameterBID = previousParameter.ID;
                    paramameterA.Position = previousParameter.Position;
                    break;
                }
                previousParameter = paramameterA;
            }
            if (parameterBID != 0)
            {
                PowerAdmin.Models.Parameter parameterB = _db.Parameters.First(i => i.ID == parameterBID);
                parameterB.Position = parameterAOldPosition;
            }
            _db.SaveChanges();
        }

        public void MoveDownParameterPosition(int parameterID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            PowerAdmin.Models.Parameter parameter = _db.Parameters.First(i => i.ID == parameterID);
            int ScriptID = parameter.ParameterSetID;

            IQueryable<PowerAdmin.Models.Parameter> query = _db.Parameters;
            query = query.Where(p => p.ParameterSetID == ScriptID && p.Managed == true && p.Hidden == false).OrderByDescending(p => p.Position);

            int? parameterAOldPosition = 0;
            int parameterBID = 0;

            PowerAdmin.Models.Parameter previousParameter = null;
            foreach (PowerAdmin.Models.Parameter paramameterA in query)
            {

                if (paramameterA.ID == parameterID && previousParameter != null)
                {
                    parameterAOldPosition = paramameterA.Position;
                    parameterBID = previousParameter.ID;
                    paramameterA.Position = previousParameter.Position;
                    break;
                }

                previousParameter = paramameterA;
            }

            if (parameterBID != 0)
            {
                PowerAdmin.Models.Parameter parameterB = _db.Parameters.First(i => i.ID == parameterBID);
                parameterB.Position = parameterAOldPosition;
            }
            _db.SaveChanges();
        }

        private string GetScriptNameById(int scriptID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            PowerAdmin.Models.Script script = _db.Scripts.First(i => i.ID == scriptID);
            return script.Name;

        }


        private void HideParameter(int parameterID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            PowerAdmin.Models.Parameter parameter = _db.Parameters.First(i => i.ID == parameterID);
            parameter.Hidden = true;
            _db.SaveChanges();
        }


        private void UnhideParameter(int parameterID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            PowerAdmin.Models.Parameter parameter = _db.Parameters.First(i => i.ID == parameterID);
            parameter.Hidden = false;
            _db.SaveChanges();
        }

        private void SetParameterManagedFalse(int parameterID)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            PowerAdmin.Models.Parameter parameter = _db.Parameters.First(i => i.ID == parameterID);
            parameter.Managed = false;
            _db.SaveChanges();
        }
    }
}