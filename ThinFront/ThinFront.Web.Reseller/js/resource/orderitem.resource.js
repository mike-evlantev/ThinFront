angular.module('app').factory('OrderItemResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/orderitems/:orderItemId', { orderItemId: '@OrderItemId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});