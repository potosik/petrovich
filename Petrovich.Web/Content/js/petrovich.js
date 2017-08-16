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
            this.delteButtonConfirmation();
            // initialize search box logic
            this.searchBoxInitialization();
        };

        this.delteButtonConfirmation = function () {
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
    }

    var petrovich = new Petrovich();
    petrovich.init();
})();

