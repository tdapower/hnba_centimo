<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="quickinfo_v2.Views.RuleBase.WebForm1" %>

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
    <script src="../../Scripts/jquery-1.11.0.js"></script>
    <style>
        #sortable1, #sortable2 {
            list-style-type: none;
            margin: 0;
            padding: 0 0 2.5em;
            float: left;
            margin-right: 10px;
        }

            #sortable1 li, #sortable2 li {
                margin: 0 5px 5px 5px;
                padding: 5px;
                font-size: 1.2em;
                width: 120px;
            background-color:blueviolet;
            }
    </style>
    <script>
        $(function () {
            $("#sortable1, #sortable2").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();
        });
    </script>


    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">




            <div class="panel panel-default" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Manage User Requests
                    </div>


                </div>
                <div class="panel-body">


                    <div class="form-group">


                          <input type="text" name="three" value="three">
                        <ul id="sortable1" class="connectedSortable">
                            <li class="ui-state-default">Item 1</li>
                            <li class="ui-state-default">Item 2</li>
                            <li class="ui-state-default">Item 3</li>
                            <li class="ui-state-default">Item 4</li>
                            <li class="ui-state-default">Item 5</li>
                        </ul>

                        <ul id="sortable2" class="connectedSortable">
                            <li class="ui-state-highlight">Item 1</li>
                            <li class="ui-state-highlight">Item 2</li>
                            <li class="ui-state-highlight">Item 3</li>
                            <li class="ui-state-highlight">Item 4</li>
                            <li class="ui-state-highlight">Item 5</li>
                        </ul>

                    </div>


                </div>

            </div>

        </form>
    </div>
</asp:Content>
