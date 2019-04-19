(function () {
    'use strict';

    angular
        .module('app')
        .controller('CustomerReviewsModule.reviewDetailController', reviewDetailController);

    review_detail.$inject = ['$scope', 'customerReviewsModuleApi', 'platformWebApp.bladeUtils', 'uiGridConstants', 'platformWebApp.uiGridHelper'];

    function reviewDetailController($scope, reviewsApi, bladeUtils, uiGridConstants, uiGridHelper) {
        var blade = $scope.blade;
        var bladeNavigationService = bladeUtils.bladeNavigationService;

        blade.headIcon = 'fa-comments';
        blade.title = 'Review details';

        var permissions = {
            update: 'customerReview:update',
            delete: 'customerReview:delete'
        };

        blade.formScope = null;
        $scope.setForm = function (form) { blade.formScope = form; };

        blade.toolbarCommands = [
            {
                name: "customerReviews.blades.review-details.commands.save", icon: 'fa fa-save',
                executeMethod: saveReview,
                permission: permissions.update
            },
            {
                name: "customerReviews.blades.review-details.commands.delete", icon: 'fa fa-save',
                executeMethod: deleteReview,
                permission: permissions.delete
            }
        ];

        function saveReview() {

        }

        function deleteReview(){

        }

        blade.refresh = function (parentRefresh) {
            blade.isLoading = true;

            return reviewsApi.getByIds(
                { ids: blade.currentEntityId },

                function (data) {
                    var dataItem = data[0];

                    blade.item = angular.copy(dataItem);
                    blade.currentEntity = blade.item;
                    blade.origItem = dataItem;

                    

                    if (parentRefresh && blade.parentBlade.refresh) {
                        blade.parentBlade.refresh();
                    }

                    blade.isLoading = false;
                },

                function (error) {
                    bladeNavigationService.setError('Error ' + error.status, blade);
                }
            );
        };


        //activation
        blade.refresh(false);

    }
})();
