<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="amcharts.aspx.cs" Inherits="AgriPanda.admin.amcharts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AgriPanda Charts V2</title>
    <link href="../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../lib/amcharts/canvg.js" type="text/javascript"></script>
    <script src="../lib/amcharts/rgbcolor.js" type="text/javascript"></script>
    <script src="../lib/amcharts/amcharts.js" type="text/javascript"></script>
    
    <asp:Literal id="LoadScript" runat="server" />

    <script type='text/javascript'>//<![CDATA[ 

        // Extend AmCharts object
        AmCharts.getExport = function (id) {
            var wrapper = document.getElementById(id);
            var svgs = wrapper.getElementsByTagName('svg');
            var options = {
                ignoreAnimation: true,
                ignoreMouse: true,
                ignoreClear: true,
                ignoreDimensions: true,
                offsetX: 0,
                offsetY: 0
            };
            var canvas = document.createElement('canvas');
            var context = canvas.getContext('2d');
            var counter = {
                height: 0,
                width: 0
            }

            // Nasty workaround until somebody figured out to support images
            function removeImages(svg) {
                var startStr = '<image';
                var stopStr = '</image>';
                var stopStrAlt = '/>';
                var start = svg.indexOf(startStr);
                var match = '';

                // Recursion
                if (start != -1) {
                    var stop = svg.slice(start, start + 1000).indexOf(stopStr);
                    if (stop != -1) {
                        svg = removeImages(svg.slice(0, start) + svg.slice(start + stop + stopStr.length, svg.length));
                    } else {
                        stop = svg.slice(start, start + 1000).indexOf(stopStrAlt);
                        if (stop != -1) {
                            svg = removeImages(svg.slice(0, start) + svg.slice(start + stop + stopStr.length, svg.length));
                        }
                    }
                }
                return svg;
            };

            // Setup canvas
            canvas.height = wrapper.offsetHeight;
            canvas.width = wrapper.offsetWidth;
            context.fillStyle = '#FFFFFF';
            context.fillRect(0, 0, canvas.width, canvas.height);

            // Add SVGs
            for (i = 0; i < svgs.length; i++) {
                var container = svgs[i].parentNode;
                var innerHTML = removeImages(container.innerHTML); // remove images from svg until its supported

                options.offsetY = counter.height;

                counter.height += container.offsetHeight;
                counter.width = container.offsetWidth;

                canvg(canvas, innerHTML, options);
            }

            // Return output data URL
            return canvas.toDataURL();
        }

        // Sample dump function
        function exportDat(id) {
            var output = AmCharts.getExport(id);
            var image = document.createElement('img');
            var link = document.createElement('a');

            // Add image data
            image.src = output;

            // Create download link with the image
            link.appendChild(image);
            link.href = output;
            link.download = 'ChartExport.png';
            link.title = 'Download the chart image';

            // Return output into doc
            document.getElementById('output').innerHTML = '';
            document.getElementById('output').appendChild(link);
        }
        //]]>  

</script>

</head>
<body>
    <asp:Literal ID="Start" runat="server" />
    <div id="chartdiv" style="width: 100%; height: 400px;"></div>
    <asp:Literal ID="Form1" runat="server" />
    <button id='exporter' onclick="exportDat('chartdiv');">Export</button>
        <div id='output'></div>
</body>
</html>
