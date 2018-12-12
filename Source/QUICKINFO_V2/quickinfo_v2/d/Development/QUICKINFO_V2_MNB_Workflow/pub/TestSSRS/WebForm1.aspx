<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="quickinfo_v2.TestSSRS.WebForm1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">
            <table>

                <tr>
                    <td>Time Slab: </td>
                    <td>
                        <asp:TextBox Width="180" runat="server" ID="txtTimeSlab" /></td>
                    <td>Date: </td>
                    <td>
                        <asp:TextBox Width="180" runat="server" ID="txtDate" /></td>


                    <td>
                        <asp:Button Text="Show Report" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" /></td>
                </tr>

            </table>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server">
            </rsweb:ReportViewer>
            <br />

        </form>

    </div>
</asp:Content>
