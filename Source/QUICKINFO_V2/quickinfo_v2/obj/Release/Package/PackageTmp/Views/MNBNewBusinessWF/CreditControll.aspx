<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreditControll.aspx.cs" Inherits="quickinfo_v2.Views.MNBNewBusinessWF.CreditControll" %>

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
            width: 95%;
            height: 95%;
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
                            <div class="panel-title">Queued Proposals</div>

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
                        <div class="panel-heading">
                            <div class="panel-title">Missed Jobs</div>

                            <div class="panel-options">
                                <%--  <a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>--%>

                                <%--    <a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
                                --%>
                                <asp:Button ID="btnRefreshMissedUploadedProposal" runat="server" Text="Refresh" OnClick="btnRefreshMissedUploadedProposal_Click" CssClass="btn btn-success" />
                                <asp:Button ID="btnTakeJobMissedUploadedProposal" runat="server" Text="Take Job" OnClick="btnTakeJobMissedUploadedProposal_Click" CssClass="btn btn-info" />
                                <a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
                            </div>
                        </div>
                        <div class="panel-body">


                            <asp:Panel ID="pnlMissedJobs" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                                <asp:UpdatePanel ID="updPnlMissedProposals" runat="server" UpdateMode="Conditional">


                                    <ContentTemplate>

                                        <asp:GridView ID="grdMissedUploadedProposal" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                            Font-Size="8pt" Style="z-index: 102;" Width="100%" CssClass="SearchGridView" RowStyle-Wrap="false" OnRowDataBound="grdMissedUploadedProposal_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                                                ForeColor="White" Height="20px" />
                                        </asp:GridView>

                                        <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="tmrUpdateTimer_Tick" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnRefreshMissedUploadedProposal" />
                                        <asp:AsyncPostBackTrigger ControlID="btnTakeJobMissedUploadedProposal" />
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
                            <div class="panel-title">Uploaded Proposal Details</div>

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
                                    <asp:TextBox ID="txtEnteredBranchCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtJobType" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                                    <div class="form-group">
                                        <label for="txtJobTypeName" class="col-sm-3 control-label">Job Type</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtJobTypeName" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" id="divQuotationNo" runat="server">
                                        <label for="txtQuotationNo" class="col-sm-3 control-label">Quotation No</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtQuotationNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
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


                                    <div class="form-group" style="display: none">
                                        <label for="txtVehicleNo" class="col-sm-3 control-label">Vehicle No</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtVehicleNo" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none">
                                        <label for="txtEngineNo" class="col-sm-3 control-label">Engine No</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtEngineNo" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none">
                                        <label for="txtChassisNo" class="col-sm-3 control-label">Chassis No</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtChassisNo" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none">
                                        <label for="rdbtnCoverNoteAvailable" class="col-sm-3 control-label">Is Cover Note Available</label>
                                        <div class="col-sm-5">
                                            <asp:Panel ID="pnlchkIsCoverNoteAvailable" runat="server">
                                                <div id="label-switch" class="make-switch" data-on-label="Available" data-off-label="Not Available" style="width: 255px;">

                                                    <asp:CheckBox ID="chkIsCoverNoteAvailable" runat="server" Checked="true" />

                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>




                                    <div class="form-group" style="display: none">
                                        <label for="txtCoverNotePeriod" class="col-sm-3 control-label">Cover Note Period</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtCoverNotePeriod" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none">
                                        <label for="txtAddressLine1" class="col-sm-3 control-label">Address Line1</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtAddressLine1" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label for="txtAddressLine2" class="col-sm-3 control-label">Address Line2</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtAddressLine2" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none">
                                        <label for="txtAddressLine3" class="col-sm-3 control-label">Address Line3</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtAddressLine3" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>




                                    <div class="form-group" style="display: none">
                                        <label for="txtYearOfMake" class="col-sm-3 control-label">Year Of Make</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtYearOfMake" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group" style="display: none">
                                        <label for="txtFirstRegDate" class="col-sm-3 control-label">FirstRegDate</label>
                                        <div class="col-sm-5">


                                            <div class="input-group" style="width: 150px">

                                                <asp:TextBox ID="txtFirstRegDate" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <a href="#"><i class="entypo-calendar"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none">
                                        <label for="txtCubicCapacity" class="col-sm-3 control-label">Cubic Capacity</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtCubicCapacity" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtCancellationType" class="col-sm-3 control-label">Cancellation Type</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtCancellationType" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtEndorsementType" class="col-sm-3 control-label">Endorsement Type</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtEndorsementType" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="form-group">
                                        <label for="txtJobRemarks" class="col-sm-3 control-label">Job Remarks</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtJobRemarks" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="grdUploadedDocs" class="col-sm-3 control-label">Uploaded Documents</label>
                                        <div class="col-sm-5">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdUploadedDocs" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                                        Font-Size="8pt" Style="z-index: 102;" Width="250px" OnRowDataBound="grdUploadedDocs_RowDataBound">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Document" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBtnViewDocument" runat="server">View</asp:LinkButton>
                                                                    <ajaxToolkit:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panl2" TargetControlID="lnkBtnViewDocument"
                                                                        CancelControlID="Button2" BackgroundCssClass="Background">
                                                                    </ajaxToolkit:ModalPopupExtender>
                                                                    <asp:Panel ID="Panl2" runat="server" CssClass="Popup" align="center" Style="display: none">

                                                                        <iframe style="width: 100%; height: 100%;" id="irm2" runat="server"></iframe>
                                                                        <br />
                                                                        <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-apps" />
                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                                                            ForeColor="White" Height="20px" />
                                                        <AlternatingRowStyle BackColor="WhiteSmoke" Font-Names="Tahoma" Font-Size="8pt" Height="15px" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>


                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJob" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJobMissedUploadedProposal" />
                                </Triggers>


                            </asp:UpdatePanel>


                        </div>

                    </div>

                </div>

                <div class="col-md-9">
                    <div class="panel panel-primary" data-collapsed="0">

                        <div class="panel-heading">
                            <div class="panel-title">
                                Scrutinizing Process
                            </div>

                        </div>

                        <div class="panel-body">

                            <div class="form-group">
                                <label for="ddlIssueType" class="col-sm-3 control-label">Issue Type</label>
                                <div class="col-sm-5">

                                    <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="ddlPolicyType" class="col-sm-3 control-label">Policy Type</label>
                                <div class="col-sm-5">

                                    <asp:DropDownList ID="ddlPolicyType" runat="server" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label for="chklPendings" class="col-sm-3 control-label">Pendings</label>
                                        <div class="col-sm-5">
                                            <asp:CheckBoxList ID="chklPendings" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJob" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJobMissedUploadedProposal" />
                                </Triggers>


                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label for="chklRejectReasons" class="col-sm-3 control-label">Reasons for Reject</label>
                                        <div class="col-sm-5">
                                            <asp:CheckBoxList ID="chklRejectReasons" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJob" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJobMissedUploadedProposal" />
                                </Triggers>
                            </asp:UpdatePanel>


                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label for="chklAttachedDocs" class="col-sm-3 control-label">Attached Documents</label>
                                        <div class="col-sm-5">
                                            <asp:CheckBoxList ID="chklAttachedDocs" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJob" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTakeJobMissedUploadedProposal" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <div class="form-group">
                                <label for="txtRemarks" class="col-sm-3 control-label">Remarks</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtScrutinizeRemarks" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>



                            <%--                            <div class="form-group">
                             

                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                   
                            </div>--%>

                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-6">

                                    <asp:Button ID="btnOpenQuotationDetails" runat="server" Text="Open Quotation Details"
                                        OnClick="btnOpenQuotationDetails_Click" CssClass="btn btn-apps" />
                                    <asp:Button ID="btnOpenQuotationCalculation" runat="server" Text="Open Quotation Calculation"
                                        OnClick="btnOpenQuotationCalculation_Click" CssClass="btn btn-apps" />
                                    <asp:Button ID="btnApproveToProcess" runat="server" Text="Approve To Process"
                                        OnClick="btnApproveToProcess_Click" CssClass="btn btn-apps" />
                                    <asp:Button ID="btnReject" runat="server" Text="Reject"
                                        OnClick="btnReject_Click" CssClass="btn btn-apps" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>




        </form>
    </div>


</asp:Content>
