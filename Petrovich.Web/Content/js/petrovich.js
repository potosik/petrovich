'use strict';

(function () {
    // logout action submit
    if (typeof (window.logout) !== 'function') {
        window.logout = function () {
            var logoutForm = document.logoutForm;
            if (logoutForm && logoutForm.submit && typeof (logoutForm.submit) === 'function') {
                logoutForm.submit();
            }
        }
    }
})();