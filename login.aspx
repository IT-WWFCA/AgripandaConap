<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AgriPanda.login" %>

<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="no-js ie6 lt8"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="no-js ie7 lt8"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="no-js ie8 lt8"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="no-js ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html lang="en" class="no-js"> <!--<![endif]-->
    <head>
        <meta charset="UTF-8" />
        <!-- <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">  -->
        <title>Ingresar a AgriPanda</title>
        <link rel="shortcut icon" href="favicon.ico"> 
        <link rel="stylesheet" type="text/css" href="css/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/style.css" />
		<link rel="stylesheet" type="text/css" href="css/animate-custom.css" />
    </head>
    <body>
        <div class="container">
            <!-- Codrops top bar -->
            <div class="codrops-top">
                <a href="http://www.wwf-mar.org:8080" target="_blank">
                    <strong>&laquo; Datos climáticos: </strong>AddVANTAGE PRO
                </a>
                <span class="right">
                    <a href="http://www.wwfca.org" target="_blank">
                        <strong>www.wwfca.org</strong>
                    </a>
                </span>
                <div class="clr"></div>
            </div><!--/ Codrops top bar -->
            <header>
                <h1><asp:Literal id="Labeltext" runat="server" /></h1>
            </header>
            <section>				
                <div id="container_demo" >
                    <asp:Literal id="toreganchor" runat="server" />                    
                    <a class="hiddenanchor" id="tologin"></a>
                    <div id="wrapper">
                        <div id="login" class="animate form">
                            <form  action="./login.aspx" autocomplete="on" name="Form1"> 
                                <input type="hidden" name="dologin" id="dologin" value="1" />
                                <h1>Ingresa a la plataforma</h1> 
                                <p> 
                                    <label for="username" class="uname" data-icon="u" > Nombre de usuario </label>
                                    <input id="username" name="username" required type="text" placeholder="miusuario"/>
                                </p>
                                <p> 
                                    <label for="password" class="youpasswd" data-icon="p"> Clave </label>
                                    <input id="password" name="password" required type="password" placeholder="ej. X8df!90EO" /> 
                                </p>
                                <p class="keeplogin"> 
									<input type="checkbox" name="loginkeeping" id="loginkeeping" value="loginkeeping" /> 
									<label for="loginkeeping">Mantenerme conectado</label>
								</p>
                                <p class="login button"> 
                                    <input type="submit" value="Entrar >>" /> 
								</p>
                                <asp:Literal id="AllowReg" runat="server" />
                            </form>
                        </div>

                        <div id="register" class="animate form">
                            <form  action="./login.aspx" autocomplete="on" name="Form1"> 
                                <input type="hidden" name="doReg" id="doReg" value="1" />
                                <h1> Registro de nueva cuenta </h1> 
                                <p> 
                                    <label for="usernamesignup" class="uname" data-icon="u">Nombre de usuario</label>
                                    <input id="usernamesignup" name="usernamesignup" required type="text" placeholder="minombredeusuario690" value="<asp:Literal id="unamesignup" runat="server" />" />
                                </p>
                                <p> 
                                    <label for="fnamesignup" class="fname" data-icon="u">Nombre(s)</label>
                                    <input id="fnamesignup" name="fnamesignup" required type="text" placeholder="Mi Nombre" value="<asp:Literal id="fnamesignup" runat="server" />" />
                                </p>
                                <p> 
                                    <label for="lnamesignup" class="lname" data-icon="u">Apellido(s)</label>
                                    <input id="lnamesignup" name="lnamesignup" required type="text" placeholder="Mi Apellido" value="<asp:Literal id="lnamesignup" runat="server" />" />
                                </p>
                                <p> 
                                    <label for="emailsignup" class="youmail" data-icon="e" > Correo electrónico</label>
                                    <input id="emailsignup" name="emailsignup" required type="email" placeholder="misupermail@mail.com" value="<asp:Literal id="email" runat="server" />"/> 
                                </p>
                                <p> 
                                    <label for="passwordsignup" class="youpasswd" data-icon="p">Clave de acceso </label>
                                    <input id="passwordsignup" name="passwordsignup" required type="password" placeholder="ej. X8df!90EO"/>
                                </p>
                                <p> 
                                    <label for="passwordsignup_confirm" class="youpasswd" data-icon="p">Confirme su clave </label>
                                    <input id="passwordsignup_confirm" name="passwordsignup_confirm" required type="password" placeholder="ej. X8df!90EO"/>
                                </p>
                                <p class="signin button"> 
									<input type="submit" value="Enviar >>"/> 
								</p>
                                <p class="change_link">  
									¿Ya eres un miembro registrado?
									<a href="#tologin" class="to_register"> Ingresa a la plataforma </a>
								</p>
                            </form>
                        </div>
						
                    </div>
                </div>  
            </section>
        </div>
    </body>
</html>
