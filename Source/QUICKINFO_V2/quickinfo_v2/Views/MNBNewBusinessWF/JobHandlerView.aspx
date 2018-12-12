<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="JobHandlerView" Title="Job Handler" CodeBehind="JobHandlerView.aspx.cs" %>


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
        function Changed(textControl) {
            alert(textControl.value);
        }
    </script>

    <div class="panel-body">


        <form role="form" class="form-horizontal form-groups-bordered" runat="server">

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="panel panel-primary" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Search Jobs
                    </div>


                </div>

                <div class="panel-body">




                    <div class="form-group">
                        <label for="txtSearchQuotationNo" class="col-sm-3 control-label">Job/Quotation No</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchQuotationNo" CssClass="form-control" runat="server"></asp:TextBox>
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


                    </div>
                </div>

                <div class="panel panel-primary" data-collapsed="0">

                    <div class="panel-heading">
                        <div class="panel-title">
                            Proposal Upload
                        </div>


                    </div>


                    <div class="panel-body">
                        <asp:TextBox ID="txtProposalUploadId" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtEnteredBranchCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                        <asp:TextBox ID="txtPolicyID" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"> </asp:TextBox>
                        <asp:TextBox ID="txtProposalNo" CssClass="form-control" runat="server" ForeColor="Transparent" BorderStyle="None" Height="1px"></asp:TextBox>


                        <div class="form-group">
                            <label for="txtJobNo" class="col-sm-3 control-label">Job No</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtJobNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>
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

                                <%--            <asp:Button ID="btnGivePriority" runat="server" Text="Give Priority"
                                    OnClick="btnGivePriority_Click" CssClass="btn btn-apps" />--%>
                                <asp:Button ID="btnRevertToScrutinizing" runat="server" Text="Revert To Scrutinizing"
                                    OnClick="btnRevertToScrutinizing_Click" CssClass="btn btn-apps" />


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
