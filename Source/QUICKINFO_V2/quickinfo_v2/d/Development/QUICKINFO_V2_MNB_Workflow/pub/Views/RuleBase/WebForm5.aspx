<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="quickinfo_v2.Views.RuleBase.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery-1.11.0.js"></script>
    <link href="StyleSheet1.css" rel="stylesheet" />

    <script src="../../Scripts/jquery/1.2.6/jquery.min.js"></script>
    <script src="../../Scripts/jqueryui/1.5.2/jquery-ui.min.js"></script>


    <script type="text/javascript">

        $(document).ready(function () {
            $("#DragWordList li").draggable({ helper: 'clone' });
            $(".txtDropTarget").droppable({
                accept: "#DragWordList li",
                drop: function (ev, ui) {
                    $(this).insertAtCaret(ui.draggable.text());


                }
            });
        });

        $.fn.insertAtCaret = function (myValue) {
            return this.each(function () {
                //IE support
                if (document.selection) {
                    this.focus();
                    sel = document.selection.createRange();
                    sel.text = myValue;
                    this.focus();
                }
                    //MOZILLA / NETSCAPE support
                else if (this.selectionStart || this.selectionStart == '0') {
                    var startPos = this.selectionStart;
                    var endPos = this.selectionEnd;
                    var scrollTop = this.scrollTop;
                    this.value = this.value.substring(0, startPos) + myValue + this.value.substring(endPos, this.value.length);
                    this.focus();
                    this.selectionStart = startPos + myValue.length;
                    this.selectionEnd = startPos + myValue.length;
                    this.scrollTop = scrollTop;
                } else {
                    this.value += myValue;
                    this.focus();
                }
            });
        };
    </script>
    <style type="text/css" media="Screen">
        #leftcolumn li {
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="leftcolumn">

            <fieldset>
                <legend>Drag to insert:</legend>
                <ul id="DragWordList">
                    <li>Flood Cover</li>
                    <li>GWP</li>
                    <li>Basic Cover</li>
                    <li>=</li>
                    <li>+</li>
                    <li>*</li>
                    <li>/</li>
                     <li>()</li>
                </ul>
            </fieldset>
        </div>
        <div id="contentwrapper">
            <div id="maincolumn">
                <div class="text">
                    <h2>Rule</h2>
                    <textarea name="txtMessage" id="txtMessage" class="txtDropTarget" cols="80" rows="15" readonly="true"></textarea>
                   
                </div>
            </div>
        </div>

    </form>
</body>
</html>
