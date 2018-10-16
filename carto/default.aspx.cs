using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgriPanda.kernel;

namespace AgriPanda.carto
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
        /// Visualization code.
        /// </summary>
        private string vizCode;
        /// <summary>
        /// Visualization latitude.
        /// </summary>
        private string vizLat;
        /// <summary>
        /// Visualization Longitude.
        /// </summary>
        private string vizLong;
        /// <summary>
        /// Visualization zoom.
        /// </summary>
        private int vizZoom = 0;
        /// <summary>
        /// Is leaflet active?
        /// </summary>
        private Boolean leaflet;
        /// <summary>
        /// Get global settings
        /// </summary>
        Dictionary<string, string> _globalSettings;
        #endregion

        # region Constructor
        /// <summary>
        /// Main function.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///---------------------------------------
            /// Get global settings
            ///--------------------------------------- 
            _globalSettings = std.get_global_settings(); // Get global settings
            ///---------------------------------------
            /// Get global perms for user
            ///---------------------------------------
            _ag = std.get_uperms(HttpContext.Current.User.Identity.Name, "A1");
            ///---------------------------------------
            /// Set user prefrered language
            ///--------------------------------------- 
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_ag["UserLanguage"]);

            if (String.IsNullOrEmpty(std.parse_incoming("vid")))
                LoadError.Text = skin.error_page(Resources.carto.ErrorNoVID, "yellow");
            else
            {
                ///---------------------------------------
                /// Get visualization info from DB
                ///--------------------------------------- 
                string MapQuery = "SELECT * FROM carto_visualizations WHERE VizID = " + HttpContext.Current.Request["vid"];
                SqlCommand cmd = new SqlCommand(MapQuery, con);
                SqlDataReader v;
                try
                {
                    con.Open();
                    v = cmd.ExecuteReader();
                    v.Read();
                    vizCode = Convert.ToString(v["VizCode"]);
                    vizLat = Convert.ToString(v["VizLat"]);
                    vizLong = Convert.ToString(v["VizLong"]);
                    vizZoom = Convert.ToInt32(v["VizZoom"]);
                    v.Close();
                }
                catch { }
                finally { con.Close(); }
                ///---------------------------------------
                /// Generate the script with VIZ info
                ///--------------------------------------- 
                leaflet = Convert.ToBoolean(_globalSettings["CartoLeaflet"]); // Get UserReg value
                if (leaflet == true)
                {
                    LoadContent.Text = getLeaflet();
                }
                else
                {
                    LoadContent.Text = getCartojs();
                }
                 
            } // else EOF
        }
        #endregion

        #region Methods
        /// <summary>
        /// Carto.js
        /// </summary>
        private string getCartojs()
        {
            string cjs = string.Empty;
            cjs = @"
<script>
      function main() {
          cartodb.createVis('map', 'https://wwfca.carto.com/api/v2/viz/" + vizCode + @"/viz.json', {
            shareable: false,
            title: true,
            description: true,
            search: false,
            tiles_loader: true,
            center_lat: " + vizLat + @",
            center_lon: " + vizLong + @",
            zoom: " + vizZoom + @"
        })
        .done(function(vis, layers) {
          // layer 0 is the base layer, layer 1 is cartodb layer
          // setInteraction is disabled by default
          layers[1].setInteraction(true);
          layers[1].on('featureOver', function(e, latlng, pos, data) {
            cartodb.log.log(e, latlng, pos, data);
          });
          // you can get the native map to work with it
          var map = vis.getNativeMap();
          // now, perform any operations you need
          // map.setZoom(3);
          // map.panTo([50.5, 30.5]);
        })
        .error(function(err) {
          console.log(err);
        });
      }
      window.onload = main;
    </script>
        ";
            return cjs;
        }

        /// <summary>
        /// leaflet.js
        /// </summary>
        private string getLeaflet()
        {
            string leaflet = string.Empty;
            leaflet = @"
<script>
      function main() {
        var map = new L.Map('map', {
          zoomControl: false,
          center: [" + vizLat + @", " + vizLong + @"],
          zoom: " + vizZoom + @"
        });
        L.tileLayer('http://tile.stamen.com/toner/{z}/{x}/{y}.png', {
          attribution: 'Stamen'
        }).addTo(map);
        cartodb.createLayer(map, 'https://wwfca.carto.com/api/v2/viz/" + vizCode + @"/viz.json')
            .addTo(map)
         .on('done', function(layer) {
          layer.setInteraction(true);
          layer.on('featureOver', function(e, latlng, pos, data) {
            cartodb.log.log(e, latlng, pos, data);
          });
          layer.on('error', function(err) {
            cartodb.log.log('error: ' + err);
          });
        }).on('error', function() {
          cartodb.log.log(""some error occurred"");
        });
      }
    // you could use $(window).load(main);
    window.onload = main;
    </script>
";
            return leaflet;
        }
        #endregion

    }
}