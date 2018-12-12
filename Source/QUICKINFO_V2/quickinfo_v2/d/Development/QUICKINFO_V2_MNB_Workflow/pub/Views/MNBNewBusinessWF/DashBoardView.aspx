<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="DashBoardView" Title="User Dashboard" CodeBehind="DashBoardView.aspx.cs" %>





<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <asp:Timer ID="tmrUpdateTimer" runat="server" Interval="10000" OnTick="tmrUpdateTimer_Tick" />
            <div class="row">
                <div class="col-md-2">
                    <div class="panel" style="height: 250px; text-align: center;">
                        <asp:Image ID="UserPhoto" runat="server" Height="150px"
                            Style="position: relative; top: 2px" />
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="panel" style="height: 250px;">
                        <div class="row">

                            <label class="control-label" style="font-size: x-large; color: black; text-align: left;">Daily Summary</label>

                        </div>
                        <div class="row">
                            <div class="row" style="margin: 5px;">
                                <div class="col-sm-12">
                                    <div class="col-sm-3">

                                        <div class="tile-stats-small tile-plum">
                                            <div class="icon"><i class="entypo-shuffle"></i></div>
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Literal ID="ltrlNotInitialised" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h2>Initial</h2>
                                            <p></p>
                                        </div>

                                    </div>

                                    <div class="col-sm-3">

                                        <div class="tile-stats-small tile-purple">
                                            <div class="icon"><i class="entypo-address"></i></div>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Literal ID="ltrlScrutinize" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h2>Scrutinize</h2>
                                            <p></p>
                                        </div>

                                    </div>
                                    <div class="col-sm-3">

                                        <div class="tile-stats-small tile-orange">
                                            <div class="icon"><i class="entypo-users"></i></div>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                    <asp:Literal ID="ltrlProcess" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h2>Process</h2>
                                            <p></p>
                                        </div>

                                    </div>
                                    <div class="col-sm-3">

                                        <div class="tile-stats-small tile-aqua">
                                            <div class="icon"><i class="entypo-search"></i></div>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Literal ID="ltrlProcessValidate" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h2>Process Validate</h2>
                                            <p></p>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 5px;">
                                <div class="col-sm-12">
                                    <div class="col-sm-3">

                                        <div class="tile-stats-small tile-red">
                                            <div class="icon"><i class="entypo-thumbs-down"></i></div>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Literal ID="ltrlReject" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h2>Reject</h2>
                                            <p></p>
                                        </div>

                                    </div>

                                    <div class="col-sm-3">

                                        <div class="tile-stats-small tile-green">
                                            <div class="icon"><i class="entypo-thumbs-up"></i></div>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Literal ID="ltrlComplete" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h2>Complete</h2>
                                            <p></p>
                                        </div>

                                    </div>

                                    <div class="col-sm-3">

                                        <div class="tile-stats-small tile-brown">
                                            <div class="icon"><i class="entypo-help"></i></div>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Literal ID="ltrlUnknown" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h2>Unknown</h2>
                                            <p></p>
                                        </div>

                                    </div>
                                </div>
                            </div>





                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="row">

                        <label class="control-label" style="font-size: x-large; color: black; text-align: left;">Cumulative Count</label>

                    </div>
                    <div class="panel" style="height: 250px;">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Chart ID="chartJobTypeCountSummery" runat="server" BackColor="Black" Height="250px" Width="400px">
                                    <Series>
                                        <asp:Series Name="SerieschartJobTypeCountSummery" Legend="Legend2" LabelBorderDashStyle="Dot" LabelForeColor="White"></asp:Series>
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
                </div>


            </div>

            <div class="row">

                <div class="col-md-8">
                    <div class="row">

                        <asp:Label ID="lblBranchName" runat="server" class="control-label" Style="font-size: x-large; color: black; text-align: left;">Branch:</asp:Label>

                    </div>
                    <div class="panel">

                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="grdJobSummary" runat="server" CssClass="myGridStyle-small "
                                    OnRowDataBound="grdJobSummary_RowDataBound" AllowPaging="True"
                                    OnPageIndexChanging="grdJobSummary_PageIndexChanging" PageSize="20">


                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True"
                                        ForeColor="White"></FooterStyle>
                                    <PagerStyle BackColor="#284775" ForeColor="White"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                    <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="row">


                        <label class="control-label" style="font-size: x-large; color: black; text-align: left;">Ready to Print</label>

                    </div>
                    <div class="panel">

                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="grdCompletedJobSummary" runat="server" CssClass="myGridStyle-small "
                                    OnRowDataBound="grdCompletedJobSummary_RowDataBound" AllowPaging="True"
                                    OnPageIndexChanging="grdCompletedJobSummary_PageIndexChanging"
                                    OnSelectedIndexChanged="grdCompletedJobSummary_SelectedIndexChanged"
                                    PageSize="20">

                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True"
                                        ForeColor="White"></FooterStyle>
                                    <PagerStyle BackColor="#284775" ForeColor="White"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                    <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
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
    </div>


</asp:Content>
