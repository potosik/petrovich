'use strict';

(function () {
    var registerFunctionIfNotRegistered = function registerFunctionIfNotRegistered(key, callback) {
        if (typeof window[key] !== 'function') {
            window[key] = callback;
        }
    };

    // logout action submit
    registerFunctionIfNotRegistered('logout', function () {
        var logoutForm = document.logoutForm;
        if (logoutForm && logoutForm.submit && typeof logoutForm.submit === 'function') {
            logoutForm.submit();
        }
    });

    function Petrovich() {
        this.init = function () {
            // initialize delete button confirmation dialog
            this.deleteButtonConfirmation();
            // initialize search box logic
            this.searchBoxInitialization();
            // initialize purchase date picker
            this.purchaseDatePickerInitialization();
        };

        this.deleteButtonConfirmation = function () {
            $('.btn-delete').click(function () {
                return confirm("Элемент будет безвозвратно удален\nВы уверены что хотите удалить данный элемент?");
            });
        };

        this.searchBoxInitialization = function () {
            $("#searchBox").autocomplete({
                source: function source(request, response) {
                    jQuery.get(_pageData.fastSearchUri, { q: request.term }, function (data) {
                        if (!data.Success || !data.Result) {
                            response([]);
                            return;
                        }

                        $.map(data.Result, function (item, index) {
                            item.GroupInformation = item.GroupTitle ? " / " + item.GroupTitle : "";

                            if (!item.ImageSmall) {
                                item.ImageSmallUri = _pageData.noImageUri;
                            }
                        });

                        response(data.Result);
                    });
                },
                minLength: _pageData.fastSearchMinLength,
                delay: _pageData.defaultDebounce,
                select: function select(event, ui) {
                    window.location.href = ui.item.SelfUri;
                },
                appendTo: "#searchBoxResults",
                open: function () {
                    $("#searchBoxResults > ul").css({
                        left: "auto",
                        right: 0
                    });

                }
            }).focus(function () {
                $(this).autocomplete("search", this.value);
            }).autocomplete("instance")._renderItem = function (ul, item) {
                var template = $("#searchBoxItemTemplate").html();
                var rendered = Mustache.to_html(template, item);
                return $("<li>").append(rendered).appendTo(ul);
            };
        };

        this.purchaseDatePickerInitialization = function () {
            var element = $('#purchasePicker');
            var monthControl = $('#purchasemonth');
            var yearControl = $('#purchaseyear');
            var month = parseInt(monthControl.val());
            var year = parseInt(yearControl.val());
            var defaultDate = undefined;

            if (month && year && !isNaN(month) && !isNaN(year)) {
                defaultDate = new Date(year, month - 1, 1);
            }
            
            $('#purchasePicker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'MM yy',
                closeText: 'Готово',
                defaultDate: defaultDate,
                onClose: function (dateText, inst) {
                    monthControl.val(inst.selectedMonth + 1);
                    yearControl.val(inst.selectedYear);
                    $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
                }
            });

            if (defaultDate) {
                element.datepicker("setDate", new Date(year, month - 1, 1));
            }
        }
    }

    var petrovich = new Petrovich();
    petrovich.init();
})();

