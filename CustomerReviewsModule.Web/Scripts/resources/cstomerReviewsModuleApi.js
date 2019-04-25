(function () {
    'use strict';
/**
 * service for customer review api access
 *  
 * */

angular.module('CustomerReviewsModule')
.factory('customerReviewsModuleApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {

        search: { method: 'POST', url: 'api/customerReviews/search' },
        update: { method: 'PUT' },
        delete: { method: 'delete' },
        getProductRating: { method: 'GET', url: 'api/customerReviews/productrating', isArray: true },
        getByIds: { method: 'GET', isArray: true }


    }); 
}]);

})();