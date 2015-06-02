var util = (function () {
    "use strict";

    var util = {};

    // Common initialization function (to be called from each page)
    util.initialize = function () {
        $('body').append(
            '<div id="notification-message">' +
                '<div class="padding">' +
                    '<div id="notification-message-close"></div>' +
                    '<div id="notification-message-header"></div>' +
                    '<div id="notification-message-body"></div>' +
                '</div>' +
            '</div>');

        $('#notification-message-close').click(function () {
            $('#notification-message').hide();
        });


        // After initialization, expose a common notification function
        util.showNotification = function (header, text) {
            $('#notification-message-header').text(header);
            $('#notification-message-body').text(text);
            $('#notification-message').slideDown('fast');
        };
    };

    return util;
})();

Office.initialize = function (reason) {
    $(document).ready(function () {
        util.initialize();
    });
};

var app = angular.module('officeAddin', []);

app.factory('$officeSvrvice', ['$q', function ($q) {
    return {
        getFileProperties: function () {
            var deferred = $q.defer();

            Office.context.document.getFilePropertiesAsync(function (asyncResult) {
                deferred.resolve(asyncResult);
            });

            return deferred.promise;
        },
        getSelectedData: function (coercionType) {
            var deferred = $q.defer();

            Office.context.document.getSelectedDataAsync(coercionType, function (asyncResult) {
                deferred.resolve(asyncResult);
            });

            return deferred.promise;
        }
    }
}]);


