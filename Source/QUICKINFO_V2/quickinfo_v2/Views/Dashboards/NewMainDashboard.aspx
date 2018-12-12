<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMainDashboard.aspx.cs" Inherits="quickinfo_v2.Views.Dashboards.NewMainDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>ChartJS Tutorial for WebDesignTuts</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">


    <script src='<%= Page.ResolveUrl("~/Scripts/Chart.js") %>'></script>
    <link rel="stylesheet" href="~/Styles/newDash/normalize.css">
    <link rel="stylesheet" href="~/Styles/newDash/main.css">

    <script>
        function myFunction() {
            myPie.segment[3].value = 145;
            myPie.update();

        }
        var pieData = [
				{
				    value: 300,
				    color: "#F7464A",
				    highlight: "#FF5A5E",
				    label: "Red"
				},
				{
				    value: 50,
				    color: "#46BFBD",
				    highlight: "#5AD3D1",
				    label: "Green"
				},
				{
				    value: 100,
				    color: "#FDB45C",
				    highlight: "#FFC870",
				    label: "Yellow"
				},
				{
				    value: 14,
				    color: "#949EB1",
				    highlight: "#A8B3C5",
				    label: "Pink"
				},
				{
				    value: 40,
				    color: "#949FB1",
				    highlight: "#A8B3C5",
				    label: "Grey"
				},
				{
				    value: 120,
				    color: "#4D5360",
				    highlight: "#616774",
				    label: "Dark Grey"
				}

        ];





        var pieOptions = {



            segmentShowStroke: true,
            animateScale: true,


            onAnimationComplete: function () {
                this.showTooltip(this.segments, true);
            },

            tooltipEvents: [],

            showTooltips: true,





        }



        window.onload = function () {
            var ctx = document.getElementById("chart-area").getContext("2d");
            window.myPie = new Chart(ctx).Pie(pieData, pieOptions);

            document.getElementById("legendDiv").innerHTML = myPie.generateLegend();


        };


	</script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <header>
                <div class="container clearfix">
                    <h1>Overview <span>July 8-12, 2013</span></h1>
                </div>
            </header>
            <div class="container clearfix">
                <div class="third widget line">
                    <div class="chart-legend">
                        <h3>Shipments per Day</h3>
                    </div>
                    <div id="canvas-container">
                        <canvas id="chart-area" height="269" style="width: 100%;height: 100%;" />
                    </div>
                    <div id="legendDiv"></div>
                </div>
                <div class="third widget line">
                    <div class="chart-legend">
                        <h3>Shipments per Day</h3>
                    </div>
                    <div class="canvas-container">
                        <canvas id="shipments"></canvas>
                    </div>
                </div>

            </div>
            <div class="push"></div>
        </div>
        <%--   <footer>
            <div class="container">
                &copy; 2013 SuprAwsm &mdash; <a href="">Contact support</a>
            </div>
        </footer>--%>
    </form>
</body>
</html>
