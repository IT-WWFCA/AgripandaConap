/**
 * WWF Honduras
 * AgriPanda v2.0.0
 * Admin Toolbars XML Generator Handler
 * Last Updated: $Date: 2016-02-29 10:24:02 -0600 (Thu, 04 Dec 2014) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2015 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Toolbars
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0005 $
 *
 */

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

namespace AgriPanda.handlers
{
    /// <summary>
    /// Summary description for toolbars
    /// </summary>
    public class toolbars : IHttpHandler
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

            GenToolbar(Convert.ToString(HttpContext.Current.Request["ic"]));

            context.Response.ContentType = "text/xml; charset=utf-8";
            context.Response.Write(xml);
        }
        #endregion

        #region Methods
        ///---------------------------------------
        /// Region where class functions are declared.
        ///---------------------------------------
        /// <summary>
        /// Create XML for user toolbar.
        /// </summary>
        private string GenToolbar(string itemCode)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);
            ///---------------------------------------
            /// Select toolbar and create toolbar.
            ///---------------------------------------
            int _isButtonWSep = 0;
            int _isEnabled = 0;
            string _adminUsers = string.Empty;
            string lang = string.Empty;
            if (_ag["UserLanguage"] == "en-US")
                lang = "EN";
            else if (_ag["UserLanguage"] == "es-HN")
                lang = "ES";
            string ItemName = "TBItemName" + lang;
            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<toolbar>";            
            string TQuery = "SELECT * FROM mods_toolbar_items WHERE ItemCode='" + itemCode + "' ORDER BY TBItemOrder";
            SqlCommand cmd = new SqlCommand(TQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    _isButtonWSep = Convert.ToInt32(r["isButtonWSep"]);
                    _isEnabled = Convert.ToInt32(r["isEnabled"]);
                    _adminUsers = Convert.ToString(r["AdminUsers"]);
                    string auid = Convert.ToString(_ag["UserID"]);
                    string gadmin = Convert.ToString(_ag["GroupAdmin"]);
                    string enable = string.Empty;
                    ///---------------------------------------
                    /// Enable or desable buttons.
                    ///---------------------------------------                    
                    if (gadmin == "1")
                        enable = "";
                    else
                    {
                        if (String.IsNullOrEmpty(_adminUsers))
                        {
                            if (_isEnabled == 0)
                                enable = "enabled=\"false\"";
                            else
                                enable = "";
                        }
                        else
                        {
                            string[] uids = _adminUsers.Split(':');
                            for (var i = 0; i < uids.Length; i++)
                            {
                                if (auid == uids[i])
                                    enable = "";
                                else
                                    enable = "enabled=\"false\"";
                            }
                        }
                    }
                    ///---------------------------------------

                    xml += "<item id=\"" + Convert.ToString(r["TBItemCode"]) + "\" type=\"button\" img=\"" + Convert.ToString(r["TBItemIcon"]) + "\" text=\"" + Convert.ToString(r[ItemName]) + "\" imgdis=\"" + Convert.ToString(r["TBItemIconDis"]) + "\" " + enable + " />";
                    
                    if (_isButtonWSep == 1)
                        xml += "<item id=\"sep\" type=\"separator\"/>";
                    else
                        xml += "";
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            xml += "<item id=\"close\" type=\"button\" img=\"close.gif\" text=\"" + Resources.toolbars.close + "\" imgdis=\"close_dis.gif\" />";
            xml += "</toolbar>";
            
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