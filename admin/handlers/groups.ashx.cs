/**
 * WWF Honduras
 * AgriPanda v1.1.0
 * Groups XML Generator Handler
 * Last Updated: $Date: 2012-04-19 09:20:08 -0600 (Thu, 19 Apr 2012) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2012 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Groups
 * @link		http://www.wwf-mar.org
 * @since		1.1.0
 * @version		$Revision: 0001 $
 *
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AgriPanda.kernel;

namespace AgriPanda.admin.handlers
{
    /// <summary>
    /// Summary description for groups
    /// </summary>
    public class groups : IHttpHandler
    {
        private class_mssql DB = new class_mssql();
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);

        private string xml;

        public void ProcessRequest(HttpContext context)
        {
            switch (HttpContext.Current.Request["code"])
            {
                case "addgroup":
                    show_form_group("new");
                    break;
                case "editgroup":
                    show_form_group("edit");
                    break;
                case "doadd":
                    do_add_group();
                    break;
                //-------------------
                default:
                    show_groups();
                    break;
            }

            context.Response.ContentType = "text/xml; charset=utf-8";
            context.Response.Write(xml);
        }

        /// <summary>
        /// Show group list.
        /// </summary>
        private string show_groups()
        {
            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<rows>";
            xml += "<head>";
            xml += "<column width=\"80\" type=\"ro\" align=\"center\" sort=\"int\">Group ID</column>";
            xml += "<column width=\"150\" type=\"ro\" align=\"left\" sort=\"str\">Group Alias</column>";
            xml += "<column width=\"200\" type=\"ro\" align=\"left\" sort=\"int\">Group Name</column>";
            xml += "<column width=\"450\" type=\"ro\" align=\"left\" sort=\"int\">Group Permissions</column>";
            xml += "<column width=\"100\" type=\"ro\" align=\"left\" sort=\"int\">Group Crops</column>";
            xml += "<settings>";
            xml += "<colwidth>px</colwidth>";
            xml += "</settings>";
            xml += "</head>";
            string GroupsQuery = "SELECT * FROM groups";
            SqlCommand cmd = new SqlCommand(GroupsQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    xml += "<row id=\"" + Convert.ToInt32(r["GroupID"]) + "\">";
                    xml += "<cell>" + Convert.ToString(r["GroupID"]) + "</cell>";
                    xml += "<cell>" + Convert.ToString(r["GroupAlias"]) + "</cell>";
                    xml += "<cell>" + Convert.ToString(r["GroupName"]) + "</cell>";
                    xml += "<cell>" + Convert.ToString(r["GroupPerms"]) + "</cell>";
                    xml += "<cell>" + Convert.ToString(r["GroupCrops"]) + "</cell>";
                    xml += "</row>";
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            xml += "</rows>";
            return xml;
        }

        /// <summary>
        /// Show Form.
        /// </summary>
        private string show_form_group(string type)
        {
            xml = "";
            return xml;
        }

        /// <summary>
        /// Add new group.
        /// </summary>
        private void do_add_group()
        {

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}