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

namespace AgriPanda.set
{
    public partial class _default : System.Web.UI.Page
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
        /// Add class_posts.
        /// </summary>
        private class_posts post = new class_posts();
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
        /// Actual date.
        /// </summary>
        private DateTime datetoday = DateTime.Today;
        /// <summary>
        /// Actual date and time.
        /// </summary>
        private DateTime timenow = DateTime.Now;

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
        //public string[] td_array;
        //public string[] tdarr;

        /// <summary>
        /// Turtle nest dropdown variable.
        /// </summary>
        private string _TorID;
        /// <summary>
        /// Collectors dropdown variable.
        /// </summary>
        private string _COLID;
        /// <summary>
        /// Colecta siembra AreaPlaya.
        /// </summary>
        private string _CSAreaPlaya;
        /// <summary>
        /// Colecta siembra CuotaConservacion.
        /// </summary>
        private string _CSCuotaConservacion;

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
            /// Set user prefrered language
            ///--------------------------------------- 
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);

            switch (HttpContext.Current.Request["code"])
            {
                ///------------------
                /// Form calls for collection and sowing
                ///------------------
                case "addcs":
                    //LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormCS("new");
                    break;

                case "editcs":
                    //LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormCS("edit");
                    break;

                /*case "doaddcs":
                    doInsertCS("new");
                    break;

                case "doeditcs":
                    doInsertCS("edit");
                    break;

                case "deletecs":
                    deleteCS();
                    break;

                ///------------------
                /// Form calls for commercialization
                ///------------------
                case "addcomm":
                    LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormCOMM("new");
                    break;

                case "editcomm":
                    LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormCOMM("edit");
                    break;

                case "doaddcomm":
                    doInsertCOMM("new");
                    break;

                case "doeditcomm":
                    doInsertCOMM("edit");
                    break;

                case "deletecomm":
                    deleteCOMM();
                    break;

                ///------------------
                /// Form calls for purchase and conservation
                ///------------------
                case "addpc":
                    LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormPC("new");
                    break;

                case "editpc":
                    LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormPC("edit");
                    break;

                case "doaddpc":
                    doInsertPC("new");
                    break;

                case "doeditpc":
                    doInsertPC("edit");
                    break;

                case "deletepc":
                    deletePC();
                    break;

                ///------------------
                /// Form calls for hatching and release
                ///------------------
                case "addhr":
                    LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormHR("new");
                    break;

                case "edithr":
                    LoadFormScripts.Text = loadScripts();
                    LoadContent.Text = showFormHR("edit");
                    break;

                case "doaddhr":
                    doInsertHR("new");
                    break;

                case "doedithr":
                    doInsertHR("edit");
                    break;

                case "deletehr":
                    deleteHR();
                    break;*/

                ///------------------
                /// External links calls
                ///------------------

                case "reldocs":
                    LoadContent.Text = post.show_all_posts(Resources.warnings.LinkPageTitle, Resources.warnings.LinkPageDescription, "6", _ag);
                    break;

                case "postreldocs":
                    LoadFormScripts.Text = post.loadPostScripts();
                    LoadContent.Text = post.show_form(Resources.warnings.LinkPageTitle, Resources.warnings.LinkPageDescription, "6", _ag, "new"); ;
                    break;

                case "editpost":
                    LoadFormScripts.Text = post.loadPostScripts();
                    LoadContent.Text = post.show_form(Resources.warnings.LinkPageTitle, Resources.warnings.LinkPageDescription, "6", _ag, "edit"); ;
                    break;

                case "doaddpost":
                    post.doInsert("new", _ag, "6", "default.aspx?code=reldocs");
                    break;

                case "deletepost":
                    post.delete(_ag, "default.aspx?code=reldocs");
                    break;

                case "viewpost":
                    LoadContent.Text = post.view_post(_ag);
                    break;

                ///------------------
                default:
                    LoadContent.Text = Dashboard(HttpContext.Current.Request["page"]);
                    break;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Show dashboard.
        /// </summary>
        private string Dashboard(string page)
        {
            StringBuilder pageContent = new StringBuilder();
            ///---------------------------------------
            

            ///---------------------------------------
            string showdash = Convert.ToString(pageContent);
            return showdash;
        }

        /// <summary>
        /// Show form for new or edit a collection and sowing log.
        /// </summary>
        private string showFormCS(string type)
        {
            StringBuilder pageContent = new StringBuilder();
            if (type != "new")
            {
                if (String.IsNullOrEmpty(std.parse_incoming("csid")))
                    pageContent.Append(skin.error_page(Resources.warnings.ErrorNoWLID, "yellow"));
                else
                {
                    ///-----------------------------------------
                    string CSQuery = "SELECT * FROM set_colecta_siembra_log WHERE CSID = " + HttpContext.Current.Request["csid"];
                    SqlCommand cmdcs = new SqlCommand(CSQuery, con);
                    SqlDataReader cs;
                    try
                    {
                        con.Open();
                        cs = cmdcs.ExecuteReader();
                        while (cs.Read())
                        {
                            _TorID = Convert.ToString(cs["TORID"]);
                            _COLID = Convert.ToString(cs["COLID"]);
                            _CSAreaPlaya = Convert.ToString(cs["CSAreaPlaya"]);
                            _CSCuotaConservacion = Convert.ToString(cs["CSCuotaConservacion"]);
                        }
                        cs.Close();
                    }
                    catch { }
                    finally { con.Close(); }
                    ///---------------------------------------
                    _button = Resources.set.ButtonEditCS;
                    _code = "doeditcs";
                }
            }
            else
            {
                _TorID = Convert.ToString(HttpContext.Current.Request["torid"]);
                _COLID = Convert.ToString(HttpContext.Current.Request["colid"]);
                _CSAreaPlaya = Convert.ToString(HttpContext.Current.Request["CSAreaPlaya"]);
                _CSCuotaConservacion = Convert.ToString(HttpContext.Current.Request["CSCuotaConservacion"]);
                ///---------------------------------------
                _button = Resources.set.ButtonAddCS;
                _code = "doaddcs";
            }
            ///-----------------------------------------
            /// Create turtle nest dropdown
            ///-----------------------------------------
            string[,] torarr;
            string _tor = skin.form_dropdown_start("torid");
            string torQuery = "SELECT * FROM set_tortugarios";
            SqlCommand cmdtor = new SqlCommand(torQuery, con);
            SqlDataReader tor;
            try
            {
                con.Open();
                tor = cmdtor.ExecuteReader();
                while (tor.Read())
                {
                    torarr = new string[,] { { Convert.ToString(tor["TORID"]), Convert.ToString(tor["TORNombre"]) } };
                    _tor += skin.form_dropdown_opts(torarr, _TorID);
                }
                tor.Close();
            }
            catch { }
            finally { con.Close(); }
            _tor += skin.form_dropdown_end();
            ///-----------------------------------------
            /// Create collectors dropdown
            ///-----------------------------------------
            string[,] colarr;
            string _col = skin.form_dropdown_start("colid");
            string colQuery = "SELECT * FROM set_colectores";
            SqlCommand cmdcol = new SqlCommand(colQuery, con);
            SqlDataReader col;
            try
            {
                con.Open();
                col = cmdtor.ExecuteReader();
                while (col.Read())
                {
                    colarr = new string[,] { { Convert.ToString(col["COLID"]), Convert.ToString(col["COLNombreColector"]) } };
                    _col += skin.form_dropdown_opts(colarr, _COLID);
                }
                col.Close();
            }
            catch { }
            finally { con.Close(); }
            _tor += skin.form_dropdown_end();
            ///-----------------------------------------
            string NewTitle = Resources.set.PageTitle;
            ///-----------------------------------------
            string[,] startform = { { "code", _code }, { "csid", HttpContext.Current.Request["wlid"] } };
            string[,] tableheader = { { "&nbsp;", "20%", "center" }, { "&nbsp;", "80%", "left" } };
            ///-----------------------------------------
            /// Rows Arrays
            ///-----------------------------------------
            string[,] td_rows;
            td_rows = new string[,] {
                                        { "<b>" + Resources.warnings.WLWTCode + "</b>", _tor },
                                        { "<b>" + Resources.warnings.WLWTCode + "</b>", _col },
                                        { "<b>" + Resources.warnings.WLTitle + "</b>", skin.form_input("CSAreaPlaya", Convert.ToString(_CSAreaPlaya), "input", "40", "") },
                                        { "<b>" + Resources.warnings.WLTitle + "</b>", skin.form_input("CSCuotaConservacion", Convert.ToString(_CSCuotaConservacion), "input", "40", "") },
                                    };
            ///-----------------------------------------
            pageContent.Append(skin.start_form(startform)); // Start of Form
            pageContent.Append(skin.start_table(NewTitle, Resources.set.PageDescription, tableheader)); // Start of Table
            for (int i = 0; i <= td_rows.GetUpperBound(0); i++)
            {
                string s1 = td_rows[i, 0];
                string s2 = td_rows[i, 1];
                string[] genrow = new string[] { s1, s2 };
                pageContent.Append(skin.add_td_row(genrow, "")); // Add form row
            }
            pageContent.Append(skin.end_table()); // End of Table
            pageContent.Append(skin.end_form(_button, "")); // End of Form

            string formCS = Convert.ToString(pageContent);
            return formCS;
        }
        #endregion
    }
}