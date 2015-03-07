using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerAdmin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExecuteCode_Click(object sender, EventArgs e)
        {
            ResultBox.Text = string.Empty;
            var myPowershell = PowerShell.Create();
            myPowershell.Commands.AddScript(PowerShellCodeBox.Text);
            var builder = new StringBuilder();
            myPowershell.Commands.AddScript(PowerShellCodeBox.Text);
            var Objects = myPowershell.Invoke();
            int errorcount = myPowershell.Streams.Error.Count;
            if (errorcount != 0)
            {
                foreach (var err in myPowershell.Streams.Error) {
                   string message = err.Exception.Message;
                }
            }
            else
            {
                if (Objects.Count == 1)
                {
                    foreach (PSObject Object in Objects)
                    {
                        builder.Append(Object.BaseObject.ToString() + "\r\n");
                    }
                    ResultBox.Text = builder.ToString();
                }
                if (Objects.Count > 1)
                {
                    foreach (PSObject Object in Objects)
                    {
                        foreach (System.Management.Automation.PSPropertyInfo prop in Object.Properties)
                        {
                            builder.Append(prop.Name + " : " + prop.Value + "\r\n");
                        }
                    }
                    ResultBox.Text = builder.ToString();
                }
            }
        }
        }
    }
