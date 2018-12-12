<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserRequestView.aspx.cs" Inherits="quickinfo_v2.Views.ITWorkflow.UserRequestView" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

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

    <%--<script type="text/javascript">
        function findImageBox() {
            var receiverID = '';
            var inputs = document.getElementsByTagName("img");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].getAttribute("id") == ('UploadedImage')) {
                    receiverID = inputs[i].id;
                    document.getElementById('<%=txtFileName.ClientID %>').value = inputs[i].getAttribute("src");

                    
                    break;
                }
            }
        }
    </script>--%>

    <div class="panel-body">
        <form role="form" class="form-horizontal form-groups-bordered" runat="server">

            <div class="panel panel-default" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Search Main Menus
                    </div>
                    
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>

                <div class="panel-body">


                    <div class="form-group">
                        <label for="txtSearchRefNo" class="col-sm-3a control-label">Reference No.</label>

                        <div class="col-sm-5a">
                        <telerik:RadTextBox ID="txtSearchSubMenuName" Runat="server" Skin="Office2007" Width="99%" EmptyMessage="Enter Policy/Proposal/Receipt or Debit..">
                        </telerik:RadTextBox>
                 
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-5a">
                                 <telerik:RadButton ID="btnSearch1" runat="server" Skin="Office2007" Text="Search" Width="100px">
                                                          </telerik:RadButton>
                                                          <telerik:RadButton ID="btnClear1" runat="server" Skin="Office2007" Text="Clear" Width="100px">
                                                          </telerik:RadButton>

                        </div>
                    </div>


                </div>
            </div>



            <div class="panel panel-default" data-collapsed="0">


                <div class="panel-body">



                    <div class="form-group">

                     <telerik:RadGrid ID="grdSubMenus" runat="server" Skin="Office2007">
                                             <MasterTableView>
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                <telerik:GridTemplateColumn FilterControlAltText="Filter Select column" HeaderText="Select"
                    UniqueName="Select">
                    <ItemTemplate>
                        <asp:LinkButton ID="LnkBtnSelect" runat="server" CommandName="Select">Select</asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle AlwaysVisible="True" />
                    </MasterTableView>
                     </telerik:RadGrid>
                        <div class="col-sm-5a">
                        </div>
                    </div>


                </div>

            </div>


            <div class="panel panel-default" data-collapsed="0">

                <div class="panel-heading">
                    <div class="panel-title">
                        Manage User Requests
                    </div>


                </div>
                <div class="panel-body"">


                    <div class="form-group">
                        <label for="txtRefNo" class="col-sm-3a control-label">Select System</label>

                        <div class="col-sm-5a">
                                                  <telerik:RadComboBox ID="ddlSearchMainMenu0" Runat="server" Skin="Office2007" Width="99%">
                                                  </telerik:RadComboBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="txtJobRmarks" class="col-sm-3a control-label">Referance No</label>

                        <div class="col-sm-5a">
                                      <telerik:RadTextBox ID="txtSearchSubMenuName0" Runat="server" Skin="Office2007" Width="99%" EmptyMessage="Enter Policy/Proposal/Receipt or Debit..">
                                      </telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3a control-label">Job Remarks</label>

                        <div class="col-sm-5a">
                                 <telerik:RadTextBox ID="txtSubMenuName" Runat="server" Skin="Office2007" Width="99%" TextMode="MultiLine">
                                 </telerik:RadTextBox>

                        </div>
                    </div>
                       <div class="form-group">
                        <label class="col-sm-3a control-label">Image/Documents Upload</label>

                        <div class="col-sm-5a">
                                                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" OnClientFileSelected="" OnClientFilesSelected="" Skin="Office2007" Width="50%">
                                                         <Localization Select="Browse" />
                                                     </telerik:RadAsyncUpload>

                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-5a">

                                                          <telerik:RadButton ID="btnAddNew" runat="server" Skin="Office2007" Text="Insert" Width="100px">
                                                          </telerik:RadButton>
                                                          <telerik:RadButton ID="btnAlter" runat="server" Skin="Office2007" Text="Update" Width="100px" Enabled="False">
                                                          </telerik:RadButton>
                                                          <telerik:RadButton ID="btnCancel" runat="server" Skin="Office2007" Text="Clear" Width="100px">
                                                          </telerik:RadButton>

                        </div>
                    </div>
                </div>

            </div>

        </form>
    </div>
</asp:Content>
