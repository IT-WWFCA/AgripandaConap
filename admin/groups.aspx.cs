/**
 * WWF honduras
 * AgriPanda v2.1.1
 * Posts Handler
 * Last Updated: $Date: 2018-08-20 11:13:10 -0600 (Mon, 20 Aug 2018) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2018 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.groups
 * @link		http://www.wwf-mar.org
 * @since		2.1.0
 * @version		$Revision: 0001 $
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
    public partial class groups : System.Web.UI.Page
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
        /// Get global settings
        /// </summary>
        Dictionary<string, string> _globalSettings;
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
        ///--------------------------------------
        /// <summary>
        /// Registration string groupname.
        /// </summary>
        private string _GroupName;
        /// <summary>
        /// Registration string group crops.
        /// </summary>
        private string _GroupCrops;
        /// <summary>
        /// Registration string group mods.
        /// </summary>
        private string _GroupMods;
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
                case "add":
                    LoadScripts.Text = std.load_bootstrap_multiselect();
                    ShowForm("new");
                    break;
                case "edit":
                    LoadScripts.Text = std.load_bootstrap_multiselect();
                    ShowForm("edit");
                    break;
                case "doadd":
                    do_add_group();
                    break;
                //case "doedit":
                    //do_edit_group();
                    //break;
                //-------------------
                default:
                    LoadScripts.Text = std.load_tooltipster_scripts();
                    LoadContent.Text = ShowGroups(Convert.ToString(HttpContext.Current.Request["page"]));
                    break;
            }
        }
        #endregion

        #region Methods
        ///---------------------------------------
        /// Region where class functions are declared.
        ///---------------------------------------
        /// <summary>
        /// Show group list.
        /// </summary>
        private string ShowGroups(string page)
        {
            StringBuilder pageContent = new StringBuilder();
            int limit = 10; // Rows displayed
            int rnum = 0; // Total rows
            string offset = std.get_pagination_offset(page, limit);
            string _addlinks = "";
            //-------------------------------
            string[,] tableheader = {
                                    { "&nbsp;", "2%", "" },
                                    { "&nbsp;&nbsp; " + Resources.groups.TitleGroupName, "55%", "left" },
                                    { Resources.global.actions, "43%", "center" }
                                    };
            pageContent.Append(skin.start_table(Resources.groups.PageTitle, Resources.groups.PageDescription, tableheader)); // Start of Table
            ///---------------------------------------
            /// Select all groups.
            ///---------------------------------------
            string UQuery = "WITH Results_CTE AS (SELECT *, ROW_NUMBER() OVER(ORDER BY GroupID ASC) AS RowNum FROM groups WHERE GroupEdit=1) SELECT *, (SELECT MAX(RowNum) FROM Results_CTE) AS TotalRows FROM Results_CTE WHERE RowNum >= " + offset + " AND RowNum < " + offset + " + " + limit;
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
                    //string isAdmin = Convert.ToString(r["UserSuperAdmin"]);
                    //string GroupID = Convert.ToString(r["GroupID"]);
                    string gid = string.Empty;
                    ///---------------------------------------
                    /// Disable tag
                    ///---------------------------------------
                    //if (isAdmin == "1")
                        ban = " &nbsp; ";
                    /*else
                    {
                        if (GroupID == _globalSettings["UserBlockedGroup"])
                            ban = " &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipUnbanButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=unban&uid=" + r["UserID"] + "\" class=\"sexybutton\"><i class=\"fa fa-user-check\"></i>" + Resources.global.UnbanTitle + "</a></span> &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipDeleteButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=delete&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-red\"><i class=\"fa fa-trash-alt\"></i>" + Resources.global.DeleteTitle + "</a></span>";
                        else
                            ban = " &nbsp; <span class=\"tooltip\" title=\"" + Resources.users.TooltipBanButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=ban&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-orange\"><i class=\"fa fa-user-slash\"></i>" + Resources.global.BanTitle + "</a></span>";
                    }*/
                    ///---------------------------------------
                    /// Enable tag
                    ///---------------------------------------
                    /*if (GroupID == _globalSettings["UserPendingGroup"])
                        gid = "<span class=\"tooltip\" title=\"" + Resources.users.TooltipEnableButton + "(" + Convert.ToString(r["UserName"]) + ")\"><a href=\"?code=enable&uid=" + r["UserID"] + "\" class=\"sexybutton sexybutton-purple\"><i class=\"fa fa-check\"></i>" + Resources.global.EnableTitle + "</a></span> &nbsp;  ";
                    else*/
                        gid = " &nbsp; ";
                    ///---------------------------------------
                    td_array = new string[] {
                                "&nbsp;",
                                "&nbsp;&nbsp;<b>" + Convert.ToString(r["GroupName"]) + "</b>",
                                "<center>" + gid + "<span class=\"tooltip\" title=\"" + Resources.groups.TooltipEditButton + "(" + Convert.ToString(r["GroupName"]) + ")\"><a href=\"?code=edit&gid=" + r["GroupID"] + "\" class=\"sexybutton sexybutton-green\"><i class=\"fa fa-edit\"></i>" + Resources.global.EditTitle + "</a></span>" + ban + "</center>",
                                };
                    pageContent.Append(skin.add_td_row(td_array, "")); // Add rows
                }
                r.Close();
            }
            catch (Exception err) { pageContent.Append(err); }
            finally { con.Close(); }
            ///---------------------------------------
            //LoadData.Text += skin.end_table(); // End of Table
            pageContent.Append(std.get_pagination(page, limit, rnum, _addlinks)); // End of Table
            ///---------------------------------------
            string grouplist = Convert.ToString(pageContent);
            return grouplist;
        }

        /// <summary>
        /// Show form to add or edit groups.
        /// </summary>
        private void ShowForm(string type)
        {
            if (type != "new")
            {
                if (String.IsNullOrEmpty(std.parse_incoming("gid")))
                    LoadContent.Text = skin.error_page(Resources.groups.ErrorNoGID, "yellow");
                ///-----------------------------------------
                string grQuery = "SELECT * FROM groups WHERE GroupID = " + HttpContext.Current.Request["gid"];
                SqlCommand gu = new SqlCommand(grQuery, con);
                SqlDataReader u;
                try
                {
                    con.Open();
                    u = gu.ExecuteReader();
                    while (u.Read())
                    {
                        _GroupName = Convert.ToString(u["GroupName"]);
                        _GroupCrops = Convert.ToString(u["GroupCrops"]);
                        _GroupMods = Convert.ToString(u["GroupMods"]);
                    }
                    u.Close();
                }
                catch { }
                finally { con.Close(); }
                ///---------------------------------------
                _button = Resources.groups.ButtonEdit;
                _code = "doedit";
            }
            else
            {
                _GroupName = Convert.ToString(HttpContext.Current.Request["GroupName"]);
                _GroupCrops = Convert.ToString(HttpContext.Current.Request["GroupCrops"]);
                _GroupMods = Convert.ToString(HttpContext.Current.Request["GroupMods"]);
                ///---------------------------------------
                _button = Resources.groups.ButtonAdd;
                _code = "doadd";
            }
            ///-----------------------------------------
            /// Create crop bootstrap-multiselect
            ///-----------------------------------------
            Dictionary<string, string> _cropidsdic = new Dictionary<string, string>();
            if(!String.IsNullOrEmpty(_GroupCrops))
            {
                string[] _cropids = _GroupCrops.Split(':');
                for (int e = 0; e < _cropids.Length; e++)
                {
                    _cropidsdic.Add(_cropids[e], _cropids[e]);
                }
            }
            else
                _cropidsdic.Add("0", "0");
            string[,] carr;
            string _crop = skin.form_bootstrap_multiselect_start("GroupCrops", "crop-multiselect");
            Dictionary<string, string> getcrops = std.get_dictionary_string_string("crops", "CropID", "CropName");
            foreach (KeyValuePair<string, string> entry in getcrops)
            {
                carr = new string[,] { { entry.Key, entry.Value } };
                if (_cropidsdic.ContainsKey(entry.Key))
                    _crop += skin.form_dropdown_opts(carr, _cropidsdic[entry.Key]);
                else
                    _crop += skin.form_dropdown_opts(carr, "");
            }
            _crop += skin.form_dropdown_end();
            ///-----------------------------------------
            /// Create Mods bootstrap-multiselect
            ///-----------------------------------------
            Dictionary<string, string> _modidsdic = new Dictionary<string, string>();
            if (!String.IsNullOrEmpty(_GroupMods))
            {
                _GroupMods = Regex.Replace(_GroupMods, "\\]$", "");
                _GroupMods = Regex.Replace(_GroupMods, "^S\\[", "");
                string[] _modids = _GroupMods.Split(':');
                for (int a = 0; a < _modids.Length; a++)
                {
                    _modidsdic.Add(_modids[a], _modids[a]);
                }
            }
            else
                _modidsdic.Add("0", "0");
            string[,] marr;
            string _mod = skin.form_bootstrap_multiselect_start("GroupMods", "mods-multiselect");
            Dictionary<string, string> getmods = std.get_dictionary_string_string("mods", "ModCode", "ModName");
            foreach (KeyValuePair<string, string> entry in getmods)
            {
                marr = new string[,] { { entry.Key, entry.Value } };
                if (_modidsdic.ContainsKey(entry.Key))
                    _mod += skin.form_dropdown_opts(marr, _modidsdic[entry.Key]);
                else
                    _mod += skin.form_dropdown_opts(marr, "");
            }
            _mod += skin.form_dropdown_end();
            ///-----------------------------------------
            string NewTitle = Resources.groups.Title;
            ///-----------------------------------------
            string[,] startform = { { "code", _code }, { "uid", HttpContext.Current.Request["uid"] } };
            string[,] tableheader = { { "&nbsp;", "20%", "center" }, { "&nbsp;", "80%", "left" } };
            ///-----------------------------------------
            /// Rows Arrays
            ///-----------------------------------------
            string[,] td_rows;
            td_rows = new string[,] {
                                         { "<b>" + Resources.groups.GroupName + "</b>", skin.form_input("GroupName", _GroupName, "input", "40", "") },
                                         { "<b>" + Resources.groups.GroupCrops + "</b>", _crop },
                                         { "<b>" + Resources.groups.GroupMods + "</b>", _mod },
                                    };
            ///-----------------------------------------
            LoadContent.Text += skin.start_form(startform); // Start of Form
            LoadContent.Text += skin.start_table(NewTitle, Resources.users.Description, tableheader); // Start of Table
            for (int i = 0; i <= td_rows.GetUpperBound(0); i++)
            {
                string s1 = td_rows[i, 0];
                string s2 = td_rows[i, 1];
                string[] genrow = new string[] { s1, s2 };
                LoadContent.Text += skin.add_td_row(genrow, ""); // Add form row
            }
            LoadContent.Text += skin.end_table(); // End of Table
            LoadContent.Text += skin.end_form(_button, ""); // End of Form
        }

        /// <summary>
        /// Do add a group.
        /// </summary>
        private void do_add_group()
        {
            ///-----------------------------------------
            /// Set up array
            ///-----------------------------------------
            _GroupName = Convert.ToString(HttpContext.Current.Request["GroupName"]);
            _GroupCrops = Convert.ToString(HttpContext.Current.Request["GroupCrops"]);
            _GroupMods = Convert.ToString(HttpContext.Current.Request["GroupMods"]);
            ///-----------------------------------------
            string _GroupAlias = _GroupName.ToLower(); // All to lowercase on string
            _GroupAlias = Regex.Replace(_GroupAlias, @"\s+", ""); // Remove any space in string
            _GroupAlias = std.Truncate(_GroupAlias, 16); // Truncate string
            ///-----------------------------------------
            string _cids = string.Empty;
            if(!String.IsNullOrEmpty(_GroupCrops))
            {
                string[] _cropids = _GroupCrops.Split(',');
                _cids = string.Join(":", _cropids);
            }
            ///-----------------------------------------
            string _mids = string.Empty;
            if (!String.IsNullOrEmpty(_GroupMods))
            {
                string[] _modids = _GroupMods.Split(',');
                _mids = string.Join(":", _modids);
                _mids = "S[" + _mids + "]";
            }
            
            string[,] group_data = {
                             { "GroupAlias", _GroupAlias },
                             { "GroupName", _GroupName },
                             { "GroupPerms", "A1" },
                             { "GroupCrops", _cids },
                             { "GroupEdit", "1" },
                             { "GroupAdmin", "0" },
                             { "GroupMods", _mids }
                             };

            string ins = DB.do_insert("groups", group_data);
            if (String.IsNullOrEmpty(ins))
                HttpContext.Current.Response.Redirect("groups.aspx");
            else
            LoadContent.Text = skin.error_page(ins, "red");
        }

        #endregion
    }
}