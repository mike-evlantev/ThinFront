angular.module('app').factory('OrderResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/orders/:orderId', { orderId: '@OrderId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});