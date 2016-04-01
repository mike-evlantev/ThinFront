angular.module('app').factory('ProductSubcategoryResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/productsubcategories/:productSubcategoryId', { productSubcategoryId: '@ProductSubcategoryId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});