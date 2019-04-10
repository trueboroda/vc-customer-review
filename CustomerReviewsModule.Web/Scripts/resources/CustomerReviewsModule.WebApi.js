angular.module('CustomerReviewsModule.Web')
.factory('CustomerReviewsModule.WebApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {

        search: { method: 'POST', url: 'api/customerReviews/search' },
        update: { method: 'PUT' },
        delete: { method: 'delete' }

    }); 
}]);
