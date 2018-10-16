/**
 * WWF Honduras
 * AgriPanda v2.0.0
 * init Javascript Generator Handler
 * Last Updated: $Date: 2015-05-04 15:07:08 -0600 (Thu, 16 Apr 2015) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2015 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.INITJS
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0002 $
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

namespace AgriPanda.handlers
{
    /// <summary>
    /// Summary description for initjs
    /// </summary>
    public class initjs : IHttpHandler
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
        /// String jscontent for output.
        /// </summary>
        private string jscontent;
        #endregion

        # region Constructor
        /// <summary>
        /// Main function.
        /// </summary>
        public void ProcessRequest(HttpContext context)
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
                case "admin":
                    showAdmin();
                    break;

                //-------------------
                default:
                    show_all(_ag["UserMods"]);
                    break;
            }

            context.Response.ContentType = "application/javascript";
            context.Response.Write(jscontent);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Show all harvest data.
        /// </summary>
        private string show_all(string userMods)
        {
            string accord = getAccordionUM(userMods);
            string trees = getAccordionUMtrees(userMods);
            string tabFunc = getTabFunc(userMods);
            string toolbarFunc = getToolbarFunc(userMods);
            jscontent = "/* Init JS Javascript */\n";
            jscontent += @"var dba = {}; //namespace

dba.start_text = ""<br/><span class = 'info_message'>Select any entity in tree to view details</span>"";
dba.i18n = {
	select_db: ""You need to select DB first"",
	select_connection: ""Select any connection first"",
	delete_connection: ""Are you sure to delete connection:<br>""
};

dba.uid = function() {
	return (new Date()).valueOf();
}
dba.get_id_chain = function(tree,id) {
	var chain = [id];
	while (id = tree.getParentId(id))
		chain.push(id);
        
	return chain.reverse().join(""|"");
}
dba.pages = {};// store opened tab
dba.add_connection = function(server,user,pass) {
	top.setTimeout(function() {
		dhx4.ajax.get(""./logic/php/connection.php?mode=add&server=""+encodeURIComponent(server)+""&user=""+encodeURIComponent(user)+""&pass=""+encodeURIComponent(pass), function(xml){
			eval(xml.xmlDoc.responseText);
		});
		dba.popup_win.close();
	},1);
}
dba.delete_connection = function(server) {
	top.setTimeout(function() {
		dhx4.ajax.get(""./logic/php/connection.php?mode=delete&server=""+encodeURIComponent(server), function(xml){
			eval(xml.xmlDoc.responseText);
		});
	},1);
};

" + tabFunc + @"

