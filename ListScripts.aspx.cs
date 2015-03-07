using PowerAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerAdmin
{
    public partial class ListScripts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Script> GetScripts([QueryString("id")] int? categoryId)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<Script> query = _db.Scripts;
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryID == categoryId);

            }

            return query;
        }
    
    }
}