angular.module('app').factory('ProductResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/products/:productId', { productId: '@ProductId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});