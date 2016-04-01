angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:1000/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');

    $urlRouterProvider.otherwise('home');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('app', {url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController'})
            .state('app.store', { url: '/store', templateUrl: '/templates/app/store/store.html', controller: 'StoreController' })
            .state('app.login', { url: '/login', templateUrl: '/templates/app/login/login.html', controller: 'LoginController' })
            .state('app.register', { url: '/register', templateUrl: '/templates/app/register/register.html', controller: 'RegisterLoginController' })
            .state('app.productdetail', { url: '/productdetail', templateUrl: '/templates/app/productdetail/productdetail.html', controller: 'ProductDetailController' })
            .state('app.cart', { url: '/cart', templateUrl: '/templates/app/cart/cart.html', controller: 'CartController' })
            .state('app.account', { url: '/account', templateUrl: '/templates/app/account/account.html', controller: 'AccountController' })
            .state('app.orderdetail', { url: '/orderdetail', templateUrl: '/templates/app/orderdetail/orderdetail.html', controller: 'OrderDetailController' })
            .state('app.ordergrid', { url: '/ordergrid', templateUrl: '/templates/app/ordergrid/ordergrid.html', controller: 'OrderGridController' })
            .state('app.address', { url: '/address', templateUrl: '/templates/app/checkout/address.html', controller: 'AddressController' })
            .state('app.delivery', { url: '/delivery', templateUrl: '/templates/app/checkout/delivery.html', controller: 'DeliveryController' })
            .state('app.payment', { url: '/payment', templateUrl: '/templates/app/checkout/payment.html', controller: 'PaymentController' })
            .state('app.orderconfirmation', { url: '/orderconfirmation', templateUrl: '/templates/app/checkout/orderconfirmation.html', controller: 'OrderConfirmationController' })
            .state('app.ordersummary', { url: '/ordersummary', templateUrl: '/templates/app/checkout/ordersummary.html', controller: 'OrderSummaryController' })
            .state('app.contact', { url: '/contact', templateUrl: '/templates/app/contact/contact.html', controller: 'ContactController' });
    
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});