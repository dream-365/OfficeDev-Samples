(function () {
    app.factory('$officeSvrvice', ['$q', function ($q) {
        return {
            getFileProperties: function () {
                var deferred = $q.defer();

                Office.context.document.getFilePropertiesAsync(function (asyncResult) {
                    deferred.resolve(asyncResult);
                });

                return deferred.promise;
            },
            getSelectedData: function (coercionType) {
                var deferred = $q.defer();

                Office.context.document.getSelectedDataAsync(coercionType, function (asyncResult) {
                    deferred.resolve(asyncResult);
                });

                return deferred.promise;
            }
        }
    }]);
})();