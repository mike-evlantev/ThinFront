angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule', 'ui.tree']);

angular.module('app').value('apiUrl', 'http://localhost:1000/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');

    $urlRouterProvider.otherwise('home');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('register', { url: '/register', templateUrl: '/templates/home/register/register.html', controller: 'RegisterController' })
        .state('login', { url: '/login', templateUrl: '/templates/home/login/login.html', controller: 'LoginController' })


        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
            .state('app.inventory', { url: '/inventory', templateUrl: '/templates/app/inventory/inventory.html', controller: 'InventoryController' });

});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});