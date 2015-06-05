(function () {
    app.controller('contactsCtrl', ['$scope', 'o365ApiSvc', 'notificationSvc', function ($scope, o365ApiSvc, notificationSvc) {
        $scope.getContacts = function () {
            o365ApiSvc.getContacts()
                       .success(function (data, status, headers, config) {
                           var list = '';

                           for(var i = 0; i < data.value.length; i++)
                           {
                               list += data.value[i].DisplayName + ', ';
                           }

                           list = list.substring(0, list.length - 2);

                           notificationSvc.info('Contacts: ' + list);
                       });
        }
    }]);
})();