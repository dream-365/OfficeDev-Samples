(function () {
    "use strict";

    app.controller('accountCtrl', ['$scope', 'adalAuthenticationService', function ($scope, adalService) {
        $scope.login = function () {
            adalService.login();
        };

        $scope.logout = function () {
            adalService.logOut();
        };
    }]);
})();