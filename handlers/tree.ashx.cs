/**
 * WWF Honduras
 * AgriPanda v2.0.0
 * Main Tree XML Generator Handler
 * Last Updated: $Date: 2015-05-13 11:04:08 -0600 (Tue, 18 Jun 2013) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2015 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Tree
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0004 $
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
        /// Add class_global.
        /// </summary>
        private class_global std = new class_global();
        /// <summary>
        /// Connect to mssql DB.
        /// </summary>
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);
        /// <summary>
        /// String xml for output.
        /// </summary>
        private string xml;
        ///---------------------------------------
        /// Set class variables
        ///--------------------------------------- 
        /// <summary>
        /// User global perms.
        /// </summary>
        private Dictionary<string, string> _ag;
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

            string modCode = Convert.ToString(HttpContext.Current.Request["accor"]);
            string mid = string.Empty;
            /// ------------------------------------
            /// Get the mod ID
            /// ------------------------------------
            string MQuery = "SELECT * FROM mods WHERE ModCode='" + modCode + "'";
            SqlCommand cmd = new SqlCommand(MQuery, con);
            SqlDataReader mq;
            try
            {
                con.Open();
                mq = cmd.ExecuteReader();
                while (mq.Read())
                {
                    mid = Convert.ToString(mq["ModID"]);
                }
                mq.Close();
            }
            catch { }
            finally { con.Close(); }

            string[] parents = get_parent_ids(mid, _ag["GroupID"]);
            string[] par = parents.Distinct().ToArray();

            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<tree id=\"0\" radio=\"1\" mid=\"" + mid + "\">";
            for (int cnt = 0; cnt < par.Length; cnt++)
            {
                xml += get_parent_items_data(par[cnt], _ag["GroupID"]);
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
        private string[] get_parent_ids(string mid, string gid)
        {
            string parent = string.Empty;
            string[] parentids;
            string ParentQuery = "SELECT * FROM mods_tree_items WHERE ModID=" + mid + " AND ItemParent=-1 ORDER by ItemPosition ASC";
            SqlCommand cmd = new SqlCommand(ParentQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string isLocked = Convert.ToString(r["isLocked"]);
                    string dbgids = Convert.ToString(r["GroupIDs"]);
                    string[] dbgid = dbgids.Split(':');
                    string gadmin = Convert.ToString(_ag["GroupAdmin"]);
                    if (gadmin == "1")
                        parent += Convert.ToString(r["ItemID"]) + ",";
                    else
                    {
                        if (isLocked == "1")
                            parent += Convert.ToString(r["ItemID"]) + ",";
                        else if (isLocked == "0")
                        {
                            for (var i = 0; i < dbgid.Length; i++)
                            {
                                if(gid == dbgid[i])
                                    parent += Convert.ToString(r["ItemID"]) + ",";
                            }
                        }                            
                    }
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
        private string get_parent_items_data(string id, string gid)
        {
            string pardata = string.Empty;
            pardata += get_parent_data(id);
            string TreeQuery = "SELECT * FROM mods_tree_items WHERE ItemParent=" + id + " ORDER by ItemPosition ASC";
            SqlCommand cmd = new SqlCommand(TreeQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int open = Convert.ToInt32(r["isOpen"]);
                    int link = Convert.ToInt32(r["isLink"]);
                    string sopen = string.Empty;
                    string islink = string.Empty;
                    string isLocked = Convert.ToString(r["isLocked"]);
                    string dbgids = Convert.ToString(r["GroupIDs"]);
                    string[] dbgid = dbgids.Split(':');
                    string gadmin = Convert.ToString(_ag["GroupAdmin"]);
                    if (open == 1)
                        sopen = " open=\"1\"";
                    else
                        sopen = "";

                    if (link == 1)
                        islink = "^link";
                    else
                        islink = "";
                    if (gadmin == "1")
                        pardata += "<item text=\"" + r["ItemName"].ToString() + "\" id=\"" + r["ItemCode"].ToString() + islink + "\" im0=\"" + r["Ima"].ToString() + "\" im1=\"" + r["Imb"].ToString() + "\" im2=\"" + r["Imc"].ToString() + "\"" + sopen + " ></item>";
                    else
                    {
                        if (isLocked == "1")
                            pardata += "<item text=\"" + r["ItemName"].ToString() + "\" id=\"" + r["ItemCode"].ToString() + islink + "\" im0=\"" + r["Ima"].ToString() + "\" im1=\"" + r["Imb"].ToString() + "\" im2=\"" + r["Imc"].ToString() + "\"" + sopen + " ></item>";
                        else if (isLocked == "0")
                        {
                            for (var i = 0; i < dbgid.Length; i++)
                            {
                                if (gid == dbgid[i])
                                    pardata += "<item text=\"" + r["ItemName"].ToString() + "\" id=\"" + r["ItemCode"].ToString() + islink + "\" im0=\"" + r["Ima"].ToString() + "\" im1=\"" + r["Imb"].ToString() + "\" im2=\"" + r["Imc"].ToString() + "\"" + sopen + " ></item>";
                            }
                        }
                    }
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
            string PDataQuery = "SELECT * FROM mods_tree_items WHERE ItemID=" + id + " AND ItemParent=-1";
            SqlCommand cmd = new SqlCommand(PDataQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                r.Read();
                int open = Convert.ToInt32(r["isOpen"]);
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
                pardata += "<item text=\"" + r["ItemName"].ToString() + "\" id=\"" + r["ItemCode"].ToString() + islink + "\" im0=\"" + r["Ima"].ToString() + "\" im1=\"" + r["Imb"].ToString() + "\" im2=\"" + r["Imc"].ToString() + "\"" + sopen + " >";
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