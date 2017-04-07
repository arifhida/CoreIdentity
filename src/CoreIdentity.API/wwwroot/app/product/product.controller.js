/// <reference path="../../lib-npm/angular/angular.min.js" />
/// <reference path="../../lib-npm/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').controller('productController', [
        '$scope', '$http', function ($scope, $http) {
            $scope.loading = false;
            $scope.q = '';
            $scope.page = 0;
            $scope.totalPage = 0;
            $scope.pageSize = 10;
            $scope.getData = function () {

            }
        }
    ]);
})();