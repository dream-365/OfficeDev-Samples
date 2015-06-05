(function () {
    app.factory('o365ApiSvc', ['$http', function ($http) {
        return {
            getContacts: function () {
                return $http.get('https://outlook.office365.com/api/v1.0/me/contacts');
            }
        };
    }]);
})();

