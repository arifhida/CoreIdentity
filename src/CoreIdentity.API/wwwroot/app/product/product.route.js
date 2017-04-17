/// <reference path="../../lib-npm/angular/angular.min.js" />
/// <reference path="../../lib-npm/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider.state('product', {
            url: "/product",
            templateUrl: "app/product/index.html",
            controller: 'productController'            
        }).state('productDetail', {
            url: '/product.detail',
            templateUrl: 'app/product/product.detail.html',
            controller: 'productDetailCtrl',
            resolve: {
                initialData: function ($http) {
                    return $http.get('api/Brand/GetAll').then(function (response) {
                        return response.data;
                    });
                },
                categoryData: function ($http) {
                    return $http.get('api/Category/GetAll').then(function (response) {
                        return response.data;
                    });
                }
            }
        });
    });
})();