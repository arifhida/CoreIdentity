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
                $http.get('api/Product/GetProduct', { headers: { 'Content-Type': 'application/json', 'q': $scope.q, 'Pagination': $scope.page + ',' + $scope.pageSize } })
                .then(function (response) {
                    var pagination = JSON.parse(response.headers('Pagination'))
                    $scope.page = pagination.CurrentPage;
                    $scope.totalPage = pagination.TotalPages;
                    $scope.pageSize = pagination.ItemsPerPage;
                    $scope.productList = response.data;
                });
            }
            $scope.getData();
        }
    ]).controller('productDetailCtrl', [
        '$scope', '$http', 'initialData', 'categoryData', function ($scope, $http, initialData, categoryData) {
            $scope.brandList = initialData;
            $scope.categoryList = categoryData;
            $scope.loading = false;
            $scope.autocomplete = false;
            $scope.categoryName = '';
            $scope.visible = function (item) {                
                return !($scope.query && $scope.query.length > 0
        && item.CategoryName.indexOf($scope.query) == -1);
            }
            $scope.findNodes = function () {

            };
            $scope.remove = function (scope) {
                scope.remove();
            };
            $scope.findNodes = function () {
                $scope.autocomplete = true;
            };
            $scope.setCategory = function (item) {
                $scope.categoryName = item.CategoryName;
                $scope.query = item.CategoryName;
                $scope.autocomplete = false;
                $scope.data.CategoryId = item.Id;
            }
            var data = {
                Id: 0,
                SKU: '',
                ProductName: '',
                BrandId: 0,
                ProductDescription: '',
                UnitPrice: 0
            };
            
            $scope.reset = function () {
                $scope.data = angular.copy(data);
                $scope.categoryName = '';
            }
            $scope.reset();

        }
    ]);
})();