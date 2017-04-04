﻿/// <reference path="../../lib-npm/angular/angular.min.js" />
/// <reference path="../../lib-npm/angular/angular.js" />


"use strict";
(function () {
    angular.module('app').controller('categoryController', function ($scope, $http, initialData) {        
        $scope.data = angular.copy(initialData);
        $scope.loading = false;
        var nodeData = null;
        $scope.toggle = function (scope) {
            scope.toggle();
        };
        var defcat = {
            Id: 0,
            ParentId: 0,
            CategoryName: '',
            CategoryDescription: '',
            Children : []
        };
        $scope.reset = function () {
            $scope.cat = angular.copy(defcat);
        }
        $scope.reset();
        $scope.addItem = function (scope,parentId) {
            console.log($scope);
            nodeData = scope.$modelValue;
            $scope.cat.ParentId = parentId;
            var element = angular.element('#myModal');            
            element.modal('show');
        }
        $scope.Save = function () {
            $http.post('api/Category/AddCategory', $scope.cat, { headers: { 'Content-Type': 'application/json' } })
            .then(function (response) {
                nodeData.Children.push(response.data);
                var element = angular.element('#myModal');
                element.modal('hide');
                $scope.reset();
            });            
        }
        $scope.del = function (scope, Id) {            
            $http.delete('api/Category/Delete?Id=' + Id, { headers: { 'Content-Type': 'application/json' } })
            .then(function (response) {
                scope.remove();
            });
            
        }

    });
})();