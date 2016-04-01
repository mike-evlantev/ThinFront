angular.module('app').controller('ProductGridController', function ($scope, $http, apiUrl) {
    function activate() {
        $http.get(apiUrl + '/reseller/productCategories')
               .then(function (response) {
                   $scope.productCategories = response.data;
               })
               .catch(function (err) {
                   //bootbox.alert('Failed to get the user');
               });
    }

    activate();

    $scope.addToStore = function (product) {
        if (product.Checked) {
            $http.post(apiUrl + '/resellers/products/' + product.ProductId)
                .then(function (response) {
                    toastr.success('Product Added Successfully');
                })
        } else {
            $http.delete(apiUrl + '/resellers/products/' + product.ProductId)
                .then(function (response) {
                    toastr.success('Product Removed Successfully');
                });
        }
    }
});