<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="neontest.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel-body">

        <div class="row">
            <div class="col-md-12">

                <div class="panel panel-primary" data-collapsed="0">

                    <div class="panel-heading">
                        <div class="panel-title">
                            Date Range Picker
			
                        </div>

                        <div class="panel-options">
                            <a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>
                            <a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
                            <a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
                            <a href="#" data-rel="close"><i class="entypo-cancel"></i></a>
                        </div>
                    </div>

                    <div class="panel-body">

                        <form role="form" class="form-horizontal form-groups-bordered">

                            <div class="form-group">
                                <label class="col-sm-3 control-label">Date Range Picker</label>

                                <div class="col-sm-5">

                                    <input type="text" class="form-control daterange" />

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">Selected Date Range</label>

                                <div class="col-sm-5">

                                    <input type="text" class="form-control daterange" data-format="YYYY-MM-DD" data-start-date="2013-08-02" data-end-date="2013-08-15" data-separator="," />

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">Disabled Date Range</label>

                                <div class="col-sm-5">

                                    <input type="text" class="form-control daterange" data-min-date="12-14-2013" data-max-date="12-26-2013" />

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">Date Range Callback</label>

                                <div class="col-sm-5">

                                    <input type="text" class="form-control daterange" data-callback />

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">Date Range with Timepicker</label>

                                <div class="col-sm-5">

                                    <input type="text" class="form-control daterange" data-time-picker="true" data-time-picker-increment="5" data-format="MM/DD/YYYY h:mm A" />

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">Date Range Inline</label>

                                <div class="col-sm-5">

                                    <div class="daterange daterange-inline" data-format="MMMM D, YYYY" data-start-date="December 6, 2013" data-end-date="December 20, 2013">
                                        <i class="entypo-calendar"></i>
                                        <span>December 6, 2013 - December 20, 2013</span>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">Date Range w/ Predefined Ranges</label>

                                <div class="col-sm-5">

                                    <div class="daterange daterange-inline add-ranges" data-format="MMMM D, YYYY" data-start-date="December 13, 2013" data-end-date="January 4, 2014">
                                        <i class="entypo-calendar"></i>
                                        <span>December 13, 2013 - January 4, 2014</span>
                                    </div>

                                </div>
                            </div>

                        </form>

                    </div>

                </div>

            </div>
        </div>


        <div class="panel-body">
            <form role="form" class="form-horizontal form-groups-bordered" id="form1">

                <div class="form-group">
                    <label for="field-1" class="col-sm-3 control-label">Field 1</label>

                    <div class="col-sm-5">
                        <input type="text" class="form-control" id="field-1" placeholder="Placeholder">
                    </div>
                </div>


                <div class="form-group">
                    <label for="field-121" class="col-sm-3 control-label">Field aaa1</label>

                    <div class="col-sm-5">
                        <input type="text" class="form-control" id="field-22" placeholder="Placeholder">
                    </div>
                </div>


                <div class="form-group">
                    <label for="field-2" class="col-sm-3 control-label">Disabled</label>

                    <div class="col-sm-5">
                        <input type="text" class="form-control" id="field-2" placeholder="Placeholder" disabled>
                    </div>
                </div>

                <div class="form-group">
                    <label for="field-3" class="col-sm-3 control-label">Password</label>

                    <div class="col-sm-5">
                        <input type="password" class="form-control" id="field-3" placeholder="Placeholder (Password)">
                    </div>
                </div>

                <div class="form-group">
                    <label for="field-1" class="col-sm-3 control-label">File Field</label>

                    <div class="col-sm-5">
                        <input type="file" class="form-control" id="field-file" placeholder="Placeholder">
                    </div>
                </div>





                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-5">
                        <button type="submit" class="btn btn-default">Sign in</button>
                    </div>
                </div>
            </form>
        </div>



    </div>

</asp:Content>