dba.create_tab = function(id,full_id,text,extra) {
	full_id = full_id||dba.get_id_chain(dba.tree,id);
	
	if (!dba.pages[full_id]) {
		var details = id.split(""^"");
		dba.tabbar.addTab(full_id,full_id,100);
		var win = dba.tabbar.cells(full_id);
		
		//using window instead of tab
		var toolbar = win.attachToolbar();
		toolbar.attachEvent(""onClick"",dba.tab_toolbar_click);
		toolbar.setIconsPath(""./imgs/"");
		toolbar.loadStruct(""xml/toolbar_""+details[0]+"".xml"");
		
		dba.tabbar.cells(full_id).setActive();
		dba.tabbar.cells(full_id).setText(text||dba.tree.getItemText(id));
		switch(details[0]) {
		case ""table"":
			dba.set_data_table(win,full_id);
			break;
		case ""query"":
			dba.set_query_layout(win);
			break;
		}
		
		dba.pages[full_id] = win;
		win.extra = extra;
	}
	else dba.tabbar.cells(full_id).setActive();
	
};
dba.tab_toolbar_click = function(id) {
	switch(id) {
        case ""close"":
        	var id = dba.tabbar.getActiveTab();
        	delete dba.pages[id];
        	dba.tabbar.tabs(id).close(true);
        	break;
        case ""run_query"":
        	var win = dba.tabbar.cells(dba.tabbar.getActiveTab());
        	win.area.parentNode.removeChild(win.area);
        	win.grid.post(""./logic/php/sql.php"",""id=""+encodeURIComponent(win.extra.join(""|""))+""&sql=""+encodeURIComponent(win.area.value));
        	break;
        case ""refresh_table"":
        	var win = dba.tabbar.cells(dba.tabbar.getActiveTab());
        	win.grid.loadXML(win.grid._refresh);
        	break;
        	
        case ""show_structure"":
        	var win = dba.tabbar.cells(dba.tabbar.getActiveTab());
        	win.grid.load(win.grid._refresh+""&struct=true"");
        	break;
" + toolbarFunc + @"        	
        default:
        	dhtmlx.alert({
			title:""Information!"",
			type:""alert-error"",
			text:""Not implemented""
        	});
        	break;
        }
};
dba.main_toolbar_click = function(id) {
	switch(id) {
		case ""add_connection"":
			var win = dba.layout.dhxWins.createWindow(""creation"",1,1,300,180);
			win.setText(""Add connection"");
			win.setModal(true);
			win.denyResize();
			win.center();
			win.attachURL(""connection.html?etc = ""+new Date().getTime());
			dba.popup_win = win;
			break;

		case ""userop"":
			var win = dba.layout.dhxWins.createWindow(""creation"",1,1,500,350);
			win.setText(""" + Resources.global.useropTit + @""");
			win.setModal(true);
			win.denyResize();
			win.center();
			wintabbar = win.attachTabbar({
				tabs: [
					{id: ""a1"", label: """ + Resources.global.useropSettings + @""", active: true},
                    {id: ""a2"", label: """ + Resources.global.useropSecurity + @"""}
				]
			});
			winform = wintabbar.tabs(""a1"").attachForm();
            winform.loadStruct(""./handlers/forms.ashx?code=userop"");
            dba.popup_win = win;
			break;
		
		case ""delete_connection"":
			var data = dba.tree.getSelectedItemId();
			if (!data) {
				dhtmlx.alert({
					title: ""Information!"",
					type: ""alert-error"",
					text: dba.i18n.select_connection
				});
				return;
			}
			data = dba.get_id_chain(dba.tree,data).split(""|"")[0];
			dhtmlx.confirm({
				title: ""Information!"",
				type: ""confirm-error"",
				text: dba.i18n.delete_connection + dba.tree.getItemText(data),
				callback: function(r) {
					if (r == true) dba.delete_connection(data.split(""^"")[1]);
				}
			});
			break;
		
		case ""sql_query"":
			var data = dba.get_id_chain(dba.tree,dba.tree.getSelectedItemId()).split(""|"");
			if (data.length<2) {
				dhtmlx.alert({
					title: ""Information!"",
					type: ""alert-error"",
					text: dba.i18n.select_db
				});
				return;
			}
			dba.create_tab(""query"",dba.uid(),""SQL : ""+dba.tree.getItemText(data[0])+"" : ""+data[1].split(""^"")[1],data);
			break;

		default:
        	dhtmlx.alert({
			title:""Information!"",
			type:""alert-error"",
			text:""Not implemented""
        	});
        	break;
        }
};
dba.set_query_layout = function(win) {
	var grid = win.grid = win.attachGrid();
	grid.enableSmartRendering(true);
	grid.setHeader(""<textarea style = 'width: 100%; height: 80px; '>Type SQL query here</textarea>"")
	grid.setInitWidths(""*"")
	grid.init();
	grid.attachEvent(""onXLE"",function() {
		this.hdr.rows[1].cells[0].firstChild.appendChild(win.area);
		this.hdr.rows[1].cells[0].className = ""grid_hdr_editable"";
		this.setSizes();
		win.area.focus();
	});
	
	var area = grid.hdr.rows[1].cells[0];
	area.className = ""grid_hdr_editable"";
	area.onselectstart = function(e) { return ((e||event).cancelBubble = true); }
	
	area = area.firstChild.firstChild;
	area.focus();
	area.select();
	dhtmlxEvent(area,""keypress"",function(e) {
		e = e||event;
		code = e.charCode||e.keyCode;
		if (e.ctrlKey && code == 13) dba.tab_toolbar_click(""run_query"");
	});
	
	win.area = area;
}
dba.set_data_table = function(win,full_id) {
	var grid = win.grid = win.attachGrid();
	grid.enableSmartRendering(true);
	grid._refresh = ""./logic/php/datagrid.php?id=""+encodeURIComponent(full_id);
	grid.loadXML(grid._refresh);
};

