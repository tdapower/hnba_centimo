<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserAdminDashboard.aspx.cs" Inherits="UserAdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <div class="main-content">


        <script type="text/javascript">
            jQuery(document).ready(function ($) {
                // Sample Toastr Notification
                setTimeout(function () {
                    var opts = {
                        "closeButton": true,
                        "debug": false,
                        "positionClass": "toast-top-right",
                        "toastClass": "black",
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    };

                    toastr.success("You have been awarded with 1 year free subscription. Enjoy it!", "Account Subcription Updated", opts);
                }, 3000);


                // Sparkline Charts
                $('.inlinebar').sparkline('html', { type: 'bar', barColor: '#ff6264' });
                $('.inlinebar-2').sparkline('html', { type: 'bar', barColor: '#445982' });
                $('.inlinebar-3').sparkline('html', { type: 'bar', barColor: '#00b19d' });
                $('.bar').sparkline([[1, 4], [2, 3], [3, 2], [4, 1]], { type: 'bar' });
                $('.pie').sparkline('html', { type: 'pie', borderWidth: 0, sliceColors: ['#3d4554', '#ee4749', '#00b19d'] });
                $('.linechart').sparkline();
                $('.pageviews').sparkline('html', { type: 'bar', height: '30px', barColor: '#ff6264' });
                $('.uniquevisitors').sparkline('html', { type: 'bar', height: '30px', barColor: '#00b19d' });


                $(".monthly-sales").sparkline([1, 2, 3, 5, 6, 7, 2, 3, 3, 4, 3, 5, 7, 2, 4, 3, 5, 4, 5, 6, 3, 2], {
                    type: 'bar',
                    barColor: '#485671',
                    height: '80px',
                    barWidth: 10,
                    barSpacing: 2
                });


                // JVector Maps
                var map = $("#map");

                map.vectorMap({
                    map: 'europe_merc_en',
                    zoomMin: '3',
                    backgroundColor: '#383f47',
                    focusOn: { x: 0.5, y: 0.8, scale: 3 }
                });



                // Line Charts




            

            


            
            });


            function getRandomInt(min, max) {
                return Math.floor(Math.random() * (max - min + 1)) + min;
            }
        </script>


        <div class="row">
            <div class="col-sm-3">

                <div class="tile-stats tile-red">
                    <div class="icon"><i class="entypo-users"></i></div>

                    <asp:Literal ID="ltrlActiveUsers" runat="server" />
                    <h3>Active users</h3>
                    <p>currently available in QuickInfo</p>
                </div>

            </div>

            <div class="col-sm-3">

                <div class="tile-stats tile-green">
                    <div class="icon"><i class="entypo-chart-bar"></i></div>

                    <asp:Literal ID="ltrlSystemPages" runat="server" />
                    <h3>Application Pages</h3>
                    <p>contains</p>
                </div>

            </div>

            <div class="col-sm-3">

                <div class="tile-stats tile-aqua">
                    <div class="icon"><i class="entypo-users"></i></div>

                    <asp:Literal ID="ltrlUserRoles" runat="server" />
                    <h3>User Roles</h3>
                    <p>to manage users</p>
                </div>

            </div>

        </div>

        <br />

        <div class="row">
            <div class="col-sm-8">

                <div class="panel panel-primary" id="charts_env">

                    <div class="panel-heading">
                        <div class="panel-title">Site Stats</div>

                        <div class="panel-options">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#user-role-bar-chart" data-toggle="tab">User Role Stats</a></li>
                               
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">

                        <div class="tab-content">

                         
                            <div class="tab-pane active" id="user-role-bar-chart">
                                <div id="user-role-bar-chart-view" class="morrischart" style="height: 300px"></div>
                            </div>


                        </div>

                    </div>

                    <table class="table table-bordered table-responsive">

                        <thead>
                            <tr>
                                <th width="50%" class="col-padding-1">
                                    <div class="pull-left">
                                        <div class="h4 no-margin">Pageviews</div>
                                        <small>54,127</small>
                                    </div>
                                    <span class="pull-right pageviews">4,3,5,4,5,6,5</span>

                                </th>
                                <th width="50%" class="col-padding-1">
                                    <div class="pull-left">
                                        <div class="h4 no-margin">Unique Visitors</div>
                                        <small>25,127</small>
                                    </div>
                                    <span class="pull-right uniquevisitors">2,3,5,4,3,4,5</span>
                                </th>
                            </tr>
                        </thead>

                    </table>

                </div>

            </div>

    
        </div>


        <br />

        <div class="row">

            <div class="col-sm-4">

                <div class="panel panel-primary">
                    <table class="table table-bordered table-responsive">
                        <thead>
                            <tr>
                                <th class="padding-bottom-none text-center">
                                    <br />
                                    <br />
                                    <span class="monthly-sales"></span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="panel-heading">
                                    <h4>Monthly Sales</h4>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            </div>

           

        </div>

        <br />


        <script type="text/javascript">
            // Code used to add Todo Tasks
            jQuery(document).ready(function ($) {
                var $todo_tasks = $("#todo_tasks");

                $todo_tasks.find('input[type="text"]').on('keydown', function (ev) {
                    if (ev.keyCode == 13) {
                        ev.preventDefault();

                        if ($.trim($(this).val()).length) {
                            var $todo_entry = $('<li><div class="checkbox checkbox-replace color-white"><input type="checkbox" /><label>' + $(this).val() + '</label></div></li>');
                            $(this).val('');

                            $todo_entry.appendTo($todo_tasks.find('.todo-list'));
                            $todo_entry.hide().slideDown('fast');
                            replaceCheckboxes();
                        }
                    }
                });
            });
        </script>

    
        <!-- Footer -->
        <footer class="main">

            <div class="pull-right">
                <a href="http://themeforest.net/item/neon-bootstrap-admin-theme/6434477" target="_blank"><strong>Purchase this theme for $21</strong></a>
            </div>

            &copy; 2013 <strong>Neon</strong> Admin Theme by <a href="http://laborator.co/" target="_blank">Laborator</a>

        </footer>
    </div>







    <div id="chat" class="fixed" data-current-user="Art Ramadani" data-order-by-status="1" data-max-chat-history="25">

        <div class="chat-inner">


            <h2 class="chat-header">
                <a href="#" class="chat-close" data-animate="1"><i class="entypo-cancel"></i></a>

                <i class="entypo-users"></i>
                Chat
		
                        <span class="badge badge-success is-hidden">0</span>
            </h2>


            <div class="chat-group" id="group-1">
                <strong>Favorites</strong>

                <a href="#" id="sample-user-123" data-conversation-history="#sample_history"><span class="user-status is-online"></span><em>Catherine J. Watkins</em></a>
                <a href="#"><span class="user-status is-online"></span><em>Nicholas R. Walker</em></a>
                <a href="#"><span class="user-status is-busy"></span><em>Susan J. Best</em></a>
                <a href="#"><span class="user-status is-offline"></span><em>Brandon S. Young</em></a>
                <a href="#"><span class="user-status is-idle"></span><em>Fernando G. Olson</em></a>
            </div>


            <div class="chat-group" id="group-2">
                <strong>Work</strong>

                <a href="#"><span class="user-status is-offline"></span><em>Robert J. Garcia</em></a>
                <a href="#" data-conversation-history="#sample_history_2"><span class="user-status is-offline"></span><em>Daniel A. Pena</em></a>
                <a href="#"><span class="user-status is-busy"></span><em>Rodrigo E. Lozano</em></a>
            </div>


            <div class="chat-group" id="group-3">
                <strong>Social</strong>

                <a href="#"><span class="user-status is-busy"></span><em>Velma G. Pearson</em></a>
                <a href="#"><span class="user-status is-offline"></span><em>Margaret R. Dedmon</em></a>
                <a href="#"><span class="user-status is-online"></span><em>Kathleen M. Canales</em></a>
                <a href="#"><span class="user-status is-offline"></span><em>Tracy J. Rodriguez</em></a>
            </div>

        </div>

        <!-- conversation template -->
        <div class="chat-conversation">

            <div class="conversation-header">
                <a href="#" class="conversation-close"><i class="entypo-cancel"></i></a>

                <span class="user-status"></span>
                <span class="display-name"></span>
                <small></small>
            </div>

            <ul class="conversation-body">
            </ul>

            <div class="chat-textarea">
                <textarea class="form-control autogrow" placeholder="Type your message"></textarea>
            </div>

        </div>

    </div>


    <!-- Chat Histories -->
    <ul class="chat-history" id="sample_history">
        <li>
            <span class="user">Art Ramadani</span>
            <p>Are you here?</p>
            <span class="time">09:00</span>
        </li>

        <li class="opponent">
            <span class="user">Catherine J. Watkins</span>
            <p>This message is pre-queued.</p>
            <span class="time">09:25</span>
        </li>

        <li class="opponent">
            <span class="user">Catherine J. Watkins</span>
            <p>Whohoo!</p>
            <span class="time">09:26</span>
        </li>

        <li class="opponent unread">
            <span class="user">Catherine J. Watkins</span>
            <p>Do you like it?</p>
            <span class="time">09:27</span>
        </li>
    </ul>




    <!-- Chat Histories -->
    <ul class="chat-history" id="sample_history_2">
        <li class="opponent unread">
            <span class="user">Daniel A. Pena</span>
            <p>I am going out.</p>
            <span class="time">08:21</span>
        </li>

        <li class="opponent unread">
            <span class="user">Daniel A. Pena</span>
            <p>Call me when you see this message.</p>
            <span class="time">08:27</span>
        </li>
    </ul>


    <!-- Sample Modal (Default skin) -->
    <div class="modal fade" id="sample-modal-dialog-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Widget Options - Default Modal</h4>
                </div>

                <div class="modal-body">
                    <p>Now residence dashwoods she excellent you. Shade being under his bed her. Much read on as draw. Blessing for ignorant exercise any yourself unpacked. Pleasant horrible but confined day end marriage. Eagerness furniture set preserved far recommend. Did even but nor are most gave hope. Secure active living depend son repair day ladies now.</p>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Sample Modal (Skin inverted) -->
    <div class="modal invert fade" id="sample-modal-dialog-2">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Widget Options - Inverted Skin Modal</h4>
                </div>

                <div class="modal-body">
                    <p>Now residence dashwoods she excellent you. Shade being under his bed her. Much read on as draw. Blessing for ignorant exercise any yourself unpacked. Pleasant horrible but confined day end marriage. Eagerness furniture set preserved far recommend. Did even but nor are most gave hope. Secure active living depend son repair day ladies now.</p>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Sample Modal (Skin gray) -->
    <div class="modal gray fade" id="sample-modal-dialog-3">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Widget Options - Gray Skin Modal</h4>
                </div>

                <div class="modal-body">
                    <p>Now residence dashwoods she excellent you. Shade being under his bed her. Much read on as draw. Blessing for ignorant exercise any yourself unpacked. Pleasant horrible but confined day end marriage. Eagerness furniture set preserved far recommend. Did even but nor are most gave hope. Secure active living depend son repair day ladies now.</p>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>

    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/jvectormap/jquery-jvectormap-1.2.2.css" id="Link1">
    <link rel="stylesheet" href="~/Styles/neon-x/assets/js/rickshaw/rickshaw.min.css" id="Link2">

    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/gsap/main-gsap.js")%>' id="script-resource-1"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery-ui/js/jquery-ui-1.10.3.minimal.min.js")%>' id="script-resource-2"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/bootstrap.min.js")%>' id="script-resource-3"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/joinable.js")%>' id="script-resource-4"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/resizeable.js")%>' id="script-resource-5"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-api.js")%>' id="script-resource-6"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/jvectormap/jquery-jvectormap-1.2.2.min.js")%>' id="script-resource-7"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/jvectormap/jquery-jvectormap-europe-merc-en.js")%>' id="script-resource-8"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/jquery.sparkline.min.js")%>' id="script-resource-9"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/rickshaw/vendor/d3.v3.js")%>' id="script-resource-10"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/rickshaw/rickshaw.min.js")%>' id="script-resource-11"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/raphael-min.js")%>' id="script-resource-12"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/morris.min.js")%>' id="script-resource-13"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/toastr.js")%>' id="script-resource-14"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-chat.js")%>' id="script-resource-15"></script>
    <script src='~/Styles/neon-x/assets/js/neon-custom.js' id="script-resource-16"></script>
    <script src='<%# Page.ResolveUrl("~/Styles/neon-x/assets/js/neon-demo.js")%>' id="script-resource-17"></script>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-28991003-3']);
        _gaq.push(['_setDomainName', 'laborator.co']);
        _gaq.push(['_setAllowLinker', true]);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>

    </html>
</asp:Content>
