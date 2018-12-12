<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentUploader.aspx.cs" Inherits="quickinfo_v2.Views.BookManagement.DocUpload.DocumentUploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/jquery-ui/css/no-theme/jquery-ui-1.10.3.custom.min.css" id="style_resource_1"/>
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/entypo.css" id="style_resource_2"/>
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/font-icons/entypo/css/animation.css" id="style_resource_3"/>
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/neon.css" id="style_resource_5"/>
    <link rel="stylesheet" href="~/Styles/neon-x/assets/css/custom.css" id="style_resource_6"/>

    <script type="text/javascript" src="../../JQuery/jquery-1.8.2.min.js"></script>


    <script src="../../../Scripts/drop_zone/dropzone-amd-module.js"></script>
    <script src="../../../Scripts/drop_zone/dropzone.js"></script>

    <link href="../../../Styles/drop_zone/dropzone.css" rel="stylesheet" />
    <link href="../../../Styles/drop_zone/StyleSheet.css" rel="stylesheet" />


    <script type="text/javascript">




        Dropzone.options.myDropzone = {

            url: "DocumentUploader.aspx?tid=<%=Request.QueryString["tid"]%>",
            // Prevents Dropzone from uploading dropped files immediately
            autoProcessQueue: false,

            init: function () {
                var submitButton = document.querySelector("#submit-all")
                myDropzone = this; // closure

                submitButton.addEventListener("click", function () {

                    myDropzone.processQueue(); // Tell Dropzone to process all queued files.
                });

                // You might want to show the submit button only when 
                // files are dropped here:
                this.on("addedfile", function () {
                    // Show submit button here and/or inform user to click it.
                });
                this.on("processing", function () {
                    this.options.autoProcessQueue = true;
                });


            }

        };
    </script>
</head>
<body>
    <div>
        <form action="/target" class="dropzone" id="my-dropzone" style="height: 550px;">
        </form>

        <button id="submit-all" class="btn btn-apps">
            Upload Documents</button>



    </div>
</body>
</html>
