<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditCalculate.aspx.cs" Inherits="quickinfo_v2.Views.Common.CreditCalculate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../Styles/neon-x/assets/js/jquery-1.10.2.min.js"></script>

    <script type="text/javascript" src="../../Styles/neon-x/assets/js/bootstrap-datepicker.js"></script>

    <script type="text/javascript" src="../../Styles/neon-x/assets/js/bootstrap.min.js"></script>

    <%--<link rel="stylesheet" href="../../Styles/neon-x/assets/css/neon.css" id="style_resource_5" />--%>
    <%--<link rel="stylesheet" href="../../Styles/neon-x/assets/css/custom.css" id="style_resource_6" />--%>

    <link rel="stylesheet" href="../../Styles/datepicker.min.css" id="style_resource_7" />

    <link href="../../Styles/neon-x/assets/css/bootstrap.min.css" rel="stylesheet" />



    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtPolicyStartDate").datepicker();
            $("#txtPolicyEndDate").datepicker();
            $("#txtPolicyCancelledDate").datepicker();


        });
    </script>


    <script type="text/javascript">
        function calcUsedDays() {
            var $datepicker1 = $("#txtPolicyStartDate");
            var $datepicker2 = $("#txtPolicyCancelledDate");


            var fromDate = $datepicker1.datepicker('getDate');
            var toDate = $datepicker2.datepicker('getDate');
            // date difference in millisec
            var diff = new Date(toDate - fromDate);
            // date difference in days
            var days = diff / 1000 / 60 / 60 / 24;
            document.getElementById("txtUsedDays").value = days;
        }
    </script>


    <script type="text/javascript">
        function calcUtilizedDays() {
            var $datepicker1 = $("#txtPolicyCancelledDate");
            var $datepicker2 = $("#txtPolicyEndDate");


            var fromDate = $datepicker1.datepicker('getDate');
            var toDate = $datepicker2.datepicker('getDate');
            // date difference in millisec
            var diff = new Date(toDate - fromDate);
            // date difference in days
            var days = diff / 1000 / 60 / 60 / 24;
            document.getElementById("txtUtilizedDays").value = days;
        }
    </script>


    <%-- <script>
        function calcDays() {
            alert('aaaa');
            var $datepicker1 = $("#txtPolicyStartDate");
            var $datepicker2 = $("#txtPolicyCancelledDate");
            $datepicker1.datepicker();
          
            alert('aaaa');
            var fromDate = $datepicker1.datepicker('getDate');
            var toDate = $datepicker2.datepicker('getDate');
            // date difference in millisec
            var diff = new Date(toDate - fromDate);
            // date difference in days
            var days = diff / 1000 / 60 / 60 / 24;

            alert(days);

            document.getElementById("txtUsedDays").value = days;
        });
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>

     <asp:TextBox ID="txtJobNo" CssClass="form-control" runat="server" Visible="false" Text="16TDA00001"></asp:TextBox>
            <asp:TextBox ID="txtVehicleRiskTypeId" CssClass="form-control" runat="server" Visible="false" Text="1"></asp:TextBox>
            <asp:TextBox ID="txtVehicleClassTypeId" CssClass="form-control" runat="server" Visible="false" Text="1"></asp:TextBox>

            <table>
                <tr>
                    <td>
                        <table style="vertical-align: top;">
                            <tr>
                                <td>Policy Start date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicyStartDate" data-format="dd/mm/yyyy" runat="server" onchange="calcUsedDays(),calcUtilizedDays()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Policy End date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicyEndDate" data-format="dd/mm/yyyy" runat="server" onchange="calcUsedDays(),calcUtilizedDays()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Policy Cancelled date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicyCancelledDate" data-format="dd/mm/yyyy" runat="server" onchange="calcUsedDays(),calcUtilizedDays()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Used Days
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsedDays" runat="server" onfocus="blur()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Utilized Days
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUtilizedDays" runat="server" onfocus="blur()"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: right;">
                                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>

                        </table>
                    </td>

                    <td>
                        <table style="vertical-align: top;">
                            <tr>
                                <td>Basic Premium
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBasicPremium" runat="server" Style="text-align: right;" Text="0.00"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>SRCC 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSRCC" runat="server" Style="text-align: right;" Text="0.00"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>TC  
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTC" runat="server" Style="text-align: right;" Text="0.00"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>CRSF
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCRSF" runat="server" Style="text-align: right;" Text="0.00" onfocus="blur()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Adiministration Fee  
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdiministrationFee" runat="server" Style="text-align: right;" Text="0.00" onfocus="blur()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Policy Fee
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolicyFee" runat="server" Style="text-align: right;" Text="0.00" onfocus="blur()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Stamp Duty
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStampDuty" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>NBT
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNBT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td>Total
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotal" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td></td>
                                <td></td>

                            </tr>
                            <tr>
                                <td></td>
                                <td></td>

                            </tr>



                        </table>
                    </td>
                </tr>

            </table>
            <br />
            <table style="vertical-align: top;">
                <tr>
                    <td>Utilization Premium 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUtilizationPremium" runat="server"></asp:DropDownList>


                    </td>
                </tr>
            </table>
            <br />

            <table style="vertical-align: top;">

                <tr>
                    <td></td>
                    <td>Without VAT</td>
                    <td>With VAT</td>
                </tr>

                <tr>
                    <td>Basic Premium
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditBasicPremium" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditBasicPremiumWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>SRCC 
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditSRCC" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditSRCCWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>TC  
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditTC" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditTCWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>
                <tr style="display:none;">
                    <td>CRSF
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditCRSF" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditCRSFWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Adiministration Fee  
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditAdiministrationFee" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditAdiministrationFeeWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Sub Total  
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditSubTotal" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditSubTotalWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>


                <tr style="display:none;">
                    <td>Policy Fee
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditPolicyFee" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditPolicyFeeWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Stamp Duty
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditStampDuty" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditStampDutyWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>NBT
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditNBT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditNBTWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>

                </tr>
                <tr>
                    <td>VAT

                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td></td>

                </tr>
                <tr>
                    <td>Total
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditTotal" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCreditTotalWithVAT" runat="server" Style="text-align: right;" Text="0.00"  onfocus="blur()"></asp:TextBox></td>

                </tr>
                <tr>
                    <td></td>
                    <td></td>

                </tr>
                <tr>
                    <td></td>
                    <td></td>

                </tr>



            </table>
        </div>
    </form>
</body>
</html>
