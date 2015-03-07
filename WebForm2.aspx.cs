using System;
using System.Management.Automation;

namespace PowerAdmin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var myPowershell = PowerShell.Create();
            myPowershell.Commands.AddScript("New-ADUser -SamAccountName "
                +  SamAccountNameTextBox.Text
                + " -GivenName " + GivenNameTextBox.Text
                + " -Surname " + SurnameTextBox.Text);
            myPowershell.Invoke();
        }
    }
}