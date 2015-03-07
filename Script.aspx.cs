using PowerAdmin.Logic;
using PowerAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerAdmin
{
    public partial class EditScript : System.Web.UI.Page
    {

        Script ActiveScript;
        ParameterSet ActiveParameterSet;


        protected void Page_Load(object sender, EventArgs e)
        {
            int ParameterSetID;
            int ScriptID;
            ParameterSetID = 0;
            ScriptID = 0;
            var db = new PowerAdmin.Models.ScriptContext();
            if (Request.QueryString["ParameterSetID"] != null)
            {
                ParameterSetID = int.Parse(Request.QueryString["ParameterSetID"]);
                ActiveParameterSet = db.ParameterSets.First(p => p.ID == ParameterSetID);
                ActiveScript = db.Scripts.First(p => p.ID == ActiveParameterSet.ScriptID);
            }

            if (Request.QueryString["ScriptID"] != null)
            {
                ScriptID = int.Parse(Request.QueryString["ScriptID"]);
                ActiveParameterSet = db.ParameterSets.First(p => p.ScriptID == ScriptID);
                ActiveScript = db.Scripts.First(p => p.ID == ScriptID);
            }
            
            if (ActiveParameterSet != null)
            {
                ShowParameters();
                if (!Page.IsPostBack)
                {
                    ParameterSetMenuProcess();
                }
            }
        }

        protected void parametersListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField InputControlHiddenField = e.Item.FindControl("InputControlHiddenField") as HiddenField;
                if (InputControlHiddenField.Value == "CheckBox")
                {
                    PlaceHolder ph = e.Item.FindControl("InputControlPlaceHolder") as PlaceHolder;
                    CheckBox InputCheckBox = new CheckBox();
                    InputCheckBox.ID = "InputControl";
                    ph.Controls.Add(InputCheckBox);
                    bool hj = ph.HasControls();
                }

                if (InputControlHiddenField.Value == "TextBox")
                {
                    PlaceHolder ph = e.Item.FindControl("InputControlPlaceHolder") as PlaceHolder;
                    TextBox InputTextBox = new TextBox();
                    InputTextBox.ID = "InputControl";
                    ph.Controls.Add(InputTextBox);
                }
            }
        }

        public void ShowParameters()
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<PowerAdmin.Models.Parameter> parameters = _db.Parameters;
            if (ActiveParameterSet == null)
            {
                parameters = null;
            }
            else
            {
                parameters = parameters.Where(p => p.InputControl != "none");
                parameters = parameters.Where(p => p.Hidden != true);
                parameters = parameters.Where(p => p.ParameterSetID == ActiveParameterSet.ID);
            }

            TextBox ParameterTextBox = null;
            CheckBox ParameterCheckBox = null;
            Label ParameterLabel = null;

            TableRow ParameterTableRow = null;
            TableCell ParameterTableCell1, ParameterTableCell2, ParameterTableCell3;


            foreach (PowerAdmin.Models.Parameter parameter in parameters)
            {
                ParameterLabel = new Label();
                ParameterTableRow = new TableRow();
                ParameterTableCell1 = new TableCell();
                ParameterTableCell2 = new TableCell();
                ParameterTableCell3 = new TableCell();

                ParameterLabel.Text = parameter.Name;

                ParameterTableCell1.Controls.Add(ParameterLabel);

                switch (parameter.InputControl)
                {
                    case "TextBox":
                        ParameterTextBox = new TextBox();
                        ParameterTextBox.ID = parameter.Name;
                        ParameterTableCell2.Controls.Add(ParameterTextBox);
                        break;
                    case "CheckBox":
                        ParameterCheckBox = new CheckBox();
                        ParameterCheckBox.ID = parameter.Name;
                        ParameterTableCell2.Controls.Add(ParameterCheckBox);
                        break;
                }

                ParameterTableRow.Cells.Add(ParameterTableCell1);
                ParameterTableRow.Cells.Add(ParameterTableCell2);
                ParameterTableRow.ToolTip = parameter.Description;
                ParameterTableRow.Cells.Add(ParameterTableCell3);
                ParametersTable.Rows.Add(ParameterTableRow);
            }
        }

        private void ParameterSetMenuProcess()
        {
            var db = new PowerAdmin.Models.ScriptContext();
            IQueryable<ParameterSet> query = db.ParameterSets;
            query = query.Where(p => p.ScriptID == ActiveScript.ID);
            query = query.Where(p => p.Hidden == false);
            foreach (ParameterSet ps in query)
            {
                MenuItem psItem = new MenuItem(ps.Name);
                psItem.NavigateUrl = "/Script.aspx?ParameterSetID=" + ps.ID;
                ParameterSetMenu.Items.Add(psItem);
                if (ps.ID == ActiveParameterSet.ID)
                {
                    psItem.Selected = true;
                }
            }
        }

        public List<string> GetParametersValue()
        {
            List<string> parameters = new List<string>();
            
            foreach (TableRow ParameterRow in ParametersTable.Rows)
            {
                foreach (TableCell tablecell in ParameterRow.Cells)
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

        private int GetFirstScriptID()
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            Script script = _db.Scripts.First();
            return script.ID;
        }

        private Script GetScriptByID(int id)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            Script script = _db.Scripts.First(p => p.ID == id);
            return script;
        }

        protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            List<string> Parameters = GetParametersValue();
            string command = BuildCommand(Parameters);
            ExecuteScript(command);
        }

        private void ExecuteScript(string command)
        {
            LogEvent.AddEvent(ActiveParameterSet.ID, "ParameterSet", "Execution", command, Page.User.Identity.Name);
            var myPowershell = PowerShell.Create();
            var builder = new StringBuilder();
            GridViewResult.DataSource = null;
            GridViewResult.DataBind();
            GridViewError.DataSource = null;
            GridViewError.DataBind();
            myPowershell.Commands.AddScript(command);
            var PSObjects = myPowershell.Invoke();
            if (myPowershell.HadErrors)
            {
                GridViewError.DataSource = myPowershell.Streams.Error;
                GridViewError.DataBind();
                foreach (var err in myPowershell.Streams.Error)
                {
                    LogEvent.AddEvent(ActiveParameterSet.ID, "ParameterSet", "Error", err.Exception.Message, Page.User.Identity.Name);
                }
            }
            else
            {
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
            }
        }
        private string BuildCommand(List<string> Parameters)
        {
            string command = ActiveScript.Name;
            Parameters = GetParametersValue();
            foreach (string parameter in Parameters)
            {
                command = command + parameter;
            }
            return command;
        }



        public void UpdateScript(Script NewScript)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            Script scriptToUpdate = _db.Scripts.First(p => p.ID == NewScript.ID);
            scriptToUpdate.Description = NewScript.Description;
            scriptToUpdate.Hidden = NewScript.Hidden;
            scriptToUpdate.CategoryID = NewScript.CategoryID;
            _db.SaveChanges();
            scriptFormView.ChangeMode(FormViewMode.ReadOnly);
        }

        public void UpdateParameterSet(ParameterSet NewParameterSet)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            ParameterSet ParameterSetToUpdate = _db.ParameterSets.First(p => p.ID == NewParameterSet.ID);
            ParameterSetToUpdate.Description = NewParameterSet.Description;
            ParameterSetToUpdate.Hidden = NewParameterSet.Hidden;
            _db.SaveChanges();
            ParameterSetFormView.ChangeMode(FormViewMode.ReadOnly);
        }


        public IQueryable<Category> GetCategories()
        {
            var db = new PowerAdmin.Models.ScriptContext();
            IQueryable<Category> query = db.Categories;
            return query;
        }


        public IQueryable<ParameterSet> GetParameterSets([QueryString("ParameterSetID")]int? ParameterSetID)
        {
            var db = new PowerAdmin.Models.ScriptContext();
            IQueryable<ParameterSet> query = db.ParameterSets;

            if ((ParameterSetID.HasValue == false) && (Request.QueryString["ScriptID"] != null))
            {
                int ScriptID = int.Parse(Request.QueryString["ScriptID"]);
                query = query.Where(p => p.ScriptID == ScriptID);
            }

            if ((ParameterSetID.HasValue == true) && (Request.QueryString["ScriptID"] == null))
            {
                int ScriptID = db.ParameterSets.Where(p => p.ID == ParameterSetID).FirstOrDefault().ScriptID;
                query = query.Where(p => p.ScriptID == ScriptID);
            }

            if ((ParameterSetID.HasValue == false) && (Request.QueryString["ScriptID"] == null))
            {
                query = null;
            }


            return query;
        }

        public IQueryable<PowerAdmin.Models.Parameter> GetParameters()
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<PowerAdmin.Models.Parameter> query = _db.Parameters;
            if (ActiveParameterSet == null)
            {
                query = null;
            }
            else
            {
                query = query.Where(p => p.InputControl != "none");
                query = query.Where(p => p.ParameterSetID == ActiveParameterSet.ID);
            }
            return query;
        }

        public IQueryable<PowerAdmin.Models.Parameter> GetOthersParameters()
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<PowerAdmin.Models.Parameter> query = _db.Parameters;
            if (ActiveParameterSet == null)
            {
                query = null;
            }
            else
            {
                query = query.Where(p => p.ParameterSetID == ActiveParameterSet.ID);
                query = query.Where(p => p.InputControl == "none" || p.Hidden == true);

            }
            return query;
        }


        protected void FormViewUpdatedEventHandler(Object sender, FormViewUpdatedEventArgs e)
        {
            scriptFormView.ChangeMode(FormViewMode.ReadOnly);
        }

        public IQueryable<Script> GetScript()
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<Script> script = _db.Scripts;
            if (ActiveScript == null)
            {
                script = null;
            }
            else
            {
                script = script.Where(p => p.ID == ActiveScript.ID);
            }
            return script;
        }

        public IQueryable<ParameterSet> GetParameterSet()
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<ParameterSet> query = _db.ParameterSets;
            if (ActiveParameterSet == null)
            {
                query = null;
            }
            else
            {
                query = query.Where(p => p.ID == ActiveParameterSet.ID);
            }
            return query;
        }

    }
}