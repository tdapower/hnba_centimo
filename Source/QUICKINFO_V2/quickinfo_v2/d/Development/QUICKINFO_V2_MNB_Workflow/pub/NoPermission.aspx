<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NoPermission.aspx.cs" Inherits="quickinfo_v2.NoPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="z-index: 112; left: 1px; width: 1024px; top: 146px; border: 4px; border-color: Red;">
        <tr align="center" style="width: 100%">
            <td>
                <asp:Label ID="lblError1" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="10pt"
                    ForeColor="Red">Sorry, You don't Have Privileges to access this page. Please Contact Your system Administrator........!</asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
