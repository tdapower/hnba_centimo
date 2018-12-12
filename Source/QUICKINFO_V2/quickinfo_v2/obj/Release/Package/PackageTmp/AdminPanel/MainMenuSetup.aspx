<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MainMenuSetup" Title="Main Menu" CodeBehind="MainMenuSetup.aspx.cs" %>

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
                        Search Main Menus
                    </div>

                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

                    <div class="form-group">
                        <label for="txtSearchMainMenuName" class="col-sm-3 control-label">Main Menu Name</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchMainMenuName" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="txtSearchDescription" class="col-sm-3 control-label">Description</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchDescription" class="form-control" runat="server"></asp:TextBox>
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
                        <asp:Panel ID="pnlMainMenuGrid" runat="server" Height="213px" ScrollBars="Vertical"
                            Style="z-index: 102;" Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                            <asp:GridView ID="grdMainMenus" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                Font-Size="8pt" Style="z-index: 102;" Width="995px" OnSelectedIndexChanged="grdMainMenus_SelectedIndexChanged"
                                OnRowDataBound="grdMainMenus_RowDataBound"   CssClass="SearchGridView" RowStyle-Wrap="false">
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
                        <div class="col-sm-5">
                        </div>
                    </div>


                </div>

            </div>


            <div class="panel panel-primary" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Manage Main Menus
                    </div>


                </div>
                <div class="panel-body">
                    <div class="form-group">

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtMainMenuCode" runat="server" Visible="false"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="txtMainMenuName" class="col-sm-3 control-label">Main Menu Name</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtMainMenuName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="txtDescription" class="col-sm-3 control-label">Description</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-5">

                            <asp:Button ID="btnAddNew" runat="server" Text="AddNew"
                                OnClick="btnAddNew_Click" CssClass="btn btn-apps" />
                            <asp:Button ID="btnAlter" runat="server" Text="Alter"
                                OnClick="btnAlter_Click" CssClass="btn btn-apps" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                CssClass="btn btn-apps" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                OnClick="btnCancel_Click" CssClass="btn btn-apps" />



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
