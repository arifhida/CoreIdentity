/// <reference path="../../lib/angular/angular.min.js" />
/// <reference path="../../lib/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').controller('roleController', [
        '$scope', '$http', '$rootScope', 'initialData', function ($scope, $http, $rootScope, initialData) {

            $scope.getData = function () {
                $http.get('api/Admin/GetAllRole').then(function (response) {
                    $scope.roleList = response.data;
                });
            }
            $scope.roleList = initialData;
            $scope.roleId = 0;
            
            $scope.roleClick = function (role) {
                var element = angular.element('#myModal');
                console.log(element);
                element.modal('show');
                $scope.roleId = role.id;
                $scope.role = role;
                
            }
            $scope.saveData = function (role) {
                var btn = angular.element('#btnSave');
                $(btn).button('loading');
            }
            console.log(initialData);
        }
    ])
    .directive('mydirective', function () {
        return {
            replace: true,
            templateUrl: 'app/role/modal.html'
        };
    });
})();