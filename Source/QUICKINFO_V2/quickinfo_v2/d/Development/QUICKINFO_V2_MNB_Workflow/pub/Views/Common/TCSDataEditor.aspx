<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TCSDataEditor.aspx.cs" Inherits="quickinfo_v2.Views.Common.TCSDataEditor" %>

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





</head>
<body>
    <form id="form1" runat="server">
        <div>


            <table>
                <tr>
                    <td>Policy No.</td>
                    <td>
                        <asp:TextBox ID="txtPolicyNo" runat="server"></asp:TextBox></td>


                    <td>Current Status</td>
                    <td>
                        <asp:TextBox ID="txtCurrentStatus" runat="server"></asp:TextBox></td>

                </tr>

                <tr>
                    <td>Party Type</td>
                    <td>
                        <asp:TextBox ID="txtPartyType" runat="server"></asp:TextBox></td>


                    <td>Party Code</td>
                    <td>
                        <asp:TextBox ID="txtPartyCode" runat="server"></asp:TextBox></td>

                    <td>NIC No.</td>
                    <td>
                        <asp:TextBox ID="txtNICNo" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Title</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>

                    <td>First Name</td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>

                    <td>Last Name</td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Communication Address</td>
                    <td>
                        <asp:TextBox ID="txtCommunicationAddressLine1" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtCommunicationAddressLine2" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtCommunicationAddressLine3" runat="server"></asp:TextBox>


                    </td>


                    <td>Phone(Work)</td>
                    <td>
                        <asp:TextBox ID="txtPhoneWork" runat="server"></asp:TextBox></td>

                    <td>Occupation</td>
                    <td>
                        <asp:TextBox ID="txtOccupation" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Prime Circle Member</td>
                    <td>
                        <asp:TextBox ID="txtPrimeCircleMember" runat="server"></asp:TextBox></td>

                    <td>VAT Customer</td>
                    <td>
                        <asp:TextBox ID="txtVATCustomer" runat="server"></asp:TextBox></td>

                    <td>Country</td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Product</td>
                    <td>
                        <asp:TextBox ID="txtProduct" runat="server"></asp:TextBox></td>

                    <td>Policy Duration</td>
                    <td>
                        <asp:TextBox ID="txtPolicyDuration" runat="server"></asp:TextBox></td>

                    <td>Policy Duration Unit</td>
                    <td>
                        <asp:DropDownList ID="ddlPolicyDurationUnit" runat="server"></asp:DropDownList>


                    </td>
                </tr>
                <tr>
                    <td>Policy Start Date</td>
                    <td>
                        <asp:TextBox ID="txtPolicyStartDate" runat="server"></asp:TextBox></td>

                    <td>Expiration/End Date</td>
                    <td>
                        <asp:TextBox ID="txtExpirationEndDate" runat="server"></asp:TextBox></td>

                    <td></td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>
                </tr>

                <tr>
                    <td>Premium Calculation Basis</td>
                    <td>

                        <asp:DropDownList ID="ddlPremiumCalculationBasis" runat="server"></asp:DropDownList>
                    </td>

                    <td>Business Party</td>
                    <td>
                        <asp:DropDownList ID="ddlBusinessParty" runat="server"></asp:DropDownList>

                    </td>

                    <td>VAT Customer</td>
                    <td>
                        <asp:DropDownList ID="ddlIsVATCustomer" runat="server"></asp:DropDownList>

                    </td>

                    <td>Stamp Duty</td>
                    <td>
                        <asp:DropDownList ID="ddlIsStampDuty" runat="server"></asp:DropDownList>

                    </td>
                </tr>

                <tr>
                    <td>Finan INST</td>
                    <td>
                        <asp:TextBox ID="txtFinanINST" runat="server"></asp:TextBox></td>

                    <td>Agent or broker code</td>
                    <td>
                        <asp:TextBox ID="txtAgentOrBrokerCode" runat="server"></asp:TextBox></td>

                    <td>Assurance Code</td>
                    <td>
                        <asp:DropDownList ID="ddlAssuranceCode" runat="server"></asp:DropDownList>
                    </td>

                    <td>Checked Stamp Duty</td>
                    <td>
                        <asp:DropDownList ID="ddlCheckedStampDuty" runat="server"></asp:DropDownList>

                    </td>
                </tr>

                <tr>
                    <td>Vehicle Type</td>
                    <td>
                        <asp:TextBox ID="txtVehicleType" runat="server"></asp:TextBox></td>

                    <td>Class of vehicle (Usage)</td>
                    <td>
                        <asp:TextBox ID="txtClassOfVehicle" runat="server"></asp:TextBox></td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>
                </tr>

                <tr>
                    <td>Pending Registration</td>
                    <td>
                        <asp:DropDownList ID="ddlIsPendingRegistration" runat="server"></asp:DropDownList>

                    </td>
                    <td colspan="6" rowspan="30">

                        <table>
                            <tr>
                                <th></th>
                                <th>Clause</th>
                                <th>Limit</th>
                                <th>Excess</th>
                            </tr>
                            <tr>
                                <td>1</td>

                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE1" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit1" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess1" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE2" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit2" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess2" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE3" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit3" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess3" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE4" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit4" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess4" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>5</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE5" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit5" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess5" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>6</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE6" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit6" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess6" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>7</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE7" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit7" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess7" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>8</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE8" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit8" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess8" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>9</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE9" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit9" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess9" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>10</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE10" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit10" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess10" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>11</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE11" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit11" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess11" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>12</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE12" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit12" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess12" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>13</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE13" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit13" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess13" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>14</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE14" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit14" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess14" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>15</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE15" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit15" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess15" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>16</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE16" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit16" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess16" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>17</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE17" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit17" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess17" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>18</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE18" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit18" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess18" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>19</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE19" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit19" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess19" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>20</td>
                                <td>
                                    <asp:DropDownList ID="ddlMOTOR_CLAUSE20" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtLimit20" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtExcess20" runat="server"></asp:TextBox></td>
                            </tr>

                        </table>

                        <table>
                            <tr>
                                <th colspan="4">Warranty</th>
                            </tr>
                            <tr>
                                <td>Warranty1</td>
                                <td>
                                    <asp:TextBox ID="txtWarranty1" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Warranty2</td>
                                <td>
                                    <asp:TextBox ID="txtWarranty2" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Warranty3</td>
                                <td>
                                    <asp:TextBox ID="txtWarranty3" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Warranty4</td>
                                <td>
                                    <asp:TextBox ID="txtWarranty4" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Warranty5</td>
                                <td>
                                    <asp:TextBox ID="txtWarranty5" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
                            </tr>

                        </table>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Pending Inspection</td>
                    <td>

                        <asp:DropDownList ID="ddlIsPendingInspection" runat="server"></asp:DropDownList>

                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Pending VIC / Copy of registration</td>
                    <td>
                        <asp:DropDownList ID="ddlIsPendingVIC" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Pending Payment</td>
                    <td>
                        <asp:DropDownList ID="ddlIsPendingPayment" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Transfer of Ownership</td>
                    <td>

                        <asp:DropDownList ID="ddlIsTransferOfOwnership" runat="server"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Particulars of Vehicle</td>
                    <td>
                        <asp:DropDownList ID="ddlIsParticularsOfVehicle" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Luxury Semi Luxury Dual Semi Luxury</td>
                    <td>
                        <asp:DropDownList ID="ddlIsLuxurySemi" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Ins. Sign. / Rubber Stamp Documents</td>
                    <td>
                        <asp:DropDownList ID="ddlIsInsSign" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Duly Completed Proposal Form</td>
                    <td>
                        <asp:DropDownList ID="ddlIsDulyCompletedProposalForm" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Cover Note Indicator</td>
                    <td>

                        <asp:DropDownList ID="ddlIsCoverNoteIndicator" runat="server"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Issue Certificate with Pending Requirements</td>
                    <td>
                        <asp:DropDownList ID="ddlIsIssueCertificateWithPendingRequirements" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Special Approval Required</td>
                    <td>
                        <asp:DropDownList ID="ddlIsSpecialApprovalRequired" runat="server"></asp:DropDownList></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Cover Note Expiry Date</td>
                    <td>
                        <asp:TextBox ID="txtCoverNoteExpiryDate" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Cover Note No</td>
                    <td>
                        <asp:TextBox ID="txtCoverNoteNo" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Cover Note Extending Privilege</td>
                    <td>
                        <asp:DropDownList ID="ddlIsCoverNoteExtendingPrivilege" runat="server"></asp:DropDownList>

                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Age of Driver</td>
                    <td>
                        <asp:TextBox ID="txtAgeofDriver" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Sample Vehicle Number</td>
                    <td>
                        <asp:TextBox ID="txtSampleVehicleNumber" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Registration Number</td>
                    <td>
                        <asp:TextBox ID="txtRegistrationNumber" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Make & Model</td>
                    <td>
                        <asp:TextBox ID="txtMakeModel" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Engine No.</td>
                    <td>
                        <asp:TextBox ID="txtEngineNo" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Chassis No.</td>
                    <td>
                        <asp:TextBox ID="txtChassisNo" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Date of First Registration</td>
                    <td>
                        <asp:TextBox ID="txtDateOfFirstRegistration" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Fuel Used</td>
                    <td>
                        <asp:TextBox ID="txtFuelUsed" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Condition</td>
                    <td>
                        <asp:TextBox ID="txtCondition" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Vehicle Mfg. Mth/Year</td>
                    <td>
                        <asp:TextBox ID="txtVehicleMfgMthYea" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Whether trailer attached</td>
                    <td>

                        <asp:DropDownList ID="ddlWhetherTrailerSttached" runat="server"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Extra Fittings</td>
                    <td>
                        <asp:DropDownList ID="ddlExtraFittings" runat="server"></asp:DropDownList>

                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Cubic Capacity in CC</td>
                    <td>
                        <asp:TextBox ID="txtCubicCapacityinCC" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Inst. with financial interest</td>
                    <td>

                        <asp:TextBox ID="txtInstWithFinancialInterest" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Registered Owner of Vehicle Y/N</td>
                    <td>

                        <asp:DropDownList ID="ddlIsRegisteredOwnerOfVehicleYN" runat="server"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Name of the registered owner</td>
                    <td>
                        <asp:TextBox ID="txtNameOfTheRegisteredOwner" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>




            </table>

        </div>
    </form>
</body>
</html>
