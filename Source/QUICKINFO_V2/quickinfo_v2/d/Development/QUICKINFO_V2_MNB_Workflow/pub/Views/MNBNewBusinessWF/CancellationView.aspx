<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CancellationView" Title="Cancellation" CodeBehind="CancellationView.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <script type="text/javascript">

        Dropzone.options.myDropzone = {
            url: "../Common/DocumentUploader.aspx?tid=<%=Request.QueryString["tid"]%>",
            // Prevents Dropzone from uploading dropped files immediately
            autoProcessQueue: false,

            init: function () {
                var submitButton = document.querySelector("#submit-all")
                myDropzone = this; // closure

                submitButton.addEventListener("click", function () {

                    myDropzone.processQueue(); // Tell Dropzone to process all queued files.
                });

                // You might want to show the submit button only when 
                // files are dropped here:
                this.on("addedfile", function () {
                    // Show submit button here and/or inform user to click it.
                });
                this.on("processing", function () {
                    this.options.autoProcessQueue = true;
                });


            }

        };

    </script>
    <script type="text/javascript">
        function Changed(textControl) {
            alert(textControl.value);
        }
    </script>

    <script type="text/javascript">
        function HideModalPopup() {
            var mpu = $find('ContentPlaceHolder1_mp2');
            mpu.hide();


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

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="panel panel-primary" data-collapsed="0" style="display: none">

                <div class="panel-heading">
                    <div class="panel-title">
                        Search Uploaded Proposal
                    </div>


                </div>

                <div class="panel-body">

                    <div class="form-group">
                        <label for="txtSearchQuotationNo" class="col-sm-3 control-label">Quotation No</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchQuotationNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="txtSearchVehicleNo" class="col-sm-3 control-label">Vehicle No</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchVehicleNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-5">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                Text="Search" CssClass="btn btn-apps" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click"
                                Text="Clear" CssClass="btn btn-apps" />

                        </div>
                    </div>





                </div>
            </div>

            <div class="panel panel-primary" data-collapsed="0">


                <div class="panel-body">



                    <div class="form-group">
                        <asp:Panel ID="pnlSearchGrid" runat="server" Height="213px" ScrollBars="Both" Style="z-index: 102;"
                            Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                            <asp:GridView ID="grdSearchResults" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                Font-Size="8pt" Style="z-index: 102;" Width="100%" OnSelectedIndexChanged="grdSearchResults_SelectedIndexChanged"
                                OnRowDataBound="grdSearchResults_RowDataBound" CssClass="SearchGridView" RowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                                    ForeColor="White" Height="20px" />
                                <AlternatingRowStyle BackColor="WhiteSmoke" Font-Names="Tahoma" Font-Size="8pt" Height="15px" />
                            </asp:GridView>
                        </asp:Panel>



                        <div class="panel panel-primary" data-collapsed="0">

                            <div class="panel-heading">
                                <div class="panel-title">
                                    Cancellation
                                </div>


                            </div>


                            <div class="panel-body">
                                <asp:TextBox ID="txtProposalUploadId" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtEnteredBranchCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                                <asp:TextBox ID="txtPolicyID" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"> </asp:TextBox>
                                <asp:TextBox ID="txtProposalNo" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"></asp:TextBox>
                                <asp:TextBox ID="txtSystemName" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"></asp:TextBox>



                                <div class="form-group" style="display: none;">
                                    <label for="txtJobNo" class="col-sm-3 control-label">Job No</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtJobNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" style="display: none;">
                                    <label for="txtDebitNo" class="col-sm-3 control-label">Debit No</label>
                                    <div class="col-sm-5">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDebitNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnSearchPolicyDebitNos" runat="server" CssClass="btn btn-primary" type="button" Text="Search" />
                                            </span>
                                        </div>
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlSearchTCSPolicyDebitNos" TargetControlID="btnSearchPolicyDebitNos"
                                            CancelControlID="btnCloseSearch2" BackgroundCssClass="Background">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlSearchTCSPolicyDebitNos" runat="server" CssClass="Popup" align="center" Style="display: none">
                                            <iframe style="width: 100%; height: 100%;" id="Iframe3" runat="server" src="SearchTCSPolicyDebit.aspx"></iframe>
                                            <br />
                                            <asp:Button ID="btnCloseSearch2" runat="server" Text="Close" CssClass="btn btn-primary" />


                                        </asp:Panel>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <label for="txtPolicyNo" class="col-sm-3 control-label">Policy No</label>
                                    <div class="col-sm-5">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtPolicyNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnSearchPolicyNos" runat="server" CssClass="btn btn-primary" type="button" Text="Search" />
                                            </span>
                                        </div>
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlSearchTCSPolicy" TargetControlID="btnSearchPolicyNos"
                                            CancelControlID="btnCloseSearch" BackgroundCssClass="Background">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlSearchTCSPolicy" runat="server" CssClass="Popup" align="center" Style="display: none">
                                            <iframe style="width: 800px; height: 600px;" id="Iframe2" runat="server" src="SearchTCSPolicy.aspx"></iframe>
                                            <br />
                                            <asp:Button ID="btnCloseSearch" runat="server" Text="Close" CssClass="btn btn-primary" />


                                        </asp:Panel>
                                    </div>
                                </div>




                                <div class="form-group">
                                    <label for="chkIsSameBranch" class="col-sm-3 control-label">Is Own Branch</label>
                                    <div class="col-sm-5">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <%--<div id="label-switch" class="make-switch" data-on-label="Yes" data-off-label="No" style="width: 255px;">--%>

                                            <asp:CheckBox ID="chkIsSameBranch" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="chkIsSameBranch_CheckedChanged" />

                                            <%--</div>--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="pnlBranch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlBranchList" runat="server">
                                            <div class="form-group">
                                                <label for="txtOtheBranchName" class="col-sm-3 control-label">Branch</label>
                                                <div class="col-sm-5">

                                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="chkIsSameBranch" />
                                    </Triggers>

                                </asp:UpdatePanel>





                                <div class="form-group">
                                    <label for="chkIsDocumentsAvailable" class="col-sm-3 control-label">Is Documents Available</label>
                                    <div class="col-sm-5">
                                        <asp:Panel ID="pnlchkIsDocumentsAvailable" runat="server">
                                            <div id="label-switch" class="make-switch" data-on-label="Available" data-off-label="Not Available" style="width: 255px;">

                                                <asp:CheckBox ID="chkIsDocumentsAvailable" runat="server" Checked="true" />

                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ddlCancellationType" class="col-sm-3 control-label">Cancellation Type</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlCancellationType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="form-group"  Style="display: none">
                                    <label for="lnkBtnCreditCalculator" class="col-sm-3 control-label">Credit Calculator</label>
                                    <div class="col-sm-5">
                                        <asp:LinkButton ID="lnkBtnCreditCalculator" runat="server" CssClass="btn btn-apps">Calculator</asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panl2" TargetControlID="lnkBtnCreditCalculator"
                                            CancelControlID="btnCloseCalculator" BackgroundCssClass="Background">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="Panl2" runat="server" CssClass="Popup" align="center" Style="display: none">
                                            <iframe style="width: 800px; height: 600px;" id="irm2" runat="server" src="../Common/CreditCalculate.aspx"></iframe>
                                            <br />
                                            <asp:Button ID="btnCloseCalculator" runat="server" Text="Close" CssClass="btn btn-apps" />
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="lnkBtnAttachment" class="col-sm-3 control-label">Documents</label>
                                    <div class="col-sm-5">
                                        <asp:LinkButton ID="lnkBtnAttachment" runat="server" CssClass="btn btn-apps">Attachment</asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="lnkBtnAttachment"
                                            CancelControlID="btnClose" BackgroundCssClass="Background">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                                            <iframe style="width: 800px; height: 600px;" id="irm1" runat="server" src="../Common/DocumentUploader.aspx"></iframe>
                                            <br />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-apps" />
                                        </asp:Panel>
                                    </div>
                                </div>






                                <div class="form-group">
                                    <label for="txtRemarks" class="col-sm-3 control-label">Remarks</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                </div>







                                <div class="form-group">
                                    <div class="col-sm-offset-3 col-sm-5">

                                        <asp:Button ID="btnAddNew" runat="server" Text="AddNew"
                                            OnClick="btnAddNew_Click" CssClass="btn btn-apps" />
                                        <%--  <asp:Button ID="btnAlter" runat="server" Text="Alter"
                                            OnClick="btnAlter_Click" CssClass="btn btn-apps" />--%>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                            CssClass="btn btn-apps" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                            CssClass="btn btn-apps" OnClick="btnCancel_Click" />


                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-5">

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lblError" runat="server" Font-Bold="False" Font-Names="Franklin Gothic Book"
                                                Font-Size="11pt" ForeColor="Red" Style="z-index: 101;" Width="1039px" Height="22px"></asp:Label>

                                            <asp:Label ID="lblMsg" runat="server" Font-Bold="False" Font-Names="Franklin Gothic Book"
                                                Font-Size="11pt" ForeColor="Red" Style="z-index: 101;" Width="1039px" Height="22px"></asp:Label>
                                            <asp:Timer ID="Timer1" runat="server" Enabled="False" OnTick="Timer1_Tick">
                                            </asp:Timer>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Timer1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
        </form>
    </div>


</asp:Content>
