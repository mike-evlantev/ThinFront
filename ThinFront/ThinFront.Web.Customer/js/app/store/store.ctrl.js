angular.module('app').controller('StoreController', function ($scope, $http, apiUrl) {
    function activate() {
        $http.get(apiUrl + '/resellers/1/products')
         .then(function (response) {
             $scope.products = response.data;
         });
    }

    activate();
});