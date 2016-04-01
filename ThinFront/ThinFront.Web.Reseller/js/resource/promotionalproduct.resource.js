angular.module('app').factory('PromotionalProductResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/promotionalproducts/:promotionalProductId', { promotionalProductId: '@PromotionalProductId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});