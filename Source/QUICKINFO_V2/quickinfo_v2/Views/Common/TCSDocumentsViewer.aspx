<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TCSDocumentsViewer.aspx.cs" Inherits="quickinfo_v2.Views.Common.TCSDocumentsViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Viewer</title>

    <script type="text/javascript" src="../JQuery/jquery-1.8.2.min.js"></script>


    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/jquery-ui/css/no-theme/jquery-ui-1.10.3.custom.min.css" id="style_resource_1">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/entypo.css" id="style_resource_2">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/animation.css" id="style_resource_3">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/neon.css" id="style_resource_5">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/custom.css" id="style_resource_6">


    <script src="../../Scripts/drop_zone/dropzone-amd-module.js"></script>
    <script src="../../Scripts/drop_zone/dropzone.js"></script>

    <link href="../../Styles/drop_zone/dropzone.css" rel="stylesheet" />
    <link href="../../Styles/drop_zone/StyleSheet.css" rel="stylesheet" />

    <link rel="stylesheet" href="../../Styles/gridViewStyle.css" type="text/css">
</head>
<body>
    <div class="panel-body">

        <form id="form1" runat="server">
            <div class="row">

                <div class="col-sm-3">
                    <asp:TextBox ID="txtDocPath" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                 

                    <asp:GridView ID="grdTCSDocs" runat="server" CssClass="myGridStyle-small "
                        Style="z-index: 102;" OnRowDataBound="grdTCSDocs_RowDataBound"
                        OnSelectedIndexChanged="grdTCSDocs_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                            ForeColor="White" Height="20px" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" Font-Names="Tahoma" Font-Size="8pt" Height="15px" />
                    </asp:GridView>


                    <br />
                    <table style="display:none;">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="To:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" Width="250px"></asp:TextBox>
                            </td>
                            <td rowspan="2">
                                <asp:Button ID="btnEmail" class="button" runat="server" Text="E-mail" OnClick="btnEmail_Click" Height="100%" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Cc:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCc" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>

                </div>

                <div class="col-sm-9">
                    <iframe id="ifrmDoc" runat="server" style="width: 100%; height: 700px" visible="true"></iframe>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
