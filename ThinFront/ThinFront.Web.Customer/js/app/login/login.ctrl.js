﻿angular.module('app').controller('LoginController', function ($scope, AuthenticationService) {
    $scope.loginData = {};

    $scope.login = function () {
        AuthenticationService.login($scope.loginData).then(
            function (response) {
                location.replace('/#/ordergrid');
            },
            function (err) {
                alert(err.error_description);
            }
        );
    };
});