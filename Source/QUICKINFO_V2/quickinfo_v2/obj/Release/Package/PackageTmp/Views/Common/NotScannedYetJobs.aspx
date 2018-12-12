<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotScannedYetJobs.aspx.cs" Inherits="quickinfo_v2.Views.Common.NotScannedYetJobs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Initial</title>

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
</head>
<body>

    <div class="panel-body" style="text-align: center;">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <div class="row">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CssClass="btn btn-apps" />

                <div class="panel-heading">
                    <div class="panel-title">
                       Job not scanned yet
                    </div>
                </div>
                <div class="col-sm-6">
                    <asp:GridView ID="grdNotScannedYetJobs" runat="server" CssClass="myGridStyle-small ">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True"
                            ForeColor="White"></FooterStyle>
                        <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                            ForeColor="White" Height="20px" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" Font-Names="Tahoma" Font-Size="8pt" Height="15px" />
                    </asp:GridView>


                </div>
            </div>




        </form>
    </div>

</body>
</html>
