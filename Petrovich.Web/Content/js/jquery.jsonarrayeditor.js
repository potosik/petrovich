"use strict";

(function ($) {
    var plugin = {};
    var container = {};

    $.fn.jsonArrayEditor = function () {
        var $this = $(this);
        $this.parent().addClass('json-array-edit-container');
        container = $this.closest('.json-array-edit-container');

        plugin.createMarkup(this);
        plugin.setInitialState();
        plugin.bindEvents();

        return this;
    };

    plugin.createMarkup = function () {
        $(plugin.markupTemplates.addButton).appendTo(container);
    };

    plugin.setInitialState = function () {
        try {
            var value = container.find('input.json-array-edit').val();
            if (!value || value == '') {
                return;
            }

            var array = JSON.parse(value);
            if (!array || !array.length) {
                return;
            }

            for (var i = 0; i < array.length; i++) {
                var item = array[i];
                plugin.createNewElement(item);
            }
        } catch (e) {
            console.log(e);
        }
    };

    plugin.bindEvents = function () {
        container.on('click', '.json-array-edit-element .input-group-addon', function () {
            $(this).closest('.json-array-edit-element').remove();
            plugin.updateValue();
        });
        container.on('click', '.json-array-edit-add .input-group-addon', function () {
            plugin.createNewElement();
        });
        container.on('keyup', 'input.json-array-edit-input', function () {
            plugin.updateValue();
        });
    };

    plugin.createNewElement = function (value) {
        var newElement = $(plugin.markupTemplates.element);
        if (value) {
            newElement.find('input.json-array-edit-input').val(value);
        }

        var addButton = container.find('.json-array-edit-add');
        newElement.insertBefore(addButton);
    }

    plugin.updateValue = function () {
        var result = [];
        
        var elements = $('.json-array-edit-element');
        for (var i = 0; i < elements.length; i++) {
            var item = $(elements[i]);
            var value = item.find('input.json-array-edit-input').val();
            if (value && value != '') {
                result.push(value);
            }
        }

        container.find('input.json-array-edit').val(JSON.stringify(result));
    }

    plugin.markupTemplates = {
        element:
            '<div class="json-array-edit-element input-group">' +
                '<input class="form-control col-xs-12 json-array-edit-input">' +
                '<span class="input-group-addon">' +
                    '<i class="fa fa-times"></i>' +
                '</span>' +
            '</div>',
        addButton: 
            '<div class="json-array-edit-add">' +
                '<span class="input-group-addon">' +
                    '<i class="fa fa-plus"></i>' +
                '</span>' +
            '</div>',
    };
})(jQuery);

