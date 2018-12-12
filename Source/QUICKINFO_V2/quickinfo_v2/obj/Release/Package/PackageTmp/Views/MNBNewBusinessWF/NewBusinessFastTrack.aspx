<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="NewBusinessFastTrack" Title="Fast Track" CodeBehind="NewBusinessFastTrack.aspx.cs" %>


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

        

            <div class="panel panel-primary" data-collapsed="0">


                <div class="panel-body">



                    <div class="form-group">
                



                        <div class="panel panel-primary" data-collapsed="0">

                            <div class="panel-heading">
                                <div class="panel-title">
                                    Fast Track New Business
                                </div>


                            </div>

                            <div class="row">
                                <div class="col-md-8">

                                    <div class="panel-body">
                                        <asp:TextBox ID="txtProposalUploadId" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtEnteredBranchCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                                        <asp:TextBox ID="txtPolicyID" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"> </asp:TextBox>
                                        <asp:TextBox ID="txtProposalNo" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"></asp:TextBox>
                                        <asp:TextBox ID="txtSystemName" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"></asp:TextBox>



                                        <div class="form-group"  style="display: none;">
                                            <label for="txtJobNo" class="col-sm-4 control-label">Job No</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtJobNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtQuotationNo" class="col-sm-4 control-label">Quotation No</label>
                                            <div class="col-sm-5">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtQuotationNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button ID="btnSearchQuotationNos" runat="server" CssClass="btn btn-primary" type="button" Text="Search" />
                                                    </span>
                                                </div>
                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlSearchQuotation" TargetControlID="btnSearchQuotationNos"
                                                    CancelControlID="btnCloseSearch" BackgroundCssClass="Background">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <asp:Panel ID="pnlSearchQuotation" runat="server" CssClass="Popup" align="center" Style="display: none">
                                                    <iframe style="width: 800px; height: 600px;" id="Iframe2" runat="server" src="SearchQuotation.aspx"></iframe>
                                                    <br />
                                                    <asp:Button ID="btnCloseSearch" runat="server" Text="Close" CssClass="btn btn-primary" />


                                                </asp:Panel>
                                            </div>
                                        </div>
                                           <div class="form-group">
                                            <label for="txtTCSQuoteNo" class="col-sm-4 control-label">TCS Quote No</label>
                                            <div class="col-sm-5">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtTCSQuoteNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button ID="btnSearchTCSQuoteNo" runat="server" CssClass="btn btn-primary" type="button" Text="Search" />
                                                    </span>
                                                </div>
                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlSearchTCSQuotateNo" TargetControlID="btnSearchTCSQuoteNo"
                                                    CancelControlID="btnCloseSearch2" BackgroundCssClass="Background">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <asp:Panel ID="pnlSearchTCSQuotateNo" runat="server" CssClass="Popup" align="center" Style="display: none">
                                                    <iframe style="width: 800px; height: 600px;" id="Iframe1" runat="server" src="SearchTCSQuoteNo.aspx"></iframe>
                                                    <br />
                                                    <asp:Button ID="btnCloseSearch2" runat="server" Text="Close" CssClass="btn btn-primary" />


                                                </asp:Panel>
                                            </div>
                                        </div>


                                    





                                     
                               
                                        <div class="form-group">
                                            <label for="txtRemarks" class="col-sm-4 control-label">Remarks</label>
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



                                <div class="col-md-4">
                                    <br />
                                    <br />
                                    <br />



                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>


</asp:Content>
