<%@ Page Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" Inherits="MenuPrivilegeAssignToUserRoles"
    Title="Privillage Assign" CodeBehind="MenuPrivilegeAssignToUserRoles.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">

            <div class="panel panel-primary" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Manage User Role Privileges
                    </div>

                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="form-group">

                        <label class="col-sm-3 control-label">User Role</label>

                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlSearchUserRole" runat="server" class="form-control"
                                OnSelectedIndexChanged="ddlSearchUserRole_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>

                            <br />

                            <asp:TextBox ID="txtUserRoleCode" runat="server" Visible="false"></asp:TextBox>
                            <br />
                            <asp:TreeView ID="tvPrivileges" runat="server">
                            </asp:TreeView>


                        </div>


                    </div>

                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-5">
                            <asp:Button ID="btnAlter" runat="server" Text="Alter"
                                OnClick="btnAlter_Click" CssClass="btn btn-apps" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                CssClass="btn btn-apps" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                OnClick="btnCancel_Click" CssClass="btn btn-apps" />
                        </div>
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


        </form>
    </div>

</asp:Content>
