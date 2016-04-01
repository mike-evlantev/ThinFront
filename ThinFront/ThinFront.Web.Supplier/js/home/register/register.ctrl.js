angular.module('app').controller('RegisterController', function ($scope, $timeout, AuthenticationService) {
    $scope.registration = {};

    $scope.register = function () {
        AuthenticationService.registerSupplier($scope.registration).then(
            function (response) {
                alert("Registration complete");
                $timeout(function () {
                    location.replace('/#/login');
                }, 2000);
            },
            function (error) {
                alert("Failed to register");
            }
        )
    };
});