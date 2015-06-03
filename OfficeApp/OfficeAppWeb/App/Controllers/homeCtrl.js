/// <reference path="../App.js" />

(function () {
    "use strict";

    app.controller('homeCtrl', ['$scope', '$officeSvrvice', 'adalAuthenticationService', function ($scope, $officeSvrvice, adalService) {
        $scope.selection = '[display the select data here]';

        $scope.getSelectedData = function () {
            $officeSvrvice.getSelectedData(Office.CoercionType.Text).then(function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                    $scope.selection = result.value;

                    util.showNotification('The selected text is:', '"' + result.value + '"');
                } else {
                    util.showNotification('Error:', result.error.message);
                }
            });
        }

        $scope.login = function () {
            adalService.login();
        };
    }]);
})();