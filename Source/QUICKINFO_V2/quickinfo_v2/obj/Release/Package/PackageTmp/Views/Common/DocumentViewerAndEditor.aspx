<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentViewerAndEditor.aspx.cs" Inherits="quickinfo_v2.Views.Common.DocumentViewerAndEditor" %>

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



</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Pages To Delete"></asp:Label><asp:TextBox ID="txtPagesToDelete" runat="server"></asp:TextBox>
            <asp:Button ID="btnDeletePages" runat="server" Text="Delete Pages" OnClick="btnDeletePages_Click" />
            <%--    <embed src="file:\\\\192.168.10.13\\HNBGI\\QUEUED_SCN_DOCS\\15HDO00190\15HDO00190.PDF" width="500" height="375" type='application/pdf'>
            <iframe id="ifrmDoc" runat="server" width="1000" height="2200" src="file:\\\\192.168.10.13\\HNBGI\\QUEUED_SCN_DOCS\\15HDO00190\15HDO00190.PDF&embedded=true" visible="true">
      
        
            </iframe>  --%>

            <%--  <div id="pdf">
                <object width="400" height="500" type="application/pdf" data="file:\\\\192.168.10.13\\HNBGI\\QUEUED_SCN_DOCS\\15HDO00190\15HDO00190.PDF?#zoom=85&scrollbar=0&toolbar=0&navpanes=0" id="pdf_content">
                    <p>Insert your error message here, if the PDF cannot be displayed.</p>
                </object>
            </div>--%>

            <%--  <div id="pdf">
                <object data="file:\\\\192.168.10.13\\HNBGI\\QUEUED_SCN_DOCS\\15HDO00190\15HDO00190.PDF" type="application/x-pdf" title="SamplePdf"
                    width="200" height="100">
                    <a href="\\\\192.168.10.13\\HNBGI\\QUEUED_SCN_DOCS\\15HDO00190\15HDO00190.PDF">aaa</a>
                </object>
            </div>--%>
            <iframe id="ifrmDoc" runat="server" width="100%" height="600px" visible="true"></iframe>
        </div>
    </form>
</body>
</html>
