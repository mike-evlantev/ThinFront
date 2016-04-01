angular.module('app').factory('AccountResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/ThinFrontUser/:ThinFrontUserId', { ThinFrontUserId: '@ThinFrontUserId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});