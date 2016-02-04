(function () {
  angular
    .module('app')
    .controller('MainController', MainController);
    

  function MainController($http, $log, adalAuthenticationService) {
    var vm = this;
    
    // Properties
    vm.isConnected;
    vm.userName;

    // Methods
    vm.connect = connect;
    vm.disconnect = disconnect;

    
    /////////////////////////////////////////
    // End of exposed properties and methods.
    
    /**
		 * This function does any initialization work the 
		 * controller needs.
		 */
    (function activate() {
      // Check connection status and show appropriate UI.
      if (adalAuthenticationService.userInfo.isAuthenticated) {
        vm.isConnected = true;
        
        // Get the user name from the user profile
        vm.userName = adalAuthenticationService.userInfo.profile.name;
      }
      else {
        vm.isConnected = false;
      }
    })();
    
    /**
		 * Expose the login method from ADAL to the view.
		 */
    function connect() {
      $log.debug('Connecting to Azure AD');
      adalAuthenticationService.login();
    };
		
		/**
		 * Expose the logOut method from ADAL to the view.
		 */
    function disconnect() {
      $log.debug('Disconnecting from Azure AD');
      adalAuthenticationService.logOut();
    };
  };
})();