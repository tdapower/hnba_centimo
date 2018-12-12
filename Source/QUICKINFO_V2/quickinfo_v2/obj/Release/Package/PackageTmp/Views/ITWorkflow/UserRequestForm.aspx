<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserRequestForm.aspx.cs" Inherits="quickinfo_v2.Views.ITWorkflow.UserRequestForm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <form role="form" class="form-horizontal form-groups-bordered" runat="server">

                                                               <div style="width: 100%; height: 20px;">                    
                                               <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                               </div>
              <div class="panel panel-default">

                    <div class="panel-heading">
                    <div class="panel-title">
                        Search User Requests
                    </div>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                                       </div>
            <div class="panel-body">


                                                 <div style="width: 100%; height: 40px;">                    
                                                 <div style="width: 20%; height: 30px;  float: left;">Referance No</div>
                                                 <div style="width: 40%; height: 30px; float: left;">
                                                     <telerik:RadTextBox ID="txtSearchSubMenuName" Runat="server" Skin="Office2007" Width="99%" EmptyMessage="Enter Polycy/Proposal/Receipt or Debit..">
                                                     </telerik:RadTextBox>
                                                     </div>      
                                                </div>
                                                  <div style="width: 100%;height: 40px; ">                    
                                                 <div style="width: 20%;height: 30px;  float: left;"></div>
                                                 <div style="width: 80%;height: 30px;  float: left;">

                                                          <telerik:RadButton ID="btnSearch1" runat="server" Skin="Office2007" Text="Search" OnClick="btnSearch1_Click" Width="100px">
                                                          </telerik:RadButton>
                                                          <telerik:RadButton ID="btnClear1" runat="server" Skin="Office2007" Text="Clear" Width="100px">
                                                          </telerik:RadButton>
                                 

                                                 </div>      
                                                </div>
                 <div style="width: 100%; ">

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
                     </div>
                         
                             
                                </div>
                                                      <div class="panel-heading">
                    <div class="panel-title">
                        Manage User Requests
                    </div>


                </div>
                <div class="panel-body">

                                                  <div style="width: 100%; height: 40px;">                    
                                                 <div style="width: 20%; height: 30px; float: left;">Select System</div>
                                                 <div style="width: 40%; height: 30px; float: left;">
                                                     <telerik:RadComboBox ID="ddlSearchMainMenu0" Runat="server" Skin="Office2007" Width="99%">
                                                     </telerik:RadComboBox>
                                                     </div>      
                                                </div>
                                                 <div style="width: 100%; height: 40px;">                    
                                                 <div style="width: 20%; height: 30px; float: left;">Referance No</div>
                                                 <div style="width: 40%; height: 30px; float: left;">
                                                     <telerik:RadTextBox ID="txtSearchSubMenuName0" Runat="server" Skin="Office2007" Width="99%" EmptyMessage="Enter Polycy/Proposal/Receipt or Debit..">
                                                     </telerik:RadTextBox>
                                                     </div>      
                                                </div>
                                                                     <div style="width: 100%;">                    
                                                 <div style="width: 20%; float: left;">Image/Document Upload</div>
                                                 <div style="width: 40%; float: left;">
                                                     <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" OnClientFileSelected="" OnClientFilesSelected="" Skin="Office2007" Width="290px">
                                                         <Localization Select="Browse" />
                                                     </telerik:RadAsyncUpload>
                                                      </div>      
                                                </div>
                                                  <div style="width: 100%; height: 70px;">                    
                                                 <div style="width: 20%; height: 30px; float: left;">Job Remarks</div>
                                                 <div style="width: 40%;height: 60px;  float: left;">
                                                     <telerik:RadTextBox ID="txtSubMenuName" Runat="server" Skin="Office2007" Width="99%" TextMode="MultiLine">
                                                     </telerik:RadTextBox>
                                                      </div>      
                                                </div>

                                                  <div style="width: 100%; height: 40px;">                    
                                                 <div style="width: 20%; height: 30px; float: left;"></div>
                                                 <div style="width: 80%; height: 30px; float: left;">
                                                  
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
</asp:Content>
