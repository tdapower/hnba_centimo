<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CoverNoteBookRequest" Title="Cover Note Book Request" CodeBehind="CoverNoteBookRequest.aspx.cs" %>


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
    <script type="text/javascript">

        $(document).ready(function () {


            $('#ContentPlaceHolder1_rbtnYes').click(function () {
                $("#InHandCoverNoteBookNumber").show();
                $("#InHandCoverNoteBookNumberStart").show();
            });

            $('#ContentPlaceHolder1_rbtnNo').click(function () {
                $("#InHandCoverNoteBookNumber").hide();
                $("#InHandCoverNoteBookNumberStart").hide();
            });

        });

    </script>

    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtExistingCoverNoteBookNumberStart").blur(function () {
                var noOfPages =<%=ConfigurationManager.AppSettings["CVR_NOTE_BOOK_NO_OF_PAGES"].ToString() %>;
                var val1 = $("#ContentPlaceHolder1_txtExistingCoverNoteBookNumberStart").val();
                if(val1!=null){
                    var end = Number(val1) + (Number(noOfPages)-1);
                    var val2 = $("#ContentPlaceHolder1_txtExistingCoverNoteBookNumberEnd").val(end);
                }

            });

            $("#ContentPlaceHolder1_txtInHandCoverNoteBookNumberStart").blur(function () {
                var noOfPages =<%=ConfigurationManager.AppSettings["CVR_NOTE_BOOK_NO_OF_PAGES"].ToString() %>;
                var val1 = $("#ContentPlaceHolder1_txtInHandCoverNoteBookNumberStart").val();
                if(val1!=null){
                    var end = Number(val1) + (Number(noOfPages)-1);
                    var val2 = $("#ContentPlaceHolder1_txtInHandCoverNoteBookNumberEnd").val(end);
                }

            });

        });
    </script>

    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">


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

                                <div class="form-group" id="InHandCoverNoteBookNumber">
                                    <label for="txtInHandCoverNoteBookNumber" class="col-sm-3 control-label">In Hand Cover Note Book Number</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtInHandCoverNoteBookNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="form-group" id="InHandCoverNoteBookNumberStart">
                                    <label for="txtInHandCoverNoteBookNumberStart" class="col-sm-3 control-label">In Hand C/N Book Sequence No.</label>

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
                                    <div class="col-sm-offset-3 col-sm-5">

                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New"
                                            OnClick="btnAddNew_Click" CssClass="btn btn-apps" />
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

                            <%-- <div class="form-group">
                                <div class="col-sm-9">
                                    <div id="rootwizard" class="form-horizontal form-wizard">

                                        <div class="steps-progress">
                                            <div class="progress-indicator"></div>
                                        </div>
                                         <asp:Literal ID="ltrlRequestHistory" runat="server"></asp:Literal>

                                        <ul>
                                            <li class="completed">
                                                <a href="#tab1" class="popover-primary" data-toggle="popover" data-trigger="hover" data-placement="top" data-content="It's so simple to create a tooltop for my website!" data-original-title="Twitter Bootstrap Popover"><span>Galle</span>Branch</a>
                                            </li>
                                            <li class="completed">
                                                <a href="#tab2" data-toggle="tab"><span>2</span>Zone</a>
                                            </li>
                                            <li class="active">
                                                <a href="#tab3" data-toggle="tab"><span>3</span>Head Office</a>
                                            </li>
                                          
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            --%>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>


</asp:Content>
