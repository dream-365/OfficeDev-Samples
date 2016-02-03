(function () {
    app.controller('contactsCtrl', ['$scope', 'o365ApiSvc', 'notificationSvc', '$officeSvrvice', function ($scope, o365ApiSvc, notificationSvc, $officeSvrvice) {
        $scope.getContacts = function () {
            o365ApiSvc.getContacts()
                       .success(function (data, status, headers, config) {
                           var rows = [];

                           for (var i = 0; i < data.value.length; i++) {
                               rows.push([data.value[i].DisplayName, data.value[i].EmailAddresses[0].Address]);
                           }

                           $officeSvrvice.insertDataTable(['Name', 'E-mail'], rows);
                       });
        }
    }]);
})();