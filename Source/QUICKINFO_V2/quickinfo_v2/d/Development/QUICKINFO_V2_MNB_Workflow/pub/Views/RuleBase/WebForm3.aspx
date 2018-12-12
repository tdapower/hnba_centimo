<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="quickinfo_v2.Views.RuleBase.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

   

     <script>
         function blocklyLoaded(blockly) {
             // Called once Blockly is fully loaded.
             window.Blockly = blockly;
         }
  </script>
  


</head>
<body>
    <form id="form1" runat="server">
       <iframe src="BlocklyFrame.aspx" width="800px" height="600px"></iframe>
    </form>
</body>
</html>
