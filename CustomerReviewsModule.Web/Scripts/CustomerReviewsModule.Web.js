//Call this to register our module to main application
var moduleTemplateName = "CustomerReviewsModule.Web";

if (AppDependencies != undefined) {
    AppDependencies.push(moduleTemplateName);
}

angular.module(moduleTemplateName, [])
.config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('workspace.CustomerReviewsModuleWeb', {
                url: '/CustomerReviewsModule.Web',
                templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                controller: [
                    '$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
                        var newBlade = {
                            id: 'blade1',
                            controller: 'CustomerReviewsModule.Web.blade1Controller',
                            template: 'Modules/$(CustomerReviewsModule.Web)/Scripts/blades/helloWorld_blade1.tpl.html',
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
            path: 'browse/CustomerReviewsModule.Web',
            icon: 'fa fa-cube',
            title: 'CustomerReviewsModule.Web',
            priority: 100,
            action: function () { $state.go('workspace.CustomerReviewsModuleWeb'); },
            permission: 'CustomerReviewsModule.WebPermission'
        };
        mainMenuService.addMenuItem(menuItem);
    }
]);
