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
            .state('app.dashboard', { url: '/dashboard', templateUrl: '/templates/app/dashboard/dashboard.html', controller: 'DashboardController' })

            .state('app.customer', { url: '/customer', abstract: true, template: '<ui-view/>' })
                .state('app.customer.grid', { url: '/grid', templateUrl: '/templates/app/customer/customer.grid.html', controller: 'CustomerGridController' })
                .state('app.customer.detail', { url: '/detail/:id', templateUrl: '/templates/app/customer/customer.detail.html', controller: 'CustomerDetailController' })
                // .state('app.customer.add', { url: '/add', templateUrl: '/templates/app/customer/customer.detail.html', controller: 'CustomerAddController' })

            .state('app.order', { url: '/order', abstract: true, template: '<ui-view/>' })
                .state('app.order.grid', { url: '/grid', templateUrl: '/templates/app/order/order.grid.html', controller: 'OrderGridController' })
                .state('app.order.detail', { url: '/detail/:id', templateUrl: '/templates/app/order/order.detail.html', controller: 'OrderDetailController' })
                // .state('app.order.add', { url: '/add', templateUrl: '/templates/app/order/order.detail.html', controller: 'OrderAddController' })

            .state('app.product', { url: '/product', abstract: true, template: '<ui-view/>' })
                .state('app.product.grid', { url: '/grid', templateUrl: '/templates/app/product/product.grid.html', controller: 'ProductGridController' })
                // .state('app.product.detail', { url: '/detail/:id', templateUrl: '/templates/app/product/product.detail.html', controller: 'ProductDetailController' })
                // .state('app.product.add', { url: '/add', templateUrl: '/templates/app/product/product.detail.html', controller: 'ProductAddController' })

            .state('app.promotion', { url: '/promotion', abstract: true, template: '<ui-view/>' })
                .state('app.promotion.grid', { url: '/grid', templateUrl: '/templates/app/promotion/promotion.grid.html', controller: 'PromotionGridController' })
                .state('app.promotion.detail', { url: '/detail/:id', templateUrl: '/templates/app/promotion/promotion.detail.html', controller: 'PromotionDetailController' })
    // .state('app.promotion.add', { url: '/add', templateUrl: '/templates/app/promotion/promotion.detail.html', controller: 'PromotionAddController' });

});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});