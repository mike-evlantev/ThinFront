angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:50896/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider)
{
   $httpProvider.interceptors.push('AuthenticationInterceptor');

   $urlRouterProvider.otherwise('/home');

   $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('customer', { url: '/customer', templateUrl: '/templates/customers/customer.html', controller: 'CustomerController' })
            .state('customer.register', { url: '/customerregister', templateUrl: '/templates/customers/register/customerregister.html', controller: 'CustomerRegisterController' })
            .state('customer.login', { url: '/customerlogin', templateUrl: '/templates/customers/login/customerlogin.html', controller: 'CustomerLoginController' })
            .state('')

        .state('reseller', { url: '/reseller', templateUrl: '/templates/resellers/reseller.html', controller: 'ResellerController' })
            .state('reseller.register', { url: '/resellerregister', templateUrl: '/templates/resellers/register/resellerregister.html', controller: 'ResellerRegisterController' })
            .state('reseller.login', { url: '/resellerlogin', templateUrl: '/templates/resellers/login/resellerlogin.html', controller: 'ResellerLoginController' })
            .state('reseller.dashboard', {url: '/resellerdashboard', templateUrl: '/templates/resellers/dashboard/resellerdashboard.html', controller: 'ResellerDashboardController'})

        .state('supplier', { url: '/supplier', templateUrl: '/templates/suppliers/supplier.html', controller: 'SupplierController' })
            .state('supplier.register', { url: '/supplierregister', templateUrl: '/templates/suppliers/register/supplierregister.html', controller: 'SupplierRegisterController' })
            .state('supplier.login', { url: '/supplierlogin', templateUrl: '/templates/suppliers/login/supplierlogin.html', controller: 'SupplierLoginController' })
            .state('supplier.dashboard', {url: 'supplierdashboard', templateUrl: 'templates/suppliers/dashboard/supplierdashboard.html', controller: 'SupplierDashboardController'})
});