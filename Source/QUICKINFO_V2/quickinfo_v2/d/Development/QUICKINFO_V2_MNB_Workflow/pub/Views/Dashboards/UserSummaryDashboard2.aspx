<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSummaryDashboard2.aspx.cs" Inherits="quickinfo_v2.Views.Dashboards.UserSummaryDashboard2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery-1.10.2.min.js") %>'></script>

    <link rel="stylesheet" href="~/Styles/dashBoardsStyles/normalize.css" id="style_resource_2">
    <link rel="stylesheet" href="~/Styles/dashBoardsStyles/style.css" id="style_resource_3">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <%--        <asp:Timer ID="tmrUpdateTimer" runat="server" Interval="30000" OnTick="tmrUpdateTimer_Tick" />--%>

        <div class="my_container">
            <ul>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/people" />
                        <span>People</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/food" />
                        <span>Food</span>
                        <span>Food</span>
                        <span>Food</span>

                    </a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/cats" />
                        <span>Cats</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/transport" />
                        <span>Transport</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/abstract" />
                        <span>Abstract</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/nightlife" />
                        <span>Nightlife</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/city" />
                        <span>City</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/sports" />
                        <span>Sports</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/people" />
                        <span>People</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/food" />
                        <span>Food</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/cats" />
                        <span>Cats</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/transport" />
                        <span>Transport</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/abstract" />
                        <span>Abstract</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/nightlife" />
                        <span>Nightlife</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/city" />
                        <span>City</span></a>
                </li>
                <li class="block">
                    <a href="#">
                        <img src="http://lorempixel.com/400/400/sports" />
                        <span>Sports</span></a>
                </li>
            </ul>
        </div>
    </form>


    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery-ui/js/jquery-ui-1.10.3.minimal.min.js")%>' id="script_resource_2"></script>

</body>
</html>
