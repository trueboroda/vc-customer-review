//Call this to register our module to main application
var moduleTemplateName = "CustomerReviewsModule";

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleTemplateName);
}

angular.module(moduleTemplateName, [])
.config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('workspace.CustomerReviewsModule', {
                url: '/CustomerReviewsModule',
                templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                controller: [
                    '$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
                        var newBlade = {
                            id: 'customerReviews',
                            controller: 'CustomerReviewsModule.reviewsListController',
                            template: 'Modules/$(CustomerReviewsModule)/Scripts/blades/reviews-list.tpl.html',
                            isClosingDisabled: true
                        };
                        bladeNavigationService.showBlade(newBlade);
                    }
                ]
            });
    }
])
.run(['$rootScope', 'platformWebApp.mainMenuService', 'platformWebApp.widgetService', '$state',
    function ($rootScope, mainMenuService, widgetService, $state) {
        //Register module in main menu
        var menuItem = {
            path: 'browse/CustomerReviewsModule',
            icon: 'fa fa-comments',
            title: 'customerReviews.blades.review-list.title',
            priority: 100,
            action: function () { $state.go('workspace.CustomerReviewsModule'); },
            permission: 'customerReview:read'
        };
        mainMenuService.addMenuItem(menuItem);


        //Register reviews widget inside product blade
        var itemReviewsWidget = {
            controller: 'CustomerReviewsModule.customerReviewWidgetController',
            template: 'Modules/$(CustomerReviewsModule)/Scripts/widgets/customerReviewWidget.tpl.html'
        };
        widgetService.registerWidget(itemReviewsWidget, 'itemDetail');

        //Register reviews widget inside product blade
        var itemRatingWidget = {
            controller: 'CustomerReviewsModule.productRatingWidgetController',
            template: 'Modules/$(CustomerReviewsModule)/Scripts/widgets/productRatingWidget.tpl.html'
        };
        widgetService.registerWidget(itemRatingWidget, 'itemDetail');
    }
]);
