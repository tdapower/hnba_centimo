<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="testpage.aspx.cs" Inherits="quickinfo_v2.Views.MNBNewBusinessWF.testpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        /* Custom dialog styles */
        #popup_container.style_1 {
            font-family: Georgia, serif;
            color: #A4C6E2;
            background: #005294;
            border-color: #113F66;
        }

            #popup_container.style_1 #popup_title {
                color: #FFF;
                font-weight: normal;
                text-align: left;
                background: #76A5CC;
                border: solid 1px #005294;
                padding-left: 1em;
            }

            #popup_container.style_1 #popup_content {
                background: none;
            }

            #popup_container.style_1 #popup_message {
                padding-left: 0em;
            }

            #popup_container.style_1 INPUT[type='button'] {
                border: outset 2px #76A5CC;
                color: #A4C6E2;
                background: #3778AE;
            }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#alert_button").click(function () {
                jAlert('This is a custom alert box', 'Alert Dialog');
            });

            $("#confirm_button").click(function () {
                jConfirm('Can you confirm this?', 'Confirmation Dialog', function (r) {
                    jAlert('Confirmed: ' + r, 'Confirmation Results');
                });
            });

            $("#prompt_button").click(function () {
                jPrompt('Type something:', 'Prefilled value', 'Prompt Dialog', function (r) {
                    if (r) alert('You entered ' + r);
                });
            });

            $("#alert_button_with_html").click(function () {
                jAlert('You can use HTML, such as <strong>bold</strong>, <em>italics</em>, and <u>underline</u>!');
            });

            $(".alert_style_example").click(function () {
                $.alerts.dialogClass = $(this).attr('id'); // set custom style class
                jAlert('This is the custom class called &ldquo;style_1&rdquo;', 'Custom Styles', function () {
                    $.alerts.dialogClass = null; // reset to default
                });
            });
        });

    </script>
    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">


            <div class="panel panel-primary" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Search Uploaded Proposal
                    </div>


                </div>

                <div class="panel-body">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="form-group">
                        <label for="txtSearchQuotationNo" class="col-sm-3 control-label">Quotation No</label>
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
                            <input id="alert_button" type="button" value="Show Alert" />
                            <asp:Button ID="btnTest" runat="server"
                                Text="Search" CssClass="btn btn-apps" OnClick="btnTest_Click" />


                        </div>
                    </div>





                </div>
            </div>


        </form>
    </div>


</asp:Content>
