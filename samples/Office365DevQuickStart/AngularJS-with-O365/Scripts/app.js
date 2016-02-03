(function () {
  angular.module('app', [
    'ngRoute',
    'AdalAngular'
  ]).config(config);
  
  // Configure the routes.
	function config($routeProvider, $httpProvider, adalAuthenticationServiceProvider) {
		$routeProvider
			.when('/', {
				templateUrl: 'views/main.html',
				controller: 'MainController',
				controllerAs: 'main'
			})

			.otherwise({
				redirectTo: '/'
			});
	
		// Initialize the ADAL provider with your clientID (found in the Azure Management Portal) and the API URL (to enable CORS requests).
		adalAuthenticationServiceProvider.init(
			{
			    clientId: clientId,
			    // The endpoints here are resources for ADAL to get tokens for.
			    endpoints: {
			        'https://graph.microsoft.com': 'https://graph.microsoft.com'
			    }
			},
			$httpProvider
			);
	};
})();
