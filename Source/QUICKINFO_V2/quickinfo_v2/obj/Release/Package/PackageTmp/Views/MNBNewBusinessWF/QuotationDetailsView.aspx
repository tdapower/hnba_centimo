<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationDetailsView.aspx.cs" Inherits="quickinfo_v2.Views.MNBNewBusinessWF.QuotationDetailsView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="stylesheet" type="text/css"   href="~/Styles/QuotationDetailsStyleSheet.css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="z-index: 0;  width: 696px; position: absolute; ">
                <tr>
                    <td style="text-align: left"></td>
                    <td style="text-align: left;">
                        <asp:HiddenField ID="hdnBranch" runat="server" />
                        <asp:HiddenField ID="hdnEmployeeEPF" runat="server" />
                        <asp:HiddenField ID="hdnJobID" runat="server" />
                        <asp:HiddenField ID="hdnRevisionID" runat="server" />
                        <asp:HiddenField ID="hdnUserCode" runat="server" />
                        <asp:HiddenField ID="hdnMode" runat="server" />
                    </td>
                    <td style="text-align: left"></td>
                    <td style="text-align: left"></td>
                    <td rowspan="1" style="width: 80px; text-align: left" valign="top"></td>
                </tr>
                <tr>
                    <td colspan="5" rowspan="1" style="text-align: left; height: 318px;" valign="top">

                        <asp:Panel ID="pnl1" runat="server" GroupingText="">
                            <table>
                                <tr>
                                    <td style="width: 153px; height: 24px;">Revision</td>
                                    <td style="width: 279px; height: 24px;">
                                        <asp:DropDownList ID="ddlRevisionID" runat="server" Width="128px"></asp:DropDownList>

                                    </td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td rowspan="1" style="width: 100px; height: 24px;" valign="top"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                </tr>
                                <tr>
                                    <td style="width: 153px; height: 24px;">Requested By</td>
                                    <td style="width: 279px; height: 24px;">
                                        <asp:TextBox ID="txtRequestedBy" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td rowspan="1" style="width: 100px; height: 24px;" valign="top"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td style="width: 100px; height: 24px;"></td>
                                </tr>
                            </table>
                            <table runat="server" id="tblMain">
                                <tr>
                                    <td style="width: 153px; height: 24px;">
                                        <asp:Label ID="Label1" runat="server" Text="Client Name" Width="144px"></asp:Label>
                                    </td>
                                    <td style="width: 279px; height: 24px;">
                                        <asp:TextBox ID="txtClientName" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label2" runat="server" Text="Period of cover" Width="120px"></asp:Label>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:DropDownList ID="ddlPeriodTypes" runat="server" Width="75px"></asp:DropDownList>
                                        <span style="position: absolute;">

                                            <asp:DropDownList ID="ddlPeriodOfCover" runat="server" Width="50px"></asp:DropDownList>

                                        </span>
                                    </td>
                                    <td style="width: 100px"></td>
                                    <td style="width: 100px; height: 24px;">Remarks
                                    </td>
                                    <td style="width: 100px; height: 24px;"></td>
                                </tr>
                                <tr style="display: none;">
                                    <td style="width: 79px; height: 21px; text-align: left">
                                        <asp:TextBox ID="txtUnderwriter" runat="server" Width="112px"></asp:TextBox>
                                    </td>
                                    <td style="width: 427px; height: 21px; text-align: left">&nbsp;
                                    </td>
                                    <td style="width: 79px; height: 21px; text-align: left">
                                        <asp:Label ID="Label8" runat="server" Text="Underwriter (EPF)" Width="120px"></asp:Label>
                                    </td>
                                    <td style="width: 427px; height: 21px; text-align: left">
                                        <asp:Label ID="lblUWName" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 80px;"></td>
                                    <td style="width: 100px; height: 21px"></td>
                                    <td style="width: 100px; height: 21px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 153px">
                                        <asp:Label ID="Label4" runat="server" Text="Vehicle/Chassi Number" Width="144px"></asp:Label>
                                    </td>
                                    <td style="width: 279px">
                                        <asp:TextBox ID="txtVehicleNo" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px; height: 22px">Agent/Broker</td>
                                    <td style="width: 100px; height: 22px">
                                        <asp:DropDownList ID="ddlAgentOrBroker" runat="server" Width="128px"></asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; height: 22px"></td>
                                    <td style="width: 100px" rowspan="5">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="200px" Height="110px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 153px; height: 22px">Sum Insured*
                                    </td>
                                    <td style="width: 279px; height: 22px">
                                        <asp:TextBox ID="txtSumInsured" runat="server" Width="124px">0.00</asp:TextBox>
                                    </td>
                                    <td style="width: 100px; height: 24px;">Agent/Broker Code
                                    </td>
                                    <td style="width: 100px; height: 24px;">



                                        <asp:TextBox ID="txtAgentOrBrokerCode" runat="server" Width="128px"></asp:TextBox>




                                    </td>
                                    <td style="width: 100px; height: 24px;"></td>
                                    <td style="width: 100px; height: 22px"></td>
                                    <td style="width: 100px; height: 22px"></td>
                                    <td style="width: 100px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 153px; height: 22px;">Vehicle Risk Type</td>
                                    <td style="width: 279px; height: 22px;">
                                        <asp:DropDownList ID="ddlVehicleRiskType" runat="server" Width="200px"></asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; height: 22px;">Leasing Type</td>
                                    <td style="width: 100px; height: 22px;">
                                        <asp:DropDownList ID="ddlLeasingType" runat="server" Width="128px"></asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; height: 22px;"></td>
                                    <td style="width: 100px; height: 22px;"></td>
                                    <td style="width: 100px; height: 22px;"></td>
                                </tr>
                                <tr>
                                    <td style="width: 153px; height: 22px">Vehicle Type</td>
                                    <td style="width: 279px; height: 22px">

                                        <asp:DropDownList ID="ddlVehicleType" runat="server" Width="200px"></asp:DropDownList>

                                    </td>
                                    <td style="width: 100px; height: 22px">Fuel Type</td>
                                    <td style="width: 100px; height: 22px">
                                        <asp:DropDownList ID="ddlFuelType" runat="server" Width="128px"></asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; height: 22px"></td>
                                    <td style="width: 100px; height: 22px"></td>
                                    <td style="width: 100px; height: 22px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 153px; height: 22px">Usage**</td>
                                    <td style="width: 279px; height: 22px">

                                        <asp:DropDownList ID="ddlUsage" runat="server" Width="128px"></asp:DropDownList>

                                    </td>
                                    <td style="width: 100px; height: 22px">
                                        <strong>Product*</strong>
                                    </td>
                                    <td style="width: 100px; height: 22px">

                                        <asp:DropDownList ID="ddlProduct" runat="server" Width="128px"></asp:DropDownList>

                                    </td>
                                    <td style="width: 100px; height: 22px"></td>
                                    <td style="width: 100px; height: 22px"></td>
                                    <td style="width: 100px; height: 22px"></td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="width: 153px; height: 22px;">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Quotation Number" Width="120px"></asp:Label>
                                    </td>
                                    <td style="width: 279px; height: 22px;">
                                        <asp:Label ID="lblQuotationNo" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 100px; height: 22px;"></td>
                                    <td style="width: 100px; height: 22px;"></td>
                                    <td style="width: 100px; height: 22px;" colspan="2"></td>
                                    <td style="width: 100px; height: 22px;"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table runat="server" id="tblCovers">
                            <tr>
                                <td style="text-align: left; height: 21px;" colspan="5">
                                    <asp:Panel ID="pnl_policy_cover" runat="server" Font-Size="Smaller" GroupingText="POLICY COVERS"
                                        Style="font-family: 'Times New Roman'" Font-Names="Arial">
                                        <table>
                                            <tr>
                                                <td style="width: 100px; height: 5px;">
                                                    <span style="font-family: Times New Roman">
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Multiple Rebate" Width="112px"
                                                            Font-Size="Larger"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="width: 44px; height: 5px;"></td>
                                                <td style="width: 67px; height: 5px;">
                                                    <asp:TextBox ID="txtMultipleRebate" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 67px; height: 5px" colspan="2">
                                               </td>
                                                <td style="width: 100px; height: 5px;"></td>
                                                <td style="width: 100px; height: 5px"></td>
                                                <td style="width: 100px; height: 5px"></td>
                                                <td style="width: 100px; height: 5px;"></td>
                                                <td style="width: 100px; height: 5px;"></td>
                                                <td style="width: 100px; height: 5px"></td>
                                                <td style="width: 100px; height: 5px;"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk1" runat="server" Text="Hire Purchase/Lease/Loan" Width="184px"
                                                        Font-Size="Larger" Font-Names="Arial" />
                                                </td>
                                                <td style="width: 67px" colspan="2">
                                                    <asp:TextBox ID="txtHirePurchase" runat="server" Width="110px"></asp:TextBox>
                                                </td>
                                                <td style="width: 67px"></td>
                                                <td style="width: 103px">

                                                    <asp:CheckBox ID="chk9" runat="server" Text="Earned NCB (20% - 75%)" Width="168px"
                                                        Font-Size="Larger" Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px">

                                                    <asp:DropDownList ID="ddlEarnedNCB" runat="server" Width="100px"></asp:DropDownList>

                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk21" runat="server" Text="Less - Points Earned" Width="160px"
                                                        Font-Size="Larger" Font-Names="Arial" Enabled="false" />
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtLessPointsEarned" runat="server" Width="70px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:CheckBox ID="chk2" runat="server" Text="Less - Voluntary" Width="128px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 44px; height: 22px"></td>
                                                <td style="width: 67px; height: 22px">

                                                    <asp:DropDownList ID="ddlVoluntary" runat="server" Width="70px"></asp:DropDownList>

                                                </td>
                                                <td style="width: 67px; height: 22px"></td>
                                                <td style="width: 103px; height: 22px">

                                                    <asp:CheckBox ID="chk10" runat="server" Text="Up - Front NCB %" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px; height: 22px">

                                                    <asp:DropDownList ID="ddlUpFrontNCB" runat="server" Width="100px"></asp:DropDownList>

                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:CheckBox ID="chk22" runat="server" Text="Driving Tuition" Width="160px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:CheckBox ID="chk3" runat="server" Text="AAC Membership" Width="128px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 44px; height: 22px"></td>
                                                <td style="width: 67px; height: 22px">
                                                    <asp:TextBox ID="txtAACMembership" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 67px; height: 22px"></td>
                                                <td style="width: 103px; height: 22px">
                                                    <asp:CheckBox ID="chk11" runat="server" Text="Windscreen" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:TextBox ID="txtWindscreen" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:CheckBox ID="chk23" runat="server" Text="Duty Free/Duty Concession" Width="200px"
                                                        Font-Size="Larger" Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk4" runat="server" Text="PAB To Driver" Width="160px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 44px; text-align: right">
                                                    <asp:DropDownList ID="ddlAddPABToDriver" runat="server" Width="40px"></asp:DropDownList>
                                                </td>
                                                <td style="width: 67px">
                                                    <asp:TextBox ID="txtAddPABToDriver" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 67px">
                                                 </td>
                                                <td style="width: 103px">
                                                    <asp:CheckBox ID="chk12" runat="server" Text="TPPD" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="ddlTPPD" runat="server" Width="100px"></asp:DropDownList>
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk24" runat="server" Text="Adjustment Fee" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 22px;">
                                                    <asp:CheckBox ID="chk5" runat="server" Text="PAB To Passenger" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 44px; text-align: right; height: 22px;">
                                                    <asp:DropDownList ID="ddlPABToPassenger" runat="server" Width="40px"></asp:DropDownList>
                                                </td>
                                                <td style="width: 67px; height: 22px;">
                                                    <asp:TextBox ID="txtAddPABToPassenger" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 67px; height: 22px">
                                                </td>
                                                <td style="width: 103px; height: 22px;">
                                                    <asp:CheckBox ID="chk13" runat="server" Text="WCI" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 22px;">
                                                    <asp:DropDownList ID="ddlWCI" runat="server" Width="40px"></asp:DropDownList>
                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px;">

                                                    <asp:CheckBox ID="chk25" runat="server" Text="Theft of Parts" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px; height: 22px;"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px;"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 2px">
                                                    <asp:CheckBox ID="chk6" runat="server" Text="Goods In Transit" Width="160px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 44px; height: 2px"></td>
                                                <td style="width: 67px; height: 2px"></td>
                                                <td style="width: 67px; height: 2px"></td>
                                                <td style="width: 103px; height: 2px">

                                                    <asp:CheckBox ID="chk14" runat="server" Text="Inclusion of Excluded Items" Width="192px"
                                                        Font-Size="Larger" Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px; height: 2px"></td>
                                                <td style="width: 100px; height: 2px"></td>
                                                <td style="width: 100px; height: 2px"></td>
                                                <td style="width: 100px; height: 2px">
                                                    <asp:CheckBox ID="chk26" runat="server" Text="SRCC - for Vehicles" Width="176px"
                                                        Font-Size="Larger" Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 2px">
                                                    <asp:CheckBox ID="chk33" runat="server" Text="TC - for Vehicles" Width="120px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 2px"></td>
                                                <td style="width: 100px; height: 2px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right">
                                                    <asp:DropDownList ID="ddlGoodsInTransit" runat="server" Width="176px"></asp:DropDownList>
                                                </td>
                                                <td style="width: 67px">
                                                    <asp:TextBox ID="txtAddGoodInTransit" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 67px"></td>
                                                <td style="width: 103px">
                                                    <asp:CheckBox ID="chk15" runat="server" Text="Learner Rider/Driver" Width="168px"
                                                        Font-Size="Larger" Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk27" runat="server" Text="SRCC - for Goods" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk34" runat="server" Text="TC - for Goods" Width="112px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:CheckBox ID="chk7" runat="server" Text="Legal Liability" Width="152px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 44px; height: 22px; text-align: right">
                                                    <asp:DropDownList ID="ddlLegalLiability" runat="server" Width="40px"></asp:DropDownList>
                                                </td>
                                                <td style="width: 67px; height: 22px">
                                                    <asp:DropDownList ID="ddlLegalLiabilityVal" runat="server" Width="70px"></asp:DropDownList>
                                                </td>
                                                <td style="width: 67px; height: 22px"></td>
                                                <td style="width: 103px; height: 22px">

                                                    <asp:CheckBox ID="chk16" runat="server" Text="CTB" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:CheckBox ID="chk28" runat="server" Text="SRCC - for PAB" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 22px">
                                                    <asp:TextBox ID="txtSRCCForPAB" runat="server" Width="70px" Text="0" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk8" runat="server" Text="Towing Charges" Width="152px" Font-Size="Larger"
                                                        Font-Names="Arial" Checked="True" Enabled="False" />
                                                </td>
                                                <td style="width: 44px"></td>
                                                <td style="width: 67px">
                                                    <asp:TextBox ID="txtAddTowingCharges" runat="server" Width="70px" Style="text-align: right">2500.00</asp:TextBox>

                                                </td>
                                                <td style="width: 67px"></td>
                                                <td style="width: 103px">

                                                    <asp:CheckBox ID="chk17" runat="server" Text="Rent A Car" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk29" runat="server" Text="TC - for PAB" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtTCforPAB" runat="server" Width="70px" Text="0" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px" colspan="2">

                                                    <asp:CheckBox ID="chk36" runat="server" Text="Less - Name Driver Discount" Width="200px"
                                                        Font-Size="Larger" Font-Names="Arial" Enabled="False" Visible="false" />

                                                </td>
                                                <td style="width: 67px; height: 22px;"></td>
                                                <td style="width: 67px; height: 22px"></td>
                                                <td style="width: 103px; height: 22px;">

                                                    <asp:CheckBox ID="chk18" runat="server" Text="Flood" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px; height: 22px;"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px;">
                                                    <asp:CheckBox ID="chk30" runat="server" Text="SRCC - for WCI" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 22px;">
                                                    <asp:TextBox ID="txtSRCCforWCI" runat="server" Width="70px" Text="0" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px; height: 22px"></td>
                                                <td style="width: 100px; height: 22px;"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chk37" runat="server" Text="NCB Protection" Width="152px" Font-Size="Larger"
                                                        Font-Names="Arial" Enabled="False" Visible="false" />

                                                </td>
                                                <td style="width: 44px"></td>
                                                <td style="width: 67px"></td>
                                                <td style="width: 67px"></td>
                                                <td style="width: 103px">

                                                    <asp:CheckBox ID="chk19" runat="server" Text="Natural Perils" Width="168px" Font-Size="Larger"
                                                        Font-Names="Arial" AutoPostBack="true" />

                                                </td>
                                                <td style="width: 100px">

                                                    <asp:CheckBox ID="chk20" runat="server" Text="Stamp Duty" Width="96px" Font-Size="Larger"
                                                        Font-Names="Arial" />

                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chk31" runat="server" Text="TC - for WCI" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtTCforWCI" runat="server" Width="70px" Text="0" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 31px">
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Large" Text="Total Premium RS. "
                                                        Width="152px"></asp:Label>
                                                    <asp:Label ID="lblTotalPremium" runat="server" Font-Bold="True" Font-Size="Large"
                                                        Text="0.00"></asp:Label>
                                                </td>
                                                <td style="width: 103px; height: 31px;">
                                                    <asp:CheckBox ID="chk35" runat="server" Text="Air Bag Replacement" Width="168px"
                                                        Font-Size="Larger" Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 31px;">
                                                    <asp:TextBox ID="txtAirBagsReplace" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px; height: 31px;">=
                                                </td>
                                                <td style="width: 100px"></td>
                                                <td style="width: 100px; height: 31px;">
                                                    <asp:CheckBox ID="chk32" runat="server" Text="Compulsary Excess" Width="176px" Font-Size="Larger"
                                                        Font-Names="Arial" />
                                                </td>
                                                <td style="width: 100px; height: 31px;">
                                                    <asp:TextBox ID="txtCompulsaryExcess" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td style="width: 100px; height: 31px;"></td>
                                                <td style="width: 100px; height: 31px;"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 17px"></td>
                                                <td style="width: 44px; height: 17px"></td>
                                                <td style="width: 67px; height: 17px"></td>
                                                <td style="width: 67px; height: 17px"></td>
                                                <td style="width: 103px; height: 17px"></td>
                                                <td style="width: 100px; height: 17px"></td>
                                                <td style="width: 100px; height: 17px"></td>
                                                <td style="width: 100px; height: 17px"></td>
                                                <td style="width: 100px; height: 17px"></td>
                                                <td style="width: 100px; height: 17px"></td>
                                                <td style="width: 100px; height: 17px"></td>
                                                <td style="width: 100px; height: 17px"></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 2165px; height: 21px;"></td>
                            </tr>


                        </table>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
