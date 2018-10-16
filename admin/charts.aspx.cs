using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgriPanda.admin
{
    public partial class charts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request["code"])
            {
                case "dsvgph":
                    dsvgph("sigatoka", "?code=doshowgph&gphid=" + Request["gphid"], "");
                    break;
                //-------------------
                default:
                    dsvgph("sigatoka", "?code=doshowgph&gphid=" + Request["gphid"], "");
                    break;
            }
        }

        /// <summary>
        /// Label handler.
        /// </summary>
        private void JSTextFunc(string file, string str, string url)
        {
            LoadScript.Text = @"
<script src=""../lib/dhtmlx/dhtmlxchart.js"" type=""text/javascript""></script>

    <link rel=""STYLESHEET"" type=""text/css"" href=""../lib/dhtmlx/dhtmlxchart.css"">
    <style>
        .dhx_chart_title{
            padding-left:3px
        }
    </style>
<script>var myChart;window.onload=function(){myChart=new dhtmlXChart({view:""bar"",container:""chart_container"",value:""#tons#"",label:""#year#"",width:50,gradient:function(gradient){gradient.addColorStop(1.0,""#00FF22"");gradient.addColorStop(0.3,""#FFFF00"");gradient.addColorStop(0.0,""#FF0000"");},tooltip:{template:""#tons#""},xAxis:{title:""Sigatoka"",template:""#year#""},yAxis:{start:0,end:2,step:0.5,title:""DSV""}});myChart.load(""./admin/handlers/" + file + @".ashx" + str + @""");}</script>
 
<div id=""chart_container"" style=""width:900px;height:300px;border:1px solid #A4BED4;float:left;margin-right:20px""></div>";
        }

        /// <summary>
        /// DSV Graph.
        /// </summary>
        private void dsvgph(string file, string str, string url)
        {
            LoadScript.Text = @"
<script src=""../lib/dhtmlx/dhtmlxchart.js"" type=""text/javascript""></script>
<link rel=""STYLESHEET"" type=""text/css"" href=""../lib/dhtmlx/dhtmlxchart.css"">
<script src=""./handlers/" + file + @".ashx" + str + @"""></script>

<script>window.onload=function(){var chart1=new dhtmlXChart({view:""line"",container:""chart1"",value:""#DSV#"",item:{borderColor:""#1293f8"",color:""#ffffff""},line:{color:""#1293f8"",width:3},tooltip:{template:""#DSV#""},offset:0,xAxis:{template:""#Date#""},yAxis:{start:0,step:0.1,end:1,template:function(value){return value%0.5?"""":value}},padding:{left:35,bottom:20},origin:0,legend:{layout:""x"",width:75,align:""center"",valign:""bottom"",marker:{type:""round"",width:15},values:[{text:""DSV"",color:""#3399ff""},{text:""DSI"",color:""#66cc00""},{text:""EE"",color:""#ff0030""}]}});chart1.addSeries({value:""#DSI#"",item:{borderColor:""#66cc00"",color:""#ffffff""},line:{color:""#66cc00"",width:3},tooltip:{template:""#DSI#""}});chart1.addSeries({value:""#EE#"",item:{borderColor:""#ff0030"",color:""#ffffff""},line:{color:""#ff0030"",width:3},tooltip:{template:""#EE#""}});chart1.parse(sigatoka,""json"");}</script>
 
<table>
    <tr>
        <td><div id=""chart1"" style=""width:800px;height:350px;border:1px solid #A4BED4;""></div></td>
    </tr>
</table>";
        }
    }
}