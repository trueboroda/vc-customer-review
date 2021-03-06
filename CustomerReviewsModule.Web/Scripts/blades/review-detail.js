(function () {
    'use strict';

    /**
     * controller for customer reviews detail blade content
     * */

    angular
        .module('CustomerReviewsModule')
        .controller('CustomerReviewsModule.reviewDetailController', reviewDetailController);

    reviewDetailController.$inject = ['$scope', 'customerReviewsModuleApi', 'platformWebApp.bladeUtils', 'platformWebApp.dialogService', 'platformWebApp.authService'];

    function reviewDetailController($scope, reviewsApi, bladeUtils, dialogService, authService) {


        $scope.customerRating = 0;
        var blade = $scope.blade;
        var bladeNavigationService = bladeUtils.bladeNavigationService;

        blade.headIcon = 'fa-comment';
        //blade.title = 'customerReviews.blades.review-detail.title';

        var permissions = {
            CR_UPDATE: 'customerReview:update',
            CR_DELETE: 'customerReview:delete'
        };

        $scope.formScope = null;
        $scope.setForm = function (form) { $scope.formScope = form; };

     

        //save changes
        function saveChanges() {
            blade.isLoading = true;

            var entityToSave = angular.copy(blade.currentEntity);

            reviewsApi.update([entityToSave], function (data) {
                blade.refresh(true);
            }, function (error) { bladeNavigationService.setError('Error ' + error.status, blade); });

        }

        function undoChanges() {
            angular.copy(blade.origEntity, blade.currentEntity);
        }

        //deleting
        function deleteReview(){
            var dialog = {
                id: "confirmDeleteReview",
                title: "customerReviews.dialogs.reviews-delete.title",
                message: "customerReviews.dialogs.reviews-delete.message",
                callback: function (remove) {
                    if (remove) {
                        $scope.isLoading = true;                        
                        
                        reviewsApi.delete({ ids: blade.currentEntityId }, function (data, headers) {
                            $scope.bladeClose();
                            blade.parentBlade.refresh();
                        },
                            function (error) {
                                bladeNavigationService.setError('Error ' + error.status, blade);
                            });

                    }
                }
            };
            dialogService.showConfirmationDialog(dialog);
        }


        function isDirty() {
            return !angular.equals(blade.currentEntity, blade.origEntity) && authService.checkPermission(permissions.CR_UPDATE);
        }

        function canSave() {
            return isDirty() && $scope.formScope && $scope.formScope.$valid;
        }


        //blade toolbar commands
        blade.toolbarCommands = [
            {
                name: "platform.commands.save", icon: 'fa fa-save',
                executeMethod: saveChanges,
                canExecuteMethod: canSave,
                permission: permissions.CR_UPDATE
            },
            {
                name: "platform.commands.reset",
                icon: 'fa fa-undo',
                executeMethod: undoChanges,
                canExecuteMethod: isDirty,
                permission: permissions.CR_UPDATE
            },
            {
                name: "platform.commands.delete",
                icon: 'fa fa-trash',
                executeMethod: deleteReview,
                canExecuteMethod: function () { return true; },
                permission: permissions.CR_DELETE
            }
        ];

        blade.refresh = function (parentRefresh) {
            blade.isLoading = true;

            return reviewsApi.getByIds(
                { ids: blade.currentEntityId },

                function (data) {
                    var reviewData = data[0];

                    var item = angular.copy(reviewData);
                    blade.currentEntity = item;
                    blade.origEntity = reviewData;

                    $scope.customerRating = item.rating;

                    blade.isLoading = false;


                    if (parentRefresh && blade.parentBlade.refresh) {
                        blade.parentBlade.refresh();
                    }

                   
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
