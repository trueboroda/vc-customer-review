angular.module('CustomerReviewsModule.Web')
.factory('CustomerReviewsModule.WebApi', ['$resource', function ($resource) {
    return $resource('api/CustomerReviewsModule.Web');
}]);
