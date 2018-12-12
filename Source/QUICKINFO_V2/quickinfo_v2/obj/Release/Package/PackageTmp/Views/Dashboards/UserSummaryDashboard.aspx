<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSummaryDashboard.aspx.cs" Inherits="quickinfo_v2.Views.Dashboards.UserSummaryDashboard" %>

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
    <link rel="stylesheet" href="../../Styles/DashboardStyle.css" type="text/css">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/bootstrap.min.css" id="style_resource7">


    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/bootstrap.min.js")%>'></script>

    <%--    <div runat="server">--%>

    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery-1.10.2.min.js") %>'></script>

    <script src='<%= Page.ResolveUrl("~/Styles/neon-x/assets/js/bootstrap-datepicker.js") %>'></script>



    <%-- </div>--%>
</head>
<body style="background-color: black; margin: 15px;margin-left:15px;">
    <form id="form1" runat="server" style="margin-left:15px;">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <asp:Timer ID="tmrUpdateTimer" runat="server" Interval="30000" OnTick="tmrUpdateTimer_Tick" />
        <div class="row">
            <div class="row">

                <label class="control-label" style="font-size: x-large; color: white; text-align: left;">SCRUTINCE UNIT</label>

            </div>
            <div class="row" style="margin: 5px;">
                <div class="col-sm-12">
                    <div class="userGroup">
                        <asp:ListView runat="server" ID="lstvScrutinizers" GroupItemCount="6" OnItemDataBound="lstvScrutinizers_ItemDataBound">
                            <LayoutTemplate>
                                <div style="">
                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                                </div>
                            </LayoutTemplate>
                            <GroupTemplate>
                                <div style="clear: both;">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div class="user">
                                    <div class="usertItem" id='prev<%# Eval("user_code")%>'>
                                        <asp:Image ID="userImg" runat="server" Height="150px" ImageUrl='<%# "../Common/ImageThumbnail.aspx?userCode="+ Eval("user_code") %>'
                                            Style="position: relative; top: 2px" />
                                        <br />
                                        <b>
                                            <asp:TextBox ID="txtUserCode" runat="server" Text='<%# Eval("user_code")%>' Visible="false" />
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("user_name")%>' ForeColor="White" />
                                        </b>
                                        <br />
                                        <asp:Literal ID="ltrlSummary" runat="server"></asp:Literal>


                                    </div>

                                </div>
                            </ItemTemplate>
                            <ItemSeparatorTemplate>
                                <div class="itemSeparator">
                                </div>
                            </ItemSeparatorTemplate>
                            <GroupSeparatorTemplate>
                                <div class="groupSeparator">
                                </div>
                            </GroupSeparatorTemplate>
                            <EmptyDataTemplate>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                </div>
            </div>




        </div>

        <div class="row">
            <div class="row">

                <label class="control-label" style="font-size: x-large; color: white; text-align: left;">PROCESS UNIT</label>

            </div>
            <div class="row" style="margin: 5px;">
                <div class="col-sm-12">
                    <div class="userGroup">
                        <asp:ListView runat="server" ID="lstvProcessers" GroupItemCount="6" OnItemDataBound="lstvProcessers_ItemDataBound">
                            <LayoutTemplate>
                                <div style="">
                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                                </div>
                            </LayoutTemplate>
                            <GroupTemplate>
                                <div style="clear: both;">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div class="user">
                                    <div class="usertItem" id='prev<%# Eval("user_code")%>'>
                                        <asp:Image ID="userImg" runat="server" Height="150px" ImageUrl='<%# "../Common/ImageThumbnail.aspx?userCode="+ Eval("user_code") %>'
                                            Style="position: relative; top: 2px" />
                                        <br />
                                        <b>
                                            <asp:TextBox ID="txtUserCode" runat="server" Text='<%# Eval("user_code")%>' Visible="false" />
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("user_name")%>' ForeColor="White" />
                                        </b>
                                        <br />
                                        <asp:Literal ID="ltrlSummary" runat="server"></asp:Literal>

                                    </div>

                                </div>
                            </ItemTemplate>
                            <ItemSeparatorTemplate>
                                <div class="itemSeparator">
                                </div>
                            </ItemSeparatorTemplate>
                            <GroupSeparatorTemplate>
                                <div class="groupSeparator">
                                </div>
                            </GroupSeparatorTemplate>
                            <EmptyDataTemplate>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                </div>
            </div>

        </div>


        <div class="row">
            <div class="row">

                <label class="control-label" style="font-size: x-large; color: white; text-align: left;">PROCESS VALIDATE</label>

            </div>
            <div class="row" style="margin: 5px;">
                <div class="col-sm-12">
                    <div class="userGroup">
                        <asp:ListView runat="server" ID="lstvProessValidators" GroupItemCount="6" OnItemDataBound="lstvProessValidators_ItemDataBound">
                            <LayoutTemplate>
                                <div style="">
                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                                </div>
                            </LayoutTemplate>
                            <GroupTemplate>
                                <div style="clear: both;">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div class="user">
                                    <div class="usertItem" id='prev<%# Eval("user_code")%>'>
                                        <asp:Image ID="userImg" runat="server" Height="150px" ImageUrl='<%# "../Common/ImageThumbnail.aspx?userCode="+ Eval("user_code") %>'
                                            Style="position: relative; top: 2px" />
                                        <br />
                                        <b>
                                            <asp:TextBox ID="txtUserCode" runat="server" Text='<%# Eval("user_code")%>' Visible="false" />
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("user_name")%>' ForeColor="White" />
                                        </b>
                                        <br />
                                        <asp:Literal ID="ltrlSummary" runat="server"></asp:Literal>

                                    </div>

                                </div>
                            </ItemTemplate>
                            <ItemSeparatorTemplate>
                                <div class="itemSeparator">
                                </div>
                            </ItemSeparatorTemplate>
                            <GroupSeparatorTemplate>
                                <div class="groupSeparator">
                                </div>
                            </GroupSeparatorTemplate>
                            <EmptyDataTemplate>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

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
