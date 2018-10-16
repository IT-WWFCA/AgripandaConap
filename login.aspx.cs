/**
 * WWF Honduras
 * AgriPanda v2.0.0
 * Login Class
 * Last Updated: $Date: 2017-04-04 09:07:09 -0600 (Fri, 21 May 2010) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2017 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Login
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0042 $
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls.Expressions;
using System.Web.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AgriPanda.kernel;

namespace AgriPanda
{
    public partial class login : System.Web.UI.Page
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
        /// Actual date.
        /// </summary>
        private DateTime datetoday = DateTime.Today;
        /// <summary>
        /// Actual date and time.
        /// </summary>
        private DateTime datenow = DateTime.Now;
        /// <summary>
        /// Is addupi login active?
        /// </summary>
        private Boolean addupi;
        /// <summary>
        /// Do sending admin emails are activated?
        /// </summary>
        private Boolean sendAdminEmails;
        /// <summary>
        /// System main title
        /// </summary>
        private string PageTitle = string.Empty;
        /// <summary>
        /// System main title
        /// </summary>
        private bool doRegHTML = false;
        /// <summary>
        /// Get global settings
        /// </summary>
        Dictionary<string, string> _globalSettings;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            _globalSettings = std.get_global_settings(); // Get global settings
            addupi = Convert.ToBoolean(_globalSettings["AddUPIisActive"]); // Get AddUPIisActive value
            sendAdminEmails = Convert.ToBoolean(_globalSettings["SendAdminEmails"]); // Get SendAdminEmail value
            PageTitle = _globalSettings["MainTitle"];
            doRegHTML = Convert.ToBoolean(_globalSettings["UserReg"]); // Get UserReg value
            string LabelColor = "grey";            
            LabelTextFunc(LabelColor, PageTitle);
            if (doRegHTML == true)
            {
                doRegisterHTML();
                toreganchor.Text = "<a class=\"hiddenanchor\" id=\"toregister\"></a>";

                if (Request["dologin"] == "1")
                    doLogin();
                else if (Request["doReg"] == "1")
                    Register();
            }
            else
            {
                if (Request["dologin"] == "1")
                    doLogin();
            }

            
        }

        protected void doLogin()
        {

            String UserNameIn = Convert.ToString(HttpContext.Current.Request["username"]);
            String PasswdIn = Convert.ToString(HttpContext.Current.Request["password"]);
            string userpass = string.Empty;
            string sessid = string.Empty;
            string userid = string.Empty;
            string UPasswd = string.Empty;

            if (String.IsNullOrEmpty(UserNameIn))
            {
                string LabelColor = "red";
                string PageTitle = "<b>Error:</b> Este usuario no existe!";
                //string PageTitle = err.Message;
                LabelTextFunc(LabelColor, PageTitle);
            }
            else
            {

                string LoginQuery = "SELECT * FROM users WHERE UserName = '" + UserNameIn + "'";
                SqlCommand cmd = new SqlCommand(LoginQuery, con);
                SqlDataReader userq;

                // Try to open database and read information.
                try
                {
                    con.Open();
                    userq = cmd.ExecuteReader();
                    userq.Read();

                    int _salt = Convert.ToInt32(userq["UserPassSalt"]);
                    UPasswd = IN.ComputeSaltedHash(PasswdIn, _salt);
                    userpass = Convert.ToString(userq["UserPassWD"]);
                    userid = Convert.ToString(userq["UserID"]);

                    userq.Close();
                }
                catch //(Exception err)
                {
                    string LabelColor = "red";
                    string PageTitle = "<b>Database Error:</b> Server not found!";
                    //string PageTitle = err.Message;
                    LabelTextFunc(LabelColor, PageTitle);
                }
                finally
                {
                    con.Close();
                }

                if (String.IsNullOrEmpty(userid))
                {
                    string LabelColor = "red";
                    string PageTitle = "<b>Error:</b> Este usuario no existe!";
                    //string PageTitle = err.Message;
                    LabelTextFunc(LabelColor, PageTitle);
                }
                else
                {
                    if (userpass == UPasswd)
                    {
                        if (addupi == true)
                        {
                            ///---------------------------------------
                            /// Close/Delete any prior addUPI session
                            ///---------------------------------------
                            string sid = string.Empty;
                            string addUPIQuery = "SELECT * FROM users_addupi_sessions WHERE UserID = '" + userid + "'";
                            SqlCommand cmdb = new SqlCommand(addUPIQuery, con);
                            SqlDataReader upiq;

                            // Try to open database and read information.
                            try
                            {
                                con.Open();
                                upiq = cmdb.ExecuteReader();
                                upiq.Read();

                                sid = upiq["SessionID"].ToString();

                                upiq.Close();
                            }
                            catch { }
                            finally { con.Close(); }
                            ///---------------------------------------
                            std.addupi_logout(sid);
                            DB.do_delete("users_addupi_sessions", "UserID=" + userid);
                            ///---------------------------------------
                            /// Get addUPI Session
                            ///---------------------------------------
                            string SessionID = std.connect_to_addupi();
                            ///---------------------------------------
                            /// Insert addUPI Session to DB
                            ///---------------------------------------
                            string ntime = datenow.ToString("yyyy-MM-dd HH:mm:ss");
                            string[,] sessdata = { { "UserID", userid }, { "SessionID", SessionID }, { "SessionDate", ntime } };
                            string ins = DB.do_insert("users_addupi_sessions", sessdata);
                            if (ins != "")
                            {
                                string LabelColor = "yellow";
                                string PageTitle = ins;
                                LabelTextFunc(LabelColor, PageTitle);
                            }
                            else
                                FormsAuthentication.RedirectFromLoginPage(UserNameIn, false);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(UserNameIn, true);
                            FormsAuthentication.RedirectFromLoginPage(UserNameIn, false);
                            ///---------------------------------------
                            /// Insert log into db
                            ///---------------------------------------
                            std.save_log(userid, "El usuario " + UserNameIn + " a ingresado al sistema.", "Login");
                            ///---------------------------------------
                            /// Update user last login in DB
                            ///---------------------------------------
                            string [,] wl_data = new string[,] {
                             { "UserLastLogin", datenow.ToString("yyyy-MM-dd HH:mm:ss") }
                             };
                            DB.do_update("users", wl_data, "UserID=" + userid);
                        }
                    }
                    else
                    {
                        string LabelColor = "yellow";
                        string PageTitle = "Clave erronea, intenta de nuevo!";
                        LabelTextFunc(LabelColor, PageTitle);
                    }
                }
            }
        }

        /// <summary>
        /// Label handler.
        /// </summary>
        private void LabelTextFunc(string LabelColor, string PageTitle)
        {
            Labeltext.Text = @"<div class='login-alert alert login-alert-" + LabelColor + @" alert-" + LabelColor + @"'>
            <div class='login-message message'>" + PageTitle + @"</div>
            </div>";
        }

        /// <summary>
        /// Allow new users registration?.
        /// </summary>
        private void doRegisterHTML()
        {
            AllowReg.Text = @"<p class=""change_link"">
									¿Quieres tener acceso a la plataforma?
                                    <a href=""#toregister"" class=""to_register"">Regístrate</a>
								</p>";
        }

        /// <summary>
        /// Register new account.
        /// </summary>
        private void Register()
        {
            ///-----------------------------------------
            /// Set up variables
            ///-----------------------------------------
            string _reg_gid = "2";
            string _reg_uname = Convert.ToString(HttpContext.Current.Request["usernamesignup"]);          
            int _reg_psalt = class_login.CreateRandomSalt();
            string _reg_fname = Convert.ToString(HttpContext.Current.Request["fnamesignup"]);
            string _reg_lname = Convert.ToString(HttpContext.Current.Request["lnamesignup"]);
            string _reg_email = Convert.ToString(HttpContext.Current.Request["emailsignup"]);
            string _reg_passwd = Convert.ToString(HttpContext.Current.Request["passwordsignup"]);
            string _reg_passwd_confirm = Convert.ToString(HttpContext.Current.Request["passwordsignup_confirm"]);
            /*string _reg_utitle = string.Empty;
            string _reg_workarea = string.Empty;
            string _reg_org = string.Empty;
            string _reg_phone = string.Empty;
            string _reg_cell = string.Empty;*/
            string _reg_cdate = Convert.ToString(DateTime.Now);
            string _reg_lang = "es-HN";
            string _reg_skin = "skyblue";
            ///-----------------------------------------
            /// Check if username already exists
            ///-----------------------------------------
            int _user_checked = 0;
            string valUserName = string.Empty;
            string wsQuery = "SELECT * FROM users WHERE UserName='" + _reg_uname + "'";
            SqlCommand cmdws = new SqlCommand(wsQuery, con);
            SqlDataReader wsr;
            try
            {
                con.Open();
                wsr = cmdws.ExecuteReader();
                wsr.Read();
                valUserName = Convert.ToString(wsr["UserName"]);
                wsr.Close();
            }
            catch { }
            finally { con.Close(); }

            if(String.IsNullOrEmpty(valUserName))
                _user_checked = 1;
            else
                _user_checked = 0;
            ///-----------------------------------------
            /// Check if email already exists
            ///-----------------------------------------
            int _email_checked = 0;
            string valUserEmail = string.Empty;
            string ueQuery = "SELECT * FROM users WHERE UserEmail='" + _reg_email + "'";
            SqlCommand cmdue = new SqlCommand(ueQuery, con);
            SqlDataReader uer;
            try
            {
                con.Open();
                uer = cmdue.ExecuteReader();
                uer.Read();
                valUserEmail = Convert.ToString(uer["UserEmail"]);
                uer.Close();
            }
            catch { }
            finally { con.Close(); }

            if (String.IsNullOrEmpty(valUserEmail))
                _email_checked = 1;
            else
                _email_checked = 0;
            ///-----------------------------------------
            /// Check confirmation of both passwords
            ///-----------------------------------------
            int _passwd_checked = 0;
            if (_reg_passwd == _reg_passwd_confirm)
                _passwd_checked = 1;

            if (_passwd_checked == 1 && _user_checked == 1 && _email_checked == 1)
            {
                ///-----------------------------------------
                /// Insert data into DB
                ///-----------------------------------------
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
                             /*{ "UserTitle", _reg_utitle },
                             { "UserWorkArea", _reg_workarea },
                             { "UserOrganization", _reg_org },
                             { "UserWorkPhone", _reg_phone },
                             { "UserCellular", _reg_cell },*/
                             { "UserCreationDate", _reg_cdate },
                             { "UserLanguage", _reg_lang },
                             { "UserSkin", _reg_skin }
                             };
                string ins = DB.do_insert("users", user_data);
                if (String.IsNullOrEmpty(ins))
                {
                    if (sendAdminEmails == true)
                    {
                        ///-----------------------------------------
                        /// SEND EMAILS TO ADMIN USERS
                        ///-----------------------------------------
                        /// Get emails of admin users from DB
                        /// ----------------------------------------
                        string _admin_emails = std.get_admin_emails();
                        ///-----------------------------------------
                        /// Send email to Admin
                        /// ----------------------------------------
                        string[] emails_array = _admin_emails.Split(';');
                        string admsubj = "Nuevo Usuario se ha Registrado";
                        string admbodymsg = "Un nuevo usuario se ha registrado en la " + PageTitle + ", está pendiente de aprobación, ingresa al sistema para aprobar o denegar su solicitud.";
                        for (var i = 0; i < emails_array.Length; i++)
                        {
                            string[] email_vars = emails_array[i].Split(',');
                            std.global_send_email(email_vars[0], email_vars[1], admsubj, admbodymsg);
                        }
                        ///-----------------------------------------
                        /// Send email to new user
                        /// ----------------------------------------
                        string ufullname = _reg_fname + " " + _reg_lname;
                        string usbj = "Bienvenido a la " + PageTitle;
                        string ubmsg = @"
" + ufullname + @", muchas gracias por suscribirte a la " + PageTitle + @".
Tu registro está pendiente de aprobación, tu usuario será habilitado a mas tardar 24 horas.

Saludos.
                    ";
                        std.global_send_email(_reg_email, ufullname, usbj, ubmsg);
                    }
                    ///-----------------------------------------
                    /// Redirect
                    /// ----------------------------------------
                    HttpContext.Current.Response.Redirect("login.aspx?dologin=1&username=" + _reg_uname + "&password=" + _reg_passwd);
                }
                else
                    Labeltext.Text = skin.error_page(ins, "red");               
            }
            else if (_user_checked == 0)
            {
                Labeltext.Text = skin.error_page("El usuario seleccionado ya existe! Escribe uno nuevo.", "red");
                unamesignup.Text = _reg_uname;
                fnamesignup.Text = _reg_fname;
                lnamesignup.Text = _reg_lname;
                email.Text = _reg_email;
            }
            else if (_email_checked == 0)
            {
                Labeltext.Text = skin.error_page("El email escrito ya es usado por otra cuenta! Escribe uno nuevo.", "red");
                unamesignup.Text = _reg_uname;
                fnamesignup.Text = _reg_fname;
                lnamesignup.Text = _reg_lname;
                email.Text = _reg_email;
            }
            else if (_passwd_checked == 0)
            {
                Labeltext.Text = skin.error_page("Las claves introducidas no concuerdan una con la otra!", "red");
                unamesignup.Text = _reg_uname;
                fnamesignup.Text = _reg_fname;
                lnamesignup.Text = _reg_lname;
                email.Text = _reg_email;
            }
        }

        /// <summary>
        /// Generate new password.
        /// </summary>
        protected void Button2_Click(object sender, System.EventArgs e)
        {
            string _uname = Convert.ToString(HttpContext.Current.Request["username"]);
            string _passwd = class_login.CreateRandomPassword(10);
            int _salt = class_login.CreateRandomSalt();
            string hash = IN.ComputeSaltedHash(_passwd, _salt);
            string _email = string.Empty;

            string UserQuery = "SELECT * FROM users WHERE UserName = " + _uname;
            SqlCommand cmd = new SqlCommand(UserQuery, con);
            SqlDataReader r;

            try
            {
                con.Open();
                r = cmd.ExecuteReader();
                r.Read();
                _email = Convert.ToString(r["UserEmail"]);
                r.Close();
            }
            catch
            {
                string LabelColor = "red";
                string PageTitle = "No user in database";
                LabelTextFunc(LabelColor, PageTitle);
            }
            finally
            {
                con.Close();
            }

            ///-----------------------------------------
            /// Set up array
            ///-----------------------------------------
            string[,] user_data = {
                                  { "UserPassWD", _passwd },
                                  { "UserPassSalt", Convert.ToString(_salt) }
                                  };

            string dbup = DB.do_update("users", user_data, "UserName=" + _uname);

            if (dbup != "")
            {
                string LabelColor = "red";
                string PageTitle = dbup;
                LabelTextFunc(LabelColor, PageTitle);
            }
            else
            {
                ///-----------------------------------------
                /// Send email and redirect
                ///-----------------------------------------
                string msgTo = _email;
                string msgFrom = "support@gpas.us";
                string msgSubject = "Password recovery GreenPanda";
                string msgBody = @"Your new password for GreenPanda System is:" + _passwd +
                           @"\n\n------------------------\nThe GreenPanda Administrator";

                string MySmtpMailServerName = "localhost";
                int MySmtpMailServerPort = 25; //25 is the default for SMTP

                SmtpClient MySmtpClient = new SmtpClient(MySmtpMailServerName, MySmtpMailServerPort);
                MySmtpClient.Send(msgFrom, msgTo, msgSubject, msgBody);

                Response.Redirect("login.aspx");
            }
        }
    }
}