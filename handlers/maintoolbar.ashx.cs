/**
 * WWF Honduras
 * AgriPanda v2.0.0
 * Main Toolbar XML Generator Handler
 * Last Updated: $Date: 2015-09-23 09:09:02 -0600 (Wed, 23 Sep 2015) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2015 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.MainToolbar
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0001 $
 *
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using AgriPanda.kernel;

namespace AgriPanda.handlers
{
    /// <summary>
    /// Summary description for maintoolbar
    /// </summary>
    public class maintoolbar : IHttpHandler
    {
        #region Variables
        /// <summary>
        /// Add mssql_class.
        /// </summary>
        private class_mssql DB = new class_mssql();
        /// <summary>
        /// Add class_global.
        /// </summary>
        private class_global std = new class_global();
        /// <summary>
        /// Connect to mssql DB.
        /// </summary>
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);
        ///---------------------------------------
        /// Set class variables
        ///--------------------------------------- 
        /// <summary>
        /// User global perms.
        /// </summary>
        private Dictionary<string, string> _ag;
        /// <summary>
        /// String xml for output.
        /// </summary>
        private string xml;
        #endregion

        # region Constructor
        /// <summary>
        /// Main function.
        /// </summary>
        public void ProcessRequest(HttpContext context)
        {
            ///---------------------------------------
            /// Get global perms for user
            ///--------------------------------------- 
            _ag = std.get_uperms(HttpContext.Current.User.Identity.Name, "A1");

            GenToolbar();

            context.Response.ContentType = "text/xml; charset=utf-8";
            context.Response.Write(xml);
        }
        #endregion

        #region Methods
        ///---------------------------------------
        /// Region where class functions are declared.
        ///---------------------------------------
        /// <summary>
        /// Create XML for main toolbar.
        /// </summary>
        private string GenToolbar()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);
            ///---------------------------------------
            /// create maintoolbar.
            ///---------------------------------------
            xml = @"<?xml version=""1.0""?>
<menu>
    <item id = ""tools"" text = """ + Resources.maintoolbar.Tools + @""">
           <item id = ""userop"" text = """ + Resources.maintoolbar.UserOptions + @""" img = ""user.png"" imgdis = ""user_dis.png"" />
    </item>
           <item id = ""sep_top_1"" type = ""separator"" />
           <item id = ""help"" text = """ + Resources.maintoolbar.Help + @""">
           <item id = ""about"" text = """ + Resources.maintoolbar.About + @""" img = ""about.gif"" imgdis = ""about_dis.gif"" />
           <item id = ""bugReporting"" text = """ + Resources.maintoolbar.BugReport + @""" img = ""bug_reporting.gif"" imgdis = ""bug_reporting_dis.gif"" />
    </item>
    <item id = ""logout"" text = """ + Resources.maintoolbar.LogOut + @""">
           <href><![CDATA[./logout.aspx]]></href>
    </item>
</menu>";
            return xml;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}