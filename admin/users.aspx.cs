/**
 * WWF honduras
 * AgriPanda v2.1.1
 * Posts Handler
 * Last Updated: $Date: 2018-08-20 11:12:10 -0600 (Thu, 01 Jul 2010) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2018 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.users
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

namespace AgriPanda.admin
{
    public partial class users : System.Web.UI.Page
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
        /// Add class_skin.
        /// </summary>
        private class_skin skin = new class_skin();
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
        ///private string xml;
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
        /// Confirmation string password.
        /// </summary>
        private string _reg_confirm_passwd;
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
        /// Registration string organization.
        /// </summary>
        private string _reg_org;
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
        /// <summary>
        /// Registration string language.
        /// </summary>
        private string _reg_lang;
        /// <summary>
        /// Registration string skin.
        /// </summary>
        private string _reg_skin;
        /// <summary>
        /// Text on each button.
        /// </summary>
        private string _button;
        /// <summary>
        /// Code for form.
        /// </summary>
        private string _code;
        /// <summary>
        /// Array for table rows.
        /// </summary>
        public string[] td_array;
        /// <summary>
        /// Get global settings
        /// </summary>
        Dictionary<string, string> _globalSettings;
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
            /// Get global settings
            ///--------------------------------------- 
            _globalSettings = std.get_global_settings(); // Get global settings
            ///---------------------------------------
            /// Set user prefrered language
            ///--------------------------------------- 
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);
            ///---------------------------------------
            /// Main request manager
            ///---------------------------------------
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
                case "view":
                    view_user();
                    break;
                case "delete":
                    delete_user();
                    break;
                case "passwd":
                    show_form_passwd();
                    break;
                case "dochangepwd":
                    do_change_passwd();
                    break;
                case "enable":
                    enable_user();
                    break;
                case "ban":
                    ban_user();
                    break;
                case "unban":
                    unban_user();
                    break;
                //-------------------
                default:
                    Loadtooltipster.Text = std.load_tooltipster_scripts();
                    LoadData.Text = show_users(Convert.ToString(HttpContext.Current.Request["page"]));
                    break;
            } 
        }
        #endregion

        #region Methods
        ///---------------------------------------
        /// Region where class functions are declared.
        ///---------------------------------------
        /// <summary>
        /// Show user list.
        /// </summary>
        private string show_users(string page)
        {
            StringBuilder pageContent = new StringBuilder();
            int limit = 10; // Rows displayed
            int rnum = 0; // Total rows
            string offset = std.get_pagination_offset(page, limit);
            string _addlinks = "";
            //-------------------------------
            /*string valUserName = string.Empty;
            string logQuery = "SELECT * FROM logs WHERE Page='Login' ORDER BY DateStamp DESC";
            SqlCommand cmdl = new SqlCommand(logQuery, con);
            SqlDataReader lr;
            try
            {
                con.Open();
                wsr = cmdws.ExecuteReader();
                wsr.Read();
                valUserName = Convert.ToString(wsr["UserName"]);
                wsr.Close();
            }
            catch { }
            finally { con.Close(); }*/
            //-------------------------------
            string[,] tableheader = {
                                    { "&nbsp;", "2%", "" },
                                    { "&nbsp;&nbsp; " + Resources.users.TitleUserName, "35%", "left" },
                                    { "&nbsp;&nbsp; " + Resources.users.TitleLastLogin, "20%", "left" },
                                    { Resources.global.actions, "43%", "center" }
                                    };
            pageContent.Append(skin.start_table(Resources.users.Title, Resources.users.Description, tableheader)); // Start of Table
            ///---------------------------------------
            /// Select all users.
            ///---------------------------------------
            //string UQuery = "SELECT * FROM users ORDER by UserCreationDate DESC";
            string UQuery = "WITH Results_CTE AS (SELECT *, ROW_NUMBER() OVER(ORDER BY UserCreationDate DESC) AS RowNum FROM users) SELECT *, (SELECT MAX(RowNum) FROM Results_CTE) AS TotalRows FROM Results_CTE WHERE RowNum >= " + offset + " AND RowNum < " + offset + " + " + limit;
            SqlCommand cmd = new SqlCommand(UQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    rnum = Convert.ToInt32(r["TotalRows"]);
                    string ban = string.Empty;
                    string isAdmin = Convert.ToString(r["UserSuperAdmin"]);
                    string GroupID = Convert.ToString(r["GroupID"]);
                    string gid = string.Empty;
                    ///---------------------------------------
                    /// Disable tag
                    ///---------------------------------------
                    if (isAdmin == "1")
                        ban = " &nbsp; ";
                    else
                    {
                        if (GroupID == _globalSettings["UserBlockedGroup"])
                            ban = " &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipUnbanButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=unban&uid=" + r["UserID"] + "\" class=\"sexybutton\"><i class=\"fa fa-user-check\"></i>" + Resources.global.UnbanTitle + "</a></span> &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipDeleteButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=delete&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-red\"><i class=\"fa fa-trash-alt\"></i>" + Resources.global.DeleteTitle + "</a></span>";
                        else
                            ban = " &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipBanButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=ban&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-orange\"><i class=\"fa fa-user-slash\"></i>" + Resources.global.BanTitle + "</a></span>";
                    }
                    ///---------------------------------------
                    /// Enable tag
                    ///---------------------------------------
                    if (GroupID == _globalSettings["UserPendingGroup"])
                        gid = "<span class=\"tooltip\" title=\"" + Resources.users.TooltipEnableButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=enable&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-purple\"><i class=\"fa fa-check\"></i>" + Resources.global.EnableTitle + "</a></span> &nbsp;  ";
                    else
                        gid = " &nbsp; ";
                    ///---------------------------------------
                    td_array = new string[] {
                                "&nbsp;",
                                "&nbsp;&nbsp;<b>" + Convert.ToString(r["UserName"]) + "</b>",
                                "<center>" + Convert.ToString(r["UserLastLogin"]) + "</center>",
                                "<center>" + gid + "<span class=\"tooltip\" title=\"" + Resources.users.ChangePasswd + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=passwd&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-blue\"><i class=\"fa fa-lock\"></i>" + Resources.global.PasswdTitle + "</a></span> &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipViewButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=view&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-yellow\"><i class=\"fa fa-eye\"></i>" + Resources.global.ViewTitle + "</a></span> &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipEditButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=edituser&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-green\"><i class=\"fa fa-edit\"></i>" + Resources.global.EditTitle + "</a></span>" + ban + "</center>",
                                };
                    pageContent.Append(skin.add_td_row(td_array, "")); // Add rows
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            ///---------------------------------------
            //LoadData.Text += skin.end_table(); // End of Table
            pageContent.Append(std.get_pagination(page, limit, rnum, _addlinks)); // End of Table
            ///---------------------------------------
            string userlist = Convert.ToString(pageContent);
            return userlist;
        }

        /// <summary>
        /// Show form to add or edit users.
        /// </summary>
        private void show_form_user(string type)
        {
            if (type != "new")
            {
                if (String.IsNullOrEmpty(std.parse_incoming("uid")))
                    LoadData.Text = skin.error_page(Resources.users.ErrorNoUID, "yellow");
                ///-----------------------------------------
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
                        _reg_org = Convert.ToString(u["UserOrganization"]);
                        _reg_phone = Convert.ToString(u["UserWorkPhone"]);
                        _reg_cell = Convert.ToString(u["UserCellular"]);
                        _reg_lang = Convert.ToString(u["UserLanguage"]);
                        _reg_skin = Convert.ToString(u["UserSkin"]);
                    }
                    u.Close();
                }
                catch { }
                finally { con.Close(); }
                ///---------------------------------------
                _button = Resources.users.ButtonEditUser;
                _code = "doedit";
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
                _reg_org = Convert.ToString(HttpContext.Current.Request["UserOrganization"]);
                _reg_phone = Convert.ToString(HttpContext.Current.Request["UserWorkPhone"]);
                _reg_cell = Convert.ToString(HttpContext.Current.Request["UserCellular"]);
                _reg_lang = Convert.ToString(HttpContext.Current.Request["UserLanguage"]);
                _reg_skin = Convert.ToString(HttpContext.Current.Request["UserSkin"]);
                ///---------------------------------------
                _button = Resources.users.ButtonAddUser;
                _code = "doadd";
            }
            ///-----------------------------------------
            /// Create group dropdown
            ///-----------------------------------------
            string[,] garr;
            string _group = skin.form_dropdown_start("GroupID");
            string gQuery = "SELECT * FROM groups WHERE GroupEdit=1";
            SqlCommand cmdut = new SqlCommand(gQuery, con);
            SqlDataReader gr;
            try
            {
                con.Open();
                gr = cmdut.ExecuteReader();
                while (gr.Read())
                {
                    garr = new string[,] { { Convert.ToString(gr["GroupID"]), Convert.ToString(gr["GroupName"]) } };
                    _group += skin.form_dropdown_opts(garr, _reg_gid);
                }
                gr.Close();
            }
            catch { }
            finally { con.Close(); }
            _group += skin.form_dropdown_end();
            ///-----------------------------------------
            /// Get languages dropdown
            ///-----------------------------------------
            string _languages = std.get_system_langs(_reg_lang);
            ///-----------------------------------------
            /// Get skin dropdown
            ///-----------------------------------------
            string _skins = std.get_system_skins(_reg_skin);
            ///-----------------------------------------
            string NewTitle = Resources.users.Title;
            ///-----------------------------------------
            string[,] startform = { { "code", _code }, { "uid", HttpContext.Current.Request["uid"] } };
            string[,] tableheader = { { "&nbsp;", "20%", "center" }, { "&nbsp;", "80%", "left" } };
            ///-----------------------------------------
            /// Rows Arrays
            ///-----------------------------------------
            string[,] td_rows;
            if (type != "new")
            {
                td_rows = new string[,] {
                                            { "<b>" + Resources.users.GroupID + "</b>", _group },
                                            { "<b>" + Resources.users.UserName + "</b>", skin.form_input("UserName", _reg_uname, "input", "40", "disabled") },
                                            { "<b>" + Resources.users.FirstName + "</b>", skin.form_input("UserFirstName", Convert.ToString(_reg_fname), "input", "40", "") },
                                            { "<b>" + Resources.users.LastName + "</b>", skin.form_input("UserLastName", Convert.ToString(_reg_lname), "input", "40", "") },
                                            { "<b>" + Resources.users.Email + "</b>", skin.form_input("UserEmail", Convert.ToString(_reg_email), "input", "40", "") },
                                            { "<b>" + Resources.users.UserTitle + "</b>", skin.form_input("UserTitle", Convert.ToString(_reg_utitle), "input", "40", "") },
                                            { "<b>" + Resources.users.WorkArea + "</b>", skin.form_input("UserWorkArea", Convert.ToString(_reg_workarea), "input", "40", "") },
                                            { "<b>" + Resources.users.Org + "</b>", skin.form_input("UserOrganization", Convert.ToString(_reg_org), "input", "40", "") },
                                            { "<b>" + Resources.users.Phone + "</b>", skin.form_input("UserWorkPhone", Convert.ToString(_reg_phone), "input", "40", "") },
                                            { "<b>" + Resources.users.Cell + "</b>", skin.form_input("UserCellular", Convert.ToString(_reg_cell), "input", "40", "") },
                                            { "<b>" + Resources.users.Lang + "</b>", _languages },
                                            { "<b>" + Resources.users.Skin + "</b>", _skins },
                                            };
            }
            else
            {
                td_rows = new string[,] {
                                            { "<b>" + Resources.users.GroupID + "</b>", _group },
                                            { "<b>" + Resources.users.UserName + "</b>", skin.form_input("UserName", _reg_uname, "input", "40", "") },
                                            { "<b>" + Resources.users.PassWD + "</b>", skin.form_input("password", _reg_passwd, "input", "40", "") },
                                            { "<b>" + Resources.users.FirstName + "</b>", skin.form_input("UserFirstName", Convert.ToString(_reg_fname), "input", "40", "") },
                                            { "<b>" + Resources.users.LastName + "</b>", skin.form_input("UserLastName", Convert.ToString(_reg_lname), "input", "40", "") },
                                            { "<b>" + Resources.users.Email + "</b>", skin.form_input("UserEmail", Convert.ToString(_reg_email), "input", "40", "") },
                                            { "<b>" + Resources.users.UserTitle + "</b>", skin.form_input("UserTitle", Convert.ToString(_reg_utitle), "input", "40", "") },
                                            { "<b>" + Resources.users.WorkArea + "</b>", skin.form_input("UserWorkArea", Convert.ToString(_reg_workarea), "input", "40", "") },
                                            { "<b>" + Resources.users.Org + "</b>", skin.form_input("UserOrganization", Convert.ToString(_reg_org), "input", "40", "") },
                                            { "<b>" + Resources.users.Phone + "</b>", skin.form_input("UserWorkPhone", Convert.ToString(_reg_phone), "input", "40", "") },
                                            { "<b>" + Resources.users.Cell + "</b>", skin.form_input("UserCellular", Convert.ToString(_reg_cell), "input", "40", "") },
                                            { "<b>" + Resources.users.Lang + "</b>", _languages },
                                            { "<b>" + Resources.users.Skin + "</b>", _skins },
                                            };
            }
            ///-----------------------------------------
            LoadData.Text += skin.start_form(startform); // Start of Form
            LoadData.Text += skin.start_table(NewTitle, Resources.users.Description, tableheader); // Start of Table
            for (int i = 0; i <= td_rows.GetUpperBound(0); i++)
            {
                string s1 = td_rows[i, 0];
                string s2 = td_rows[i, 1];
                string[] genrow = new string[] { s1, s2 };
                LoadData.Text += skin.add_td_row(genrow, ""); // Add form row
            }
            LoadData.Text += skin.end_table(); // End of Table
            LoadData.Text += skin.end_form(_button, ""); // End of Form
        }

        /// <summary>
        /// Show form to add or edit users.
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
            _reg_org = Convert.ToString(HttpContext.Current.Request["UserOrganization"]);
            _reg_phone = Convert.ToString(HttpContext.Current.Request["UserWorkPhone"]);
            _reg_cell = Convert.ToString(HttpContext.Current.Request["UserCellular"]);
            _reg_cdate = Convert.ToString(DateTime.Now);
            _reg_lang = Convert.ToString(HttpContext.Current.Request["UserLanguage"]);
            _reg_skin = Convert.ToString(HttpContext.Current.Request["UserSkin"]);

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
                             { "UserOrganization", _reg_org },
                             { "UserWorkPhone", _reg_phone },
                             { "UserCellular", _reg_cell },
                             { "UserCreationDate", _reg_cdate },
                             { "UserLanguage", _reg_lang },
                             { "UserSkin", _reg_skin }
                             };

            string ins = DB.do_insert("users", user_data);
            if (String.IsNullOrEmpty(ins))
                HttpContext.Current.Response.Redirect("users.aspx");
            else
                LoadData.Text = skin.error_page(ins, "red");
        }

        /// <summary>
        /// Show form to add or edit users.
        /// </summary>
        private void do_edit_user()
        {
            if (String.IsNullOrEmpty(std.parse_incoming("uid")))
                LoadData.Text = skin.error_page(Resources.users.ErrorNoUID, "yellow");
            else
            {
                ///-----------------------------------------
                /// Set up array
                ///-----------------------------------------
                _reg_gid = Convert.ToString(HttpContext.Current.Request["GroupID"]);
                _reg_fname = Convert.ToString(HttpContext.Current.Request["UserFirstName"]);
                _reg_lname = Convert.ToString(HttpContext.Current.Request["UserLastName"]);
                _reg_email = Convert.ToString(HttpContext.Current.Request["UserEmail"]);
                _reg_utitle = Convert.ToString(HttpContext.Current.Request["UserTitle"]);
                _reg_workarea = Convert.ToString(HttpContext.Current.Request["UserWorkArea"]);
                _reg_org = Convert.ToString(HttpContext.Current.Request["UserOrganization"]);
                _reg_phone = Convert.ToString(HttpContext.Current.Request["UserWorkPhone"]);
                _reg_cell = Convert.ToString(HttpContext.Current.Request["UserCellular"]);
                _reg_lang = Convert.ToString(HttpContext.Current.Request["UserLanguage"]);
                _reg_skin = Convert.ToString(HttpContext.Current.Request["UserSkin"]);

                string[,] user_data = {
                             { "GroupID", _reg_gid },
                             { "UserFirstName", _reg_fname },
                             { "UserLastName", _reg_lname },
                             { "UserEmail", _reg_email },
                             { "UserTitle", _reg_utitle },
                             { "UserWorkArea", _reg_workarea },
                             { "UserOrganization", _reg_org },
                             { "UserWorkPhone", _reg_phone },
                             { "UserCellular", _reg_cell },
                             { "UserLanguage", _reg_lang },
                             { "UserSkin", _reg_skin }
                             };

                string upd = DB.do_update("users", user_data, "UserID=" + HttpContext.Current.Request["uid"]);
                if (String.IsNullOrEmpty(upd))
                    HttpContext.Current.Response.Redirect("users.aspx?code=view&uid=" + HttpContext.Current.Request["uid"]);
                else
                    LoadData.Text = skin.error_page(upd, "red");
            }
        }

        /// <summary>
        /// Show user details.
        /// </summary>
        private void view_user()
        {
            string[,] tableheader = {
                                    { "&nbsp;", "30%", "right" },
                                    { "&nbsp;", "70%", "left" }
                                    };            
            ///---------------------------------------
            /// Select all users.
            ///---------------------------------------
            string UQuery = "SELECT * FROM users WHERE UserID = " + HttpContext.Current.Request["uid"];
            SqlCommand cmd = new SqlCommand(UQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    _reg_gid = Convert.ToString(r["GroupID"]);
                    _reg_uname = Convert.ToString(r["UserName"]);
                    _reg_fname = Convert.ToString(r["UserFirstName"]);
                    _reg_lname = Convert.ToString(r["UserLastName"]);
                    _reg_email = Convert.ToString(r["UserEmail"]);
                    _reg_utitle = Convert.ToString(r["UserTitle"]);
                    _reg_workarea = Convert.ToString(r["UserWorkArea"]);
                    _reg_org = Convert.ToString(r["UserOrganization"]);
                    _reg_phone = Convert.ToString(r["UserWorkPhone"]);
                    _reg_cell = Convert.ToString(r["UserCellular"]);
                    _reg_lang = Convert.ToString(r["UserLanguage"]);
                    _reg_skin = Convert.ToString(r["UserSkin"]);
                }
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            ///---------------------------------------
            string _groupname = string.Empty;
            string grQuery = "SELECT * FROM groups WHERE GroupID=" + _reg_gid;
            SqlCommand cu = new SqlCommand(grQuery, con);
            SqlDataReader u;
            try
            {
                con.Open();
                u = cu.ExecuteReader();
                u.Read();
                if (_reg_gid == "2")
                    _groupname = "<font color=\"red\">" + Convert.ToString(u["GroupName"]) + "</font>";
                else
                    _groupname = Convert.ToString(u["GroupName"]);
                u.Close();
            }
            catch { }
            finally { con.Close(); }
            ///---------------------------------------
            string[,] td_rows = new string[,] {
                                            { "<b>" + Resources.users.GroupName + "</b>", _groupname },
                                            { "<b>" + Resources.users.UserName + "</b>", _reg_uname },
                                            { "<b>" + Resources.users.FirstName + "</b>", _reg_fname },
                                            { "<b>" + Resources.users.LastName + "</b>", _reg_lname },
                                            { "<b>" + Resources.users.Email + "</b>", _reg_email },
                                            { "<b>" + Resources.users.UserTitle + "</b>", _reg_utitle },
                                            { "<b>" + Resources.users.WorkArea + "</b>", _reg_workarea },
                                            { "<b>" + Resources.users.Org + "</b>", _reg_org },
                                            { "<b>" + Resources.users.Phone + "</b>", _reg_phone },
                                            { "<b>" + Resources.users.Cell + "</b>", _reg_cell },
                                            { "<b>" + Resources.users.Lang + "</b>", _reg_lang },
                                            { "<b>" + Resources.users.Skin + "</b>", _reg_skin },
                                            };

            LoadData.Text += skin.start_table(Resources.users.Title, Resources.users.Description, tableheader); // Start of Table
            for (int i = 0; i <= td_rows.GetUpperBound(0); i++)
            {
                string s1 = td_rows[i, 0];
                string s2 = td_rows[i, 1];
                string[] genrow = new string[] { s1, s2 };
                LoadData.Text += skin.add_td_row(genrow, ""); // Add form row
            }
            LoadData.Text += skin.end_table(); // End of Table
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        private void delete_user()
        {
            if (String.IsNullOrEmpty(std.parse_incoming("uid")))
                LoadData.Text = skin.error_page(Resources.users.ErrorNoUID, "yellow");
            else
            {
                string del = DB.do_delete("Users", "UserID=" + HttpContext.Current.Request["uid"]);
                if (String.IsNullOrEmpty(del))
                    HttpContext.Current.Response.Redirect("users.aspx");
                else
                    LoadData.Text = skin.error_page(del, "red");
            }
        }

        /// <summary>
        /// Show form to change passwd.
        /// </summary>
        private void show_form_passwd()
        {
            if (String.IsNullOrEmpty(std.parse_incoming("uid")))
                LoadData.Text = skin.error_page(Resources.users.ErrorNoUID, "yellow");
            else
            {
                _reg_passwd = Convert.ToString(HttpContext.Current.Request["password"]);
                _reg_confirm_passwd = Convert.ToString(HttpContext.Current.Request["confirm_password"]);
                ///---------------------------------------
                _button = Resources.users.ChangePasswd;
                _code = "dochangepwd";
                ///-----------------------------------------
                string NewTitle = Resources.users.Title;
                ///-----------------------------------------
                string[,] startform = { { "code", _code }, { "uid", HttpContext.Current.Request["uid"] } };
                string[,] tableheader = { { "&nbsp;", "20%", "center" }, { "&nbsp;", "80%", "left" } };
                ///-----------------------------------------
                /// Rows Arrays
                ///-----------------------------------------
                string[,] td_rows;
                    td_rows = new string[,] {
                                            { "<b>" + Resources.users.NewPasswd + "</b>", skin.form_password("password", _reg_passwd, "input", "40", "") },
                                            };
                ///-----------------------------------------
                LoadData.Text += skin.start_form(startform); // Start of Form
                LoadData.Text += skin.start_table(NewTitle, Resources.users.Description, tableheader); // Start of Table
                for (int i = 0; i <= td_rows.GetUpperBound(0); i++)
                {
                    string s1 = td_rows[i, 0];
                    string s2 = td_rows[i, 1];
                    string[] genrow = new string[] { s1, s2 };
                    LoadData.Text += skin.add_td_row(genrow, ""); // Add form row
                }
                LoadData.Text += skin.end_table(); // End of Table
                LoadData.Text += skin.end_form(_button, ""); // End of Form
            }
        }

        /// <summary>
        /// Do change user password.
        /// </summary>
        private void do_change_passwd()
        {
            ///-----------------------------------------
            /// Set up array
            ///-----------------------------------------
            _reg_passwd = Convert.ToString(HttpContext.Current.Request["password"]);
            _reg_psalt = class_login.CreateRandomSalt();

            string hash = IN.ComputeSaltedHash(_reg_passwd, _reg_psalt);
            string salt = Convert.ToString(_reg_psalt);
            string[,] user_data = {
                             { "UserPassWD", hash },
                             { "UserPassSalt", salt },
                             };

            string upd = DB.do_update("users", user_data, "UserID=" + HttpContext.Current.Request["uid"]);
            if (String.IsNullOrEmpty(upd))
                HttpContext.Current.Response.Redirect("users.aspx?code=view&uid=" + HttpContext.Current.Request["uid"]);
            else
                LoadData.Text = skin.error_page(upd, "red");
        }

        /// <summary>
        /// Enable user.
        /// </summary>
        private void enable_user()
        {
            if (String.IsNullOrEmpty(std.parse_incoming("uid")))
                LoadData.Text = skin.error_page(Resources.users.ErrorNoUID, "yellow");
            else
            {
                string uQuery = "SELECT * FROM users WHERE UserID=" + HttpContext.Current.Request["uid"];
                SqlCommand cu = new SqlCommand(uQuery, con);
                SqlDataReader u;
                try
                {
                    con.Open();
                    u = cu.ExecuteReader();
                    u.Read();
                    _reg_fname = Convert.ToString(u["UserFirstName"]);
                    _reg_lname = Convert.ToString(u["UserLastName"]);
                    _reg_email = Convert.ToString(u["UserEmail"]);
                    u.Close();
                }
                catch { }
                finally { con.Close(); }
                ///-----------------------------------------
                /// Set up array
                ///-----------------------------------------
                _reg_gid = _globalSettings["UserBaseGroup"];

                string[,] user_data = {
                             { "GroupID", _reg_gid }
                             };

                string upd = DB.do_update("users", user_data, "UserID=" + HttpContext.Current.Request["uid"]);
                if (String.IsNullOrEmpty(upd))
                {
                    ///-----------------------------------------
                    /// Send email to new user
                    /// ----------------------------------------                    
                    string ufullname = _reg_fname + " " + _reg_lname;
                    string usbj = "Tu suscripción a la " + _globalSettings["MainTitle"] + " ha sido activada!";
                    string ubmsg = @"
" + ufullname + @", muchas gracias por suscribirte a la " + _globalSettings["MainTitle"] + @".
Tu registro ha sido activado por un administrador.

Saludos.
                    ";
                    std.global_send_email(_reg_email, ufullname, usbj, ubmsg);
                    /// ----------------------------------------
                    HttpContext.Current.Response.Redirect("users.aspx?code=view&uid=" + HttpContext.Current.Request["uid"]);
                }
                else
                    LoadData.Text = skin.error_page(upd, "red");
            }
        }

        /// <summary>
        /// Ban user.
        /// </summary>
        private void ban_user()
        {
            string oldgid = string.Empty;
            if (String.IsNullOrEmpty(std.parse_incoming("uid")))
                LoadData.Text = skin.error_page(Resources.users.ErrorNoUID, "yellow");
            else
            {
                string uQuery = "SELECT * FROM users WHERE UserID=" + HttpContext.Current.Request["uid"];
                SqlCommand cu = new SqlCommand(uQuery, con);
                SqlDataReader u;
                try
                {
                    con.Open();
                    u = cu.ExecuteReader();
                    u.Read();
                    oldgid = Convert.ToString(u["GroupID"]);
                    u.Close();
                }
                catch { }
                finally { con.Close(); }
                ///-----------------------------------------
                /// Set up array
                ///-----------------------------------------
                _reg_gid = _globalSettings["UserBlockedGroup"];

                string[,] user_data = {
                             { "GroupID", _reg_gid },
                             { "UserOldGroupID", oldgid }
                             };

                string upd = DB.do_update("users", user_data, "UserID=" + HttpContext.Current.Request["uid"]);
                if (String.IsNullOrEmpty(upd))
                {
                    HttpContext.Current.Response.Redirect("users.aspx?code=view&uid=" + HttpContext.Current.Request["uid"]);
                }
                else
                    LoadData.Text = skin.error_page(upd, "red");
            }
        }

        /// <summary>
        /// Unban user.
        /// </summary>
        private void unban_user()
        {
            string oldgid = string.Empty;
            if (String.IsNullOrEmpty(std.parse_incoming("uid")))
                LoadData.Text = skin.error_page(Resources.users.ErrorNoUID, "yellow");
            else
            {
                string ubQuery = "SELECT * FROM users WHERE UserID=" + HttpContext.Current.Request["uid"];
                SqlCommand ubu = new SqlCommand(ubQuery, con);
                SqlDataReader u;
                try
                {
                    con.Open();
                    u = ubu.ExecuteReader();
                    u.Read();
                    oldgid = Convert.ToString(u["UserOldGroupID"]);
                    u.Close();
                }
                catch { }
                finally { con.Close(); }
                ///-----------------------------------------
                /// Set up array
                ///-----------------------------------------
                string[,] user_data = {
                             { "GroupID", oldgid },
                             { "UserOldGroupID", "0" }
                             };

                string upd = DB.do_update("users", user_data, "UserID=" + HttpContext.Current.Request["uid"]);
                if (String.IsNullOrEmpty(upd))
                {
                    HttpContext.Current.Response.Redirect("users.aspx?code=view&uid=" + HttpContext.Current.Request["uid"]);
                }
                else
                    LoadData.Text = skin.error_page(upd, "red");
            }
        }
        #endregion
    }
}