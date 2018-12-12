<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="quickinfo_v2.Views.RuleBase.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        $(document).ready(function() {
 
            // Initialise the first table (as before)
            $("#table-1").tableDnD();
 
            // Make a nice striped effect on the table
            $("#table-2 tr:even').addClass('alt')");
 
            // Initialise the second table specifying a dragClass and an onDrop function that will display an alert
            $("#table-2").tableDnD({
                onDragClass: "myDragClass",
                onDrop: function(table, row) {
                    var rows = table.tBodies[0].rows;
                    var debugStr = "Row dropped was "+row.id+". New order: ";
                    for (var i=0; i<rows.length; i++) {
                        debugStr += rows[i].id+" ";
                    }
                    $(#debugArea).html(debugStr);
                },
                onDragStart: function(table, row) {
                    $(#debugArea).html("Started dragging row "+row.id);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="table-1" cellspacing="0" cellpadding="2">
                <tr id="1">
                    <td>1</td>
                    <td>One</td>
                    <td>
                        <input type="text" name="three" value="three"></td>
                </tr>
                <tr id="2">
                    <td>2</td>
                    <td>Two</td>
                    <td>
                        <input type="text" name="three" value="three"></td>
                </tr>
                <tr id="3">
                    <td>3</td>
                    <td>Three</td>
                    <td>
                        <input type="text" name="three" value="three"></td>
                </tr>
                <tr id="4">
                    <td>4</td>
                    <td>Four</td>
                    <td>some text</td>
                </tr>
                <tr id="5">
                    <td>5</td>
                    <td>Five</td>
                    <td>some text</td>
                </tr>
                <tr id="6">
                    <td>6</td>
                    <td>Six</td>
                    <td>some text</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
