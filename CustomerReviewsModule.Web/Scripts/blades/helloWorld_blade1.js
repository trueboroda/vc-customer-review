angular.module('CustomerReviewsModule.Web')
.controller('CustomerReviewsModule.Web.blade1Controller', ['$scope', 'CustomerReviewsModule.WebApi', function ($scope, api) {
    var blade = $scope.blade;
    blade.title = 'CustomerReviewsModule.Web';

    blade.refresh = function () {
        api.get(function (data) {
            blade.data = data.result;
            blade.isLoading = false;
        });
    };

    blade.refresh();
}]);
