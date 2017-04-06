/// <reference path="../scripts/app.js" />
/// <reference path="../lib/angular/angular.min.js" />
/// <reference path="../lib/angular/angular.js" />
/// <reference path="../lib-npm/angular-ui-router.min.js" />
/// <reference path="../lib-npm/angular-ui-router.js" />

"use strict";
(function () {
    angular.module('app').config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $urlRouterProvider.otherwise("main");
        $stateProvider
        .state('main', {
            url: "/",
            templateUrl: "app/main/main.html",
            controller: 'mainController'
        }).state('login', {
            url: "/login",
            templateUrl: "app/login/login.html",
            controller: 'loginController'
        }).state('roles', {
            url: '/roles',
            templateUrl: 'app/role/roles.html',
            controller: 'roleController'
            //resolve: {
            //    initialData: function ($http) {
            //        return $http.get('api/Role/GetData').then(function (response) {
            //            return response.data;
            //        });
            //    }
            //}
        }).state('category', {
            url: '/category',
            templateUrl: 'app/category/index.html',
            controller:'categoryController',
            resolve: {
                initialData: function ($http) {
                    return $http.get('api/Category/GetAll').then(function (response) {
                        return response.data;
                    });
                }
            }
        });
        $locationProvider.html5Mode(true);
    });   
})();
