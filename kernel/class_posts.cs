/**
 * WWF honduras
 * AgriPanda v2.0.0
 * Posts Handler
 * Last Updated: $Date: 2016-02-29 14:54:10 -0600 (Fri, 19 Feb 2016) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2016 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Posts
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0002 $
 *
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace AgriPanda.kernel
{
    public class class_posts
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
        private DateTime datetoday = DateTime.Today;

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
        /// Path to uploads on server.
        /// </summary>
        public string serverpath = HttpContext.Current.Server.MapPath("~/uploads");

        ///-----------------------------------------
        /// Set variables
        ///-----------------------------------------
        public string _post_title = string.Empty;
        public string _post_type = string.Empty;
        public string _post_url = string.Empty;
        public string _post_text = string.Empty;
        public string _post_catid = string.Empty;
        public string _user_id = string.Empty;
        public string _mod_id = string.Empty;
        public string _creation_date = string.Empty;
        public string _start_date = string.Empty;
        public string _end_date = string.Empty;
        public string _file_name = string.Empty;
        public string _new_file_name = string.Empty;
        public string _noext_file_name = string.Empty;
        public string _file_ext = string.Empty;
        public int _file_size = 0;
        public int _max_file_size = 2100000;

        public string _cat_id = string.Empty;
        public string _cat_title = string.Empty;
        public string _cat_description = string.Empty;
        #endregion

        #region Methods
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// POSTS AND UPLOAD FUNCTIONS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /*========================================================================*/
        /// <summary>
        /// Show all posts for one mod.
        /// </summary>
        public string show_all_posts(string PageTitle, string PageDescription, string modid, Dictionary<string, string> _ag)
        {
            StringBuilder pageContent = new StringBuilder();
            string catid = Convert.ToString(HttpContext.Current.Request["cid"]);
            string[,] tableheader = {
                                    { "&nbsp;", "2%", "" },
                                    { "&nbsp;&nbsp; " + Resources.posts.PostTitle, "68%", "left" },
                                    { "&nbsp;&nbsp; " + Resources.posts.PostDate, "15%", "left" },
                                    { Resources.global.actions, "15%", "center" }
                                    };
            pageContent.Append(skin.start_table(PageTitle, PageDescription, tableheader)); // Start of Table

            ///---------------------------------------
            /// Select all posts related to this Mod.
            ///---------------------------------------
            string Query = "SELECT * FROM posts WHERE ModID=" + modid + " AND CatID=" + catid + " ORDER BY PostCreationDate DESC";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string uid = Convert.ToString(r["UserID"]);
                    ///---------------------------------------
                    /// Delete tag
                    ///---------------------------------------
                    string delete = string.Empty;
                    if (_ag["UserID"] == uid || _ag["UserSuperAdmin"] == "1")
                        delete = " &nbsp; <a href=\"?code=deletepost&pid=" + r["PostID"] + "\">" + Resources.global.DeleteTitle + "</a>";
                    else
                        delete = " &nbsp; ";
                    ///---------------------------------------
                    /// Edit tag
                    ///---------------------------------------
                    string edit = string.Empty;
                    if (_ag["UserSuperAdmin"] == "1")
                        edit = " &nbsp; <a href=\"?code=editpost&pid=" + r["PostID"] + "\">" + Resources.global.EditTitle + "</a>";
                    else
                        edit = " &nbsp; ";
                    ///---------------------------------------
                    /// Convert dates to string
                    ///---------------------------------------
                    DateTime dt = Convert.ToDateTime(r["PostCreationDate"]);
                    ///---------------------------------------
                    td_array = new string[] {
                                "<center>&nbsp;</center>",
                                "&nbsp;&nbsp;<b><a href=\"?code=viewpost&pid=" + r["PostID"] + "\">" + Convert.ToString(r["PostTitle"]) + "</a></b>",
                                "&nbsp;&nbsp;" + dt.ToString("MMM dd, yyyy",  CultureInfo.CreateSpecificCulture(_ag["UserLanguage"])),
                                "<center>" + edit + delete + "</center>",
                                };

                    pageContent.Append(skin.add_td_row(td_array, "")); // Add rows
                }
                r.Close();
            }
            catch (Exception err) { pageContent.Append(err.Message); }
            finally { con.Close(); }
            ///---------------------------------------
            pageContent.Append(skin.end_table()); // End of Table
            string html = Convert.ToString(pageContent);
            return html;
        }

        /// <summary>
        /// Show form for new or edit posts.
        /// </summary>
        public string show_form(string PageTitle, string PageDescription, string modid, Dictionary<string, string> _ag, string type)
        {
            StringBuilder pageContent = new StringBuilder();            
            ///-----------------------------------------
            if (type != "new")
            {
                if (String.IsNullOrEmpty(std.parse_incoming("pid")))
                    pageContent.Append(skin.error_page(Resources.posts.ErrorNoPID, "yellow"));
                else
                {
                    ///-----------------------------------------
                    string Query = "SELECT * FROM posts WHERE PostID = " + HttpContext.Current.Request["pid"];
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataReader r;
                    try
                    {
                        con.Open();
                        r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            _post_title = Convert.ToString(r["PostTitle"]);
                            _post_type = Convert.ToString(r["PostType"]);
                            _post_url = Convert.ToString(r["PostURL"]);
                            _post_text = Convert.ToString(r["PostText"]);
                            _post_catid = Convert.ToString(r["CatID"]);
                        }
                        r.Close();
                    }
                    catch { }
                    finally { con.Close(); }
                    ///---------------------------------------
                    _button = Resources.posts.ButtonEdit;
                    _code = "doeditpost";
                }
            }
            else
            {
                _post_title = Convert.ToString(HttpContext.Current.Request["PostTitle"]);
                _post_type = Convert.ToString(HttpContext.Current.Request["PostType"]);
                _post_url = Convert.ToString(HttpContext.Current.Request["PostURL"]);
                _post_text = Convert.ToString(HttpContext.Current.Request["PostText"]);
                _post_catid = Convert.ToString(HttpContext.Current.Request["CatID"]);
                _mod_id = modid;
                ///---------------------------------------
                _button = Resources.posts.ButtonAdd;
                _code = "doaddpost";
            }
            ///---------------------------------------
            /// Get category dropdown
            ///---------------------------------------            
            string[,] catarr;
            string _catdropdown = skin.form_dropdown_start("CatID");
            string CatQuery = "SELECT * FROM posts_categories WHERE ModID=" + modid;
            SqlCommand ccmd = new SqlCommand(CatQuery, con);
            SqlDataReader cr;
            try
            {
                con.Open();
                cr = ccmd.ExecuteReader();
                while (cr.Read())
                {
                    catarr = new string[,] { { Convert.ToString(cr["CatID"]), Convert.ToString(cr["CatTitle"]) } };
                    _catdropdown += skin.form_dropdown_opts(catarr, _post_catid);
                }
                cr.Close();
            }
            catch (Exception err) { pageContent.Append(err.Message); }
            finally { con.Close(); }
            _catdropdown += skin.form_dropdown_end();
            ///-----------------------------------------
            /// Radio options
            ///-----------------------------------------
            string[] radiopts = { "post", "url" };
            Dictionary<string, string> _radopts = new Dictionary<string, string>();
            for (var i = 0; i < radiopts.Length; i++)
            {
                _radopts.Add(radiopts[i], Resources.posts.ResourceManager.GetString(radiopts[i]));
            }                
            ///-----------------------------------------
            string[,] startform = { { "code", _code }, { "pid", HttpContext.Current.Request["pid"] } };
            string[,] tableheader = { { "&nbsp;", "20%", "center" }, { "&nbsp;", "80%", "left" } };
            ///-----------------------------------------
            /// Rows Arrays
            ///-----------------------------------------
            string[,] td_rows;
            td_rows = new string[,] {
                                        { "<b>" + Resources.posts.PostTitleField + "</b>", skin.form_input("PostTitle", Convert.ToString(_post_title), "input", "40", "") },
                                        { "<b>" + Resources.posts.PostCatID + "</b>", _catdropdown },
                                        { "<b>" + Resources.posts.PostRadioOptions + "</b>", skin.form_radio("PostType", radiopts, "", radiopts, _radopts, "") },
                                        { "<b>" + Resources.posts.PostTextDesc + "</b>", skin.form_post_selector("PostText", Convert.ToString(_post_text), "PostURL", Convert.ToString(_post_url), "") },
                                    };
            ///-----------------------------------------
            pageContent.Append(skin.start_form(startform)); // Start of Form
            pageContent.Append(skin.start_table(PageTitle, PageDescription, tableheader)); // Start of Table
            for (int i = 0; i <= td_rows.GetUpperBound(0); i++)
            {
                string s1 = td_rows[i, 0];
                string s2 = td_rows[i, 1];
                string[] genrow = new string[] { s1, s2 };
                pageContent.Append(skin.add_td_row(genrow, "")); // Add form row
            }
            pageContent.Append(skin.end_table()); // End of Table
            pageContent.Append(skin.end_form(_button, "")); // End of Form
            //pageContent.Append(loadMiniScripts());
            string form = Convert.ToString(pageContent);
            return form;
        }

        /// <summary>
        /// Show all categories.
        /// </summary>
        public string category(Dictionary<string, string> _ag, string modid)
        {
            StringBuilder pageContent = new StringBuilder();
            string htmltext = string.Empty;
            string catInfo = string.Empty;
            string[] catsArray;
            string[] catInfoSplit;
            ///---------------------------------------
            /// Get category details
            ///---------------------------------------            
            string Query = "SELECT * FROM posts_categories WHERE ModID=" + modid;
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    _cat_title = Convert.ToString(r["CatTitle"]);
                    _cat_description = Convert.ToString(r["CatDescription"]);
                    _cat_id = Convert.ToString(r["CatID"]);
                    catInfo += _cat_id + ":" + _cat_title + ":" + _cat_description + ";";
                }
                r.Close();
            }
            catch (Exception err) { pageContent.Append(err.Message); }
            finally { con.Close(); }
            ///--------------------------------------- 
            catInfo = Regex.Replace(catInfo, ";$", "");
            catsArray = catInfo.Split(';');
            ///---------------------------------------
            if (catsArray.Length > 1)
            {
                string[,] tableheader = {
                                    { "&nbsp;", "2%", "" },
                                    { "&nbsp;&nbsp; " + Resources.posts.PostTitle, "83%", "left" },
                                    { Resources.global.actions, "15%", "center" }
                                    };
                pageContent.Append(skin.start_table(Resources.posts.postCategoriesPageTitle, Resources.posts.postCategoriesPageDesc, tableheader)); // Start of Table
                for (var i = 0; i < catsArray.Length; i++)
                {
                    catInfoSplit = catsArray[i].Split(':');
                    td_array = new string[] {
                                "<center>&nbsp;</center>",
                                "&nbsp;&nbsp;<b><a href=\"?code=showposts&cid=" + catInfoSplit[0] + "\">" + catInfoSplit[1] + "</a></b><br />" + catInfoSplit[2],
                                "<center></center>",
                                };

                    pageContent.Append(skin.add_td_row(td_array, "")); // Add rows
                }
                pageContent.Append(skin.end_table()); // End of Table
            }
            else
            {
                catInfoSplit = catsArray[0].Split(':');
                show_all_posts(catInfoSplit[1], catInfoSplit[2], modid, _ag);
            }
            ///---------------------------------------
            string html = Convert.ToString(pageContent);
            return html;
        }

        /// <summary>
        /// Load jQuery scripts for post type selector.
        /// </summary>
        public string loadPostScripts()
        {
            string scripts = @"<style type=""text/css"">
      .box{
            padding:10px 0px 10px 0px;
            display:none;
                margin - top: 20px;
            border:0px solid #000;
    }
</style>
<script src=""../lib/tinymce/tinymce.min.js""></script>
<script type=""text/javascript"">
  tinymce.init({
            selector: '#mytinymce',
            menubar: false,
            height: 250,
            width: 650
  });
</script>
<script src=""../lib/jquery.periodpicker/jquery.min.js""></script>
<script type=""text/javascript"">
$(document).ready(function(){
    $('input[type=""radio""]').click(function(){
                    if ($(this).attr(""value"") == ""post""){
            $("".box"").not("".post"").hide();
            $("".post"").show();
                    }
                    if ($(this).attr(""value"") == ""url""){
            $("".box"").not("".url"").hide();
            $("".url"").show();
                    }
                });
            });
</script>
   ";
            return scripts;
        }

        /// <summary>
        /// Load mini upload form scripts.
        /// </summary>
        public string loadMiniScripts()
        {
            string scripts = @"<script src=""../lib/mini-upload-form/assets/js/jquery.knob.js""></script>
            <!--jQuery File Upload Dependencies-->
            <script src=""../lib/mini-upload-form/assets/js/jquery.ui.widget.js""></script>
            <script src=""../lib/mini-upload-form/assets/js/jquery.iframe-transport.js""></script>
            <script src=""../lib/mini-upload-form/assets/js/jquery.fileupload.js""></script>
            <!--Our main JS file-->
            <script src=""../lib/mini-upload-form/assets/js/script.js""></script>";
            return scripts;
        }

        /// <summary>
        /// Insert new warning to DB.
        /// </summary>
        public void doInsert(string type, Dictionary<string, string> _ag, string modid, string redirect)
        {
            ///-----------------------------------------
            /// Set variables
            ///-----------------------------------------
            _post_title = Convert.ToString(HttpContext.Current.Request["PostTitle"]);
            _post_type = Convert.ToString(HttpContext.Current.Request["PostType"]);
            _post_url = Convert.ToString(HttpContext.Current.Request["PostURL"]);
            _post_text = Convert.ToString(HttpContext.Current.Request["PostText"]);
            _post_catid = Convert.ToString(HttpContext.Current.Request["CatID"]);
            int filecount = Convert.ToInt32(HttpContext.Current.Request.Files.Count);
            ///-----------------------------------------
            /// Encode HTML tags
            ///-----------------------------------------
            string _post_text_encode = WebUtility.HtmlEncode(_post_text);
            ///-----------------------------------------
            string lastid = string.Empty;
            ///-----------------------------------------
            /// Set up arrays
            ///-----------------------------------------
            string[,] post_data;
            string[,] file_data;
            string ins = string.Empty;
            string insup = string.Empty;
            ///-----------------------------------------
            if (type == "new")
            {
                _creation_date = Convert.ToString(DateTime.Now);
                _user_id = _ag["UserID"];
                _mod_id = modid;

                if (filecount > 0)
                {
                    for (int k = 0; k < HttpContext.Current.Request.Files.Count; k++)
                    {
                        HttpPostedFile ifile = HttpContext.Current.Request.Files[k]; // Get uploaded file
                        _file_name = Path.GetFileName(ifile.FileName);
                        _file_ext = Path.GetExtension(ifile.FileName);
                        _file_size = ifile.ContentLength;
                        _noext_file_name = Path.GetFileNameWithoutExtension(ifile.FileName);
                        _file_ext = _file_ext.ToLower();                        
                        ///-----------------------------------------
                        /// Validate uploads
                        ///-----------------------------------------
                        string[] acceptedFileTypes = new string[7];
                        acceptedFileTypes[0] = ".pdf";
                        acceptedFileTypes[1] = ".doc";
                        acceptedFileTypes[2] = ".docx";
                        acceptedFileTypes[3] = ".jpg";
                        acceptedFileTypes[4] = ".jpeg";
                        acceptedFileTypes[5] = ".gif";
                        acceptedFileTypes[6] = ".png";
                        bool acceptFile = false;
                        //should we accept the file?
                        for (int l = 0; l <= 6; l++)
                        {
                            if (_file_ext == acceptedFileTypes[l])
                            {
                                //accept the file, yay!
                                acceptFile = true;
                            }
                        }
                        if (!acceptFile)
                        {
                            string exterr = "The file you are trying to upload is not a permitted file type!";
                            HttpContext.Current.Response.Redirect("~/error.aspx?err=" + exterr + "&color=red");
                        }
                        //Is the file too big to upload?
                        if (_file_size > _max_file_size)
                        {
                            //get Max upload size in MB                 
                            double maxFileSize = Math.Round(_max_file_size / 1024.0, 1);
                            string sizerr = "Filesize of the file is too large. Maximum file size permitted is " + maxFileSize + " KB";
                            HttpContext.Current.Response.Redirect("~/error.aspx?err=" + sizerr + "&color=red");
                        }                       
                    }
                }

                post_data = new string[,] {
                             { "PostTitle", _post_title },
                             { "PostCreationDate", _creation_date },
                             //{ "PostStartDate", _std },
                             //{ "PostEndDate", _end },
                             { "PostType", _post_type },
                             { "PostURL", _post_url },
                             { "PostText", _post_text_encode },
                             { "UserID", _user_id },
                             { "ModID", _mod_id },
                             { "CatID", _post_catid }
                             };

                ins = DB.do_insert("posts", post_data);
                ///---------------------------------------
                /// Insert upload to DB and file to server if posttype is "post"
                ///---------------------------------------  
                if (_post_type == "post")
                {
                    ///---------------------------------------
                    /// Get the last ID inserted
                    ///---------------------------------------                
                    string Query = "SELECT MAX(PostID) AS LastID FROM posts";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataReader r;
                    try
                    {
                        con.Open();
                        r = cmd.ExecuteReader();
                        r.Read();
                        lastid = Convert.ToString(r["LastID"]);
                        r.Close();
                    }
                    catch { }
                    finally { con.Close(); }
                    ///-----------------------------------------
                    /// Manage file to upload
                    ///-----------------------------------------                 
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[i]; // Get uploaded file
                        if (file.ContentLength > 0)
                        {
                            //saving code here
                            _file_name = Path.GetFileName(file.FileName);
                            _file_ext = Path.GetExtension(file.FileName);
                            _file_size = file.ContentLength;
                            _noext_file_name = Path.GetFileNameWithoutExtension(file.FileName);
                            _file_ext = _file_ext.ToLower();
                            _new_file_name = _file_name;
                            string filename_only = string.Empty;
                            int count = 1; // Count for new file name if the same name already exists
                            ///---------------------------------------
                            /// Does the file already exist? Change the name to a new one.
                            ///---------------------------------------
                            while (File.Exists(HttpContext.Current.Server.MapPath(Path.Combine("~/uploads/", _new_file_name))))
                            {
                                /*string samerr = "A file with the name " + _file_name + " already exists on the server.";
                                HttpContext.Current.Response.Redirect("~/error.aspx?err=" + samerr + "&color=red");*/
                                string tempFileName = string.Format("{0}({1})", _noext_file_name, count++);
                                _new_file_name = tempFileName + _file_ext;                                
                            }
                            string[] splitFile = _new_file_name.Split('.');
                            filename_only = splitFile[0];
                            ///---------------------------------------
                            /// Set array for file getting last PostID
                            ///---------------------------------------                
                            file_data = new string[,] {
                             { "UFName", filename_only },
                             { "UFCreationDate", _creation_date },
                             { "UFExtension", _file_ext },
                             { "UFSize", Convert.ToString(_file_size) },
                             { "UserID", _user_id },
                             { "PostID", lastid },
                             };

                            if (file != null)
                            {
                                insup = DB.do_insert("uploads", file_data);
                                file.SaveAs(HttpContext.Current.Server.MapPath(Path.Combine("~/uploads/", _new_file_name)));
                            }
                            if (!String.IsNullOrEmpty(insup))
                                HttpContext.Current.Response.Redirect("~/error.aspx?err=" + insup + "&color=red");
                        }
                    }               
                }

                ///---------------------------------------
                /// Insert log into db
                ///---------------------------------------
                std.save_log(_ag["UserID"], "A new post has been inserted.", "Posts");
            }
            else
            {
                post_data = new string[,] {
                             { "PostTitle", _post_title },
                             //{ "PostStartDate", _std },
                             //{ "PostEndDate", _end },
                             { "PostType", _post_type },
                             { "PostURL", _post_url },
                             { "PostText", _post_text_encode },
                             { "CatID", _post_catid }
                             };

                ins = DB.do_update("posts", post_data, "PostID=" + HttpContext.Current.Request["pid"]);
                ///---------------------------------------
                /// Insert log into db
                ///---------------------------------------
                std.save_log(_ag["UserID"], "The post with id:" + HttpContext.Current.Request["pid"] + " has been modified.", "Posts");
            }

            if (String.IsNullOrEmpty(ins))
                HttpContext.Current.Response.Redirect(redirect);
            else
                HttpContext.Current.Response.Redirect("~/error.aspx?err=" + ins + "&color=red");
            //LoadContent.Text = skin.error_page(colors[_wlColor], "red");
        }

        /// <summary>
        /// Delete post and files related.
        /// </summary>
        public void delete(Dictionary<string, string> _ag, string redirect)
        {
            if (String.IsNullOrEmpty(std.parse_incoming("pid")))
                HttpContext.Current.Response.Redirect("~/error.aspx?err=" + Resources.posts.ErrorNoPID + "&color=red");
            else
            {
                string del = DB.do_delete("posts", "PostID=" + HttpContext.Current.Request["pid"]);
                if (String.IsNullOrEmpty(del))
                {
                    ///---------------------------------------
                    /// Delete uploads if post is deleted
                    ///---------------------------------------
                    // Get filename from DB
                    Dictionary<string, string> UpList = new Dictionary<string, string>();
                    string Query = "SELECT * FROM uploads WHERE PostID=" + HttpContext.Current.Request["pid"];
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataReader r;
                    try
                    {
                        con.Open();
                        r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            string fname = Convert.ToString(r["UFName"]) + Convert.ToString(r["UFExtension"]);
                            UpList.Add(Convert.ToString(r["UFID"]), fname);
                        }
                        r.Close();
                    }
                    catch { }
                    finally { con.Close(); }
                    ///---------------------------------------
                    if (UpList.Count > 0) // Check if the dictionary has upload files
                    {
                        foreach (KeyValuePair<string, string> entry in UpList)
                        {
                            // do something with entry.Value or entry.Key
                            string delup = DB.do_delete("uploads", "UFID=" + entry.Key);
                            //Does the file already exist?
                            if (File.Exists(HttpContext.Current.Server.MapPath(Path.Combine("~/uploads/", entry.Value))))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(Path.Combine("~/uploads/", entry.Value)));
                            }
                            /*if (!String.IsNullOrEmpty(delup))
                                HttpContext.Current.Response.Redirect("~/error.aspx?err=" + delup + "&color=red");*/
                        }
                    }
                    ///---------------------------------------
                    /// Insert log into db
                    ///---------------------------------------
                    std.save_log(_ag["UserID"], "The post with id:" + HttpContext.Current.Request["pid"] + " has been DELETED!", "Posts");
                    HttpContext.Current.Response.Redirect(redirect);
                }
                else
                    HttpContext.Current.Response.Redirect("~/error.aspx?err=" + del + "&color=red");
            }
        }

        /// <summary>
        /// View post, upload and URL.
        /// </summary>
        public string view_post(Dictionary<string, string> _ag)
        {
            StringBuilder pageContent = new StringBuilder();
            string htmltext = string.Empty;
            string fname = string.Empty;
            string filedir = string.Empty;
            ///---------------------------------------
            /// Get post details
            ///---------------------------------------            
            string Query = "SELECT * FROM posts WHERE PostID=" + HttpContext.Current.Request["pid"];
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                r.Read();
                _post_title = Convert.ToString(r["PostTitle"]);
                _post_type = Convert.ToString(r["PostType"]);
                _post_url = Convert.ToString(r["PostURL"]);
                _post_text = Convert.ToString(r["PostText"]);
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            ///---------------------------------------
            /// Get Upload info
            ///---------------------------------------
            Dictionary<string, string> UpList = new Dictionary<string, string>();
            Dictionary<string, string> UpExt = new Dictionary<string, string>();
            Dictionary<string, string> UpSize = new Dictionary<string, string>();
            string UPQuery = "SELECT * FROM uploads WHERE PostID=" + HttpContext.Current.Request["pid"];
            SqlCommand ucmd = new SqlCommand(UPQuery, con);
            SqlDataReader ur;
            try
            {
                con.Open();
                ur = ucmd.ExecuteReader();
                while (ur.Read())
                {
                    fname = Convert.ToString(ur["UFName"]) + Convert.ToString(ur["UFExtension"]);
                    UpList.Add(Convert.ToString(ur["UFID"]), fname);
                    UpExt.Add(Convert.ToString(ur["UFID"]), Convert.ToString(ur["UFExtension"]));
                    UpSize.Add(Convert.ToString(ur["UFID"]), Convert.ToString(ur["UFSize"]));
                }
                ur.Close();
            }
            catch { }
            finally { con.Close(); }
            ///---------------------------------------            
            string decoded_text = WebUtility.HtmlDecode(_post_text); // Decode HTML from text
            htmltext += decoded_text;
            string[] imgFileTypes = new string[4];
            imgFileTypes[0] = ".jpg";
            imgFileTypes[1] = ".jpeg";
            imgFileTypes[2] = ".gif";
            imgFileTypes[3] = ".png";
            bool imageFile = false;
            ///---------------------------------------
            /// Take action using post type
            ///---------------------------------------
            if (_post_type == "post")
            {
                ///---------------------------------------
                if (UpList.Count > 0) // Check if the dictionary has upload files
                {
                    foreach (KeyValuePair<string, string> entry in UpList)
                    {
                        filedir = "/uploads/" + UpList[entry.Key];
                        //is this an image?
                        for (int i = 0; i <= 3; i++)
                        {
                            if (UpExt[entry.Key] == imgFileTypes[i])
                            {
                                //accept the file, yay!
                                imageFile = true;
                            }
                        }
                        if (!imageFile)
                        {
                            double FileSize = Math.Round(Convert.ToInt32(UpSize[entry.Key]) / 1024.0, 1);
                            htmltext += skin.show_post_file(filedir, UpList[entry.Key], Convert.ToString(FileSize));
                        }
                        else
                        {
                            htmltext += skin.show_post_image(filedir);
                        }
                    }
                }
                ///---------------------------------------

                pageContent.Append(skin.standalone_start(_post_title, "")); // Start of Table
                pageContent.Append(skin.add_standalone_row(htmltext)); // Add rows
            }
            else
            {
                HttpContext.Current.Response.Redirect(_post_url);
            }
            
            ///---------------------------------------
            string html = Convert.ToString(pageContent);
            return html;
        }

        #endregion
    }
}