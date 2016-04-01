angular.module('app').factory('PromotionResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/promotions/:promotionId', { promotionId: '@PromotionId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});