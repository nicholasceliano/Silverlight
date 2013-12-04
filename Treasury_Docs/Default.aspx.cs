using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hess.ActiveDirectory;

namespace TDocsDataService
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string editUsers = string.Empty,
                paramValue,
                currentUser = HttpContext.Current.User.Identity.Name.ToString();

            #region Gets User Information

            ADUsers users = new ADUsers();
            users = users.GetUsersByGroup("TDocsEditUser");
            users = users.GetUsersByGroup("CorporateBusinessSystems");
            if (users != null)
            {
                foreach (ADUsersItem u in users)
                    editUsers = editUsers + u.UserId + ";";
            }

            #endregion

            paramValue = "Params=" + currentUser + "|" + editUsers;
            paramSilverLight.Attributes.Add("value", paramValue);
        }
    }
}