<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchTCSPolicy.aspx.cs" Inherits="quickinfo_v2.Views.MNBNewBusinessWF.SearchTCSPolicy" %>

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

       
            var txtQuoteNo = parent.document.getElementById("ContentPlaceHolder1_txtPolicyNo");
            txtQuoteNo.value = row.cells[1].getElementsByTagName("span")[0].innerHTML;

            var txtSelectedPolicyeNo = document.getElementById("txtSelectedPolicyeNo");
            txtSelectedPolicyeNo.value = row.cells[1].getElementsByTagName("span")[0].innerHTML;


            var rbtnNonTakaful = document.getElementById("rbtnNonTakaful");
            var rbtnTakaful = document.getElementById("rbtnTakaful");

            var txtSystemName =  parent.document.getElementById("ContentPlaceHolder1_txtSystemName");
          
            if (rbtnNonTakaful.checked) {
                txtSystemName.value = "TCS";
            }
            if (rbtnTakaful.checked) {
                txtSystemName.value = "Takaful";
            }

          //  alert('test0');
      

  
            var txtProposalNo = parent.document.getElementById("ContentPlaceHolder1_txtProposalNo");
            txtProposalNo.value = row.cells[2].getElementsByTagName("span")[0].innerHTML;
           // alert('test1');

            var txtPolicyID = parent.document.getElementById("ContentPlaceHolder1_txtPolicyID");
            txtPolicyID.value = row.cells[3].getElementsByTagName("span")[0].innerHTML;
          //  alert('test2');


            
            //document.cookie = "CookiedQuoteNo=" + row.cells[1].getElementsByTagName("span")[0].innerHTML + ";";

            //Session("QuotationNo") = QuoteNo;


            <%Session["PolicyNo"] = txtSelectedPolicyeNo.Text; %>
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
                        Search TCS Policies
                    </div>


                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="txtSelectedPolicyeNo" CssClass="form-control" runat="server" onfocus="blur()"></asp:TextBox>

                
                    <div class="form-group">


                        <div class="col-sm-5">
                            <asp:RadioButton ID="rbtnNonTakaful" Text="Non-Takaful" runat="server" Checked="true" GroupName="rbtnSystem" Style="font-size: large;" />
                            <asp:RadioButton ID="rbtnTakaful" Text="Takaful" runat="server" GroupName="rbtnSystem" Style="font-size: large;" />


                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtSearchPolicyNo" class="col-sm-3 control-label">Policy No.</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchPolicyNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtSearchProposalNo" class="col-sm-3 control-label">Proposal No</label>

                        <div class="col-sm-5">
                            <asp:TextBox ID="txtSearchProposalNo" CssClass="form-control" runat="server"></asp:TextBox>
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
                                Font-Size="8pt" Style="z-index: 102;" Width="100%" CssClass="SearchGridView" RowStyle-Wrap="false" AutoGenerateColumns="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName="Select" OnClientClick="return GetSelectedRow(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Policy No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPolicyNo" runat="server" Text='<%# Eval("POL_POLICY_NUMBER") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Proposal No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProposalNo" runat="server" Text='<%# Eval("POL_PROPOSAL_NUMBER") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Policy Id" ControlStyle-Width="0px" HeaderStyle-Width="0px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPolicyID" runat="server" Text='<%# Eval("POL_POLICY_ID") %>'></asp:Label>
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
