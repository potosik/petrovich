'use strict';

(function () {
    var registerFunctionIfNotRegistered = function (key, callback) {
        if (typeof (window[key]) !== 'function') {
            window[key] = callback;
        }
    }

    // logout action submit
    registerFunctionIfNotRegistered('logout', function () {
        var logoutForm = document.logoutForm;
        if (logoutForm && logoutForm.submit && typeof (logoutForm.submit) === 'function') {
            logoutForm.submit();
        }
    });
    
    function Petrovich() {
        this.init = function () {
            // initialize delete button confirmation dialog
            this.delteButtonConfirmation();
        }

        this.delteButtonConfirmation = function () {
            $('.btn-delete').click(function () {
                return confirm("Элемент будет безвозвратно удален\nВы уверены что хотите удалить данный элемент?");
            });
        }
    }

    var petrovich = new Petrovich();
    petrovich.init();
})();