<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DailyJobSummary.aspx.cs" Inherits="quickinfo_v2.Views.Reports.DailyJobSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/neon.css" id="style_resource_5">

    <div class="panel-body">

        <form role="form" class="form-horizontal form-groups-bordered" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>


            <div class="row">
                <div class="col-md-6">
                    <div class="panel-body">

                        <div class="form-group">
                            <label for="txtFromDate" class="col-sm-3 control-label">From Date</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtFromDate" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>
                         
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtToDate" class="col-sm-3 control-label">To Date</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtToDate" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>
                                <asp:Button ID="btnView" runat="server" Text="View"
                                    CssClass="btn btn-apps" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-6">
                    <div class="panel">
                        <asp:Literal ID="ltrlData" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>


        </form>
    </div>

</asp:Content>
