using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerAdmin.Models;
using System.Management.Automation;

using System.Data;
using System.Configuration;
using System.Collections;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PowerAdmin
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ProcessMenu();
            }

        }

        public void ProcessMenu()
        {
            var cats = GetCategories();
            foreach (var cat in cats)
            {
                MenuItem catItem = new MenuItem(cat.Name);
                MenuTop.Items.Add(catItem);
                var verbs = GetVerbs(cat.Name);

                foreach (var verb in verbs)
                {
                    MenuItem verbItem = new MenuItem(verb.Key);
                    catItem.ChildItems.Add(verbItem);
                    var scripts = GetScriptsByCategorieByVerb(verb.Key, cat.Name);
                    foreach (var script in scripts)
                    {
                        MenuItem scriptItem = new MenuItem(script.Name);
                        scriptItem.NavigateUrl = "/Script.aspx?ScriptID=" + script.ID;
                        verbItem.ChildItems.Add(scriptItem);
                    }
                }
            }
        }

        public IQueryable<Category> GetCategories()
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<Category> query = _db.Categories;
            query = query.Where(p => p.Hidden == false);
            return query;
        }

        public IQueryable<Script> GetScriptsByCategorieByVerb(string verb, string categoryName)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<Script> query = _db.Scripts;
            query = query.Where(p => p.Hidden == false);
            query = query.Where(p => String.Compare(p.Category.Name, categoryName) == 0);
            query = query.Where(p => String.Compare(p.Verb, verb) == 0).OrderBy(p => p.Name);
            return query;
        }

        public IQueryable<IGrouping<string, Script>> GetVerbs(string categoryName)
        {
            var _db = new PowerAdmin.Models.ScriptContext();
            IQueryable<Script> Scripts = _db.Scripts;
            var query = Scripts.Where(p => String.Compare(p.Category.Name, categoryName) == 0);
            query = query.Where(p => p.Hidden == false);
            var query2 = query.GroupBy(script => script.Verb);
            int countquery = query2.Count();
            return query2;
        }


    }
}