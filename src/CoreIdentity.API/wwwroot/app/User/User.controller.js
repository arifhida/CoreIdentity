/// <reference path="../../lib/angular/angular.min.js" />

"use strict";
(function () {
    angular.module('app').controller('UsersController', [
        '$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {
            $scope.getData = function () {
                $http.get('api/Admin/GetUser').then(function (response) {
                    if (response.status == 200) {
                        $scope.Userlist = response.data;
                        console.log($scope.Userlist)
                    } else {
                        console.log(response);
                    }

                });
            }
        }
    ]).controller('DetailUserController', function ($scope, $http, $stateParams, initialData) {
        var param = $stateParams.id;
        $scope.id = param;
        $scope.roles = initialData;
        console.log($scope.roles);
        $scope.InRoles = [];
        $scope.loading = false;
        $http.get('api/Admin/GetUserById?Id=' + $scope.id)
        .then(function (response) {
            $scope.data = response.data;
        });
        $scope.checked = function (role, Roles) {
            if (angular.isArray(Roles)) {
                for (var i = Roles.length; i--;) {
                    if (Roles[i].Role.Id == role.Id)
                        return true;
                }                
            } else {
                return false;
            }
        }
        $scope.change = function (role) {            
            if ($scope.data != null && $scope.data.Roles.length > 0) {
                var p = new Object();
                var exist = false;
                for(var i=0;i < $scope.data.Roles.length; i++){
                   debugger;
                    if($scope.data.Roles[i].Role.Id == role.Id){
                       exist = true;
                       p = $scope.data.Roles[i];
                       console.log(exist);
                    }
                    
                }           
                
                if(exist){
                    $scope.data.Roles.pop(p);
                }else{
                    p.Id = 0;
                    p.Role = role;
                    $scope.data.Roles.push(p);
                }
                
            }else{
                var p = new Object();
                p.Id = 0;
                p.Role = role;
                $scope.data.Roles.push(p);
            }            
        }


        $scope.Save = function () {
            $scope.loading = true;
            $http.post('api/Admin/UpdateUserData', $scope.data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    //console.log(response);
                    $state.go('user');
                    $scope.loading = false;
                }, function (error) {
                    console.log(error);
                    $scope.loading = false;
                });

        }

    }
    ).controller('newUserController', function ($scope, $http, initialData) {
        $scope.roles = initialData;
        $scope.Save = function () {
            $http.post('api/Admin/AddUser', $scope.data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    console.log(response);
                    $state.go('user');
                }, function (error) {
                    console.log(error);
                });

        }
    });
})();