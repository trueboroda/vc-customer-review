angular.module('CustomerReviewsModule')
.factory('CustomerReviewsModuleApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {

        search: { method: 'POST', url: 'api/customerReviews/search' },
        update: { method: 'PUT' },
        delete: { method: 'delete' }

    }); 
}]);
