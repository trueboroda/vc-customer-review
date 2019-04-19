angular.module('CustomerReviewsModule')
    .controller('CustomerReviewsModule.productRatingWidgetController', ['$scope', 'customerReviewsModuleApi', function ($scope, reviewsApi) {
        var blade = $scope.blade;

        $scope.productRating;

        var filter = {};

        function refresh() {
            $scope.loading = true;


            reviewsApi.getProductRating(filter, function (data) {
                $scope.loading = false;
                $scope.productRating = data.rating;
            });

        }

        $scope.$watch("blade.itemId", function (id) {

            filter.productId = id;
            if (id) refresh();
        });
    }]);
