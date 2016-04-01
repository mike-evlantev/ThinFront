angular.module('app').factory('ProductCategoryResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/productcategories/:productCategoryId', { productCategoryId: '@ProductCategoryId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});