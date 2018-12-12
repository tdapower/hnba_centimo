<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainDashboard.aspx.cs" Inherits="quickinfo_v2.Views.Dashboards.MainDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/jquery-ui/css/no-theme/jquery-ui-1.10.3.custom.min.css" id="style_resource_1">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/entypo.css" id="style_resource_2">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/animation.css" id="style_resource_3">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/neon.css" id="style_resource_5">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/custom.css" id="style_resource_6">
    <link rel="stylesheet" href="../../Styles/gridViewStyle.css" type="text/css">

    <%--    <div runat="server">--%>

    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery-1.10.2.min.js") %>'></script>

    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/bootstrap-datepicker.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/Scripts/jquery.signalR.js") %>' type="text/javascript"></script>
    <script src='<%= Page.ResolveUrl("~/signalr/hubs") %>' type="text/javascript"></script>


    <%-- </div>--%>
</head>
<body style="background-color: black; margin: 15px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <asp:Timer ID="tmrUpdateTimer" runat="server" Interval="10000" OnTick="tmrUpdateTimer_Tick" />
        <div class="row">
            <div class="row" style="margin: 5px;">
                <div class="col-sm-6">

                    <label class="col-sm-6 control-label" style="font-size: x-large; color: white; text-align: left;">For the day</label>
                </div>
            </div>
            <div class="col-sm-2">

                <div class="tile-stats tile-plum">
                    <div class="icon"><i class="entypo-shuffle"></i></div>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltrlNotInitialised" runat="server"></asp:Literal>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <h3>Initial</h3>
                    <p></p>
                </div>

            </div>

            <div class="col-sm-2">

                <div class="tile-stats tile-purple">
                    <div class="icon"><i class="entypo-address"></i></div>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltrlScrutinize" runat="server"></asp:Literal>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <h3>Scrutinize</h3>
                    <p></p>
                </div>

            </div>
            <div class="col-sm-2">

                <div class="tile-stats tile-orange">
                    <div class="icon"><i class="entypo-users"></i></div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:Literal ID="ltrlProcess" runat="server"></asp:Literal>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <h3>Process</h3>
                    <p></p>
                </div>

            </div>
            <div class="col-sm-2">

                <div class="tile-stats tile-aqua">
                    <div class="icon"><i class="entypo-search"></i></div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltrlProcessValidate" runat="server"></asp:Literal>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <h3>Process Validate</h3>
                    <p></p>
                </div>

            </div>
            <div class="col-sm-2">

                <div class="tile-stats tile-red">
                    <div class="icon"><i class="entypo-thumbs-down"></i></div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltrlReject" runat="server"></asp:Literal>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="UpdatePanel3"
                        CancelControlID="btnClose" BackgroundCssClass="Background">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Style="display: none">
                        <iframe style="width: 800px; height: 600px;" id="Iframe1" runat="server" src="../Common/RejectedJobList.aspx"></iframe>
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-apps" />
                    </asp:Panel>
                    <h3>Reject</h3>
                    <p></p>
                </div>

            </div>

            <div class="col-sm-2">

                <div class="tile-stats tile-green">
                    <div class="icon"><i class="entypo-thumbs-up"></i></div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltrlComplete" runat="server"></asp:Literal>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <h3>Complete</h3>
                    <p></p>
                </div>

            </div>

            <div class="col-sm-2" id="btnUnknown">

                <div class="tile-stats tile-brown">
                    <div class="icon"><i class="entypo-help"></i></div>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltrlUnknown" runat="server"></asp:Literal>


                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>


                    </asp:UpdatePanel>
                    <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="UpdatePanel6"
                        CancelControlID="btnClose" BackgroundCssClass="Background">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                        <iframe style="width: 800px; height: 600px;" id="irm1" runat="server" src="../Common/UnknownDocs.aspx"></iframe>
                        <br />
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-apps" />
                    </asp:Panel>
                    <h3>
                        <asp:Label ID="lblUnknown" runat="server" Text="Unknown"></asp:Label>

                    </h3>
                    <p></p>


                </div>

            </div>
        </div>
        <div class="row">
            <div class="row" style="margin: 5px;">
                <div class="col-sm-6">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Chart ID="chartJobTypeCountSummery" runat="server" BackColor="Black" Height="500px" Width="700px">
                                <Series>
                                    <asp:Series Name="SerieschartJobTypeCountSummery" Legend="Legend1" LabelBorderDashStyle="Dot" LabelForeColor="White"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartAreachartJobTypeCountSummery" BackColor="Black" BorderDashStyle="Solid" ShadowColor="#999999"></asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BorderColor="Transparent" />
                            </asp:Chart>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-6">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Chart ID="chartJobTypeCount" runat="server" BackColor="Black" Height="500px" Width="700px">
                                <Series>
                                    <asp:Series Name="Series1" Legend="LegendchartJobTypeCount" LabelBorderDashStyle="Dot" LabelForeColor="White"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartAreachartJobTypeCount" BackColor="Black" BorderDashStyle="Solid" ShadowColor="#999999"></asp:ChartArea>
                                </ChartAreas>
                                <BorderSkin BorderColor="Transparent" />
                            </asp:Chart>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>


        </div>



        <div class="row">
            <div class="row" style="margin: 5px;">
                <div class="col-sm-6">

                    <label for="grdZonalSummaryForTheMonth" class="col-sm-6 control-label" style="font-size: x-large; color: white; text-align: left;">For the month</label>
                </div>
            </div>
            <div class="row" style="margin: 5px;">
                <div class="col-sm-6">

                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdZonalSummaryForTheMonth" runat="server" CssClass="myGridStyle" OnRowCreated="grdZonalSummaryForTheMonth_RowCreated"
                                OnRowDataBound="grdZonalSummaryForTheMonth_RowDataBound">
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>




    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/jvectormap/jquery-jvectormap-1.2.2.css" id="Link1">






    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/gsap/main-gsap.js")%>' id="script_resource_1"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery-ui/js/jquery-ui-1.10.3.minimal.min.js")%>' id="script_resource_2"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/bootstrap.min.js")%>' id="script_resource_3"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/joinable.js")%>' id="script_resource_4"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/resizeable.js")%>' id="script_resource_5"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-api.js")%>' id="script_resource_6"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jvectormap/jquery-jvectormap-1.2.2.min.js")%>' id="script_resource_7"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jvectormap/jquery-jvectormap-europe-merc-en.js")%>' id="script_resource_8"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery.sparkline.min.js")%>' id="script_resource_9"></script>

    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/raphael-min.js")%>' id="script_resource_12"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/morris.min.js")%>' id="script_resource_13"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/toastr.js")%>' id="script_resource_14"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-chat.js")%>' id="script_resource_15"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-custom.js")%>' id="script_resource_16"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-demo.js")%>' id="script_resource_17"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/fileinput.js")%>' id="script_resource_18"></script>

    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/bootstrap-switch.min.js")%>'></script>


    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery.responsivetable.min.js")%>' type="text/javascript"></script>


    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/rickshaw/vendor/d3.v3.js")%>' id="script_resource_19"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/rickshaw/rickshaw.min.js")%>' id="script_resource_20"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/raphael-min.js")%>' id="script_resource_21"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/morris.min.js")%>' id="script_resource_22"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery.peity.min.js")%>' id="script_resource_23"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-charts.js")%>' id="script_resource_24"></script>
    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery.sparkline.min.js")%>' id="script_resource_25"></script>
</body>
</html>
