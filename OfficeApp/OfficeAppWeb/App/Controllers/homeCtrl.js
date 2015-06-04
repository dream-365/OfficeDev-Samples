/// <reference path="../App.js" />

(function () {
    "use strict";

    app.controller('homeCtrl', ['$scope', '$officeSvrvice', 'o365ApiSvc', function ($scope, $officeSvrvice, o365ApiSvc) {
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

        $scope.getContacts = function()
        {
            o365ApiSvc.getContacts()
                       .success(function (data, status, headers, config) {
                           util.showNotification('Info:', 'get contacts succeed!');
            });
        }
    }]);
})();