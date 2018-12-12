<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActiveJobsDashboard.aspx.cs" Inherits="quickinfo_v2.Views.Dashboards.ActiveJobsDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/neon.css" id="style_resource_5">

    <title></title>
</head>
<body>
   <iframe style="width: 100%; height: 1000px; left: 0; top: 0" id="irm2" runat="server" src="back.aspx" z-index="0"></iframe>
 
    <div class="panel-body" style="left: 0; top: 0; position: absolute; width: 100%;">

        <form role="form" class="form-horizontal form-groups-bordered" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <asp:Timer ID="tmrUpdateTimer" runat="server" Interval="10000" OnTick="tmrUpdateTimer_Tick" />
            <div class="row">
                <div class="col-md-4">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 400px; background-color: transparent; overflow: auto">



                        <div class="panel-body">

                            <asp:Panel ID="pnlNotTakenScrutinizing" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlNotTakenScrutinizing" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdNotTakenScrutinizing" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Style="z-index: 102;" Width="100%" CssClass="DashGridView" RowStyle-Wrap="false" OnRowDataBound="grdNotTakenScrutinizing_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
                <div class="col-md-4">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 400px; background-color: transparent; overflow: auto">

                        <div class="panel-body">

                            <asp:Panel ID="pnlNotTakenProcessing" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlNotTakenProcessing" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdNotTakenProcessing" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Style="z-index: 102;" Width="100%" CssClass="DashGridView" RowStyle-Wrap="false" OnRowDataBound="grdNotTakenProcessing_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
                <div class="col-md-4">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 400px; background-color: transparent; overflow: auto">

                        <div class="panel-body">

                            <asp:Panel ID="Panel1" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlNotTakenValidating" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdNotTakenValidating" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Style="z-index: 102;" Width="100%" CssClass="DashGridView" RowStyle-Wrap="false" OnRowDataBound="grdNotTakenValidating_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-4">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 400px; background-color: transparent; overflow: auto">



                        <div class="panel-body">

                            <asp:Panel ID="pnlTakenScrutinizing" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlTakenScrutinizing" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdTakenScrutinizing" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Style="z-index: 102;" Width="100%" CssClass="DashGridView" RowStyle-Wrap="false" OnRowDataBound="grdTakenScrutinizing_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
                <div class="col-md-4">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 400px; background-color: transparent; overflow: auto">

                        <div class="panel-body">

                            <asp:Panel ID="pnlTakenProcessing" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlTakenProcessing" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdTakenProcessing" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Style="z-index: 102;" Width="100%" CssClass="DashGridView" RowStyle-Wrap="false" OnRowDataBound="grdTakenProcessing_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
                <div class="col-md-4">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 400px; background-color: transparent; overflow: auto">

                        <div class="panel-body">

                            <asp:Panel ID="Panel2" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlTakenValidating" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdTakenValidating" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Style="z-index: 102;" Width="100%" CssClass="DashGridView" RowStyle-Wrap="false" OnRowDataBound="grdTakenValidating_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>

</body>
</html>