function init() {
	
	dba.layout = new dhtmlXLayoutObject(document.body, ""2U"");
	dba.layout.cells(""a"").setText(""" + Resources.global.herarchy + @""");
	dba.layout.cells(""a"").setWidth(220);
	dba.layout.cells(""b"").setText(""Details"");
	dba.layout.cells(""a"").hideHeader();

	dba.accord = dba.layout.cells(""a"").attachAccordion({
		/*items: [
			{ id: ""a1"", text: ""Main Page"" },
			{ id: ""a2"", text: ""Navigation"" },
			{ id: ""a3"", text: ""Feedback"" }
		]*/
	" + accord + @"
	});

	" + trees + @"
	/*dba.treeb = dba.accord.cells(""DBS"").attachTree();
	//dba.treeb.setSkin(skin);
	dba.treeb.setImagePath(""./lib/dhtmlx3/skins/" + _ag["UserSkin"] + @"/imgs/dhxtree_" + _ag["UserSkin"] + @"/"");
	dba.treeb.loadXML(""./handlers/tree.ashx?accor=2"");
	dba.treeb.attachEvent(""onClick"", function (id) {
		if (id.split(""^"")[1] == ""link"") dba.create_tab(id);
			return true;
		});*/
	/*dba.treea = dba.accord.cells(""a2"").attachTree();
	dba.treea.setSkin(skin);
	dba.treea.setImagePath(""./lib/dhtmlx/imgs/csh_dhx_skyblue/"");
	dba.treea.loadStruct(""./handlers/tree.ashx?accor=1"");
	dba.treea.attachEvent(""onClick"", function (id) {
		if (id.split(""^"")[1] == ""link"") dba.create_tab_a(id);
			return true;
		});*/
	
	/*dba.tree = dba.layout.cells(""a"").attachTree(0);
	dba.tree.setIconSize(18,18);
	dba.tree.setImagePath(""./imgs/tree/"");
	dba.tree.setXMLAutoLoadingBehaviour(""function"");
	dba.tree.setXMLAutoLoading(function(id) {
		dba.tree.loadXML(""./logic/php/tree.php?id=""+id+""&full_id=""+encodeURIComponent(dba.get_id_chain(dba.tree,unescape(id))));
	});
	
	dba.tree.loadXML(""./logic/php/tree.php"");
	dba.tree.attachEvent(""onClick"",function(id) {
		if (id.split(""^"")[0] == ""table"") dba.create_tab(id);
		return true;
	});*/
	
	/*dba.toolbar = dba.layout.attachToolbar();
	dba.toolbar.setIconsPath(""./lib/dhtmlx3/imgs/"");
	dba.toolbar.attachEvent(""onClick"",dba.main_toolbar_click);
	dba.toolbar.loadStruct(""./xml/buttons.xml"");*/
	
	dba.tabbar = dba.layout.cells(""b"").attachTabbar();
	dba.tabbar.addTab(""start"",""" + Resources.global.welcome + @""",""100"");
    //dba.tabbar.cells(""start"").attachHTMLString(dba.start_text); //dba.tabbar.setContentHTML(""start"",dba.start_text)
	dba.tabbar.cells(""start"").attachURL(""./intro.aspx"");
	dba.tabbar.cells(""start"").setActive(); //dba.tabbar.setTabActive(""start"");
	dba.tabbar.enableTabCloseButton(true);
    //dba.tabbar.setTabActive(""start"")

	dba.sb = dba.layout.attachStatusBar();
	dba.sb.setText(""" + Resources.config.Copyright + @""");
	var text = dba.sb.getText();
	dba.menu = dba.layout.attachMenu();
	dba.menu.setIconsPath(""./lib/dhtmlx3/imgs/"");
	dba.menu.attachEvent(""onClick"", dba.main_toolbar_click);
	dba.menu.loadStruct(""./xml/dhxmenu.xml"");
	
	dba.tabbar.attachEvent(""onTabClose"",function(id) {
		delete dba.pages[id];
		return true;
	});
}";
            return jscontent;
        }

        /// <summary>
        /// Show harvest data per year.
        /// </summary>
        private string showAdmin()
        {
            jscontent = "/* Init JS Javascript for Administrator */";
            return jscontent;
        }

        /// <summary>
        /// Get accordion items.
        /// </summary>
        private string getAccordion()
        {
            string accord = string.Empty;            
            accord = "items: [\n";
            string MQuery = "SELECT * FROM mods ORDER BY ModPos ASC";
            SqlCommand cmd = new SqlCommand(MQuery, con);
            SqlDataReader mq;
            try
            {
                con.Open();
                mq = cmd.ExecuteReader();
                while (mq.Read())
                {
                    accord += "	{ id: \"" + mq["ModCode"] + "\", text: \"" + mq["ModName"] + "\" },\n";
                }
                mq.Close();
            }
            catch { }
            finally { con.Close(); }
            accord = Regex.Replace(accord, ",$", "");
            accord += "]";
            return accord;
        }

        /// <summary>
        /// Get accordion items from UserMods.
        /// </summary>
        private string getAccordionUM(string um)
        {
            string accord = string.Empty;
            accord = "items: [\n";
            um = Regex.Replace(um, "\\]$", "");
            um = Regex.Replace(um, "^S\\[", "");
            string[] tMods = um.Split(':');
            for (var i = 0; i < tMods.Length; i++)
            {
                accord += "	{ id: \"" + tMods[i] + "\", text: \"" + Resources.global.ResourceManager.GetString("mod" + tMods[i]) + "\" },\n";
            }
            accord += "]";
            return accord;
        }

        /// <summary>
        /// Get accordion trees from UserMods.
        /// </summary>
        private string getAccordionUMtrees(string um)
        {
            string trees = string.Empty;
            um = Regex.Replace(um, "\\]$", "");
            um = Regex.Replace(um, "^S\\[", "");
            string[] tMods = um.Split(':');
            for (var i = 0; i < tMods.Length; i++)
            {
                trees += @"
	dba.tree" + i + @" = dba.accord.cells(""" + tMods[i] + @""").attachTree();
	dba.tree" + i + @".setImagePath(""./lib/dhtmlx3/skins/" + _ag["UserSkin"] + @"/imgs/dhxtree_" + _ag["UserSkin"] + @"/"");
	dba.tree" + i + @".loadXML(""./handlers/tree.ashx?accor=" + tMods[i] + @""");
	dba.tree" + i + @".attachEvent(""onClick"", function (id) {
		if (id.split(""^"")[1] == ""link"") dba.create_tab" + i + @"(id);
			return true;
		});

";
            }
            return trees;
        }

        /// <summary>
        /// Get creation_tab funtions from UserMods.
        /// </summary>
        private string getTabFunc(string um)
        {
            string tabFunc = string.Empty;
            um = Regex.Replace(um, "\\]$", "");
            um = Regex.Replace(um, "^S\\[", "");
            string[] tMods = um.Split(':');
            for (var i = 0; i < tMods.Length; i++)
            {
                tabFunc += @"dba.create_tab" + i + @" = function(id,full_id,text,extra) {
	full_id = full_id||dba.get_id_chain(dba.tree" + i + @",id);
	
	if (!dba.pages[full_id]) {
		var details = id.split(""^"");
		dba.tabbar.addTab(full_id,full_id,100);
		var win = dba.tabbar.cells(full_id);
		
		//using window instead of tab
		var toolbar = win.attachToolbar();
		toolbar.attachEvent(""onClick"",dba.tab_toolbar_click);
		toolbar.setIconsPath(""./lib/dhtmlx3/skins/" + _ag["UserSkin"] + @"/imgs/"");
		toolbar.loadStruct(""./handlers/toolbars.ashx?ic="" + details[0]);
		
		dba.tabbar.cells(full_id).setActive();
		dba.tabbar.cells(full_id).setText(text||dba.tree" + i + @".getItemText(id));
		switch(details[0]) {
		    case ""Empty"": win.attachURL(""./intro.aspx"");
                        break;";
		
                string mid = string.Empty;
                /// ------------------------------------
                /// Get the mod ID
                /// ------------------------------------
                string MQuery = "SELECT * FROM mods WHERE ModCode='" + tMods[i] + "'";
                SqlCommand cmd = new SqlCommand(MQuery, con);
                SqlDataReader mq;
                try
                {
                    con.Open();
                    mq = cmd.ExecuteReader();
                    while (mq.Read())
                    {
                        mid = Convert.ToString(mq["ModID"]);
                    }
                    mq.Close();
                }
                catch { }
                finally { con.Close(); }                
                /// ------------------------------------
                /// Get the item data
                /// ------------------------------------
                string mesg = string.Empty;
                string IQuery = "SELECT * FROM mods_tree_items WHERE ModID=" + mid + " AND isLink=1";
                SqlCommand cmdI = new SqlCommand(IQuery, con);
                SqlDataReader iq;
                try
                {
                    con.Open();
                    iq = cmdI.ExecuteReader();
                    while (iq.Read())
                    {
                        string itemURL = Convert.ToString(iq["ItemURL"]);
                        string iURL = string.Empty;
                        string iType = Convert.ToString(iq["ItemType"]);
                        if (String.IsNullOrEmpty(itemURL))
                            iURL = "./intro.aspx";
                        else
                            iURL = itemURL;

                        if (iType == "1")
                        {
                            tabFunc += @"
                    case """ + Convert.ToString(iq["ItemCode"]) + @""": win.attachURL(""" + iURL + @""");
                        break;";
                        }
                        else if (iType == "2")
                        {
                            tabFunc += @"
                        case """ + Convert.ToString(iq["ItemCode"]) + @""": win.attachScheduler(null, ""week"");
                        scheduler.load(""" + iURL + @""", ""json"");
                        scheduler.config.readonly = true;
                        var dp = new dataProcessor(""" + iURL + @"&code=new"");
                        dp.init(scheduler);
                        break;";
                        }
                        else if (iType == "3")
                        {
                            string itemPostURL = Convert.ToString(iq["ItemPostURL"]);
                            tabFunc += @"
                        case """ + Convert.ToString(iq["ItemCode"]) + @""": var form = win.attachForm();
                        form.loadStruct(""" + iURL + @""");
                        form.attachEvent(""onButtonClick"", function(id){
				             if (id == ""Proceed"") {
					             form.send(""" + itemPostURL + @""", ""post"", function(xml, response){
						              win.attachURL(response);
					             });
				             }
			            });
                        break;";
                        }
                    }
                    iq.Close();
                }
                catch { }
                finally { con.Close(); }

                tabFunc += @"	}
		
		dba.pages[full_id] = win;
		win.extra = extra;
	}
	else dba.tabbar.cells(full_id).setActive();
	
};
";
            }
            return tabFunc;
        }

        /// <summary>
        /// Get creation_tab funtions from UserMods.
        /// </summary>
        private string getToolbarFunc(string um)
        {
            string toolbarFunc = string.Empty;
            um = Regex.Replace(um, "\\]$", "");
            um = Regex.Replace(um, "^S\\[", "");
            string[] tMods = um.Split(':');
            for (var i = 0; i < tMods.Length; i++)
            {
                string mid = string.Empty;
                /// ------------------------------------
                /// Get the mod ID
                /// ------------------------------------
                string MQuery = "SELECT * FROM mods WHERE ModCode='" + tMods[i] + "'";
                SqlCommand cmd = new SqlCommand(MQuery, con);
                SqlDataReader mq;
                try
                {
                    con.Open();
                    mq = cmd.ExecuteReader();
                    while (mq.Read())
                    {
                        mid = Convert.ToString(mq["ModID"]);
                    }
                    mq.Close();
                }
                catch { }
                finally { con.Close(); }
                /// ------------------------------------
                /// Get ItemCode string
                /// ------------------------------------
                string ItemCode = string.Empty;
                string ICQuery = "SELECT ItemCode FROM mods_tree_items WHERE ModID=" + mid;
                SqlCommand cmdic = new SqlCommand(ICQuery, con);
                SqlDataReader icq;
                try
                {
                    con.Open();
                    icq = cmdic.ExecuteReader();
                    while (icq.Read())
                    {
                        ItemCode += Convert.ToString(icq["ItemCode"]) + ":";
                    }
                    icq.Close();
                }
                catch { }
                finally { con.Close(); }
                ItemCode = Regex.Replace(ItemCode, ":$", "");
                /// ------------------------------------
                /// Generate each case with ItemCode string
                /// ------------------------------------
                string[] tbcase = ItemCode.Split(':');
                for (var t = 0; t < tbcase.Length; t++)
                {
                    string TBQuery = "SELECT * FROM mods_toolbar_items WHERE ItemCode='" + tbcase[t] + "'";
                    SqlCommand cmdtb = new SqlCommand(TBQuery, con);
                    SqlDataReader tbq;
                    try
                    {
                        con.Open();
                        tbq = cmdtb.ExecuteReader();
                        while (tbq.Read())
                        {
                            string itemURL = Convert.ToString(tbq["TBItemLink"]);
                            string iURL = string.Empty;
                            string iType = Convert.ToString(tbq["TBItemLinkType"]);
                            if (String.IsNullOrEmpty(itemURL))
                                iURL = "./intro.aspx";
                            else
                                iURL = itemURL;
                                                  
                            if (iType == "1")
                            {
                                toolbarFunc += @"
        case """ + Convert.ToString(tbq["TBItemCode"]) + @""":
                var win = dba.tabbar.cells(dba.tabbar.getActiveTab());
                win.attachURL(""" + Convert.ToString(tbq["TBItemLink"]) + @""");
                break;
";
                            }
                            else if (iType == "2")
                            {
                                toolbarFunc += @"
        case """ + Convert.ToString(tbq["TBItemCode"]) + @""":
                var win = dba.tabbar.cells(dba.tabbar.getActiveTab());
                win.attachScheduler(null, ""week"");
                scheduler.load(""" + iURL + @""", ""json"");
                scheduler.config.readonly = true;
                var dp = new dataProcessor(""" + iURL + @"&code=new"");
                dp.init(scheduler);
                break;
";
                            }

                        }
                        tbq.Close();
                    }
                    catch { }
                    finally { con.Close(); }
                }
            }
            

            return toolbarFunc;
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