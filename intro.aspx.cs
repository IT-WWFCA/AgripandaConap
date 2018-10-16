using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgriPanda.kernel;

namespace AgriPanda
{
    public partial class intro : System.Web.UI.Page
    {
        #region Variables
        /// <summary>
        /// Add class_global.
        /// </summary>
        private class_global std = new class_global();
        ///---------------------------------------
        /// Set class variables
        ///--------------------------------------- 
        /// <summary>
        /// User global perms.
        /// </summary>
        private Dictionary<string, string> _ag;
        #endregion

        # region Constructor
        protected void Page_Load(object sender, EventArgs e)
        {
            ///---------------------------------------
            /// Get global perms for user
            ///--------------------------------------- 
            _ag = std.get_uperms(HttpContext.Current.User.Identity.Name, "A1");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);
            DateTime timenow = DateTime.Now;
            Welcome.Text = Resources.global.welcomeIntro + " " + timenow + "<br />" + timenow.ToString("MMM dd, yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture(_ag["UserLanguage"]));
        }
        #endregion
    }
}