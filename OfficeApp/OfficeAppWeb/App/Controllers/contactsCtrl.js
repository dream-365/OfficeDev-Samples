(function () {
    app.controller('contactsCtrl', ['$scope', 'o365ApiSvc', function ($scope, o365ApiSvc) {
        $scope.getContacts = function () {
            o365ApiSvc.getContacts()
                       .success(function (data, status, headers, config) {
                           util.showNotification('Info:', 'get contacts succeed!');
                       });
        }
    }]);
})();