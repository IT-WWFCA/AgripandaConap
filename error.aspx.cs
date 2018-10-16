using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgriPanda.kernel;

namespace AgriPanda
{
    public partial class error : System.Web.UI.Page
    {
        #region Variables
        /// <summary>
        /// Add class_global.
        /// </summary>
        private class_global std = new class_global();
        /// <summary>
        /// Add class_skin.
        /// </summary>
        private class_skin skin = new class_skin();
        ///---------------------------------------
        /// Set class variables
        ///--------------------------------------- 
        /// <summary>
        /// User global perms.
        /// </summary>
        private Dictionary<string, string> _ag;
        /// <summary>
        /// Actual date.
        /// </summary>
        private DateTime datetoday = DateTime.Today;

        /// <summary>
        /// Array for table rows.
        /// </summary>
        public string[] td_array;
        #endregion

        # region Constructor
        /// <summary>
        /// Main function.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///---------------------------------------
            /// Get global perms for user
            ///--------------------------------------- 
            _ag = std.get_uperms(HttpContext.Current.User.Identity.Name, "A1");
            ///---------------------------------------
            /// Set user prefrered language
            ///--------------------------------------- 
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);
            ///---------------------------------------
            StringBuilder pageContent = new StringBuilder();
            string[,] tableheader = {
                                    { "&nbsp;&nbsp; " + Resources.global.error_title, "100%", "left" }
                                    };
            pageContent.Append(skin.start_table(Resources.global.error_page_title, "", tableheader)); // Start of Table
            
            td_array = new string[] {
                                skin.error_page(Convert.ToString(HttpContext.Current.Request["err"]), Convert.ToString(HttpContext.Current.Request["color"])),
                                };
            pageContent.Append(skin.add_td_row(td_array, "")); // Add rows
            ///---------------------------------------
            pageContent.Append(skin.end_table()); // End of Table
            string errorpage = Convert.ToString(pageContent);
            LoadContent.Text = errorpage;
        }
        #endregion
    }
}