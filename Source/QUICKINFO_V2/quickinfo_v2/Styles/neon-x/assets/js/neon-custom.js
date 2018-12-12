
    "use strict"; $(document).ready(function () {
        public_vars.$body = $("body"); public_vars.$pageContainer = public_vars.$body.find(".page-container"); public_vars.$chat = public_vars.$pageContainer.find('#chat'); public_vars.$horizontalMenu = public_vars.$pageContainer.find('header.navbar'); public_vars.$sidebarMenu = public_vars.$pageContainer.find('.sidebar-menu'); public_vars.$mainMenu = public_vars.$sidebarMenu.find('#main-menu'); public_vars.$mainContent = public_vars.$pageContainer.find('.main-content'); setup_sidebar_menu(); setup_horizontal_menu(); public_vars.$sidebarMenu.find(".sidebar-collapse-icon").on('click', function (ev)
            ev.preventDefault(); var with_animation = $(this).hasClass('with-animation'); if (with_animation) {
                public_vars.$mainMenu.stop().slideToggle('normal', function ()
            }
        }); public_vars.$horizontalMenu.find(".horizontal-mobile-menu a").on('click', function (ev) {
            ev.preventDefault(); var $menu = public_vars.$horizontalMenu.find('.navbar-nav'), with_animation = $(this).hasClass('with-animation'); if (with_animation) {
                $menu.stop().slideToggle('normal', function () {
                    $menu.attr('height', 'auto'); if ($menu.css('display') == 'none')
                });
            }
        }); public_vars.$sidebarMenu.data('initial-state', (public_vars.$pageContainer.hasClass('sidebar-collapsed') ? 'closed' : 'open')); if (is('tabletscreen'))
            var nicescroll_defaults = { cursorcolor: '#d4d4d4', cursorborder: '1px solid #ccc', railpadding: { right: 3 }, cursorborderradius: 1, autohidemode: true, sensitiverail: true }; public_vars.$body.find('.dropdown .scroller').niceScroll(nicescroll_defaults); $(".dropdown").on("shown.bs.dropdown", function ()
                var fs_tm = 0; fixed_sidebar.niceScroll({ cursorcolor: '#454a54', cursorborder: '1px solid #454a54', railpadding: { right: 3 }, railalign: 'right', cursorborderradius: 1 }); fixed_sidebar.on('click', 'li a', function ()
            }
        }
            $(".scrollable").each(function (i, el) {
                var $this = $(el), height = attrDefault($this, 'height', $this.height()); if ($this.is(':visible')) {
                    $this.removeClass('scrollable'); if ($this.height() < parseInt(height, 10))
                }
            });
        }
            ev.preventDefault(); var $this = jQuery(this).closest('.panel'); blockUI($this); $this.addClass('reloading'); setTimeout(function () {
                unblockUI($this)
            }, 900);
        }).on('click', '.panel > .panel-heading > .panel-options > a[data-rel="close"]', function (ev) {
            ev.preventDefault(); var $this = $(this), $panel = $this.closest('.panel'); var t = new TimelineLite({
                onComplete: function () {
                    $panel.slideUp(function ()
                }
            }); t.append(TweenMax.to($panel, .2, { css: { scale: 0.95 } })); t.append(TweenMax.to($panel, .5, { css: { autoAlpha: 0, transform: "translateX(100px) scale(.95)" } }));
        }).on('click', '.panel > .panel-heading > .panel-options > a[data-rel="collapse"]', function (ev) {
            ev.preventDefault(); var $this = $(this), $panel = $this.closest('.panel'), $body = $panel.children('.panel-body, .table'), do_collapse = !$panel.hasClass('panel-collapse'); if ($panel.is('[data-collapsed="1"]'))
        }); $('[data-toggle="buttons-radio"]').each(function () {
            var $buttons = $(this).children(); $buttons.each(function (i, el) {
                var $this = $(el); $this.click(function (ev)
            });
        }); $('[data-toggle="buttons-checkbox"]').each(function () {
            var $buttons = $(this).children(); $buttons.each(function (i, el) {
                var $this = $(el); $this.click(function (ev)
            });
        }); $('[data-loading-text]').each(function (i, el) {
            var $this = $(el); $this.on('click', function (ev)
        }); $('[data-toggle="popover"]').each(function (i, el) {
            var $this = $(el), placement = attrDefault($this, 'placement', 'right'), trigger = attrDefault($this, 'trigger', 'click'), popover_class = $this.hasClass('popover-secondary') ? 'popover-secondary' : ($this.hasClass('popover-primary') ? 'popover-primary' : ($this.hasClass('popover-default') ? 'popover-default' : '')); $this.popover({ placement: placement, trigger: trigger }); $this.on('shown.bs.popover', function (ev)
        }); $('[data-toggle="tooltip"]').each(function (i, el) {
            var $this = $(el), placement = attrDefault($this, 'placement', 'top'), trigger = attrDefault($this, 'trigger', 'hover'), popover_class = $this.hasClass('tooltip-secondary') ? 'tooltip-secondary' : ($this.hasClass('tooltip-primary') ? 'tooltip-primary' : ($this.hasClass('tooltip-default') ? 'tooltip-default' : '')); $this.tooltip({ placement: placement, trigger: trigger }); $this.on('shown.bs.tooltip', function (ev)
        }); if ($.isFunction($.fn.knob)) {
            $(".knob").knob({
                change: function (value) { }, release: function (value) { }, cancel: function () { }, draw: function () {
                    if (this.$.data('skin') == 'tron') {
                        var a = this.angle(this.cv), sa = this.startAngle, sat = this.startAngle, ea, eat = sat + a, r = 1; this.g.lineWidth = this.lineWidth; this.o.cursor && (sat = eat - 0.3) && (eat = eat + 0.3); if (this.o.displayPrevious) { ea = this.startAngle + this.angle(this.v); this.o.cursor && (sa = ea - 0.3) && (ea = ea + 0.3); this.g.beginPath(); this.g.strokeStyle = this.pColor; this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sa, ea, false); this.g.stroke(); }
                    }
                }
            });
        }
            $(".slider").each(function (i, el) {
                var $this = $(el), $label_1 = $('<span class="ui-label"></span>'), $label_2 = $label_1.clone(), orientation = attrDefault($this, 'vertical', 0) != 0 ? 'vertical' : 'horizontal', prefix = attrDefault($this, 'prefix', ''), postfix = attrDefault($this, 'postfix', ''), fill = attrDefault($this, 'fill', ''), $fill = $(fill), step = attrDefault($this, 'step', 1), value = attrDefault($this, 'value', 5), min = attrDefault($this, 'min', 0), max = attrDefault($this, 'max', 100), min_val = attrDefault($this, 'min-val', 10), max_val = attrDefault($this, 'max-val', 90), is_range = $this.is('[data-min-val]') || $this.is('[data-max-val]'); if (is_range) {
                    $this.slider({
                        range: true, orientation: orientation, min: min, max: max, values: [min_val, max_val], step: step, slide: function (e, ui) {
                            var opts = $this.data('uiSlider').options, min_val = (prefix ? prefix : '') + ui.values[0] + (postfix ? postfix : ''), max_val = (prefix ? prefix : '') + ui.values[1] + (postfix ? postfix : ''); $label_1.html(min_val); $label_2.html(max_val); if (fill)
                        }
                    }); var $handles = $this.find('.ui-slider-handle'); $label_1.html((prefix ? prefix : '') + min_val + (postfix ? postfix : '')); $handles.first().append($label_1); $label_2.html((prefix ? prefix : '') + max_val + (postfix ? postfix : '')); $handles.last().append($label_2);
                }
                    $this.slider({
                        range: attrDefault($this, 'basic', 0) ? false : "min", orientation: orientation, min: min, max: max, value: value, step: step, slide: function (ev, ui) {
                            var opts = $this.data('uiSlider').options, val = (prefix ? prefix : '') + opts.value + (postfix ? postfix : ''); $label_1.html(val); if (fill)
                        }
                    }); var $handles = $this.find('.ui-slider-handle'); $label_1.html((prefix ? prefix : '') + value + (postfix ? postfix : '')); $handles.html($label_1);
                }
            })
        }
            $(".select2").each(function (i, el)
        }
            $("select.selectboxit").each(function (i, el)
        }
            $(".typeahead").each(function (i, el) {
                var $this = $(el), opts = { name: $this.attr('name') ? $this.attr('name') : ($this.attr('id') ? $this.attr('id') : 'tt') }; if ($this.hasClass('tagsinput'))
            });
        }
            $(".datepicker").each(function (i, el) {
                var $this = $(el), opts = { format: attrDefault($this, 'format', 'mm/dd/yyyy'), startDate: attrDefault($this, 'startDate', ''), endDate: attrDefault($this, 'endDate', ''), daysOfWeekDisabled: attrDefault($this, 'disabledDays', ''), startView: attrDefault($this, 'startView', 0), }, $n = $this.next(), $p = $this.prev(); $this.datepicker(opts); if ($n.is('.input-group-addon') && $n.has('a')) {
                    $n.on('click', function (ev)
                }
                    $p.on('click', function (ev)
                }
            });
        }
            $(".timepicker").each(function (i, el) {
                var $this = $(el), opts = { template: attrDefault($this, 'template', false), showSeconds: attrDefault($this, 'showSeconds', false), defaultTime: attrDefault($this, 'defaultTime', 'current'), showMeridian: attrDefault($this, 'showMeridian', true), minuteStep: attrDefault($this, 'minuteStep', 15), secondStep: attrDefault($this, 'secondStep', 15) }, $n = $this.next(), $p = $this.prev(); $this.timepicker(opts); if ($n.is('.input-group-addon') && $n.has('a')) {
                    $n.on('click', function (ev)
                }
                    $p.on('click', function (ev)
                }
            });
        }
            $(".colorpicker").each(function (i, el) {
                var $this = $(el), opts = {}, $n = $this.next(), $p = $this.prev(), $preview = $this.siblings('.input-group-addon').find('.color-preview'); $this.colorpicker(opts); if ($n.is('.input-group-addon') && $n.has('a')) {
                    $n.on('click', function (ev)
                }
                    $p.on('click', function (ev)
                }
                    $this.on('changeColor', function (ev) { $preview.css('background-color', ev.color.toHex()); }); if ($this.val().length)
                }
            });
        }
            $(".daterange").each(function (i, el) {
                var ranges = { 'Today': [moment(), moment()], 'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)], 'Last 7 Days': [moment().subtract('days', 6), moment()], 'Last 30 Days': [moment().subtract('days', 29), moment()], 'This Month': [moment().startOf('month'), moment().endOf('month')], 'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')] }; var $this = $(el), opts = { format: attrDefault($this, 'format', 'MM/DD/YYYY'), timePicker: attrDefault($this, 'timePicker', false), timePickerIncrement: attrDefault($this, 'timePickerIncrement', false), separator: attrDefault($this, 'separator', ' - '), }, min_date = attrDefault($this, 'minDate', ''), max_date = attrDefault($this, 'maxDate', ''), start_date = attrDefault($this, 'startDate', ''), end_date = attrDefault($this, 'endDate', ''); if ($this.hasClass('add-ranges'))
                    var drp = $this.data('daterangepicker'); if ($this.is('[data-callback]'))
                });
            });
        }
            $("[data-mask]").each(function (i, el) {
                var $this = $(el), mask = $this.data('mask').toString(), opts = { numericInput: attrDefault($this, 'numeric', false), radixPoint: attrDefault($this, 'radixPoint', ''), rightAlignNumerics: attrDefault($this, 'numericAlign', 'left') == 'right' }, placeholder = attrDefault($this, 'placeholder', ''), is_regex = attrDefault($this, 'isRegex', ''); if (placeholder.length)
                    case "phone": mask = "(999) 999-9999"; break; case "currency": case "rcurrency": var sign = attrDefault($this, 'sign', '$');; mask = "999,999,999.99"; if ($this.data('mask').toLowerCase() == 'rcurrency')
                }
            });
        }
            $("form.validate").each(function (i, el) {
                var $this = $(el), opts = {
                    rules: {}, messages: {}, errorElement: 'span', errorClass: 'validate-has-error', highlight: function (element) { $(element).closest('.form-group').addClass('validate-has-error'); }, unhighlight: function (element) { $(element).closest('.form-group').removeClass('validate-has-error'); }, errorPlacement: function (error, element) {
                        if (element.closest('.has-switch').length)
                    }
                }, $fields = $this.find('[data-validate]'); $fields.each(function (j, el2) {
                    var $field = $(el2), name = $field.attr('name'), validate = attrDefault($field, 'validate', '').toString(), _validate = validate.split(','); for (var k in _validate) {
                        var rule = _validate[k], params, message; if (typeof opts['rules'][name] == 'undefined')
                            opts['rules'][name][rule] = true; message = $field.data('message-' + rule); if (message)
                        }
                                if ($.inArray(params[1], ['min', 'max', 'minlength', 'maxlength', 'equalTo']) != -1) {
                                    opts['rules'][name][params[1]] = params[2]; message = $field.data('message-' + params[1]); if (message)
                                }
                            }
                    }
                }); $this.validate(opts);
            });
        }
            $(".form-wizard").each(function (i, el) {
                var $this = $(el), $progress = $this.find(".steps-progress div"), _index = $this.find('> ul > li.active').index(); var checkFormWizardValidaion = function (tab, navigation, index) {
                    if ($this.hasClass('validate')) {
                        var $valid = $this.valid(); if (!$valid)
                    }
                }; $this.bootstrapWizard({
                    tabClass: "", onTabShow: function ($tab, $navigation, index)
                }); $this.data('bootstrapWizard').show(_index); $(window).on('neon.resize', function ()
            });
        }
            var $this = $(el), $pct_counter = $this.find('.pct-counter'), $progressbar = $this.find('.tile-progressbar span'), percentage = parseFloat($progressbar.data('fill')), pct_len = percentage.toString().length; if (typeof scrollMonitor == 'undefined')
                var tile_progress = scrollMonitor.create(el); tile_progress.fullyEnterViewport(function () {
                    $progressbar.width(percentage + '%'); tile_progress.destroy(); var o = { pct: 0 }; TweenLite.to(o, 1, {
                        pct: percentage, ease: Quint.easeInOut, onUpdate: function ()
                    });
                });
            }
        }); $(".tile-stats").each(function (i, el) {
            var $this = $(el), $num = $this.find('.num'), start = attrDefault($num, 'start', 0), end = attrDefault($num, 'end', 0), prefix = attrDefault($num, 'prefix', ''), postfix = attrDefault($num, 'postfix', ''), duration = attrDefault($num, 'duration', 1000), delay = attrDefault($num, 'delay', 1000); if (start < end) {
                if (typeof scrollMonitor == 'undefined')
                    var tile_stats = scrollMonitor.create(el); tile_stats.fullyEnterViewport(function () {
                        var o = { curr: start }; TweenLite.to(o, duration / 1000, {
                            curr: end, ease: Power1.easeInOut, delay: delay / 1000, onUpdate: function ()
                        }); tile_stats.destroy()
                    });
                }
            }
        }); if ($.isFunction($.fn.tocify) && $("#toc").length) {
            $("#toc").tocify({ context: '.tocify-content', selectors: "h2,h3,h4,h5" }); var $this = $(".tocify"), watcher = scrollMonitor.create($this.get(0)); $this.width($this.parent().width()); watcher.lock(); watcher.stateChange(function ()
        }
    }); var wid = 0; $(window).resize(function () { clearTimeout(wid); wid = setTimeout(trigger_resizable, 200); });
})(jQuery, window); function fit_main_content_height() {
    if (public_vars.$sidebarMenu.length && public_vars.$sidebarMenu.hasClass('fixed') == false) {
        if (isxs()) {
            public_vars.$sidebarMenu.css('min-height', ''); public_vars.$mainContent.css('min-height', ''); if (typeof reset_mail_container_height != 'undefined')
        }
    }
}
    var $items_with_submenu = public_vars.$sidebarMenu.find('li:has(ul)'), submenu_options = { submenu_open_delay: 0.5, submenu_open_easing: Sine.easeInOut, submenu_opened_class: 'opened' }, root_level_class = 'root-level', is_multiopen = public_vars.$mainMenu.hasClass('multiple-expanded'); public_vars.$mainMenu.find('> li').addClass(root_level_class); $items_with_submenu.each(function (i, el) {
        var $this = $(el), $link = $this.find('> a'), $submenu = $this.find('> ul'); $this.addClass('has-sub'); $link.click(function (ev) {
            ev.preventDefault(); if (!is_multiopen && $this.hasClass(root_level_class)) {
                var close_submenus = public_vars.$mainMenu.find('.' + root_level_class).not($this).find('> ul'); close_submenus.each(function (i, el)
            }
                var current_height; if (!$submenu.is(':visible'))
            }
        });
    }); public_vars.$mainMenu.find('.' + submenu_options.submenu_opened_class + ' > ul').addClass('visible'); if (public_vars.$mainMenu.hasClass('auto-inherit-active-class'))
        var is_collapsed = public_vars.$pageContainer.hasClass('sidebar-collapsed'); if (is_collapsed) {
            if ($search_el.hasClass('focused') == false)
        }
    }); $search_input.on('blur', function (ev) {
        var is_collapsed = public_vars.$pageContainer.hasClass('sidebar-collapsed'); if (is_collapsed)
    }); var show_hide_menu = $(''); public_vars.$sidebarMenu.find('.logo-env').append(show_hide_menu);
}
    $submenu.addClass('visible').height(''); current_height = $submenu.outerHeight(); var props_from = { opacity: .2, height: 0, top: -20 }, props_to = { height: current_height, opacity: 1, top: 0 }; if (isxs())
        css: props_to, ease: options.submenu_open_easing, onComplete: function ()
    });
}
    if (public_vars.$pageContainer.hasClass('sidebar-collapsed') && $this.hasClass('root-level'))
        css: { height: 0, opacity: .2 }, ease: options.submenu_open_easing, onComplete: function ()
    });
}
    if ($active_element.length) {
        var $parent = $active_element.parent().parent(); $parent.addClass('active'); if (!$parent.hasClass('root-level'))
    }
}
    var $nav_bar_menu = public_vars.$horizontalMenu.find('.navbar-nav'), $items_with_submenu = $nav_bar_menu.find('li:has(ul)'), $search = public_vars.$horizontalMenu.find('li#search'), $search_input = $search.find('.search-input'), $search_submit = $search.find('form'), root_level_class = 'root-level'
        var $this = $(el), $link = $this.find('> a'), $submenu = $this.find('> ul'); $this.addClass('has-sub'); setup_horizontal_menu_hover($this, $submenu); $link.click(function (ev) {
            if (isxs()) {
                ev.preventDefault(); if (!is_multiopen && $this.hasClass(root_level_class)) {
                    var close_submenus = $nav_bar_menu.find('.' + root_level_class).not($this).find('> ul'); close_submenus.each(function (i, el)
                }
                    var current_height; if (!$submenu.is(':visible'))
                }
            }
        });
    }); if ($search.hasClass('search-input-collapsed')) {
        $search_submit.submit(function (ev) {
            if ($search.hasClass('search-input-collapsed'))
        }); $search_input.on('blur', function (ev)
    }
}
    var del = 0.5, trans_x = -10, ease = Quad.easeInOut; TweenMax.set($sub, { css: { autoAlpha: 0, transform: "translateX(" + trans_x + "px)" } }); $item.hoverIntent({
        over: function () {
            if (isxs())
        }, out: function () {
            if (isxs())
                    css: { autoAlpha: 0, transform: "translateX(" + trans_x + "px)" }, ease: ease, onComplete: function ()
                });
        }, timeout: 300, interval: 50
    });
}
    if (typeof $el.data(data_var) != 'undefined')
}
    $tab.prevAll().addClass('completed'); $tab.nextAll().removeClass('completed'); var items = $nav.children().length, pct = parseInt((index + 1) / items * 100, 10), $first_tab = $nav.find('li:first-child'), margin = $first_tab.find('span').position().left + 'px'; if ($first_tab.hasClass('active'))
}
    $(".checkbox-replace:not(.neon-cb-replacement), .radio-replace:not(.neon-cb-replacement)").each(function (i, el) {
        var $this = $(el), $input = $this.find('input'), $wrapper = $('<label class="cb-wrapper" />'), $checked = $('<div class="checked" />'), checked_class = 'checked', is_radio = $input.is('[type="radio"]'), $related, name = $input.attr('name'); $this.addClass('neon-cb-replacement'); $input.wrap($wrapper); $wrapper = $input.parent(); $wrapper.append($checked).next('label').on('click', function (ev)
            if (is_radio)
        }).trigger('change');
    });
}
    if (typeof $el == 'string')
}
    var top = el.offsetTop; var left = el.offsetLeft; var width = el.offsetWidth; var height = el.offsetHeight; while (el.offsetParent) { el = el.offsetParent; top += el.offsetTop; left += el.offsetLeft; }
}
    var transitions = ['page-fade', 'page-left-in', 'page-right-in', 'page-fade-only']; for (var i in transitions) {
        var transition_name = transitions[i]; if (public_vars.$body.hasClass(transition_name)) {
            public_vars.$body.addClass(transition_name + '-init')
        }
    }
}
    var hidden, state, visibilityChange; if (typeof document.hidden !== "undefined")
}