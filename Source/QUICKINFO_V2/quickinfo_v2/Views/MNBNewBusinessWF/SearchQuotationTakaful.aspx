﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchQuotationTakaful.aspx.cs" Inherits="quickinfo_v2.Views.MNBNewBusinessWF.SearchQuotationTakaful" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/jquery-ui/css/no-theme/jquery-ui-1.10.3.custom.min.css" id="style_resource_1">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/entypo.css" id="style_resource_2">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/animation.css" id="style_resource_3">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/neon.css" id="style_resource_5">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/custom.css" id="style_resource_6">


    <script type="text/javascript">
        function GetSelectedRow(lnk) {
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            //  var customerId = row.cells[0].innerHTML;
            // var city = row.cells[1].getElementsByTagName("input")[0].value;
            var QuoteNo = row.cells[1].getElementsByTagName("span")[0].innerHTML;
            // alert(" City:" + city);
            //alert(QuoteNo);
            var txtQuoteNo = parent.document.getElementById("ContentPlaceHolder1_txtQuotationNo");
            txtQuoteNo.value = row.cells[1].getElementsByTagName("span")[0].innerHTML;

            var txtSelectedQuoteNo = document.getElementById("txtSelectedQuoteNo");
            txtSelectedQuoteNo.value = row.cells[1].getElementsByTagName("span")[0].innerHTML;

            //document.cookie = "CookiedQuoteNo=" + row.cells[1].getElementsByTagName("span")[0].innerHTML + ";";

            //Session("QuotationNo") = QuoteNo;


            <%Session["QuotationNo"] = txtSelectedQuoteNo.Text; %>
            // alert('gg');
            parent.HideModalPopup();


        }


    </script>


</head>
<body>

    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">


            <div class="panel panel-primary" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Search Quotations
                    </div>


                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="txtSelectedQuoteNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>


                    <div class="form-group">
                        <label for="txtSearchQuotationNo" class="col-sm-3 control-label">Quotation No.</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchQuotationNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtSearchVehicleNo" class="col-sm-3 control-label">Vehicle No</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchVehicleNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>





                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-5">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                Text="Search" CssClass="btn btn-apps" />

                        </div>
                    </div>





                </div>
            </div>

            <div class="panel panel-primary" data-collapsed="0">


                <div class="panel-body">



                    <div class="form-group">
                        <asp:Panel ID="pnlSearchGrid" runat="server" Height="600px" ScrollBars="Both" Style="z-index: 102;"
                            Width="100%" BorderColor="#C0C0FF" BorderWidth="1px">
                            <asp:GridView ID="grdSearchResults" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="Tahoma" OnRowDataBound="grdSearchResults_RowDataBound"
                                Font-Size="8pt" Style="z-index: 102;" Width="100%" CssClass="SearchGridView" RowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName="Select" OnClientClick="return GetSelectedRow(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quotation No.">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("QUOTATION_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>




                                </Columns>
                                <RowStyle ForeColor="Black" BackColor="White" Height="15px" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma" Font-Size="Larger"
                                    ForeColor="White" Height="20px" />
                                <AlternatingRowStyle BackColor="WhiteSmoke" Font-Names="Tahoma" Font-Size="8pt" Height="15px" />
                            </asp:GridView>
                        </asp:Panel>

                    </div>

                    <div class="panel panel-primary" data-collapsed="0">


                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-5">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" Font-Bold="False" Font-Names="Franklin Gothic Book"
                                            Font-Size="11pt" ForeColor="Red" Style="z-index: 101;" Width="1039px" Height="22px"></asp:Label>

                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="False" Font-Names="Franklin Gothic Book"
                                            Font-Size="11pt" ForeColor="Red" Style="z-index: 101;" Width="1039px" Height="22px"></asp:Label>
                                        <asp:Timer ID="Timer1" runat="server" Enabled="False" OnTick="Timer1_Tick">
                                        </asp:Timer>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
