/// <reference path="../../lib/angular/angular.min.js" />
/// <reference path="../../lib/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').controller('roleController', [
        '$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {
            $scope.loading = false;
            $scope.q = '';
            $scope.page = 0;
            $scope.totalPage = 0;
            $scope.pageSize = 10;
            $scope.getData = function () {
                $http.get('api/Role/GetData', { headers: { 'Content-Type': 'application/json', 'q': $scope.q, 'Pagination': $scope.page + ',' + $scope.pageSize } }).then(function (response) {
                    $scope.roleList = response.data;
                    var pagination = JSON.parse(response.headers('Pagination'))
                    $scope.page = pagination.CurrentPage;
                    $scope.totalPage = pagination.TotalPages;
                    $scope.pageSize = pagination.ItemsPerPage;
                });
            }
            //$scope.roleList = angular.copy(initialData);
            $scope.RoleId = 0;
            var original = $scope.Role;
            $scope.getData();
            $scope.roleClick = function (role) {
                var element = angular.element('#myModal');
                console.log(element);
                element.modal('show');
                $scope.RoleId = role.Id;
                $scope.Role = angular.copy(role);
                $scope.Mode = 'edit';
            };
            $scope.newRole = function () {
                $scope.RoleId = 0;
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
                    $http.post('api/Role/UpdateRole', role, { headers: { 'Content-Type': 'application/json' } })
                        .then(function (response) {                            
                            $scope.loading = false;
                            for (var i = 0; i < $scope.roleList.length; i++) {
                                debugger;
                                if($scope.roleList[i].Id == role.Id)
                                {
                                    $scope.roleList.splice(i, 1,role);                                    
                                }                                
                            }                            
                            element.modal('hide');
                            $scope.reset();
                        }, function (error) {
                            console.log(error);
                            $scope.loading = false;
                        });
                } else {
                    $http.post('api/Role/AddRole', role, { headers: { 'Content-Type': 'application/json' } })
                        .then(function (response) {                            
                            $scope.loading = false;
                            $scope.roleList.push(response.data);                           
                            element.modal('hide');
                        }, function (error) {
                            console.log(error);
                            $scope.loading = false;
                        });
                }          
                               
            }

            $scope.delete = function (role) {                
                $http.delete('api/Role/DeleteRole?Id=' + role.Id, { headers: { 'Content-Type': 'application/json' } })
                    .then(function (response) {
                        for (var i = 0; i < $scope.roleList.length; i++) {
                            debugger;
                            if ($scope.roleList[i].Id == role.Id) {
                                $scope.roleList.splice(i, 1);
                            }
                        }
                    });
            }
            $scope.reset = function () {
                $scope.Role = angular.copy(original);
                $scope.frmRole.$setPristine();
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