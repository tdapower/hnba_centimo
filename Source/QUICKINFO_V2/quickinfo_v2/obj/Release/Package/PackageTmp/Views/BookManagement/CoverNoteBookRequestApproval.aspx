<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CoverNoteBookRequestApproval" Title="Cover Note Book Request Approval" CodeBehind="CoverNoteBookRequestApproval.aspx.cs" %>


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
            <div class="row">
                <div class="col-md-3">

                    <div class="panel panel-primary" data-collapsed="0" style="height: 600px">

                        <!-- panel head -->
                        <div class="panel-heading">
                            <div class="panel-title">Pending Requests</div>


                            <div class="panel-options">
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-sm-5">
                                </div>
                            </div>




                            <asp:Panel ID="pnl1" runat="server" Height="100%" ScrollBars="Auto" Style="z-index: 102;"
                                Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">

                                <asp:GridView ID="grdPendingApprovals" runat="server" BackColor="White" BorderColor="#CCCCCC" OnRowDataBound="grdPendingApprovals_RowDataBound"
                                    OnSelectedIndexChanged="grdPendingApprovals_SelectedIndexChanged" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="8pt" Style="z-index: 102;" Width="100%" CssClass="SearchGridView" RowStyle-Wrap="false">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                                        ForeColor="White" Height="20px" />
                                </asp:GridView>


                            </asp:Panel>
                        </div>




                    </div>
                </div>
                <div class="col-md-9">

                    <div class="panel panel-primary" data-collapsed="0">

                        <div class="panel-heading">
                            <div class="panel-title">
                                Search
                            </div>


                        </div>

                        <div class="panel-body">

                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <div class="form-group">
                                <label for="txtSearchRequestedDate" class="col-sm-3 control-label">Requested Date</label>

                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtSearchRequestedDate" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtSearchRequestedNo" class="col-sm-3 control-label">Request No.</label>

                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtSearchRequestedNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-group">
                                <label for="ddlSearchBranch" class="col-sm-3 control-label">Branch</label>

                                <div class="col-sm-5">
                                    <asp:DropDownList ID="ddlSearchBranch" runat="server" CssClass="form-control">
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
                                            Request Cover Note Book
                                        </div>


                                    </div>
                                    <div class="panel-body">

                                        <asp:TextBox ID="txtBookReqSeqNo" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                                        <div class="form-group">
                                            <label for="txtReqNo" class="col-sm-3 control-label">Request No.</label>


                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtReqNo" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtReqDate" class="col-sm-3 control-label">Date</label>


                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtReqDate" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="ddlBranch" class="col-sm-3 control-label">Branch</label>

                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <label for="txtExistingCoverNoteBookNumber" class="col-sm-3 control-label">Existing Cover Note Book Number</label>

                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtExistingCoverNoteBookNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <label for="txtExistingCoverNoteBookNumberStart" class="col-sm-3 control-label">Existing Book's C/N Sequence No.</label>

                                            <div class="col-sm-6">
                                                <div class="row">

                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtExistingCoverNoteBookNumberStart" CssClass="form-control" runat="server" class="toCalculate"></asp:TextBox>
                                                    </div>
                                                    <label for="txtExistingCoverNoteBookNumberEnd" class="col-sm-1 control-label">To</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtExistingCoverNoteBookNumberEnd" CssClass="form-control" runat="server" class="toCalculate"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <label for="txtReason" class="col-sm-3 control-label">Reason(s) To Request C/N Book(s)</label>

                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtReason" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label for="rbtnYes" class="col-sm-3 control-label">Cover Note Book(s) In Hand</label>

                                            <div class="col-sm-5">

                                                <asp:RadioButton ID="rbtnYes" runat="server" CssClass="radio" GroupName="grp1" Text="Yes" />

                                                <asp:RadioButton ID="rbtnNo" runat="server" CssClass="radio" GroupName="grp1" Text="No" />

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtInHandCoverNoteBookNumber" class="col-sm-3 control-label">Existing Cover Note Book Number</label>

                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtInHandCoverNoteBookNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <label for="txtInHandCoverNoteBookNumberStart" class="col-sm-3 control-label">Existing Book's C/N Sequence No.</label>

                                            <div class="col-sm-6">
                                                <div class="row">

                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtInHandCoverNoteBookNumberStart" CssClass="form-control" runat="server" class="toCalculate"></asp:TextBox>
                                                    </div>
                                                    <label for="txtInHandCoverNoteBookNumberEnd" class="col-sm-1 control-label">To</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtInHandCoverNoteBookNumberEnd" CssClass="form-control" runat="server" class="toCalculate"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtAppRejRemarks" class="col-sm-3 control-label">Approve / Reject Remarks</label>

                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtAppRejRemarks" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <div class="col-sm-offset-3 col-sm-5">

                                                <asp:Button ID="btnApproveBM" runat="server" Text="Approve" OnClick="btnApproveBM_Click"
                                                    CssClass="btn btn-apps" Visible="false" />
                                                <asp:Button ID="btnRejectBM" runat="server" Text="Reject" OnClick="btnRejectBM_Click"
                                                    CssClass="btn btn-apps"  Visible="false"/>


                                 <%--               <asp:Button ID="btnApproveZM" runat="server" Text="Approve" OnClick="btnApproveZM_Click"
                                                    CssClass="btn btn-apps"  Visible="false"/>
                                                <asp:Button ID="btnRejectZM" runat="server" Text="Reject" OnClick="btnRejectZM_Click"
                                                    CssClass="btn btn-apps"  Visible="false"/>--%>

                                                <asp:Button ID="btnApproveHDO" runat="server" Text="Approve" OnClick="btnApproveHDO_Click"
                                                    CssClass="btn btn-apps"  Visible="false"/>
                                                <asp:Button ID="btnRejectHDO" runat="server" Text="Reject" OnClick="btnRejectHDO_Click"
                                                    CssClass="btn btn-apps"  Visible="false"/>


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
                </div>
            </div>
        </form>
    </div>


</asp:Content>
