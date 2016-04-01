angular.module('app').factory('AddressResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/addresses/:addressId', { addressId: '@AddressId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});