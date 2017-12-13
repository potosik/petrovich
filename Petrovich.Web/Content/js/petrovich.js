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

    // update url parameter
    registerFunctionIfNotRegistered('urlParam', function (paramName, value) {
        var uri = URI(window.location.href).removeQuery('page');
        if (!value) {
            uri.removeQuery(paramName);
        } else {
            uri.setQuery(paramName, value);
        }

        window.location.href = uri.normalize().toString();
    });

    // get url string parameter
    registerFunctionIfNotRegistered('getUrlParam', function getParameterByName(paramName, url) {
        if (!url) url = window.location.href;
        paramName = paramName.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + paramName + "(=([^&#]*)|&|#|$)");
        var results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    });

    function Petrovich() {
        this.init = function () {
            // initialize delete button confirmation dialog
            this.deleteButtonConfirmation();
            // initialize search box logic
            this.searchBoxInitialization();
            // initialize purchase date picker
            this.purchaseDatePickerInitialization();
            // initialize input masks
            this.inputMasksInitialization();
            // initialize smart cart
            this.smartCartInitialization();
            // initialize table filter
            this.tableFilterInitialization();
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
                open: function open() {
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
                onClose: function onClose(dateText, inst) {
                    monthControl.val(inst.selectedMonth + 1);
                    yearControl.val(inst.selectedYear);
                    $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
                }
            });

            if (defaultDate) {
                element.datepicker("setDate", new Date(year, month - 1, 1));
            }
        };

        this.inputMasksInitialization = function () {
            $('#price,#assessedvalue').inputmask('9{1,7},99', { numericInput: true });
        };

        this.smartCartInitialization = function () {
            $(function () {
                var storageKey = 'petrovich-smartcart-bid';
                var storageValue = JSON.parse(localStorage.getItem(storageKey) || '[]');
                var products = $('.sc-product-item');
                var selectedIds = storageValue.map(function (item) {
                    return item.product_id;
                });

                if (storageValue.length > 0) {
                    for (var i = 0; i < storageValue.length; i++) {
                        products.each(function () {
                            var element = $(this);
                            var id = element.find('input[name="product_id"]').val();
                            if (selectedIds.indexOf(id) > -1) {
                                element.closest('.sc-product-item').addClass('sc-added-item');
                            }
                        });
                    }
                }

                // Initialize Smart Cart    	
                $('#smartcart').smartCart({
                    resultName: 'serializedList',
                    cart: storageValue
                });

                function getIndexByKey(arr, value) {
                    for (var i = 0, iLen = arr.length; i < iLen; i++) {
                        if (arr[i].unique_key === value) {
                            return i;
                        }
                    }
                    return -1;
                }

                // Initialize Smart Cart Events
                $("#smartcart").on("cartCleared", clearCartEvent);

                function clearCartEvent(e) {
                    localStorage.removeItem(storageKey);
                }

                $("#smartcart").on("itemAdded", addItemEvent);
                function addItemEvent(e, cartItem) {
                    var fullObject = JSON.parse(localStorage.getItem(storageKey) || '[]');
                    fullObject.push(cartItem);
                    localStorage.setItem(storageKey, JSON.stringify(fullObject));
                }

                $("#smartcart").on("itemRemoved", removeItemEvent);
                function removeItemEvent(e, cartItem) {
                    var fullObject = JSON.parse(localStorage.getItem(storageKey) || '[]');
                    var index = getIndexByKey(fullObject, cartItem.unique_key);
                    if (index > -1) {
                        fullObject.splice(index, 1);
                    }
                    localStorage.setItem(storageKey, JSON.stringify(fullObject));
                }

                $("#smartcart").on("quantityUpdated", function operation(e, cartItem) {
                    removeItemEvent(e, cartItem);
                    addItemEvent(e, cartItem);
                });
            });
        };

        this.submitSmartCart = function () {
            if ($("#smartcart .sc-cart-item").length) {
                $("#smartcart .sc-cart-checkout").click();
            } else {
                $('.x_panel.bid.hide').removeClass('hide');
            }
        };

        this.tableFilterInitialization = function () {
            var filterParamName = 'filter';
            var tableSearchContainer = $('.table_search');
            var tableSearchBtn = tableSearchContainer.find('.btn-table-search');
            var tableSearchInput = tableSearchContainer.find('.input-table-search');

            var filterParameterValue = window.getUrlParam(filterParamName);
            if (filterParameterValue) {
                tableSearchInput.val(filterParameterValue);
            }

            tableSearchBtn.click(function () {
                var value = tableSearchInput.val();
                window.urlParam(filterParamName, value);
            });

            tableSearchInput.on('keyup', function (e) {
                if (e.keyCode == 13) {
                    tableSearchBtn.click();
                }
            });
        };
    }

    window.petrovich = new Petrovich();
    petrovich.init();
})();

