<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BookManager" Title="Book Manager" CodeBehind="BookManager.aspx.cs" %>


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
    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtCoverNoteSerialNumberStart").blur(function () {
                var noOfPages =<%=ConfigurationManager.AppSettings["CVR_NOTE_BOOK_NO_OF_PAGES"].ToString() %>;
                var val1 = $("#ContentPlaceHolder1_txtCoverNoteSerialNumberStart").val();
                var end = Number(val1) + (Number(noOfPages)-1);
                var val2 = $("#ContentPlaceHolder1_txtCoverNoteSerialNumberEnd").val(end);


            });



        });
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
                        <label for="ddlSearchBranch" class="col-sm-3 control-label">Branch</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchBranch" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="txtSearchChannel" class="col-sm-3 control-label">Channel</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchChannel" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="ddlSearchProduct" class="col-sm-3 control-label">Product</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchProduct" runat="server" CssClass="form-control">
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
                                    Manage Books
                                </div>


                            </div>
                            <div class="panel-body">

                                <asp:TextBox ID="txtBookMGRId" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>


                                <div class="form-group">
                                    <label for="ddlBookReqNo" class="col-sm-3 control-label">Cover Note Book Request No.</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlBookReqNo" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlBookReqNo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtBookNumber" class="col-sm-3 control-label">Book Number</label>


                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtBookNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtCoverNoteSerialNumberStart" class="col-sm-3 control-label">Cover Note Serial Number</label>

                                    <div class="col-sm-8">
                                        <div class="row">

                                            <label for="txtCoverNoteSerialNumberStart" class="col-sm-1 control-label">Start</label>
                                            <div class="col-sm-8">
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtCoverNoteSerialNumberStart" CssClass="form-control" runat="server" class="toCalculate"></asp:TextBox>
                                                </div>
                                                <label for="txtCoverNoteSerialNumberEnd" class="col-sm-1 control-label">End</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtCoverNoteSerialNumberEnd" CssClass="form-control" runat="server" class="toCalculate"></asp:TextBox>
                                                </div>
                                                <label for="txtNoOfCopies" class="col-sm-3  control-label" style="display: none;">No. of Copies</label>
                                                <div class="col-sm-3" style="display: none;">
                                                    <asp:TextBox ID="txtNoOfCopies" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <label for="ddlBranch" class="col-sm-3 control-label">Branch</label>

                                    <div class="col-sm-5">

                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                                                </asp:DropDownList>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlBookReqNo" />
                                            </Triggers>
                                        </asp:UpdatePanel>


                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ddlChannel" class="col-sm-3 control-label">Channel</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlChannel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtChannelCode" class="col-sm-3 control-label">Channel Code</label>

                                    <div class="col-sm-5">


                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:TextBox ID="txtChannelCode" CssClass="form-control" runat="server"></asp:TextBox>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlChannel" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtIssuedDate" class="col-sm-3 control-label">Issued Date</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtIssuedDate" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>

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
