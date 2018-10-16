/**
 * WWF Honduras
 * AgriPanda v1.1.0
 * Admin Tree XML Generator Handler
 * Last Updated: $Date: 2012-08-24 12:03:08 -0600 (Fri, 24 Aug 2012) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2012 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Tree
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

namespace AgriPanda.admin.handlers
{
    /// <summary>
    /// Summary description for tree
    /// </summary>
    public class tree : IHttpHandler
    {

        #region Variables
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

        # region Constructor
        /// <summary>
        /// Main function.
        /// </summary>
        public void ProcessRequest(HttpContext context)
        {
            string[] parents = get_parent_ids();
            string[] par = parents.Distinct().ToArray();
            
            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<tree id=\"0\" radio=\"1\">";
            for (int cnt = 0; cnt < par.Length; cnt++)      
            {
                xml += get_parent_items_data(par[cnt]);
            }
            xml += "</tree>";
            context.Response.ContentType = "text/xml; charset=utf-8";
            context.Response.Write(xml);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get Parent ids.
        /// </summary>
        private string[] get_parent_ids()
        {
            string parent = string.Empty;
            string[] parentids;
            string ParentQuery = "SELECT * FROM trees_items WHERE TreeID=2 AND ItemParent=-1 ORDER by ItemOrder ASC";
            SqlCommand cmd = new SqlCommand(ParentQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    parent += Convert.ToString(r["ItemID"]) + ",";
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            parent = Regex.Replace(parent, ",$", "");
            parentids = parent.Split(',');
            return parentids;
        }

        /// <summary>
        /// Get Parent items.
        /// </summary>
        private string get_parent_items_data(string id)
        {
            string pardata = string.Empty;                            
                pardata += get_parent_data(id);
                string TreeQuery = "SELECT * FROM trees_items WHERE ItemParent=" + id + " ORDER by ItemOrder ASC";
                SqlCommand cmd = new SqlCommand(TreeQuery, con);
                SqlDataReader r;
                try
                {
                    con.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        int open = Convert.ToInt32(r["ItemOpen"]);
                        int link = Convert.ToInt32(r["isLink"]);
                        string sopen = string.Empty;
                        string islink = string.Empty;
                        if (open == 1)
                            sopen = " open=\"1\"";
                        else
                            sopen = "";

                        if (link == 1)
                            islink = "^link";
                        else
                            islink = "";

                        pardata += "<item text=\"" + r["Text"].ToString() + "\" id=\"" + r["TextID"].ToString() + islink + "\" im0=\"" + r["Ima"].ToString() + "\" im1=\"" + r["Imb"].ToString() + "\" im2=\"" + r["Imc"].ToString() + "\"" + sopen + " >";
                        pardata += "</item>";
                    }
                    r.Close();
                }
                catch { }
                finally { con.Close(); }
                pardata += "</item>";
            return pardata;
        }

        /// <summary>
        /// Get Parent data.
        /// </summary>
        private string get_parent_data(string id)
        {
            string pardata = string.Empty;
            string PDataQuery = "SELECT * FROM trees_items WHERE ItemID=" + id + " AND ItemParent=-1";
            SqlCommand cmd = new SqlCommand(PDataQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                r.Read();
                int open = Convert.ToInt32(r["ItemOpen"]);
                int link = Convert.ToInt32(r["isLink"]);
                string sopen = string.Empty;
                string islink = string.Empty;
                if (open == 1)
                    sopen = " open=\"1\"";
                else
                    sopen = "";
                
                if (link == 1)
                    islink = "^link";
                else
                    islink = "";
                pardata += "<item text=\"" + r["Text"].ToString() + "\" id=\"" + r["TextID"].ToString() + islink + "\" im0=\"" + r["Ima"].ToString() + "\" im1=\"" + r["Imb"].ToString() + "\" im2=\"" + r["Imc"].ToString() + "\"" + sopen + " >";
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            return pardata;
        }

        /// <summary>
        /// Get Sub Parent data.
        /// </summary>
        private string get_subparent_data(string id)
        {
            string pardata = string.Empty;
            string PDataQuery = "SELECT * FROM trees_items WHERE ItemID=" + id + " AND ItemParent>0";
            SqlCommand cmd = new SqlCommand(PDataQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                r.Read();
                int open = Convert.ToInt32(r["ItemOpen"]);
                int link = Convert.ToInt32(r["isLink"]);
                string sopen = string.Empty;
                string islink = string.Empty;
                if (open == 1)
                    sopen = " open=\"1\"";
                else
                    sopen = "";

                if (link == 1)
                    islink = "^link";
                else
                    islink = "";
                pardata += "<item text=\"" + r["Text"].ToString() + "\" id=\"" + r["TextID"].ToString() + islink + "\" im0=\"" + r["Ima"].ToString() + "\" im1=\"" + r["Imb"].ToString() + "\" im2=\"" + r["Imc"].ToString() + "\"" + sopen + " >";
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            return pardata;
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