/**
 * WWF Honduras
 * AgriPanda v1.2.0
 * User Handler
 * Last Updated: $Date: 2014-12-03 11:09:08 -0600 (Mon, 16 Apr 2012) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2014 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Users
 * @link		http://www.wwf-mar.org
 * @since		1.2.0
 * @version		$Revision: 0013 $
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
    /// Summary description for users
    /// </summary>
    public class users : IHttpHandler
    {
        #region Variables
        /// <summary>
        /// Add class_mssql.
        /// </summary>
        private class_mssql DB = new class_mssql();
        /// <summary>
        /// Add class_global.
        /// </summary>
        private class_global std = new class_global();
        /// <summary>
        /// Add class_login.
        /// </summary>
        private class_login IN = new class_login();
        /// <summary>
        /// Connect to mssql DB.
        /// </summary>
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);
        ///---------------------------------------
        /// Set class variables
        ///--------------------------------------- 
        /// <summary>
        /// String xml for output.
        /// </summary>
        private string xml;
        /// <summary>
        /// Registration string group ID.
        /// </summary>
        private string _reg_gid;
        /// <summary>
        /// Registration string username.
        /// </summary>
        private string _reg_uname;
        /// <summary>
        /// Registration string password.
        /// </summary>
        private string _reg_passwd;
        /// <summary>
        /// Registration string password salt.
        /// </summary>
        private int _reg_psalt;
        /// <summary>
        /// Registration string firstname.
        /// </summary>
        private string _reg_fname;
        /// <summary>
        /// Registration string lastname.
        /// </summary>
        private string _reg_lname;
        /// <summary>
        /// Registration string email.
        /// </summary>
        private string _reg_email;
        /// <summary>
        /// Registration string user title.
        /// </summary>
        private string _reg_utitle;
        /// <summary>
        /// Registration string work area.
        /// </summary>
        private string _reg_workarea;
        /// <summary>
        /// Registration string phone.
        /// </summary>
        private string _reg_phone;
        /// <summary>
        /// Registration string cellphone.
        /// </summary>
        private string _reg_cell;
        /// <summary>
        /// Registration string creation date.
        /// </summary>
        private string _reg_cdate;
        #endregion

        # region Constructor
        /// <summary>
        /// Main function.
        /// </summary>
        public void ProcessRequest(HttpContext context)
        {
            switch (HttpContext.Current.Request["code"])
            {
                case "adduser":
                    show_form_user("new");
                    break;
                case "edituser":
                    show_form_user("edit");
                    break;
                case "doadd":
                    do_add_user();
                    break;
                case "doedit":
                    do_edit_user();
                    break;
                //-------------------
                default:
                    show_users();
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
        /// Show user list.
        /// </summary>
        private string show_users()
        {
            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<rows>";
            xml += "<head>";
            xml += "<column width=\"80\" type=\"ro\" align=\"center\" sort=\"int\">User ID</column>";
            xml += "<column width=\"200\" type=\"ro\" align=\"left\" sort=\"str\">User Name</column>";
            xml += "<column width=\"80\" type=\"ro\" align=\"center\" sort=\"int\">Group ID</column>";
            xml += "<settings>";
            xml += "<colwidth>px</colwidth>";
            xml += "</settings>";
            xml += "</head>";
            string UsersQuery = "SELECT * FROM users";
            SqlCommand cmd = new SqlCommand(UsersQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    xml += "<row id=\"" + Convert.ToInt32(r["UserID"]) + "\">";
                    xml += "<cell>" + Convert.ToString(r["UserID"]) + "</cell>";
                    xml += "<cell>" + Convert.ToString(r["UserName"]) + "</cell>";
                    xml += "<cell>" + Convert.ToString(r["GroupID"]) + "</cell>";
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
        private string show_form_user(string type)
        {

            if (type != "new")
            {
                string UsersQuery = "SELECT * FROM users WHERE UserID = " + HttpContext.Current.Request["uid"];
                SqlCommand cmdu = new SqlCommand(UsersQuery, con);
                SqlDataReader u;
                try
                {
                    con.Open();
                    u = cmdu.ExecuteReader();
                    while (u.Read())
                    {
                        _reg_gid = Convert.ToString(u["GroupID"]);
                        _reg_uname = Convert.ToString(u["UserName"]);
                        _reg_fname = Convert.ToString(u["UserFirstName"]);
                        _reg_lname = Convert.ToString(u["UserLastName"]);
                        _reg_email = Convert.ToString(u["UserEmail"]);
                        _reg_utitle = Convert.ToString(u["UserTitle"]);
                        _reg_workarea = Convert.ToString(u["UserWorkArea"]);
                        _reg_phone = Convert.ToString(u["UserWorkPhone"]);
                        _reg_cell = Convert.ToString(u["UserCellular"]);
                    }
                    u.Close();
                }
                catch { }
                finally { con.Close(); }
            }
            else
            {
                _reg_gid = Convert.ToString(HttpContext.Current.Request["GroupID"]);
                _reg_uname = Convert.ToString(HttpContext.Current.Request["UserName"]);
                _reg_passwd = Convert.ToString(HttpContext.Current.Request["password"]);
                _reg_fname = Convert.ToString(HttpContext.Current.Request["UserFirstName"]);
                _reg_lname = Convert.ToString(HttpContext.Current.Request["UserLastName"]);
                _reg_email = Convert.ToString(HttpContext.Current.Request["UserEmail"]);
                _reg_utitle = Convert.ToString(HttpContext.Current.Request["UserTitle"]);
                _reg_workarea = Convert.ToString(HttpContext.Current.Request["UserWorkArea"]);
                _reg_phone = Convert.ToString(HttpContext.Current.Request["UserWorkPhone"]);
                _reg_cell = Convert.ToString(HttpContext.Current.Request["UserCellular"]);
            }

            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<items>";
            xml += "<item type=\"settings\" position=\"label-left\" labelWidth=\"120\" inputWidth=\"150\"/>";
            xml += "<item type=\"fieldset\" name=\"data\" inputWidth=\"auto\" label=\"User Information\">";
            xml += "<item type=\"input\" name=\"UserName\" value=\""+ _reg_uname +"\" label=\"User Name\"/>";
            xml += "<item type=\"password\" name=\"password\" value=\""+ _reg_passwd +"\" label=\"Password\"/>";
            xml += "<item type=\"input\" name=\"UserFirstName\" value=\""+ _reg_fname +"\" label=\"First Name\"/>";
            xml += "<item type=\"input\" name=\"UserLastName\" value=\""+ _reg_lname +"\" label=\"Last Name\"/>";
            xml += "<item type=\"input\" name=\"UserTitle\" value=\""+ _reg_utitle +"\" label=\"Title\"/>";
            xml += "<item type=\"select\" name=\"GroupID\" label=\"Group ID\">";

            string GroupsQuery = "SELECT * FROM groups";
            SqlCommand cmd = new SqlCommand(GroupsQuery, con);
            SqlDataReader q;
            try
            {
                con.Open();
                q = cmd.ExecuteReader();
                while (q.Read())
                {
                    xml += "<option value=\"" + Convert.ToString(q["GroupID"]) + "\" label=\"" + Convert.ToString(q["GroupName"]) + "\"/>";
                }
                q.Close();
            }
            catch { }
            finally { con.Close(); }
		    xml += "</item>";
            xml += "<item type=\"input\" name=\"UserWorkArea\" value=\""+ _reg_workarea +"\" label=\"Work Area\"/>";
            xml += "<item type=\"input\" name=\"UserEmail\" value=\""+ _reg_email +"\" label=\"E-mail Address\"/>";
            xml += "<item type=\"input\" name=\"UserWorkPhone\" value=\""+ _reg_phone +"\" label=\"Work Telephone\"/>";
            xml += "<item type=\"input\" name=\"UserCellular\" value=\""+ _reg_cell +"\" label=\"Movil Phone\"/>";
		    xml += "<item type=\"button\" name=\"SaveButton\" value=\"Save\"/>";
            xml += "</item>";
            xml += "</items>";
            return xml;
        }

        /// <summary>
        /// Add new user.
        /// </summary>
        private void do_add_user()
        {
            ///-----------------------------------------
            /// Set up array
            ///-----------------------------------------
            _reg_gid = Convert.ToString(HttpContext.Current.Request["GroupID"]);
            _reg_uname = Convert.ToString(HttpContext.Current.Request["UserName"]);
            _reg_passwd = Convert.ToString(HttpContext.Current.Request["password"]);
            _reg_psalt = class_login.CreateRandomSalt();
            _reg_fname = Convert.ToString(HttpContext.Current.Request["UserFirstName"]);
            _reg_lname = Convert.ToString(HttpContext.Current.Request["UserLastName"]);
            _reg_email = Convert.ToString(HttpContext.Current.Request["UserEmail"]);
            _reg_utitle = Convert.ToString(HttpContext.Current.Request["UserTitle"]);
            _reg_workarea = Convert.ToString(HttpContext.Current.Request["UserWorkArea"]);
            _reg_phone = Convert.ToString(HttpContext.Current.Request["UserWorkPhone"]);
            _reg_cell = Convert.ToString(HttpContext.Current.Request["UserCellular"]);
            _reg_cdate = Convert.ToString(DateTime.Now);

            string hash = IN.ComputeSaltedHash(_reg_passwd, _reg_psalt);
            string salt = Convert.ToString(_reg_psalt);
            string[,] user_data = {
                             { "GroupID", _reg_gid },
                             { "UserName", _reg_uname },
                             { "UserPassWD", hash },
                             { "UserPassSalt", salt },
                             { "UserFirstName", _reg_fname },
                             { "UserLastName", _reg_lname },
                             { "UserEmail", _reg_email },
                             { "UserTitle", _reg_utitle },
                             { "UserWorkArea", _reg_workarea },
                             { "UserWorkPhone", _reg_phone },
                             { "UserCellular", _reg_cell },
                             { "UserCreationDate", _reg_cdate }
                             };

            DB.do_insert("users", user_data);
        }

        /// <summary>
        /// Edit new user.
        /// </summary>
        private void do_edit_user()
        {
            ///-----------------------------------------
            /// Set up array
            ///-----------------------------------------
            _reg_gid = Convert.ToString(HttpContext.Current.Request["GroupID"]);
            _reg_uname = Convert.ToString(HttpContext.Current.Request["UserName"]);
            _reg_passwd = Convert.ToString(HttpContext.Current.Request["password"]);
            _reg_psalt = class_login.CreateRandomSalt();
            _reg_fname = Convert.ToString(HttpContext.Current.Request["UserFirstName"]);
            _reg_lname = Convert.ToString(HttpContext.Current.Request["UserLastName"]);
            _reg_email = Convert.ToString(HttpContext.Current.Request["UserEmail"]);
            _reg_utitle = Convert.ToString(HttpContext.Current.Request["UserTitle"]);
            _reg_workarea = Convert.ToString(HttpContext.Current.Request["UserWorkArea"]);
            _reg_phone = Convert.ToString(HttpContext.Current.Request["UserWorkPhone"]);
            _reg_cell = Convert.ToString(HttpContext.Current.Request["UserCellular"]);
            _reg_cdate = Convert.ToString(DateTime.Now);

            string hash = IN.ComputeSaltedHash(_reg_passwd, _reg_psalt);
            string salt = Convert.ToString(_reg_psalt);
            string[,] user_data = {
                             { "GroupID", _reg_gid },
                             { "UserName", _reg_uname },
                             { "UserPassWD", hash },
                             { "UserPassSalt", salt },
                             { "UserFirstName", _reg_fname },
                             { "UserLastName", _reg_lname },
                             { "UserEmail", _reg_email },
                             { "UserTitle", _reg_utitle },
                             { "UserWorkArea", _reg_workarea },
                             { "UserWorkPhone", _reg_phone },
                             { "UserCellular", _reg_cell },
                             { "UserCreationDate", _reg_cdate }
                             };

            DB.do_insert("users", user_data);
            DB.do_update("users", user_data, "UserID=" + HttpContext.Current.Request["uid"]);
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