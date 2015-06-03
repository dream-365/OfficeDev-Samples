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

var app = angular.module('officeAddin', ['ngRoute', 'AdalAngular']);

app.config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalProvider) {

    $routeProvider.when("/Home", {
        controller: "homeCtrl",
        templateUrl: "/App/Views/Home.html",
    }).otherwise({ redirectTo: "/Home" });

    var endpoints = {
        // Map the location of a request to an API to a the identifier of the associated resource
        "http://localhost":
            "[APP URI ID]",
    };

    adalProvider.init(
        {
            instance: 'https://login.microsoftonline.com/',
            tenant: '[tenant-name.onmicrosoft.com]',
            clientId: '[app-client-id]',
            extraQueryParameter: 'nux=1',
            endpoints: endpoints,
            cacheLocation: 'localStorage', // enable this for IE, as sessionStorage does not work for localhost.  
            // Also, token acquisition for the To Go API will fail in IE when running on localhost, due to IE security restrictions.
        },
        $httpProvider
        );

}]);

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


