angular.module('app').factory('AddressTypeResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/addresstypes/:addressTypeId', { addressTypeId: '@AddressTypeId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});