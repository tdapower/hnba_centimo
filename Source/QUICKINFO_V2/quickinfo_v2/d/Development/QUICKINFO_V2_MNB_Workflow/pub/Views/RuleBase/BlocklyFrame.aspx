<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlocklyFrame.aspx.cs" Inherits="quickinfo_v2.Views.RuleBase.BlocklyFrame" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">

    <script type="text/javascript" src="../../Scripts/Blockly/blockly_compressed.js"></script>
    <script type="text/javascript" src="../../Scripts/Blockly/blocks_compressed.js"></script>
    <script type="text/javascript" src="../../Scripts/Blockly/msg/js/en.js"></script>


    <style>
        html, body {
            background-color: #fff;
            margin: 0;
            padding: 0;
            overflow: hidden;
            height: 100%;
        }

        .blocklySvg {
            height: 100%;
            width: 100%;
        }
    </style>
    <script>
        function init() {
            Blockly.inject(document.body,
                { path: '../../', toolbox: document.getElementById('toolbox') });
            // Let the top-level application know that Blockly is ready.
            window.parent.blocklyLoaded(Blockly);
        }
    </script>
</head>
<body onload="init()">
    <xml id="toolbox" style="display: none">
    <category name="Blocks">
      <block type="logic_boolean"></block>
      <block type="math_number">
        <field name="NUM">42</field>
      </block>
      <block type="controls_for">
        <value name="FROM">
          <block type="math_number">
            <field name="NUM">1</field>
          </block>
        </value>
        <value name="TO">
          <block type="math_number">
            <field name="NUM">10</field>
          </block>
        </value>
      </block>
    </category>
  </xml>
</body>
</html>
