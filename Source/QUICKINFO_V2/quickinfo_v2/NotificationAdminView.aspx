<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NotificationAdminView.aspx.cs" Inherits="quickinfo_v2.NotificationAdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form role="form" class="form-horizontal form-groups-bordered" runat="server">
        <table>
            <tr>
                <td>Pin Code</td>
                <td>
                    <asp:TextBox ID="txtDadPinCode" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Title</td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Message</td>
                <td>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Type</td>
                <td>
                    <asp:RadioButton ID="rdbSuccess" Text="Success" runat="server" GroupName="grpType" />
                    <asp:RadioButton ID="rdbInfo" Text="Info" runat="server" GroupName="grpType" />
                    <asp:RadioButton ID="rdbWarning" Text="Warning" runat="server" GroupName="grpType" />
                    <asp:RadioButton ID="rdbError" Text="Error" runat="server" GroupName="grpType" />


                </td>
            </tr>
            <tr>
                <td>Timeout</td>
                <td>
                    <asp:TextBox ID="txtTimeout" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>For Branch</td>
                <td>
                    <asp:TextBox ID="txtBranch" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSendNotification" runat="server" Text="Send" OnClick="btnSendNotification_Click" /></td>
                <td></td>
            </tr>
        </table>








    </form>
</asp:Content>
