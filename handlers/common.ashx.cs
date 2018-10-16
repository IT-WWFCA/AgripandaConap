/**
 * WWF Honduras
 * AgriPanda v1.1.0
 * Common XML Generator Handler
 * Last Updated: $Date: 2013-06-19 18:43:08 -0600 (Wed, 19 Jun 2013) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2012 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Common
 * @link		http://www.wwf-mar.org
 * @since		1.1.0
 * @version		$Revision: 0001 $
 *
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using AgriPanda.kernel;

namespace AgriPanda.handlers
{
    /// <summary>
    /// Summary description for common
    /// </summary>
    public class common : IHttpHandler
    {
        #region Variables
        /// <summary>
        /// Add global_class.
        /// </summary>
        private class_global std = new class_global();
        /// <summary>
        /// Add mssql_class.
        /// </summary>
        private class_mssql DB = new class_mssql();
        /// <summary>
        /// Connect to mssql DB.
        /// </summary>
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);
        /// <summary>
        /// String xml for output.
        /// </summary>
        private string xml;
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            switch (HttpContext.Current.Request["code"])
            {
                case "branches":
                    show_branches();
                    break;

                case "initform":
                    show_init_form(Convert.ToInt32(HttpContext.Current.Request["tool"]), Convert.ToString(HttpContext.Current.Request["did"]));
                    break;

                case "showdata":
                    show_rawdata(Convert.ToString(HttpContext.Current.Request["did"]));
                    break;

                //-------------------
                default:
                    show_all();
                    break;
            }

            context.Response.ContentType = "text/xml; charset=utf-8";
            context.Response.Write(xml);
        }

        #region Methods
        /// <summary>
        /// Show all branche.
        /// </summary>
        private string show_branches()
        {
            Dictionary<string, string> divisions = new Dictionary<string, string>();
            string DIVQuery = "SELECT * FROM farms_belize_divisions";
            SqlCommand cmddiv = new SqlCommand(DIVQuery, con);
            SqlDataReader q;
            try
            {
                con.Open();
                q = cmddiv.ExecuteReader();
                while (q.Read())
                {
                    divisions.Add(Convert.ToString(q["DivID"]), Convert.ToString(q["DivName"]));
                }
                q.Close();
            }
            catch { }
            finally { con.Close(); }
            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<rows>";
            xml += "<head>";
            xml += "<column width=\"50\" type=\"ch\" align=\"center\" sort=\"int\"></column>";
            xml += "<column width=\"300\" type=\"ro\" align=\"left\" sort=\"str\">Branches</column>";
            xml += "<column width=\"120\" type=\"ro\" align=\"left\" sort=\"str\">Division</column>";
            xml += "<settings>";
            xml += "<colwidth>px</colwidth>";
            xml += "</settings>";
            xml += "</head>";
            string FBQuery = "SELECT * FROM farms_belize_branches";
            SqlCommand cmd = new SqlCommand(FBQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    xml += "<row id=\"" + Convert.ToInt32(r["BranchID"]) + "\">";
                    xml += "<cell></cell>";
                    xml += "<cell>" + Convert.ToString(r["BranchName"]) + "</cell>";
                    xml += "<cell>" + divisions[Convert.ToString(r["DivID"])] + "</cell>";
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
        /// Show form.
        /// </summary>
        private string show_init_form(int tool, string did)
        {
            string[] toolArray = new string[] { "Harvest", "Nutrition", "Soil Management", "Integrated Pest Management", "Equipment", "Water Management", "Farm Owners" };
            xml += "<items>";
            xml += "<item type=\"settings\" position=\"label-left\" labelWidth=\"120\" inputWidth=\"150\"/>";
            xml += "<item type=\"fieldset\" name=\"data\" inputWidth=\"auto\" label=\"" + toolArray[tool] + " Management Tools\">";
            xml += "<item type=\"label\" label=\"Select Branches\">";
            string BranchDataQuery = "SELECT * FROM farms_belize_branches WHERE DivID=" + did;
            SqlCommand cmd = new SqlCommand(BranchDataQuery, con);
            SqlDataReader q;
            try
            {
                con.Open();
                q = cmd.ExecuteReader();
                while (q.Read())
                {
                    xml += "<item type=\"checkbox\" value=\"" + Convert.ToString(q["BranchID"]) + "\" name=\"" + Convert.ToString(q["BranchID"]) + "\" label=\"" + Convert.ToString(q["BranchName"]) + "\" />";
                }
                q.Close();
            }
            catch { }
            finally { con.Close(); }
            xml += "</item>";           
            xml += "<item type=\"button\" name=\"GoButton\" value=\"Continue >>\"/>";
            xml += "</item>";
            xml += "</items>";
            
            return xml;
        }

        /// <summary>
        /// Testing only method.
        /// </summary>
        private string show_rawdata(string did)
        {
            Dictionary<string, string> branches = new Dictionary<string, string>();
            string BranchDataQuery = "SELECT * FROM farms_belize_branches WHERE DivID=" + did;
            SqlCommand cmd = new SqlCommand(BranchDataQuery, con);
            SqlDataReader q;
            try
            {
                con.Open();
                q = cmd.ExecuteReader();
                while (q.Read())
                {
                    branches.Add(Convert.ToString(q["BranchID"]), Convert.ToString(q["BranchID"]));
                }
                q.Close();
            }
            catch { }
            finally { con.Close(); }

            foreach (var d in branches)
            {                
                if (Convert.ToString(HttpContext.Current.Request[d.Key]) != null)
                xml += "<item>" + d.Key + "</item>";
            }

            
            
            return xml;
        }

        /// <summary>
        /// Show all comon data.
        /// </summary>
        private string show_all()
        {
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