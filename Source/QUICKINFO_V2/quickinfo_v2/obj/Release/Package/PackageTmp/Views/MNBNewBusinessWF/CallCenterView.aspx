<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CallCenterView" Title="Call Center View" CodeBehind="CallCenterView.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <div class="col-md-4">
                    <div class="panel" style="height: 250px;">
                        <div class="form-group">
                            <label for="ddlBranch" class="col-sm-3 control-label">Branch</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ddlStatus" class="col-sm-3 control-label">Status</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtDateFrom" class="col-sm-3 control-label">Date From</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtDateFrom" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>

                            </div>


                        </div>
                        <div class="form-group">
                            <label for="txtDateTo" class="col-sm-3 control-label">To</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtDateTo" CssClass="form-control datepicker" data-format="dd/mm/yyyy" runat="server"></asp:TextBox>

                            </div>

                        </div>

                        <div class="form-group">
                            <label for="txtPolicyNo" class="col-sm-3 control-label">Policy No</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtPolicyNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtJobNo" class="col-sm-3 control-label">Job No</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtJobNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-sm-5">
                                <asp:Button ID="btnSearch" runat="server" Text="Search"
                                    CssClass="btn btn-apps" OnClick="btnSearch_Click" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="panel">
                        <asp:GridView ID="grdSearchResults" runat="server" CssClass="myGridStyle-small " OnRowDataBound="grdSearchResults_RowDataBound"
                            AllowPaging="True" OnPageIndexChanging="grdSearchResults_PageIndexChanging" PageSize="20" AllowSorting="true"
                            OnSorting="grdSearchResults_Sorting">
                            <Columns>
                                <asp:TemplateField HeaderText="" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnViewFollowup" runat="server">View Followup</asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panl2" TargetControlID="lnkBtnViewFollowup"
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
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True"
                                ForeColor="White"></FooterStyle>
                            <PagerStyle BackColor="#284775" ForeColor="White"
                                HorizontalAlign="Center"></PagerStyle>
                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                    </div>
                </div>



            </div>



        </form>
    </div>


</asp:Content>
