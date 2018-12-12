<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="UserRegistration" Title="User Registration" CodeBehind="UserRegistration.aspx.cs" %>


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
                        Search Users
                    </div>


                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="form-group">
                        <label for="ddlSearchCompany" class="col-sm-3 control-label">Company</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchCompany" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtSearchUserCode" class="col-sm-3 control-label">User Code</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchUserCode" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="txtSearchUserName" class="col-sm-3 control-label">User Name</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchUserName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="field-121" class="col-sm-3 control-label">EPF</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchEPF" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="field-121" class="col-sm-3 control-label">User Role</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchUserRole" runat="server" CssClass="form-control">
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
                            <asp:GridView ID="grdUsers" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma"
                                Font-Size="8pt" Style="z-index: 102;" Width="100%" OnSelectedIndexChanged="grdUsers_SelectedIndexChanged"
                                OnRowDataBound="grdUsers_RowDataBound" CssClass="SearchGridView" RowStyle-Wrap="false">
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
                                    Manage Users
                                </div>


                            </div>
                            <div class="panel-body">


                                <div class="form-group">
                                    <label for="txtUserCode" class="col-sm-3 control-label">User Code</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtUserCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="txtUserName" class="col-sm-3 control-label">User Name</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="ddlCompany" class="col-sm-3 control-label">Company</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ddlBranch" class="col-sm-3 control-label">Main Branch</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtOtherBranches" class="col-sm-3 control-label">Other Branches</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtOtherBranches" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtEPF" class="col-sm-3 control-label">EPF</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtEPF" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtEmail" class="col-sm-3 control-label">E-Mail</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtUserCode" class="col-sm-3 control-label">Workflow User Role</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="txtUserCode" class="col-sm-3 control-label">Status</label>

                                    <div class="col-sm-5">
                                        <asp:RadioButton ID="rdbtnActive" runat="server" GroupName="grpStatus" Text="Active"
                                            Checked="true" />
                                        <asp:RadioButton ID="rdbtnInActive" runat="server" GroupName="grpStatus" Text="In-Active" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="rbtnTravellingYes" class="col-sm-3 control-label">Is Travelling Entitled</label>

                                    <div class="col-sm-5">
                                        <asp:RadioButton ID="rbtnTravellingYes" runat="server" GroupName="grpTravelling" Text="Yes"
                                            Checked="true" />
                                        <asp:RadioButton ID="rbtnTravellingNo" runat="server" GroupName="grpTravelling" Text="No" />
                                    </div>
                                </div>



                                <div class="form-group">
                                    <label for="txtUserCode" class="col-sm-3 control-label">Systems</label>

                                    <div class="col-sm-5">
                                        <asp:GridView ID="grdSystems" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" DataKeyNames="SYSTEM_CODE" GridLines="Vertical"
                                            OnRowDataBound="grdSystems_RowDataBound" CssClass="SearchGridView">
                                            <AlternatingRowStyle BackColor="#DCDCDC" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="System ID" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSystemCode" runat="server" Text='<%# Bind("SYSTEM_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="System Name">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SYSTEM_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Allowed" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkIsSystemAllowed" runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Role">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlSystemUserRole" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle BackColor="#006699" ForeColor="Black" />

                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                                                ForeColor="White" Height="20px" />


                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                        </asp:GridView>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-sm-offset-3 col-sm-6">

                                        <asp:Button ID="btnAddNew" runat="server" Text="AddNew"
                                            OnClick="btnAddNew_Click" CssClass="btn btn-apps" />
                                        <asp:Button ID="btnAlter" runat="server" Text="Alter"
                                            OnClick="btnAlter_Click" CssClass="btn btn-apps" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                            CssClass="btn btn-apps" />
                                        <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click"
                                            CssClass="btn btn-apps" />
                                        <asp:Button ID="btnUnlockUser" runat="server" Text="Unlock User" OnClick="btnUnlockUser_Click"
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
