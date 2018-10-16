/**
 * WWF Honduras
 * AgriPanda v2.0.0
 * Admin Toolbars XML Generator Handler
 * Last Updated: $Date: 2016-02-29 09:47:02 -0600 (Thu, 04 Dec 2014) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2016 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Toolbars
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0003 $
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

namespace AgriPanda.admin.handlers
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
        /// <summary>
        /// String[] for items.
        /// </summary>
        private string[] items;
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

            switch (HttpContext.Current.Request["code"])
            {
                case "users":
                    GenToolbar("5");
                    break;
                //-------------------
                default:
                    GenToolbar("2");
                    break;
            } 
            
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
        private string GenToolbar(string itemid)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);            
            ///---------------------------------------
            /// Select toolbar.
            ///---------------------------------------            
            string TQuery = "SELECT * FROM trees_items WHERE ItemID=" + itemid;
            SqlCommand cmd = new SqlCommand(TQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    /// ---------------------------------
                    /// Deserialize the string
                    /// ---------------------------------
                    string toolbar = Convert.ToString(r["ItemToolbar"]);
                    if (String.IsNullOrEmpty(toolbar))
                        toolbar = "separator:sep1;";
                    else
                    {
                        toolbar = Regex.Replace(toolbar, "\\]$", "");
                        toolbar = Regex.Replace(toolbar, "^S\\[", "");
                    }
                    toolbar += "button:close:close:true";
                    items = toolbar.Split(';');
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            ///---------------------------------------

            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<toolbar>";
            for (int c = 0; c < items.Length; c++)
            {
                string enable = string.Empty;
                string[] itm = items[c].Split(':');

                /// ---------------------------------
                /// Get resource string
                /// ---------------------------------
                string txt = Resources.toolbars.ResourceManager.GetString(itm[1]);
                ///----------------------------------
                if (itm[0] == "button")
                {
                    if (itm[3] == "false")
                        enable = "enabled=\"false\"";
                    else
                        enable = "";
                    xml += "<item id=\"" + Convert.ToString(itm[1]) + "\" type=\"" + Convert.ToString(itm[0]) + "\" img=\"" + Convert.ToString(itm[2]) + ".gif\" text=\"" + txt + "\" imgdis=\"" + Convert.ToString(itm[2]) + "_dis.gif\" " + enable + "/>";
                }
                else
                {
                    xml += "<item id=\"" + Convert.ToString(itm[1]) + "\" type=\"" + Convert.ToString(itm[0]) + "\"/>";
                }
            }
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