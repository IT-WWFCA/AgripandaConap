using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AgriPanda.kernel;

namespace AgriPanda
{
    public partial class logout : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);
        private class_mssql DB = new class_mssql();
        private class_global std = new class_global();
        /// <summary>
        /// Is addupi login active?
        /// </summary>
        private Boolean addupi = Properties.Settings.Default.addUPIisActive;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (addupi == true)
            {
                string uname = User.Identity.Name;
                string userid = string.Empty;
                string UserQuery = "SELECT * FROM users WHERE UserName = '" + uname + "'";
                SqlCommand cmd = new SqlCommand(UserQuery, con);
                SqlDataReader userq;

                // Try to open database and read information.
                try
                {
                    con.Open();
                    userq = cmd.ExecuteReader();
                    userq.Read();

                    userid = userq["UserID"].ToString();

                    userq.Close();
                }
                catch { }
                finally { con.Close(); }

                string sessid = string.Empty;
                string addUPIQuery = "SELECT * FROM users_addupi_sessions WHERE UserID = '" + userid + "'";
                SqlCommand cmdb = new SqlCommand(addUPIQuery, con);
                SqlDataReader upiq;

                // Try to open database and read information.
                try
                {
                    con.Open();
                    upiq = cmdb.ExecuteReader();
                    upiq.Read();

                    sessid = upiq["SessionID"].ToString();

                    upiq.Close();
                }
                catch { }
                finally { con.Close(); }

                ///---------------------------------------
                /// Close/Delete any prior addUPI session
                ///---------------------------------------
                std.addupi_logout(sessid);
                DB.do_delete("users_addupi_sessions", "UserID=" + userid);
            }

            FormsAuthentication.SignOut();
            Response.Redirect("~/login.aspx");
        }
    }
}