angular.module('app').factory('ThinFrontUserResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/thinfrontuser/:thinFrontUserId', { thinFrontUserId: '@ThinFrontUserId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});