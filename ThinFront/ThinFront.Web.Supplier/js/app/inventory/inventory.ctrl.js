angular.module('app').controller('InventoryController', function (apiUrl, $scope, $stateParams, $http, InventoryResource, ProductCategoryResource, ProductSubcategoryResource, ProductResource) {
    function activate() {
        $http.get(apiUrl + '/supplier/inventories')
               .then(function (response) {
                   $scope.inventories = response.data;
               })
               .catch(function (err) {
                   //bootbox.alert('Failed to get the user');
               });
    }

    activate();
});