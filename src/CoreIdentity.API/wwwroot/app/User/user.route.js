/// <reference path="../../lib-npm/angular/angular.min.js" />
/// <reference path="../../lib-npm/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider.state('user', {
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
        });
    });
})();