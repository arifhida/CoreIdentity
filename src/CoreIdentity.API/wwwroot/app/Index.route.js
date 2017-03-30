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
        }).state('user', {
            url: '/User',
            templateUrl: 'app/User/index.html',
            controller: 'UsersController'
        }).state('userDetail', {
            url: '/user/:id',
            templateUrl: 'app/User/user.detail.html',
            controller: 'DetailUserController',
            resolve: {
                initialData: function ($http) {
                    return $http.get('api/Admin/GetAllRole').then(function (response) {
                        return response.data;
                    });
                }
            }

        }).state('userNew', {
            url: '/newUser',
            templateUrl: 'app/User/user.new.html',
            controller: 'newUserController',
            resolve: {
                initialData: function ($http) {
                    return $http.get('api/Admin/GetAllRole').then(function (response) {
                        return response.data;
                    });
                }
            }
        }).state('roles', {
            url: '/roles',
            templateUrl: 'app/role/roles.html',
            controller: 'roleController',
            resolve: {
                initialData: function ($http) {
                    return $http.get('api/Admin/GetAllRole').then(function (response) {
                        return response.data;
                    });
                }
            }
        });
        $locationProvider.html5Mode(true);
    });   
})();
