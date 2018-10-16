<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AgriPanda.carto._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carto maps | Agripanda</title>
    <link href="../css/default.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
    <style>
      html, body, #map {
        height: 100%;
        padding: 0;
        margin: 0;
      }
    </style>
    <link rel="stylesheet" href="http://libs.cartocdn.com/cartodb.js/v3/3.15/themes/css/cartodb.css" />
</head>
<body>
    <asp:Literal id="LoadError" runat="server" />
    <div id="map"></div>

    <!-- include cartodb.js library -->
    <script src="http://libs.cartocdn.com/cartodb.js/v3/3.15/cartodb.js"></script>

    <asp:Literal id="LoadContent" runat="server" />
    
    
</body>
</html>
