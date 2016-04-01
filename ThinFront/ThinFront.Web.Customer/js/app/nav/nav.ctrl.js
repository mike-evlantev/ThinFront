angular.module('app').controller('NavController', function ($scope, $state) {
    $scope.state = $state.current;
});