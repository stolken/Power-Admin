using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerAdmin.Logic;
using PowerAdmin.Models;

namespace PowerAdmin
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ResetSingleScriptButton_Click(object sender, EventArgs e)
        {
            PowerAdmin.Logic.Database.Seed("-Name " + ResetSingleScriptTextbox.Text, true);
        }

        protected void ResetByQueryButton_Click(object sender, EventArgs e)
        {
            PowerAdmin.Logic.Database.Seed(ResetByQueryTextbox.Text, ResetByQueryCheckBox.Checked);
        }

        protected void RebuildDatabaseButton_Click(object sender, EventArgs e)
        {
            PowerAdmin.Logic.Database.Reinit();
        }

        protected void AddNewScriptButton_Click(object sender, EventArgs e)
        {
            PowerAdmin.Logic.Database.Seed("", false);
        }


    }
}