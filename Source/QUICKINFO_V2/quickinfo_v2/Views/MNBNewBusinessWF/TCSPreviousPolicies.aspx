<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TCSPreviousPolicies.aspx.cs" Inherits="quickinfo_v2.Views.MNBNewBusinessWF.TCSPreviousPolicies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table {
            color: #333; /* Lighten up font color */
            font-family: Helvetica, Arial, sans-serif; /* Nicer font */
            width: 100%;
            border-collapse: collapse;
            border-spacing: 0;
        }

        td, th {
            border: 1px solid #CCC;
            height: 30px;
        }
        /* Make cells a bit taller */

        th {
            background: #F3F3F3; /* Light grey background */
            font-weight: bold; /* Make sure they're bold */
        }

        td {
            background: #FAFAFA; /* Lighter grey background */
            text-align: center; /* Center our text */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="ltrlData" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
