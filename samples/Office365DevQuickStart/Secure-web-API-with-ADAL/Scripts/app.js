(function () {
    angular.module('app', [
      'ngRoute',
      'AdalAngular'
    ]).config(config);

    function config($routeProvider, $httpProvider, adalAuthenticationServiceProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'views/main.html',
                controller: 'MainController',
                controllerAs: 'main'
            }).otherwise({
                redirectTo: '/'
            });

        adalAuthenticationServiceProvider.init(
            {
                // Use this value for the public instance of Azure AD
                instance: 'https://login.microsoftonline.com/',

                // The 'common' endpoint is used for multi-tenant applications like this one
                tenant: 'common',

                clientId: clientId
            },
            $httpProvider
            );
    }
})();