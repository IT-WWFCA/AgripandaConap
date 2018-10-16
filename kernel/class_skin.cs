/**
 * WWF honduras
 * AgriPanda v1.1.0
 * Skin Handler
 * Last Updated: $Date: 2012-08-14 11:40:40 -0600 (Tue, 14 Aug 2012) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2012 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Skin
 * @link		http://www.wwf-mar.org
 * @since		1.1.0
 * @version		$Revision: 0001 $
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AgriPanda.kernel
{
    public class class_skin
    {
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// FORM ELEMENTS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generates the start of a form.
        /// </summary>
        public string start_form(string[,] hiddens)
        {
            string form = "<form action='" + HttpContext.Current.Request.ServerVariables["URL"] + "' id='formID flight' class='formular' method='post' name='theMainForm' enctype='multipart/form-data'>\n";

            /*foreach (string val in hiddens) {
                form += "<input type='hidden' name='" + val + "' value='" + val + "'>\n";
			}*/

            for (int i = 0; i <= hiddens.GetUpperBound(0); i++)
            {
                string s1 = hiddens[i, 0]; // First column...
                string s2 = hiddens[i, 1]; // Second column...
                form += "<input type='hidden' name='" + s1 + "' value='" + s2 + "'>\n";
            }

            return form;
        }

        /// <summary>
        /// Generates a hidden tag.
        /// </summary>
        public string form_hidden(string[,] hiddens)
        {
            string form = "";

            for (int i = 0; i <= hiddens.GetUpperBound(0); i++)
            {
                string s1 = hiddens[i, 0]; // First column...
                string s2 = hiddens[i, 1]; // Second column...
                form += "<input type='hidden' name='" + s1 + "' value='" + s2 + "'>\n";
            }

            return form;
        }

        /// <summary>
        /// Generates the end of a form.
        /// </summary>
        public string end_form(string text, string extra)
        {
            string html = "";

            if (text != "")
            {
                html += "<div class='pformstrip'><div class=\"buttonsefe\"><button type='submit' class=\"positive\" accesskey='s'><img src='" + Resources.config.ImagesDIR + "check.png' border='0' alt='' valign='absmiddle'>" + text + "</button>" + extra + "</div></div>\n";
            }

            html += "</form>";

            return html;
        }

        /// <summary>
        /// Generates the end of page with button.
        /// </summary>
        public string end_button(string text, string link)
        {
            string html = "";

            if (text != "")
            {
                html += "<div class='pformstrip'><div class=\"buttonsefe\"><a href='" + link + "' class=\"positive\"><img src='" + Resources.config.ImagesDIR + "check.png' border='0' alt='' valign='absmiddle'>" + text + "</a></div></div>\n";
            }

            return html;
        }

        /// <summary>
        /// Generates a form input tag.
        /// </summary>
        public string form_input(string name, string value, string type, string size, string validator)
        {
            return "<input type='" + type + "' name='" + name + "' value=\"" + value + "\" size='" + size + "' class='textinput' " + validator + ">";
        }

        /// <summary>
        /// Generates an upload tag.
        /// </summary>
        public string form_upload(string name = "FILE_UPLOAD")
        {
            return "<input type='file' name='" + name + "' size='30' class='textinput'>";
        }

        /// <summary>
        /// Generates an upload tag with multiple enabled.
        /// </summary>
        public string form_upload_multiple(string name = "FILE_UPLOAD")
        {
            return "<input type='file' name='" + name + "' size='30' class='textinput' multiple>";
        }

        /// <summary>
        /// Generates a textarea tag.
        /// </summary>
        public string form_textarea(string name, string value, string style, string id, string cols = "60", string rows = "5", string wrap = "soft")
        {
            if (id != "")
            {
                id = "id=" + id + "";
            }

            if (style != "")
            {
                style = "style=" + style + "";
            }

            return "<textarea name='" + name + "' cols='" + cols + "' rows='" + rows + "' wrap='" + wrap + "' " + id + " " + style + " class='multitext'>" + value + "</textarea>";
        }

        /// <summary>
        /// Generates a dropdown tag.
        /// </summary>
        public string form_dropdown(string name, string[,] list, string default_val)
        {
            string html = "<select name='" + name + "' class='dropdown'>\n";

            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                string s1 = list[i, 0]; // First column...
                string s2 = list[i, 1]; // Second column...

                string selected = "";

                if ((default_val != "") && (s1 == default_val))
                {
                    selected = " selected";
                }

                html += "<option value='" + s1 + "'" + selected + ">" + s2 + "</option>\n";
            }

            html += "</select>\n\n";

            return html;
        }

        /// <summary>
        /// Generates the start of a dropdown tag.
        /// </summary>
        public string form_dropdown_start(string name)
        {
            return "<select name='" + name + "' class='dropdown'>\n";
        }

        /// <summary>
        /// Generates the start of a bootstrap-multiselect tag.
        /// </summary>
        public string form_bootstrap_multiselect_start(string name, string id)
        {
            string html = @"<script type=""text/javascript"">
    $(document).ready(function() {
        $('#" + id + @"').multiselect({
                    includeSelectAllOption: true
        });
            });
</script>";
            html += "<select name=\"" + name + "\" id=\"" + id + "\" multiple=\"multiple\">\n";
            return html;
        }

        /// <summary>
        /// Generates the end of a dropdown tag.
        /// </summary>
        public string form_dropdown_end()
        {
            return "</select>\n\n";
        }

        /// <summary>
        /// Generates the options of a dropdown tag.
        /// </summary>
        public string form_dropdown_opts(string[,] list, string default_val)
        {
            string html = "";
            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                string s1 = list[i, 0]; // First column...
                string s2 = list[i, 1]; // Second column...

                string selected = "";

                if ((!String.IsNullOrEmpty(default_val)) && (s1 == default_val))
                {
                    selected = " selected";
                }

                html += "<option value='" + s1 + "'" + selected + ">" + s2 + "</option>\n";
            }

            return html;
        }

        /// <summary>
        /// Generates the start of a dropdown tag.
        /// </summary>
        public string form_multiselect_start(string name)
        {
            string html = @"<script type='text/javascript'>
                                $(function () {
                                $.localise('ui-multiselect', { /*language: 'en',*/path: '/Lib/multiselect/js/locale/' });
                                $('.multiselect').multiselect();
                                $('#switcher').themeswitcher();
                                });
	                        </script>";
            html += "\n<select id='" + name + "' name='" + name + "[]' class='multiselect' multiple='multiple'>\n";
            return html;
        }

        /// <summary>
        /// Generates a multiselect form tag.
        /// </summary>
        public string form_multiselect(string name, string[] list, string[] default_val, string size = "5")
        {
            string html = "<select name='" + name + "' class='dropdown' multiple='multiple' size='" + size + "'>\n";
            foreach (string val in list)
            {

                string selected = "";

                if (default_val.Length > 0)
                {
                    /*if ( Array.BinarySearch( val[0], default_val ) )
                    {
                        selected = " selected=\"selected\"";
                    }*/
                }

                html += "<option value='" + val[0] + "'" + selected + ">" + val[1] + "</option>\n";
            }

            html += "</select>\n\n";

            return html;
        }

        /// <summary>
        /// Generates a yes/no form tag.
        /// </summary>
        public string form_yes_no(string name, int default_val)
        {
            string yes = "Yes <input type='radio' name='" + name + "' value='1' id='green'>";
            string no = "<input type='radio' name='" + name + "' value='0' id='red'> No";

            if (default_val == 1)
            {

                yes = "Yes <input type='radio' name='" + name + "' value='1' checked id='green'>";
            }
            else
            {
                no = "<input type='radio' name='" + name + "' value='0' checked id='red'> No";
            }


            return "&nbsp;&nbsp;" + yes + "&nbsp;&nbsp;&nbsp;" + no;
        }

        /// <summary>
        /// Generates radio buttons tag.
        /// </summary>
        public string form_radio(string name, string[] list, string default_val, string[] ids, Dictionary<string, string> text, string js)
        {
            string html = "";
            for (var i = 0; i < list.Length; i++)
            {
                string selected = "";

                if (Convert.ToString(list[i]) == default_val)
                {
                    selected = " checked";
                }
                html += "<input type='radio' name='" + name + "' value='" + list[i] + "' id='" + ids[i] + "'" + selected + " " + js + "> " + text[list[i]] + "</br>\n";
            }
            return html;
        }

        /// <summary>
        /// Generates a checkbox tag.
        /// </summary>
        public string form_checkbox(string name, int checked_val = 0, int val = 1)
        {
            if (checked_val == 1)
            {

                return "<input type='checkbox' name='" + name + "' value='" + val + "' checked='checked'>";
            }
            else
            {
                return "<input type='checkbox' name='" + name + "' value='" + val + "'>";
            }
        }

        /// <summary>
        /// Generates a form password tag.
        /// </summary>
        public string form_password(string name, string value, string type, string size, string validator)
        {
            string html = @"<script type='text/javascript'>
                        $(function () {
                        $('.password').pstrength();
                        });
                     </script>";
            html += "<input type='" + type + "' name='" + name + "' value=\"" + value + "\" size='" + size + "' class='password textinput " + validator + "'>";
            return html;
        }

        /// <summary>
        /// Generates a form datepicker tag.
        /// </summary>
        public string form_datepicker(string name, string value, string type, string size, string validator)
        {
            string html = @"<script type='text/javascript'>
	                            $(function() {
		                        $('#datepicker').datepicker();
	                            });
	                       </script>";
            html += "<input type='" + type + "' name='" + name + "' value=\"" + value + "\" size='" + size + "' class='textinput " + validator + "' id='datepicker'>";
            return html;
        }

        /// <summary>
        /// Generates a form datepicker inline mode.
        /// </summary>
        public string form_datepicker_inline(string name, string value, string type, string size, string language)
        {
            ///-----------------------------------------
            /// Set language
            ///-----------------------------------------
            string lang = string.Empty;
            if (language == "en-US")
                lang = "en";
            else if (language == "es-HN")
                lang = "es";
            ///-----------------------------------------
            string html = @"<input type='" + type + "' name='" + name + "' value='" + value + "' size='" + size + @"' class='textinput' id='datetimepicker'>
 <script>
    jQuery.datetimepicker.setLocale('" + lang + @"');
    jQuery('#datetimepicker').datetimepicker({
     i18n:{
      es:{
       months:[
        'Enero','Febrero','Marzo','Abril',
        'Mayo','Junio','Julio','Augosto',
        'Septiembre','Octubre','Noviembre','Diciembre',
       ],
       dayOfWeek:[
        ""Dom"", ""Lun"", ""Mar"", ""Mie"", 
        ""Jue"", ""Vie"", ""Sab"",
       ]
      }
     },
     timepicker:false,
     inline:true,
     format:'d.m.Y'
    });
</script>";
            return html;
        }

        /// <summary>
        /// Generates a form periodpicker tag.
        /// </summary>
        public string form_periodpicker(string namea, string nameb, string valuea, string valueb, string type, string size, string language, string lockdates)
        {
            ///-----------------------------------------
            /// Set initial start and end dates variables
            ///-----------------------------------------
            DateTime datetoday = DateTime.Today;
            DateTime dateini = datetoday.AddDays(-3);
            string format = "dd.MM.yyyy";
            string stdate = string.Empty;
            string enddate = string.Empty;
            if (String.IsNullOrEmpty(valuea))
                stdate = "";
            else
            {
                DateTime dateva = DateTime.Parse(valuea);
                stdate = dateva.ToString(format);
            }
            if (String.IsNullOrEmpty(valueb))
                enddate = "";
            else
            {
                DateTime datevb = DateTime.Parse(valueb);
                enddate = datevb.ToString(format);
            }
            ///-----------------------------------------
            /// Set min and max dates
            ///-----------------------------------------
            string _minDate = string.Empty;
            string _maxDate = string.Empty;
            if (String.IsNullOrEmpty(lockdates))
            {
                _minDate = datetoday.AddDays(-7).ToString(format);
                _maxDate = datetoday.AddDays(7).ToString(format);
            }
            else
            {
                string[] ldates = lockdates.Split(',');
                string[] midate = ldates[0].Split('-');
                _minDate = midate[2] + "." + midate[1] + "." + midate[0];
                string[] madate = ldates[1].Split('-');
                _maxDate = madate[2] + "." + madate[1] + "." + madate[0];
            }
            ///-----------------------------------------
            /// Set language
            ///-----------------------------------------
            string lang = string.Empty;
            if (language == "en-US")
                lang = "en";
            else if (language == "es-HN")
                lang = "es";
            ///-----------------------------------------
            string html = @"<input name=""" + namea + @""" type=""text"" value=""" + stdate + @""" id=""periodpickerstart""/>
    <input name=""" + nameb + @""" type=""text"" value=""" + enddate + @""" id=""periodpickerend""/>
    <script>
	$('#periodpickerstart').periodpicker({    
        end: '#periodpickerend',
        resizeButton: false,
        minDate: '" + _minDate + @"',
        maxDate: '" + _maxDate + @"',
        lang: '" + lang + @"',
        i18n: {
         'es' : {
           'Select week #' : 'Seleccione semana #',
           'Select period' : 'Seleccione periodo',
           'Open fullscreen' : 'Abrir ventana completa',
           'Close' : 'Cerrar',
           'OK' : 'Listo',
           'Choose period' : 'Seleccione periodo'
          }
        }
    });
	///$('#periodpickerstart').periodpicker('value', ['" + dateini.ToString(format) + @"', '" + datetoday.ToString(format) + @"']);
    </script>";
            return html;
        }

        /// <summary>
        /// Generates a form colorpicker tag.
        /// </summary>
        public string form_colorpicker(string name, string value, string type, string size, string validator)
        {
            string cval = string.Empty;
            ///-----------------------------------------
            /// Set up reverse colors dictionary
            ///-----------------------------------------
            Dictionary<string, string> colors = new Dictionary<string, string>();
            colors.Add("green", "rgb(0, 255, 0)");
            colors.Add("yellow", "rgb(255, 255, 0)");
            colors.Add("orange", "rgb(255, 168, 0)");
            colors.Add("red", "rgb(255, 0, 0)");
            ///-----------------------------------------
            if (String.IsNullOrEmpty(value))
                cval = "rgb(255, 255, 255)";
            else
                cval = colors[value];
            ///-----------------------------------------
            string html = "<input name=\"" + name + "\" type='text' id=\"showPaletteOnly\"/>";
            html += @"<script type='text/javascript'>//<![CDATA[

function printColor(color) {
   var text = ""You chose... "" + color.toHexString();    
   $("".label"").text(text);

        }

$(""#showPaletteOnly"").spectrum({
        color: """ + cval + @""",    
    showPaletteOnly: true,
    change: function(color) {
                printColor(color);
            },
    palette: [
        [""rgb(0, 255, 0)"", ""rgb(255, 255, 0)"", ""rgb(255, 168, 0)"", ""rgb(255, 0, 0)""]
    ]
});

//]]> 

</script>";
            return html;
        }

        /// <summary>
        /// Generates a form post text/url selector.
        /// </summary>
        public string form_post_selector(string name, string value, string urlname, string urlvalue, string validator)
        {
            string html = "<div class=\"post box\">" + Resources.posts.postdesc + "</br>" + form_textarea(name, value, "", "mytinymce", "50") + "</br>" + form_upload_multiple() + "</div>";
            html += "<div class=\"url box\">" + Resources.posts.urldesc + "</br>" + form_input(urlname, urlvalue, "input", "40", "") + "</div>";
            return html;
        }

        /// <summary>
        /// Generates a form double date input tag.
        /// </summary>
        public string form_doubledateinput(string name, string nameb, string value, string valueb, string type, string label, string labelb)
        {
            string html = "<div id='flight'>";
            html += "<label>" + label + "<br /><input type='" + type + "' name='" + name + "' value='" + value + "'></label>";
            html += "<label>" + labelb + "<br /><input type='" + type + "' name='" + nameb + "' value='" + valueb + "'></label>";
            html += "</div>";
            html += @"
                            <script>
                            $(':date').dateinput({ trigger: true, format: 'mm/dd/yyyy', offset: [340, 48] })
                            // use the same callback for two different events. possible with bind
                            $(':date').bind('onShow onHide', function()  {
	                            $(this).parent().toggleClass('active'); 
                            });
                            // when first date input is changed
                            $(':date:first').data('dateinput').change(function() {
	                            // we use it's value for the seconds input min option
	                            $(':date:last').data('dateinput').setMin(this.getValue(), true);
                            });
                            </script>
                        ";
            return html;
        }

        /// <summary>
        /// Generates a form input tag for autocomplete search.
        /// </summary>
        public string form_inputsq(string name, string value, string type, string size)
        {
            return "<input type='" + type + "' name='" + name + "' value=\"" + value + "\" size='" + size + "' id='query' class='textinput'>";
        }

        /// <summary>
        /// Generates a form input tag for autocomplete search.
        /// </summary>
        public string form_input_slider(string name, string value, string size)
        {
            string html = "<div class='layout-slider'>";
            html += "<input id='Slider2' type='slider' name='" + name + "' value=\"" + value + "\" size='" + size + "'>";
            html += "</div>";
            html += @"<script type='text/javascript' charset='utf-8'>
                         jQuery('#Slider2').slider({ from: 0, to: 100, scale: [0, '|', 25, '|', '50', '|', 75, '|', 100], step: 0.5, round: 1, dimension: '&nbsp;%', skin: 'plastic' });
                      </script>
                     ";
            return html;

        }

        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// SCREEN ELEMENTS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generates a sub title of the page.
        /// </summary>
        public string add_subtitle(string title, string id = "subtitle")
        {
            return "<div id='" + id + "'>" + title + "</div>\n";
        }

        /// <summary>
        /// Generates an action button.
        /// </summary>
        public string action_button(string[,] button, string delbot)
        {
            int cando = 0;
            string html = "<center>";
            for (int i = 0; i <= button.GetUpperBound(0); i++)
            {
                string s1 = button[i, 0];
                string s2 = button[i, 1];
                string s3 = button[i, 2];
                string s4 = button[i, 4];
                cando = Convert.ToInt32(button[i, 3]);
                html += cando == 1 ? "&nbsp;&nbsp;<a href='" + s1 + "' id='" + s4 + "' title='" + s3 + "'><img src='" + Resources.config.ImagesDIR + s2 + ".png' border='0' alt='" + s3 + "' title='" + s3 + "' /></a>" : "";
            }
            html += cando == 1 ? delbot : "&nbsp;&nbsp;<div class='disabledact'>" + Resources.global.blockedactions + "</div>";
            html += "</center>";

            return html;
        }

        /// <summary>
        /// Generates the start of a table page.
        /// </summary>
        public string start_table(string title, string desc, string[,] td_header)
        {
            string html = "";
            string width = "";
            string text_align = "";

            if (title != "")
            {
                html += "<div class='tableborder'>\n<div class='maintitle'>" + title + "</div>\n";

                if (desc != "")
                {
                    html += "<div class='subtitle'>" + desc + "</div>\n";
                }
            }

            html += "<table width='100%' cellspacing='0' cellpadding='5' align='center' border='0'>\n";

            html += "<tr>\n";

            for (int i = 0; i <= td_header.GetUpperBound(0); i++)
            {
                string s1 = td_header[i, 0]; // First column...
                string s2 = td_header[i, 1]; // Second column...
                string s3 = td_header[i, 2]; // Third column...

                if (s3 != "")
                {
                    text_align = "align='" + s3 + "'";
                }
                else
                {
                    text_align = "";
                }
                if (s2 != "")
                {
                    width = " width='" + s2 + "' " + text_align;
                }
                else
                {
                    width = "";
                }

                if (s1 != "{none}")
                {
                    html += "<td class='titlemedium'" + width + ">" + s1 + "</td>\n";
                }
            }

            html += "</tr>\n";

            return html;
        }

        /// <summary>
        /// Generates the start of a standalone page.
        /// </summary>
        public string standalone_start(string title, string desc)
        {
            string html = "";

            if (title != "")
            {
                html += "<div class='tableborder'>\n<div class='maintitle'>" + title + "</div>\n";

                if (desc != "")
                {
                    html += "<div class='subtitle'>" + desc + "</div>\n";
                }
            }

            return html;
        }

        /// <summary>
        /// Generates a standalone row.
        /// </summary>
        public string add_standalone_row(string text)
        {
            return "<div class='pformstrip'>" + text + "</div>\n";
        }

        /// <summary>
        /// Generates a row on table.
        /// </summary>
        public string add_td_row(string[] td_array, string td_css, string align = "middle")
        {
            string html = "";

            html += "<tr>\n";

            int count = td_array.Length;
            int td_colspan = count;

            for (int i = 0; i < count; i++)
            {

                string td_col;

                if ((i % 2) == 1)
                {
                    td_col = "tdrow2";
                }
                else
                {
                    td_col = "tdrow1";
                }

                if (td_css != "")
                {
                    td_col = td_css;
                }

                if (td_array[i].Length > 0)
                {
                    html += "<td class='" + td_col + "' valign='" + align + "'>" + td_array[i] + "</td>\n";
                }
                else
                {
                    html += "<td class='" + td_col + "' valign='" + align + "'>" + td_array[i] + "</td>\n";
                }
            }

            html += "</tr>\n";

            return html;
        }

        /// <summary>
        /// Generates end of table.
        /// </summary>
        public string end_table()
        {
            return "</table>\n<div class='pformfooter'>&nbsp;</div>\n</div>\n";
        }

        /// <summary>
        /// Generates end of table.
        /// </summary>
        public string end_table_pagination(string pages)
        {
            return "</table>\n<div class='pformfooterpag'>" + pages + "</div>\n</div>\n<br />\n";
        }

        /// <summary>
        /// Generates an error page.
        /// </summary>
        public string error_page(string error, string color)
        {
            string html = "";
            html += "<div class='error-message login-alert-" + color + "'>" + error + "</div>\n";
            return html;
        }

        /// <summary>
        /// Generates the previous page link.
        /// </summary>
        public string pagination_previous_link(string url)
        {
            return "<li><a href='" + url + "' title='" + Resources.global.tpl_previous + "'>&lt;</a></li>";
        }

        /// <summary>
        /// Generates the next page link.
        /// </summary>
        public string pagination_next_link(string url)
        {
            return "<li><a href='" + url + "' title='" + Resources.global.tpl_next + "'>&gt;</a></li>";
        }

        /// <summary>
        /// Generates the current page link.
        /// </summary>
        public string pagination_current_page(int page)
        {
            return "<li class=\"active\">" + page + "</li>";
        }

        /// <summary>
        /// Generates the start dots pagination.
        /// </summary>
        public string pagination_start_dots(string url)
        {
            return "<li><a href='" + url + "' title='" + Resources.global.tpl_gotofirst + "'>&laquo;</a></li>";
        }

        /// <summary>
        /// Generates the end dots pagination.
        /// </summary>
        public string pagination_end_dots(string url)
        {
            return "<li><a href='" + url + "' title='" + Resources.global.tpl_gotolast + "'>&raquo;</a></li>";
        }

        /// <summary>
        /// Generates the pagination page link.
        /// </summary>
        public string pagination_page_link(string url, int page)
        {
            return "<li><a href='" + url + "' title='" + page + "'>" + page + "</a></li>";
        }

        /// <summary>
        /// Generates the full pagination.
        /// </summary>
        public string pagination_compile(string previous_link, string start_dots, string pages, string end_dots, string next_link)
        {
            return "<ul class=\"pagination\">" + previous_link + start_dots + pages + end_dots + next_link + "</ul>";
        }

        /// <summary>
        /// Generates cookie trail start.
        /// </summary>
        public string cookie_trail_start()
        {
            return "<div class='cookietrail'><img src='" + Resources.config.ImagesDIR + "arrow_cookietrail.png' width='16' height='16' /> ";
        }

        /// <summary>
        /// Generates cookie trail end.
        /// </summary>
        public string cookie_trail_end()
        {
            return "</div>";
        }

        /// <summary>
        /// Generates tabs.
        /// </summary>
        public string create_tabs(string[] tabs, string[] content)
        {
            string html = "<ul class='tabs'>\n";
            if (tabs != null)
            {
                for (int i = 0; i < tabs.Length; i++)
                {
                    html += "<li><a href='#" + tabs[i] + "'>" + tabs[i] + "</a></li>\n";
                }
            }
            html += "</ul>\n";
            html += "<div class='panes'>\n";
            if (content != null)
            {
                for (int j = 0; j < content.Length; j++)
                {
                    html += "<div>\n";
                    html += content[j];
                    html += "</div>\n";
                }
            }
            html += @"</div>
                    <script>
                    // perform JavaScript after the document is scriptable.
                    $(function() {
	                    // setup ul.tabs to work as tabs for each div directly under div.panes
	                    $('ul.tabs').tabs('div.panes > div');
                    });
                    </script>
                    ";
            return html;
        }

        /// <summary>
        /// Generates script for tooltip with animation.
        /// </summary>
        public string tooltip_js()
        {
            string html = @"
            <script>
            $('#bots img[title]').tooltip({effect: 'bouncy'});
            </script>

            <script>
            // create custom animation algorithm for jQuery called 'bouncy'
            $.easing.bouncy = function (x, t, b, c, d) {
                var s = 1.70158;
                if ((t/=d/2) < 1) return c/2*(t*t*(((s*=(1.525))+1)*t - s)) + b;
                return c/2*((t-=2)*t*(((s*=(1.525))+1)*t + s) + 2) + b;
            }
            // create custom tooltip effect for jQuery Tooltip
            $.tools.tooltip.addEffect('bouncy',

	            // opening animation
	            function(done) {
		            this.getTip().animate({top: '+=15'}, 500, 'bouncy', done).show();
	            },

	            // closing animation
	            function(done) {
		            this.getTip().animate({top: '-=15'}, 500, 'bouncy', function()  {
			            $(this).hide();
			            done.call();
		            });
	            }
            );
            $('#bots img[title]').tooltip({effect: 'bouncy'});
            </script>
            ";

            return html;
        }

        /// <summary>
        /// Generates script for show and hide div.
        /// </summary>
        public string showhide_js()
        {
            string html = @"
            <script>
            $(document).ready(function() {
	           if($('div.trigger').length > 0) {
		            $('div.trigger').click(function() {
			            if ($(this).hasClass('open')) {
				            $(this).removeClass('open');
				            $(this).addClass('close');
				            $(this).next().slideDown(100);
				            return false;
			            } else {
				            $(this).removeClass('close');
				            $(this).addClass('open');
				            $(this).next().slideUp(100);
				            return false;
			            }			
		            });
	            }
	
            });
            </script>
            ";

            return html;
        }

        /// <summary>
        /// Generates script for show and hide div.
        /// </summary>
        public string show_map()
        {
            return "<iframe marginheight='0' hspace='0' width='720' height='400' marginwidth='0' noresize='noresize' id='mapview' allowTransparency='true' frameborder='0' height='13' scrolling='no' src='" + Resources.config.WebSiteURL + "maps.aspx' vspace='0'></iframe>";
        }

        /// <summary>
        /// Show post image gallery.
        /// </summary>
        public string show_post_image(string imgurl)
        {
            return "<br /><br /><img src='" + imgurl + "' />";
        }

        /// <summary>
        /// Show post image gallery.
        /// </summary>
        public string show_post_file(string fileurl, string fname, string fsize)
        {
            return "<br /><br /><a href='" + fileurl + "' target='_blank'>" + fname + "</a> - (" + fsize + " kb)";
        }
    }
}