/**
 * WWF honduras
 * AgriPanda v2.1.0
 * Global Handler
 * Last Updated: $Date: 2018-08-22 16:53:40 -0600 (Thu, 01 Jul 2010) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2018 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Global
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0014 $
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;

namespace AgriPanda.kernel
{
    public class class_global
    {
        #region Variables
        /// <summary>
        /// Add class_global.
        /// </summary>
        private class_mssql DB = new class_mssql();
        /// <summary>
        /// Add class_skin.
        /// </summary>
        private class_skin skin = new class_skin();
        /// <summary>
        /// Connect to mssql DB.
        /// </summary>
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);
        /// <summary>
        /// Actual date.
        /// </summary>
        public DateTime datetoday = DateTime.Today;
        /// <summary>
        /// System Variables string.
        /// </summary>
        public string _sysvar = string.Empty;
        /// <summary>
        /// String for user global permissions.
        /// </summary>
        public string ugperms;
        /// <summary>
        /// String for user global user mod access.
        /// </summary>
        public string ugmods;
        /// <summary>
        /// Array for user global permissions.
        /// </summary>
        public string[] _ag;
        /// <summary>
        /// Required string for user global permissions.
        /// </summary>
        public string _req;
        #endregion


        #region Methods
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// GLOBAL FUNCTIONS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /*========================================================================*/
        /// 
        /*========================================================================*/
        /// <summary>
        /// Get global permissions for users.
        /// </summary>
        public Dictionary<string, string> get_uperms(string uname, string mid)
        {
            Dictionary<string, string> _uperms = new Dictionary<string, string>();
            Dictionary<string, string> _umods = new Dictionary<string, string>();
            string UPermsQuery = "SELECT p.*, u.* FROM groups p, users u WHERE p.GroupID=u.GroupID and u.UserName='" + uname + "'";
            SqlCommand cmd = new SqlCommand(UPermsQuery, con);
            SqlDataReader uvq;
            try
            {
                con.Open();
                uvq = cmd.ExecuteReader();
                uvq.Read();
                //int cnt = 0;
                //int cn = 0;
                ugperms = Convert.ToString(uvq["GroupPerms"]);
                if (ugperms != null)
                {
                    _ag = ugperms.Split(':');
                }
                /*for (cnt = 0; cnt < _ag.Length; cnt++)
                {
                    if (_ag[cnt] == mid)
                    {
                        _req = "yes";
                    }
                }
                if (_req == null)
                {
                    _uperms.Add("Access", "no");
                }
                else
                {
                    _uperms.Add("Access", "yes");
                }*/

                ///-----------------------------------------
                /// Check the mods that user can have access to.
                ///-----------------------------------------
                /// Get the serialized string
                ///-----------------------------------------
                ugmods = Convert.ToString(uvq["GroupMods"]);
                ///-----------------------------------------
                /// Deserialized string
                ///-----------------------------------------
                /*string[] sMods = umods(ugmods);
                for (cn = 0; cn < sMods.Length; cn++)
                {
                    if (sMods[cn] == mid)
                        _req = "yes";
                }
                if (String.IsNullOrEmpty(_req))
                    _uperms.Add("Access", "no");
                else
                    _uperms.Add("Access", "yes");*/

                _uperms.Add("Crops", Convert.ToString(uvq["GroupCrops"]));
                _uperms.Add("GroupAdmin", Convert.ToString(uvq["GroupAdmin"]));
                _uperms.Add("GroupID", Convert.ToString(uvq["GroupID"]));
                _uperms.Add("UserCanAdd", Convert.ToString(uvq["UserCanAdd"]));
                _uperms.Add("UserCanEdit", Convert.ToString(uvq["UserCanEdit"]));
                _uperms.Add("UserCanDelete", Convert.ToString(uvq["UserCanDelete"]));
                _uperms.Add("UserSuperAdmin", Convert.ToString(uvq["UserSuperAdmin"]));
                _uperms.Add("UserID", Convert.ToString(uvq["UserID"]));
                _uperms.Add("UserLanguage", Convert.ToString(uvq["UserLanguage"]));
                _uperms.Add("UserSkin", Convert.ToString(uvq["UserSkin"]));
                _uperms.Add("UserMods", ugmods);
            }
            catch{ }
            finally{ con.Close(); }
            return _uperms;
        }

        /// <summary>
        /// Check if object is numeric.
        /// </summary>
        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;
            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;
            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

        /// <summary>
        /// Check if string is valid datetime.
        /// </summary>
        public static bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Save log for every actions.
        /// </summary>
        public void save_log(string uid, string note, string page)
        {
            string[,] log_data = {
                             { "UserID", uid },
                             { "DateStamp", Convert.ToString(DateTime.Now) },
                             { "Note",  note},
                             { "Page",  page}
                             };

            DB.do_insert("logs", log_data);
        }

        /// <summary>
        /// Get System Variables from DB.
        /// </summary>
        public string get_system_vars(string svkey)
        {
            string sysvar = string.Empty;
            string SYSVARQuery = "SELECT * FROM system_vars WHERE SVKEY='" + svkey + "'";
            SqlCommand cmd = new SqlCommand(SYSVARQuery, con);
            SqlDataReader q;
            try
            {
                con.Open();
                q = cmd.ExecuteReader();
                q.Read();
                sysvar = Convert.ToString(q["SVContent"]);
                q.Close();
            }
            catch { }
            finally { con.Close(); }
            return sysvar;
        }

        /// <summary>
        /// Get System Languages from config file.
        /// </summary>
        public string get_system_langs(string lid)
        {
            string langs = Resources.config.SystemLanguages;
            string[] items = langs.Split(';');
            string[,] larr;
            string html = string.Empty;
            html = skin.form_dropdown_start("UserLanguage");
            for (int c = 0; c < items.Length; c++)
            {
                string[] itm = items[c].Split(':');
                larr = new string[,] { { itm[0], itm[1] } };
                html += skin.form_dropdown_opts(larr, Convert.ToString(lid));
            }
            html += skin.form_dropdown_end();
            return html;
        }

        /// <summary>
        /// Get System Skins from config file.
        /// </summary>
        public string get_system_skins(string sid)
        {
            string skins = Resources.config.SystemSkins;
            string[] items = skins.Split(';');
            string[,] sarr;
            string html = string.Empty;
            html = skin.form_dropdown_start("UserSkin");
            for (int c = 0; c < items.Length; c++)
            {
                string[] itm = items[c].Split(':');
                sarr = new string[,] { { itm[0], itm[1] } };
                html += skin.form_dropdown_opts(sarr, Convert.ToString(sid));
            }
            html += skin.form_dropdown_end();
            return html;
        }

        /// <summary>
        /// Makes incoming info "safe"
        /// </summary>
        public string parse_incoming(string sParam)
        {
            string sResult = string.Empty;
            if ((HttpContext.Current.Request[sParam] != null) && (HttpContext.Current.Request[sParam].Trim() != ""))
            {
                sResult = HttpContext.Current.Request[sParam].Trim();
            }
            return sResult;
        }

        /// <summary>
        /// Create the pagination global system
        /// </summary>
        public string get_pagination(string page, int limit, int rnum, string addlink)
        {
            StringBuilder pags = new StringBuilder();
            string pageCompile = string.Empty;
            double pageCount = (double)rnum / limit;
            pageCount = Math.Ceiling(pageCount);
            int pageNo = 0;
            if (String.IsNullOrEmpty(page))
                pageNo = 1;
            else
                pageNo = Convert.ToInt32(page);
            ///-----------------------------------------
            if (pageNo > 1)
            {
                pags.Append(skin.pagination_start_dots("?page=" + 1 + addlink));
                pags.Append(skin.pagination_previous_link("?page=" + (pageNo - 1) + addlink));
            }                
            for (int i = 1; i <= pageCount; i++)
            {
                if (i == pageNo)
                    pags.Append(skin.pagination_current_page(i));
                else if (i > pageNo - 4 && i < pageNo + 4)
                    pags.Append(skin.pagination_page_link("?page=" + i.ToString("0") + addlink, i));
            }
            if (pageNo < pageCount)
            {                
                pags.Append(skin.pagination_next_link("?page=" + (pageNo + 1) + addlink));
                pags.Append(skin.pagination_end_dots("?page=" + pageCount + addlink));
            }                
            ///----------------------------------------- 
            pageCompile = skin.end_table_pagination("<div class=\"center\"><ul class=\"pagination\">" + pags.ToString() + "</ul></div>");
            return pageCompile;
        }

        /// <summary>
        /// Get the offset for pagination
        /// </summary>
        public string get_pagination_offset(string page, int limit)
        {
            string offset = string.Empty;
            if (String.IsNullOrEmpty(page))
                page = "1";
            int getoffset = (Convert.ToInt32(page) * limit) + 1 - limit;
            offset = Convert.ToString(getoffset);
            return offset;
        }

        /// <summary>
        /// Count how many rows on table
        /// </summary>
        public int get_rows_db(string db)
        {
            int rownum = 0;
            string CountQuery = "SELECT COUNT(*) as count FROM " + db;
            SqlCommand cmd = new SqlCommand(CountQuery, con);
            SqlDataReader cq;
            try
            {
                con.Open();
                cq = cmd.ExecuteReader();
                cq.Read();
                rownum = Convert.ToInt32(cq["count"]);
                cq.Close();
            }
            catch { }
            finally { con.Close(); }
            return rownum;
        }

        /// <summary>
        /// Get global settings from DB
        /// </summary>
        public Dictionary<string, string> get_global_settings()
        {
            Dictionary<string, string> _settings = new Dictionary<string, string>();
            string CountQuery = "SELECT * FROM global_settings";
            SqlCommand cmd = new SqlCommand(CountQuery, con);
            SqlDataReader gs;
            try
            {
                con.Open();
                gs = cmd.ExecuteReader();
                while (gs.Read())
                {
                    _settings.Add(Convert.ToString(gs["GSName"]), Convert.ToString(gs["GSValue"]));
                }
                gs.Close();
            }
            catch { }
            finally { con.Close(); }
            return _settings;
        }

        /// <summary>
        /// Get admin users from DB
        /// </summary>
        public string get_admin_emails()
        {
            ///-----------------------------------------
            /// Get admin group ID
            ///-----------------------------------------
            string _agID = string.Empty;
            string agQuery = "SELECT * FROM groups WHERE GroupAdmin=1";
            SqlCommand cmd = new SqlCommand(agQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                r.Read();
                _agID = Convert.ToString(r["GroupID"]);
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            ///-----------------------------------------
            /// Get emails from admin users using admin group ID
            ///-----------------------------------------
            string _auEmails = string.Empty;
            string auQuery = "SELECT * FROM users WHERE GroupID=" + _agID;
            SqlCommand cu = new SqlCommand(auQuery, con);
            SqlDataReader u;
            try
            {
                con.Open();
                u = cu.ExecuteReader();
                while (u.Read())
                {
                    string fullname = Convert.ToString(u["UserFirstName"]) + " " + Convert.ToString(u["UserLastName"]);
                    _auEmails += Convert.ToString(u["UserEmail"]) + "," + fullname + ";";
                }
                u.Close();
            }
            catch { }
            finally { con.Close(); }
            ///-----------------------------------------
            _auEmails = Regex.Replace(_auEmails, ";$", "");
            return _auEmails;
        }

        /// <summary>
        /// Get start and last date for datepicker (COMPLETE).
        /// </summary>
        public string getDates(string tblname, string colname, int addoneday)
        {
            string dates = string.Empty;
            DateTime sdt = datetoday.AddDays(-7);
            DateTime edt = datetoday;
            string SQuery = "SELECT MAX(" + colname + ") AS MaxWSDate, MIN(" + colname + ") AS MinWSDate FROM " + tblname;
            SqlCommand cmd = new SqlCommand(SQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    sdt = Convert.ToDateTime(r["MinWSDate"]);
                    edt = Convert.ToDateTime(r["MaxWSDate"]);
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            if (addoneday == 1)
                edt = edt.AddDays(1); // Add one day if necessary
            dates += sdt.ToString("yyyy-MM-dd") + "," + edt.ToString("yyyy-MM-dd");
            return dates;
        }

        /// <summary>
        /// Get user mods
        /// </summary>
        public string[] umods(string um)
        {
            string[] sMods;
            string modlist = string.Empty;
            um = Regex.Replace(um, "\\]$", "");
            um = Regex.Replace(um, "^S\\[", "");
            string[] tMods = um.Split(';');
            for (var i = 0; i < tMods.Length; i++)
            {
                string[] stMods = tMods[i].Split(':');
                for (var e = 1; e < stMods.Length; e++)
                {
                    modlist += stMods[0] + stMods[i] + ":";
                }
            }
            modlist = Regex.Replace(modlist, ":$", "");
            sMods = modlist.Split(':');
            return sMods;
        }

        /// <summary>
        /// Send email using global settings.
        /// </summary>
        public void global_send_email(string email, string name, string subj, string bodymsg)
        {
            Dictionary<string, string> _gs = get_global_settings();
            var fromAddress = new MailAddress(_gs["SMTPFromEmail"], _gs["SMTPFromName"]);
            var toAddress = new MailAddress(email, name);
            string fromPassword = _gs["SMTPFromPasswd"];
            string subject = subj;
            string body = bodymsg;

            var smtp = new SmtpClient
            {
                Host = _gs["SMTPHost"], // Host = "smtp.office365.com",
                Port = Convert.ToInt32(_gs["SMTPPort"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// Generic call FUNCTIONS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get a dictionary of any table from DB <string, string>
        /// </summary>
        public Dictionary<string, string> get_dictionary_string_string(string table, string item, string name)
        {
            Dictionary<string, string> _generic = new Dictionary<string, string>();
            string Query = "SELECT * FROM " + table;
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader g;
            try
            {
                con.Open();
                g = cmd.ExecuteReader();
                while (g.Read())
                {
                    _generic.Add(Convert.ToString(g[item]), Convert.ToString(g[name]));
                }
                g.Close();
            }
            catch { }
            finally { con.Close(); }
            return _generic;
        }

        /// <summary>
        /// Get a dictionary of any table from DB <int, string>
        /// </summary>
        public Dictionary<int, string> get_dictionary_int_string(string table, string item, string name)
        {
            Dictionary<int, string> _generic = new Dictionary<int, string>();
            string Query = "SELECT * FROM " + table;
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader g;
            try
            {
                con.Open();
                g = cmd.ExecuteReader();
                while (g.Read())
                {
                    _generic.Add(Convert.ToInt32(g[item]), Convert.ToString(g[name]));
                }
                g.Close();
            }
            catch { }
            finally { con.Close(); }
            return _generic;
        }

        /// <summary>
        /// Get a list of any table from DB <int>
        /// </summary>
        public List<int> get_list_int(string table, string item)
        {
            List<int> _generic = new List<int>();
            string Query = "SELECT * FROM " + table;
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader l;
            try
            {
                con.Open();
                l = cmd.ExecuteReader();
                while (l.Read())
                {
                    _generic.Add(Convert.ToInt32(l[item]));
                }
                l.Close();
            }
            catch { }
            finally { con.Close(); }
            return _generic;
        }

        /// <summary>
        /// Get a substring of the first N characters.
        /// </summary>
        public string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length);
            }
            return source;
        }

        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// Javascript call FUNCTIONS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load jQuery scripts for tooltipster.
        /// </summary>
        public string load_tooltipster_scripts()
        {
            StringBuilder pageContent = new StringBuilder();
            pageContent.Append("\n<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Resources.config.MainURL + "/lib/tooltipster-master/dist/css/tooltipster.bundle.min.css\" />\n");
            pageContent.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Resources.config.MainURL + "/lib/tooltipster-master/dist/css/plugins/tooltipster/sideTip/themes/tooltipster-sideTip-light.min.css\" />\n");
            pageContent.Append("<script type=\"text/javascript\" src=\"http://code.jquery.com/jquery-1.10.0.min.js\"></script>\n");
            pageContent.Append("<script type=\"text/javascript\" src=\"" + Resources.config.MainURL + "/lib/tooltipster-master/dist/js/tooltipster.bundle.min.js\"></script>\n");

            string scripts = Convert.ToString(pageContent);
            scripts += @"<script>
        $(document).ready(function() {
            $('.tooltip').tooltipster({
                theme: 'tooltipster-light'
            });
        });
    </script>";
            return scripts;
        }

        /// <summary>
        /// Load jQuery scripts for rangepicker.
        /// </summary>
        public string load_rangepicker_scripts()
        {
            StringBuilder pageContent = new StringBuilder();
            pageContent.Append("<link rel=\"stylesheet\" href=\"" + Resources.config.MainURL + "/lib/jquery.periodpicker/jquery.periodpicker.css\">");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/jquery.periodpicker/moment.js\"></script>");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/jquery.periodpicker/jquery.min.js\"></script>");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/jquery.periodpicker/jquery.mousewheel.min.js\"></script>");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/jquery.periodpicker/jquery.periodpicker.min.js\"></script>");
            pageContent.Append("<link rel=\"stylesheet\" type=\"text / css\" href=\"" + Resources.config.MainURL + "/lib/colorpicker/spectrum.css\">");
            pageContent.Append("<script type='text/javascript' src=\"" + Resources.config.MainURL + "/lib/colorpicker/spectrum.js\"></script>");

            string scripts = Convert.ToString(pageContent);
            scripts += @"<style type='text/css'>
