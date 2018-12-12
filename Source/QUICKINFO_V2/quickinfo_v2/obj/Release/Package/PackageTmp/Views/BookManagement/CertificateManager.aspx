<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CertificateManager" Title="Certificate Manager" CodeBehind="CertificateManager.aspx.cs" %>


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
                        Search Certificates
                    </div>


                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="form-group">
                        <label for="txtSearchSerialNumber" class="col-sm-3 control-label">Serial Number</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchSerialNumber" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="ddlSearchStatus" class="col-sm-3 control-label">Status</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="txtSearchPolicyNumber" class="col-sm-3 control-label">Policy Number</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchPolicyNumber" CssClass="form-control" runat="server"></asp:TextBox>
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
                                    Manage Certificates
                                </div>


                            </div>
                            <div class="panel-body">

                                <asp:TextBox ID="txtSeqNo" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                                <div class="form-group">
                                    <label for="txtCertificateSerialNumber" class="col-sm-3 control-label">Certificate Serial Number</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtCertificateSerialNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ddlStatus" class="col-sm-3 control-label">Status</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>




                                <div class="form-group">
                                    <label for="txtPolicyNo" class="col-sm-3 control-label">Policy No.</label>

                                    <div class="col-sm-5">

                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtPolicyNo" CssClass="form-control" runat="server"></asp:TextBox>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlStatus" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="ddlProduct" class="col-sm-3 control-label">Product</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="txtPrintedDate" class="col-sm-3 control-label">Printed Date</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtPrintedDate" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>

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
