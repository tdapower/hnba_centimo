<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BookSerialNoManager" Title="Book Manager" CodeBehind="BookSerialNoManager.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function jsValidateNum(obj) {
            if (isNaN(obj.value)) {
                alert('Numeric Expected');
                obj.value = ''
                obj.focus()
            }
        }
    </script>


    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">


            <div class="panel panel-primary" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Search Books
                    </div>


                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="form-group">
                        <label for="txtSearchBookNumber" class="col-sm-3 control-label">Book Number</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchBookNumber" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="ddlSearchSerialNumber" class="col-sm-3 control-label">Serial Number</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchSerialNumber" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="ddlSearchStatus" class="col-sm-3 control-label">Product</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
                            </asp:DropDownList>
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
                        <asp:Panel ID="pnlUserGrid" runat="server" Height="213px" ScrollBars="Both" Style="z-index: 102;"
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
                                    Manage Books Serial Number
                                </div>


                            </div>
                            <div class="panel-body">

                                <asp:TextBox ID="txtSeqId" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>



                                <div class="form-group">
                                    <label for="ddlBookNumbers" class="col-sm-3 control-label">Book Number</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlBookNumbers" runat="server" CssClass="form-control" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlBookNumbers_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtBranch" class="col-sm-3 control-label">Branch</label>

                                    <div class="col-sm-5">


                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtBranch" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlBookNumbers" />
                                            </Triggers>
                                        </asp:UpdatePanel>


                                    </div>
                                </div>



                                <div class="form-group">
                                    <label for="ddlSerialNumber" class="col-sm-3 control-label">Serial Number</label>

                                    <div class="col-sm-5">

                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlSerialNumber" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSerialNumber_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlBookNumbers" />
                                            </Triggers>
                                        </asp:UpdatePanel>




                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="ddlStatus" class="col-sm-3 control-label">Status</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="lnkBtnAttachment" class="col-sm-3 control-label">Attachments</label>
                                    <div class="col-sm-5">
                                        <asp:LinkButton ID="lnkBtnAttachment" runat="server" CssClass="btn btn-apps" Width="150px">Upload Documents</asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="lnkBtnAttachment"
                                            CancelControlID="btnClose" BackgroundCssClass="Background">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                                            <iframe style="width: 800px; height: 600px;" id="irm1" runat="server" src="DocUpload/DocumentUploader.aspx"></iframe>
                                            <br />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                                        </asp:Panel>
                                        <asp:LinkButton ID="lnkBtnAttachedDocs" runat="server" CssClass="btn btn-apps" Width="180px">View Uploaded Documentss</asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="Background"
                                            CancelControlID="btnClose" PopupControlID="Panel2" TargetControlID="lnkBtnAttachedDocs">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="Panel2" runat="server" align="center" CssClass="Popup" Style="display: none; background-color: White;">
                                            <iframe id="Iframe1" runat="server" style="width: 800px; height: 580px;"></iframe>
                                            <br />
                                            <asp:Button ID="Button1" runat="server" CssClass="button" Text="Close" />
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtRemarks" class="col-sm-3 control-label">Remarks</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-offset-3 col-sm-5">

                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New"
                                            OnClick="btnAddNew_Click" CssClass="btn btn-apps" />
                                        <asp:Button ID="btnAlter" runat="server" Text="Alter"
                                            OnClick="btnAlter_Click" CssClass="btn btn-apps" />
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
                    </div>
                </div>
            </div>
        </form>
    </div>


</asp:Content>
