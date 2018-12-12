<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CentralizeUserMgt" Title="Centralize User Management" CodeBehind="CentralizeUserMgt.aspx.cs" %>


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
                        Centralize User Management
                    </div>


                </div>

                <div class="panel-body">



                    <div class="form-group">
                        <label for="txtUserCode" class="col-sm-3 control-label">User Code</label>
                        <div class="col-sm-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtUserCode" CssClass="form-control" runat="server"   onfocus="blur()"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button ID="btnSearchUserCode" runat="server" CssClass="btn btn-primary" type="button" Text="Search" />
                                </span>
                            </div>
                        </div>
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlSearchUserCode" TargetControlID="btnSearchUserCode"
                            CancelControlID="btnCloseSearch" BackgroundCssClass="Background">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlSearchUserCode" runat="server" CssClass="Popup" align="center" Style="display: none">
                            <iframe style="width: 800px; height: 600px;" id="Iframe2" runat="server" src="SearchUser.aspx"></iframe>
                            <br />
                            <asp:Button ID="btnCloseSearch" runat="server" Text="Close" CssClass="btn btn-primary" />


                        </asp:Panel>
                    </div>
                </div>


                <div class="form-group">
                    <label for="txtUserName" class="col-sm-3 control-label">User Name</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"  onfocus="blur()"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtUserName" class="col-sm-3 control-label">Photo</label>
                    <div class="col-sm-5">
                        <asp:FileUpload ID="fuPhoto" runat="server" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="rdbtnActive" class="col-sm-3 control-label">Status</label>

                    <div class="col-sm-5">
                        <asp:RadioButton ID="rdbtnActive" runat="server" GroupName="grpStatus" Text="Active"
                            Checked="true" />
                        <asp:RadioButton ID="rdbtnInActive" runat="server" GroupName="grpStatus" Text="In-Active" />
                    </div>
                </div>








                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-5">



                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                            Text="Save" CssClass="btn btn-apps" />
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click"
                            Text="Clear" CssClass="btn btn-apps" />

                    </div>
                </div>


                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-5">

                        <asp:Label ID="lblError" runat="server" Font-Bold="False" Font-Names="Franklin Gothic Book"
                            Font-Size="11pt" ForeColor="Red" Style="z-index: 101;" Width="1039px" Height="22px"></asp:Label>

                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Names="Franklin Gothic Book"
                            Font-Size="11pt" ForeColor="Red" Style="z-index: 101;" Width="1039px" Height="22px"></asp:Label>
                        <asp:Timer ID="Timer1" runat="server" Enabled="False" OnTick="Timer1_Tick">
                        </asp:Timer>

                    </div>
                </div>
                </div>
            </div>






        </form>
    </div>


</asp:Content>
