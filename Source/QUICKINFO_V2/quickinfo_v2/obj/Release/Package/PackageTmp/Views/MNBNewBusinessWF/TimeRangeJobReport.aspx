<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="TimeRangeJobReport" Title="Time Range Job Report" CodeBehind="TimeRangeJobReport.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .lbl {
            font-size: 12px;
            font-style: italic;
            font-weight: bold;
        }
    </style>

    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>

            <div class="row">
                <div class="col-md-4">
                    <div class="panel" style="height: 250px;">

                        <div class="form-group">
                            <label for="txtDateFrom" class="col-sm-3 control-label">Date From</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtDateFrom" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>

                            </div>


                        </div>
                        <div class="form-group">
                            <label for="txtDateTo" class="col-sm-3 control-label">To</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtDateTo" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Time From</label>

                            <div class="col-sm-3">
                                <input type="text" runat="server"  id="txtTimeFrom" class="form-control timepicker" data-template="dropdown" data-show-seconds="false" data-default-time="08:30 AM" data-show-meridian="false" data-minute-step="5" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label">Time To</label>

                            <div class="col-sm-3">
                                <input type="text"  runat="server"  id="txtTimeTo" class="form-control timepicker" data-template="dropdown" data-show-seconds="false" data-default-time="17:00 PM" data-show-meridian="false" data-minute-step="5" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-5">
                                <asp:Button ID="btnSearch" runat="server" Text="Search"
                                    CssClass="btn btn-apps" OnClick="btnSearch_Click" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="panel">
                        <asp:Literal ID="ltrlSummary" runat="server"></asp:Literal>

                        <asp:Literal ID="ltrlDetail" runat="server"></asp:Literal>
                    </div>
                </div>



            </div>



        </form>
    </div>


</asp:Content>
