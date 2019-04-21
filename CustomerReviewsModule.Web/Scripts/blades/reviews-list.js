angular.module('CustomerReviewsModule')
    .controller('CustomerReviewsModule.reviewsListController', ['$scope', 'customerReviewsModuleApi', 'platformWebApp.bladeUtils', 'platformWebApp.dialogService', 'uiGridConstants', 'platformWebApp.uiGridHelper', 
        function ($scope, reviewsApi, bladeUtils, dialogService, uiGridConstants, uiGridHelper) {
            $scope.uiGridConstants = uiGridConstants;

            var blade = $scope.blade;
            var bladeNavigationService = bladeUtils.bladeNavigationService;



            blade.permissions = {
                CR_DELETE: 'customerReview:delete',
                CR_UPDATE: 'customerReview:update'
            };



            blade.headIcon = 'fa-comments';
            blade.title = 'customerReviews.blades.review-list.title';
            blade.toolbarCommands = [
                {
                    name: "platform.commands.refresh", icon: 'fa fa-refresh',
                    executeMethod: blade.refresh,
                    canExecuteMethod: function () {
                        return true;
                    }
                },
                {
                    name: "platform.commands.delete", icon: 'fa fa-trash-o',
                    executeMethod: function () {
                        $scope.deleteList($scope.gridApi.selection.getSelectedRows());
                    },
                    canExecuteMethod: function () {
                        return $scope.gridApi && _.any($scope.gridApi.selection.getSelectedRows());
                    },
                    permission: blade.permissions.CR_DELETE
                },
                {
                    name: "customerReviews.blades.review-list.commands.approve", icon: 'fa fa-check',
                    executeMethod: function () {
                        approveList($scope.gridApi.selection.getSelectedRows());
                    },
                    canExecuteMethod: function () {
                        return $scope.gridApi && _.any($scope.gridApi.selection.getSelectedRows());
                    },
                    permission: blade.permissions.CR_DELETE
                },
                {
                    name: "customerReviews.blades.review-list.commands.ban", icon: 'fa fa-ban',
                    executeMethod: function () {
                        banList($scope.gridApi.selection.getSelectedRows());
                    },
                    canExecuteMethod: function () {
                        return $scope.gridApi && _.any($scope.gridApi.selection.getSelectedRows());
                    },
                    permission: blade.permissions.CR_DELETE
                }
            ];


           
            //refreshing/loading table data
            blade.refresh = function () {
                blade.isLoading = true;
                reviewsApi.search(angular.extend(filter, {
                    searchPhrase: filter.keyword ? filter.keyword : undefined,
                    sort: uiGridHelper.getSortExpression($scope),
                    skip: ($scope.pageSettings.currentPage - 1) * $scope.pageSettings.itemsPerPageCount,
                    take: $scope.pageSettings.itemsPerPageCount
                }), function (data) {
                    blade.isLoading = false;
                    $scope.pageSettings.totalItems = data.totalCount;
                    blade.currentEntities = data.results;
                }, function (error) {
                    bladeNavigationService.setError('Error ' + error.status, blade);
                });
            };

            //open detail blaid
            blade.selectNode = function (data) {
                $scope.selectedNodeId = data.id;

                var newBlade = {
                    id: 'reviewDetail',
                    currentEntityId: data.id,
                    //currentEntity: data,
                    title: 'customerReviews.blades.review-detail.title',
                    controller: 'CustomerReviewsModule.reviewDetailController',
                    template: 'Modules/$(CustomerReviewsModule)/Scripts/blades/review-detail.tpl.html'
                };

                bladeNavigationService.showBlade(newBlade, blade);
            };



          

            //delete selected reviews with confirmation
            $scope.deleteList = function (list) {
                var dialog = {
                    id: "confirmDeleteReview",
                    title: "customerReviews.dialogs.reviews-delete.title",
                    message: "customerReviews.dialogs.reviews-delete.message",
                    callback: function (remove) {
                        if (remove) {
                            $scope.isLoading = true;
                            closeChildrenBlades();

                            var itemIds = _.pluck(list, 'id');
                            reviewsApi.delete({ ids: itemIds }, function (data, headers) {
                                blade.refresh();
                            },
                                function (error) {
                                    bladeNavigationService.setError('Error ' + error.status, blade);
                                });

                        }
                    }
                };
                dialogService.showConfirmationDialog(dialog);
            };

            function closeChildrenBlades() {
                angular.forEach(blade.childrenBlades.slice(), function (child) {
                    bladeNavigationService.closeBlade(child);
                });
            }

            

            //approving/baning
            function banList(reviews) {
                approveList(reviews, false);
            }

            function approveList(reviews, approve) {
                if (approve == undefined) {
                    approve = true;
                }

                $scope.isLoading = true;
                closeChildrenBlades();
                _.forEach(reviews, function (value) { value.isActive = approve; });

                reviewsApi.update(reviews, function (data, headers) {
                    blade.refresh();
                },
                    function (error) {
                        bladeNavigationService.setError('Error ' + error.status, blade);
                    });

            }


            // simple and advanced filtering
            var filter = $scope.filter = blade.filter || {};

            filter.criteriaChanged = function () {
                if ($scope.pageSettings.currentPage > 1) {
                    $scope.pageSettings.currentPage = 1;
                } else {
                    blade.refresh();
                }
            };

            // ui-grid
            $scope.setGridOptions = function (gridOptions) {
                uiGridHelper.initialize($scope, gridOptions, function (gridApi) {
                    uiGridHelper.bindRefreshOnSortChanged($scope);
                });
                bladeUtils.initializePagination($scope);
            };

        }]);