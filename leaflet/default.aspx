<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AgriPanda.leaflet._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quick Start - Leaflet</title>

    <meta charset="utf-8" />
    <!-- <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY" async defer></script> -->
	<script src="https://maps.googleapis.com/maps/api/js" async defer></script>

	<meta name="viewport" content="width=device-width, initial-scale=1.0">

<!--  <link rel="stylesheet" href="../Leaflet/dist/leaflet.css" />
	<script type="text/javascript" src="../Leaflet/dist/leaflet-src.js"></script>-->

	<link rel="stylesheet" href="https://unpkg.com/leaflet@1.0.1/dist/leaflet.css" />
	<script src="https://unpkg.com/leaflet@1.0.1/dist/leaflet-src.js"></script>

	<script src='https://unpkg.com/leaflet.gridlayer.googlemutant@latest/Leaflet.GoogleMutant.js'></script>
	
	<link rel="shortcut icon" type="image/x-icon" href="docs/images/favicon.ico" />
    <style>
        body {
            padding: 0;
            margin: 0;
        }
        html, body, #map {
            height: 100vh;
            width: 100vw;
        }
    </style>

</head>
<body>
<div id="map" class="map"></div>
<script>

    var mapopts = {
        //      zoomSnap: 0.1
    };

    var map = L.map('map', mapopts).setView([15.433, -87.92753], 15);

    //var map = L.map('mapid').setView([15.433, -87.92753], 15);

    var roadMutant = L.gridLayer.googleMutant({
        maxZoom: 24,
        type: 'roadmap'
    }).addTo(map);

    var satMutant = L.gridLayer.googleMutant({
        maxZoom: 24,
        type: 'satellite'
    });

    var terrainMutant = L.gridLayer.googleMutant({
        maxZoom: 24,
        type: 'terrain'
    });

    var hybridMutant = L.gridLayer.googleMutant({
        maxZoom: 24,
        type: 'hybrid'
    });

    L.control.layers({
        Roadmap: roadMutant,
        Aerial: satMutant,
        Terrain: terrainMutant,
        Hybrid: hybridMutant,
    }, {}, {
            collapsed: false
        }).addTo(map);

    var rectangle = L.rectangle(madBounds).addTo(map);

    //    map.addLayer(osm);
    //    map.addLayer(kittens);
    //    map.addLayer(debug);

    var grid = L.gridLayer({
        attribution: 'Grid Layer',
        //      tileSize: L.point(150, 80),
        //      tileSize: tileSize
    });

    grid.createTile = function (coords) {
        var tile = L.DomUtil.create('div', 'tile-coords');
        tile.innerHTML = [coords.x, coords.y, coords.z].join(', ');

        return tile;
    };

    map.addLayer(grid);

</script>
</body>
</html>
