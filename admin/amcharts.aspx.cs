using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgriPanda.kernel;

namespace AgriPanda.admin
{
    public partial class amcharts : System.Web.UI.Page
    {
        #region Variables
        /// <summary>
        /// Add mssql_global.
        /// </summary>
        private class_global std = new class_global();
        /// <summary>
        /// Add mssql_skin.
        /// </summary>
        private class_skin skin = new class_skin();
        /// <summary>
        /// Connect to mssql DB.
        /// </summary>
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request["code"])
            {
                case "dsvch":
                    dsvamch("sigatoka", "?code=doshowamch&chid=" + Request["chid"] + "&maxval=" + Request["maxval"]);
                    break;
                //-------------------
                default:
                    dsvamch("sigatoka", "?code=doshowamch&chid=" + Request["chid"] + "&maxval=" + Request["maxval"]);
                    break;
            }
        }

        /// <summary>
        /// DSV Graph.
        /// </summary>
        private void dsvamch(string file, string str)
        {
            LoadScript.Text = "<script src=\"./handlers/" + file + ".ashx" + str + "\" type=\"text/javascript\"></script>";
            string _desc = string.Empty;
            //-------------------------------
            // Get farms names
            //-------------------------------
            Dictionary<string, string> wsnames = new Dictionary<string, string>();
            string WSQuery = "SELECT * FROM weather";
            SqlCommand cmdb = new SqlCommand(WSQuery, con);
            SqlDataReader rw;
            try
            {
                con.Open();
                rw = cmdb.ExecuteReader();
                while (rw.Read())
                {
                    wsnames.Add(Convert.ToString(rw["WSID"]), Convert.ToString(rw["WSLocation"]));
                }
                rw.Close();
            }
            catch { }
            finally { con.Close(); }
            //-------------------------------
            string SGPHQuery = "SELECT * FROM ipm_sigatoka_graphs WHERE GPHID=" + Request["chid"];
            SqlCommand cmd = new SqlCommand(SGPHQuery, con);
            SqlDataReader r;
            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                r.Read();
                _desc = "Chart from " + wsnames[Convert.ToString(r["WSID"])];
                Start.Text += skin.standalone_start(Convert.ToString(r["GPHName"]), _desc); // Add Titles
                r.Close();
            }
            catch { }
            finally { con.Close(); }
            Form1.Text += "<div class=\"basic-row\"><input type=\"radio\" name=\"group\" id=\"rb1\" onclick=\"setPanSelect()\">Select &nbsp;&nbsp;<input type=\"radio\" checked=\"true\" name=\"group\" id=\"rb2\" onclick=\"setPanSelect()\">Pan</div> ";
            string[,] startform = { { "code", "doshowamch" }, { "chid", Request["chid"] } };
            Form1.Text += skin.start_form(startform); // Start of Form
            Form1.Text += "<div class=\"basic-row\"><b>Insert New Max Value</b>: " + skin.form_input("maxval", Request["maxval"], "input", "8", "") + "</div>";
            Form1.Text += "<div class=\"basic-row\">" + skin.end_form("Update", "") + "</div>"; // End of Form
        }
    }
}