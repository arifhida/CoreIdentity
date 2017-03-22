/// <reference path="../../lib/angular/angular.min.js" />
/// <reference path="../../lib/angular/angular.js" />

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
        $scope.InRoles = [];
        $http.get('api/Admin/GetUserById?Id=' + $scope.id)
        .then(function (response) {
            $scope.data = response.data;            
        });
        $scope.Save = function () {
            $http.post('api/Admin/UpdateUserSync', $scope.data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    console.log(response);
                }, function (error) {
                    console.log(error);
                });
           
        }
        
    }
    ).controller('newUserController', function ($scope, $http, initialData) {
        $scope.roles = initialData;
        $scope.Save = function () {
            $http.post('api/Admin/AddUser', $scope.data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    console.log(response);
                }, function (error) {
                    console.log(error);
                });
            
        }
    });
})();