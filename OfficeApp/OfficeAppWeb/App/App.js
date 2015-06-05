var util = (function () {
    "use strict";

    var util = {};

    // Common initialization function (to be called from each page)
    util.initialize = function () {
    };

    util.showNotification = function () {
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

    $routeProvider.when("/home", {
        controller: "homeCtrl",
        templateUrl: "/App/Views/Home.html"
    }).when('/contacts', {
        controller: "contactsCtrl",
        templateUrl: "/App/Views/contacts.html"
    }).otherwise({ redirectTo: "/home" });

    var endpoints = {
        // Map the location of a request to an API to a the identifier of the associated resource
        "https://outlook.office365.com/api": "https://outlook.office365.com"
    };

    adalProvider.init(
        {
            instance: 'https://login.microsoftonline.com/',
            tenant: config.tenant,
            clientId: config.clientId,
            extraQueryParameter: 'nux=1',
            endpoints: endpoints,
            cacheLocation: 'localStorage', // enable this for IE, as sessionStorage does not work for localhost.  
            // Also, token acquisition for the To Go API will fail in IE when running on localhost, due to IE security restrictions.
        },
        $httpProvider
        );

}]);