.sp-palette {
max-width: 200px;
}

.label {
    background: #444;
    padding:8px;    
    border-radius: 4px;
    color: #eee;
    margin-top: 10px;
    display:inline-block;
}
</style>";
            return scripts;
        }

        /// <summary>
        /// Load jQuery scripts for datepicker.
        /// </summary>
        public string load_datepicker_scripts()
        {
            StringBuilder pageContent = new StringBuilder();
            pageContent.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Resources.config.MainURL + "/lib/jquery.datetimepicker-master/jquery.datetimepicker.css\"/>");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/jquery.datetimepicker-master/jquery.js\"></script>");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/jquery.datetimepicker-master/build/jquery.datetimepicker.full.min.js\"></script>");

            string scripts = Convert.ToString(pageContent);
            return scripts;
        }

        /// <summary>
        /// Load jQuery scripts for bootstrap multiselect.
        /// </summary>
        public string load_bootstrap_multiselect()
        {
            StringBuilder pageContent = new StringBuilder();
            pageContent.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Resources.config.MainURL + "/lib/bootstrap-multiselect/bootstrap.min.css\"/>");
            pageContent.Append("<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js\"></script>");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/bootstrap-multiselect/bootstrap.min.js\"></script>");
            pageContent.Append("<script src=\"" + Resources.config.MainURL + "/lib/bootstrap-multiselect/bootstrap-multiselect.js\"></script>");
            pageContent.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Resources.config.MainURL + "/lib/bootstrap-multiselect/bootstrap-multiselect.css\"/>");

            string scripts = Convert.ToString(pageContent);
            return scripts;
        }

        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// ADDUPI FUNCTIONS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// <summary>
        /// Connect to addUPI Server.
        /// </summary>
        public string connect_to_addupi()
        {
            string SessionID = string.Empty;
            XmlDocument d = new XmlDocument();
            d.Load(Resources.config.addUPIServer + "addUPI?function=login&user=" + Resources.config.addUPIUser + "&passwd=" + Resources.config.addUPIPasswd + "&timeout=7200&mode=t");
            XmlNodeList n = d.GetElementsByTagName("string");
            if (n != null)
            {
                    SessionID = n[0].InnerText;
            }
            return SessionID;
        }

        /// <summary>
        /// Get addUPI Session from DB.
        /// </summary>
        public string get_addupi_session(string userid)
        {
            string SessionID = string.Empty;
            string addUPIQuery = "SELECT * FROM users_addupi_sessions WHERE UserID = '" + userid + "'";
            SqlCommand cmdb = new SqlCommand(addUPIQuery, con);
            SqlDataReader upiq;

            // Try to open database and read information.
            try
            {
                con.Open();
                upiq = cmdb.ExecuteReader();
                upiq.Read();

                SessionID = upiq["SessionID"].ToString();

                upiq.Close();
            }
            catch { }
            finally { con.Close(); }
            return SessionID;
        }

        /// <summary>
        /// Get addUPI Nodes from DB.
        /// </summary>
        public Dictionary<string, string> get_addupi_nodes(string wsid, string period)
        {
            Dictionary<string, string> _nodes = new Dictionary<string, string>();
            string AQuery = "SELECT * FROM weather_addupi_nodes WHERE WSID=" + wsid + " AND AvgType=" + period;
            SqlCommand cmda = new SqlCommand(AQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmda.ExecuteReader();
                while (r.Read())
                {
                    _nodes.Add(Convert.ToString(r["SensorID"]), Convert.ToString(r["NodeID"]));
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            return _nodes;
        }

        /// <summary>
        /// Get value for each addUPI node.
        /// </summary>
        public string get_addupi_node_val(string SessID, string node, string ntime)
        {
            string val = string.Empty;
            XmlDocument dval = new XmlDocument();
            dval.Load(Resources.config.addUPIServer + "addUPI?function=getdata&session-id=" + SessID + "&id=" + node + "&date=" + ntime + "&mode=t");
            XmlNodeList a = dval.GetElementsByTagName("v");
            if (a != null)
            {
                foreach (XmlNode curr in a)
                {
                    val = curr.InnerText;
                }
            }
            return val;
        }

        /// <summary>
        /// Get value for each addUPI node.
        /// </summary>
        public void addupi_logout(string SessID)
        {
            string SessionID = string.Empty;
            XmlDocument d = new XmlDocument();
            d.Load(Resources.config.addUPIServer + "addUPI?function=logout&session-id=" + SessID + "&mode=t");
            XmlNodeList n = d.GetElementsByTagName("result");
            if (n == null)
            {
                foreach (XmlNode curr in n)
                {
                    SessionID = "Out";
                }
            }
        }
        #endregion

    }
}