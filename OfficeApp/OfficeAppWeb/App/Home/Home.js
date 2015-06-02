/// <reference path="../App.js" />

(function () {
    "use strict";

    app.controller('homeController', ['$scope', '$officeSvrvice', function ($scope, $officeSvrvice) {
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
    }]);
})();