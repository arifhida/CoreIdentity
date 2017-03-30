/// <reference path="../../lib/angular/angular.min.js" />
/// <reference path="../../lib/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').controller('roleController', [
        '$scope', '$http', '$rootScope', 'initialData', function ($scope, $http, $rootScope, initialData) {
            $scope.loading = false;
            $scope.getData = function () {
                $http.get('api/Admin/GetAllRole').then(function (response) {
                    $scope.roleList = response.data;
                });
            }
            $scope.roleList = initialData;
            $scope.RoleId = 0;


            $scope.roleClick = function (role) {
                var element = angular.element('#myModal');
                console.log(element);
                element.modal('show');
                $scope.RoleId = role.Id;
                $scope.Role = angular.copy(role);
                $scope.Mode = 'edit';
            };
            $scope.newRole = function () {
                $scope.Mode = 'new';
                var element = angular.element('#myModal');
                console.log(element);
                element.modal('show');
            };
            $scope.saveData = function (role) {
                var btn = angular.element('#btnSave');
                $scope.loading = true;
                role.Id = $scope.RoleId;
                var element = angular.element('#myModal');
                if ($scope.Mode == 'edit') {
                    $http.post('api/Admin/UpdateRole', role, { headers: { 'Content-Type': 'application/json' } })
                        .then(function (response) {                            
                            $scope.loading = false;
                            element.modal('hide');
                        }, function (error) {
                            console.log(error);
                            $scope.loading = false;
                        });
                } else {
                    $http.post('api/Admin/AddRole', role, { headers: { 'Content-Type': 'application/json' } })
                        .then(function (response) {                            
                            $scope.loading = false;
                            element.modal('hide');
                        }, function (error) {
                            console.log(error);
                            $scope.loading = false;
                        });
                }
                            
                
                    
                               
            }

        }
    ])
    .directive('mydirective', function () {
        return {
            replace: true,
            templateUrl: 'app/role/modal.html'
        };
    });
})();