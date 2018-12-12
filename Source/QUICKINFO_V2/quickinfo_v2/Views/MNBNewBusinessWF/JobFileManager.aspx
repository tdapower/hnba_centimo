<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="JobFileManager" Title="Job File Manager" CodeBehind="JobFileManager.aspx.cs" %>


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
                    <asp:TextBox ID="txtFilePath" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>

                    <asp:TextBox ID="txtJobNo" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>




                    <div class="form-group">
                        <label for="txtSearchQuotationNo" class="col-sm-3 control-label">Job/Quotation No</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchQuotationNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="ddlJobType" class="col-sm-3 control-label">Job Type</label>
                        <div class="col-sm-5">

                            <asp:DropDownList ID="ddlJobType" runat="server"></asp:DropDownList>

                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-5">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                Text="Search" CssClass="btn btn-apps" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click"
                                Text="Clear" CssClass="btn btn-apps" />


                            <asp:Button ID="btnReProcess" runat="server" OnClick="btnReProcess_Click"
                                Text="Re-Process" CssClass="btn btn-apps" />

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
