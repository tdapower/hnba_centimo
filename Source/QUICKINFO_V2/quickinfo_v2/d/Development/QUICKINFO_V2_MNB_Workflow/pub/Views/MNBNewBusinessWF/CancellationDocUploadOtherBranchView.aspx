<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CancellationDocUploadOtherBranchView.aspx.cs"
    Inherits="quickinfo_v2.Views.MNBNewBusinessWF.CancellationDocUploadOtherBranchView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/neon.css" id="style_resource_5">
    <script type="text/javascript">
        function jsValidateNum(obj) {
            if (isNaN(obj.value)) {
                alert('Numeric Expected');
                obj.value = ''
                obj.focus()
            }
        }
    </script>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 850px;
            height: 650px;
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
                <div class="col-md-3">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 600px">

                        <!-- panel head -->
                        <div class="panel-heading">
                            <div class="panel-title">Jobs to Cancel</div>

                            <div class="panel-options">
                                <%--  <a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>--%>

                                <%--    <a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
                                --%>
                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CssClass="btn btn-success" />
                                <asp:Button ID="btnTakeJob" runat="server" Text="Take Job" OnClick="btnTakeJob_Click" CssClass="btn btn-info" />
                                <a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
                            </div>
                        </div>

                        <!-- panel body -->
                        <div class="panel-body">

                            <asp:Panel ID="pnlSearchGrid" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlProposals" runat="server" UpdateMode="Conditional">


                                    <ContentTemplate>

                                        <asp:GridView ID="grdUploadedProposal" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Font-Size="8pt" Style="z-index: 102;" Width="100%" CssClass="SearchGridView" RowStyle-Wrap="false" OnRowDataBound="grdUploadedProposal_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>

                                        <asp:Timer ID="tmrUpdateTimer" runat="server" Interval="10000" OnTick="tmrUpdateTimer_Tick" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
                                        <asp:AsyncPostBackTrigger ControlID="btnTakeJob" />
                                        <asp:AsyncPostBackTrigger ControlID="tmrUpdateTimer" EventName="Tick" />
                                    </Triggers>


                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>

                    </div>

                </div>

                <div class="col-md-9">

                    <div class="panel panel-invert" data-collapsed="0">
                        <!-- setting the attribute data-collapsed="1" will collapse the panel -->

                        <!-- panel head -->
                        <div class="panel-heading">
                            <div class="panel-title">Upload Docs for Cancellations of Other Branches</div>

                            <div class="panel-options">
                                <a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-2" class="bg"><i class="entypo-cog"></i></a>
                                <a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
                                <a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
                                <a href="#" data-rel="close"><i class="entypo-cancel"></i></a>
                            </div>
                        </div>

                        <!-- panel body -->
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtProposalUploadId" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtProposalUploadUserCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtUserBranch" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtEnteredBranchCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                                    <asp:TextBox ID="txtJobType" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                                    <div class="form-group">
                                        <label for="txtJobTypeName" class="col-sm-3 control-label">Job Type</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtJobTypeName" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="form-group" id="divJobNo" runat="server">
                                        <label for="txtJobNo" class="col-sm-3 control-label">Job No</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtJobNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group" id="divPolicyNo" runat="server">
                                        <label for="txtPolicyNo" class="col-sm-3 control-label">Policy No</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtPolicyNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                        </div>
                                    </div>



                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJob" />
                                </Triggers>


                            </asp:UpdatePanel>


                        </div>

                    </div>

                </div>

                <div class="col-md-9">
                    <div class="panel panel-primary" data-collapsed="0">

                        <div class="panel-heading">
                            <div class="panel-title">
                                Branch Document Upload 
                            </div>

                        </div>

                        <div class="panel-body">
                            <div class="form-group">
                                <label for="txtRemarks" class="col-sm-3 control-label">Remarks</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>
                            </div>

                            <%--                            <div class="form-group">
                             

                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                   
                            </div>--%>

                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-6">
                                    <asp:Button ID="btnDone" runat="server" Text="Done"
                                        OnClick="btnDone_Click" CssClass="btn btn-apps" />
                                </div>
                            </div>




                        </div>
                    </div>
                </div>

            </div>




        </form>
    </div>


</asp:Content>
