<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BlacklistPolicy" Title="Blacklist Policy" CodeBehind="BlacklistPolicy.aspx.cs" %>


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
                       Blacklist Policy
                    </div>


                </div>

                <div class="panel-body">


                    
                    <div class="form-group">
                        <label for="txtVehicleNo" class="col-sm-3 control-label">Vehicle No</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtVehicleNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="txtPolicyNo" class="col-sm-3 control-label">Policy No</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtPolicyNo" CssClass="form-control" runat="server"></asp:TextBox>
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
                    
                           

                            <asp:Button ID="btnBlacklistPolicy" runat="server" OnClick="btnBlacklistPolicy_Click"
                                Text="Blacklist Policy" CssClass="btn btn-apps" />
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
