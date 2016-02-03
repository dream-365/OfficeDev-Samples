(function () {
    "use strict";

    app.controller('mainCtrl', ['$scope', 'adalAuthenticationService', function ($scope, adalService) {
        $scope.login = function () {
            adalService.login();
        };

        $scope.logout = function () {
            adalService.logOut();
        };
    }]);
})();