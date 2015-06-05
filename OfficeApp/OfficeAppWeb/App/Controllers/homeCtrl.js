/// <reference path="../App.js" />

(function () {
    "use strict";

    app.controller('homeCtrl', ['$scope', '$officeSvrvice', 'o365ApiSvc', 'notificationSvc', function ($scope, $officeSvrvice, o365ApiSvc, notificationSvc) {
        $scope.selection = '[display the select data here]';

        $scope.getSelectedData = function () {
            $officeSvrvice.getSelectedData(Office.CoercionType.Text).then(function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                    $scope.selection = result.value;

                    notificationSvc.info('The selected text is:' + '"' + result.value + '"');
                } else {

                    notificationSvc.danger(result.error.message);
                }
            });
        }

    }]);
})();