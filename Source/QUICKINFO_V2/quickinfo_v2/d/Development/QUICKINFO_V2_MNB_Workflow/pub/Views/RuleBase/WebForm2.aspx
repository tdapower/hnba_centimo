<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="quickinfo_v2.Views.RuleBase.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function jsValidateNum(obj) {
            if (isNaN(obj.value)) {
                alert('Numeric Expected');
                obj.value = ''
                obj.focus()
            }
        }
    </script>
    <script src="../../Scripts/Blockly/blockly_compressed.js"></script>
    <script src="../../Scripts/Blockly/blocks_compressed.js"></script>
    <script src="../../Scripts/Blockly/msg/js/en.js"></script>

    <script type="text/javascript">
        var toolbox = '<xml>';
        toolbox += '  <block type="controls_if"></block>';
        toolbox += '  <block type="controls_whileUntil"></block>';
        toolbox += '</xml>';
        Blockly.inject(document.getElementById('blocklyDiv'),
            { path: './', toolbox: toolbox });
    </script>
    <script type="text/javascript">
        Blockly.inject(document.getElementById('blocklyDiv'),
            { path: './', toolbox: document.getElementById('toolbox') });
    </script>

    <div id="blocklyDiv" style="height: 480px; width: 600px;"></div>
    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">




            <div class="panel panel-default" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Manage User Requests
                    </div>


                </div>
                <div class="panel-body">
                </div>

            </div>

        </form>
    </div>
</asp:Content>
