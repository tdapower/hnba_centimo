<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobStatusDashboard.aspx.cs" Inherits="quickinfo_v2.Views.Dashboards.JobStatusDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/TableStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        setInterval("settime()", 1000);

        function settime() {
            var dateTime = new Date();
            var hour = dateTime.getHours();
            var minute = dateTime.getMinutes();
            var second = dateTime.getSeconds();

            if (minute < 10)
                minute = "0" + minute;

            if (second < 10)
                second = "0" + second;

            var time = "" + hour + ":" + minute + ":" + second;

            document.getElementById("clock").value = time;
        }
    </script>

</head>
<body style="background-color: black; margin: 15px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <asp:Timer ID="tmrUpdateTimer" runat="server" Interval="10000" OnTick="tmrUpdateTimer_Tick" />
        <div class="row">
            <div class="row" style="margin: 5px;">
                <div class="col-sm-12">
                    <div style="position: fixed; bottom: 0; right: 0; width: 900px; border: none;">
                        <input type="text" id="clock" style="border: none; font-size: 200px; color: white; font-weight: bolder; background-color: transparent; color: white; opacity: 0.6; text-shadow: -1px -1px 0 #000,    5px -1px 0 #000,    -1px 1px 0 #000,    1px 1px 0 #000;" />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltrlGeneratedTable" runat="server"></asp:Literal>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <%--           <table class="responstable">

                        <tr>
                            <th>Main driver</th>
                            <th data-th="Driver details"><span>First name</span></th>
                            <th>Surname</th>
                            <th>Date of birth</th>
                            <th>Relationship</th>
                        </tr>

                        <tr>
                            <td>
                                <input type="radio" /></td>
                            <td>Steve</td>
                            <td>Foo</td>
                            <td>01/01/1978</td>
                            <td>Policyholder</td>
                        </tr>

                        <tr>
                            <td>
                                <input type="radio" /></td>
                            <td>Steffie</td>
                            <td>Foo</td>
                            <td>01/01/1978</td>
                            <td>Spouse</td>
                        </tr>

                        <tr>
                            <td>
                                <input type="radio" /></td>
                            <td>Stan</td>
                            <td>Foo</td>
                            <td>01/01/1994</td>
                            <td>Son</td>
                        </tr>

                        <tr>
                            <td>
                                <input type="radio" /></td>
                            <td>Stella</td>
                            <td>Foo</td>
                            <td>01/01/1992</td>
                            <td>Daughter</td>
                        </tr>

                    </table>--%>
                </div>
            </div>



        </div>




    </form>




    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/jvectormap/jquery-jvectormap-1.2.2.css" id="Link1">
</body>
</html>
