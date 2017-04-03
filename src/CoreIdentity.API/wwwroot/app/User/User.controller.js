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
    ]).controller('DetailUserController', function ($scope, $http, $stateParams, initialData,$state) {
        var param = $stateParams.id;
        $scope.id = param;
        $scope.roles = initialData;
        console.log($scope.roles);
        $scope.Deleted = [];
        $scope.loading = false;
        $http.get('api/User/GetById?Id=' + $scope.id)
        .then(function (response) {
            $scope.data = response.data;            
        });
        $scope.checked = function (Id,Roles) {            
            if (angular.isArray(Roles)) {
                for (var i = 0;i < Roles.length; i++) {
                    if (Roles[i].Role.Id == Id && Roles[i].Delete == false)
                        return true;
                }                
            } else {
                return false;
            }
        }
        $scope.change = function (role) {
            if ($scope.data != null) {
                var p = new Object();
                var exist = false;
                for (var i = 0; i < $scope.data.Roles.length; i++) {
                    if ($scope.data.Roles[i].Role.Id == role.Id) {
                        exist = true;
                        $scope.data.Roles[i].Delete = true;
                    }

                } if (exist == false) {
                    p.Id = 0;
                    p.Role = role;
                    $scope.data.Roles.push(p);
                }
            }
        }
        $scope.click = function (role) {
            debugger;
            if ($scope.data != null) {
                var p = new Object();
                var exist = false;                
                for(var i=0;i < $scope.data.Roles.length; i++){                   
                    if($scope.data.Roles[i].Role.Id == role.Id){
                        exist = true;
                        p = $scope.data.Roles[i];
                        $scope.data.Roles.splice(i, 1);
                        console.log(exist);
                    }
                    
                }           
                
                if (exist) {                    
                    $scope.Deleted.push(p);
                } else {
                    var deleted = false;
                    if($scope.Deleted.length > 0){
                        for(var i=0; i < $scope.Deleted.length;i++){
                            if($scope.Deleted[i].Role.Id == role.Id){
                                p = $scope.Deleted[i];
                                $scope.Deleted.splice(i, 1);
                                deleted = true;
                            }
                        }   
                    }
                    if(deleted == false){                   
                        p.Id = 0;
                        p.Role = role;                   
                    }                 
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
            $http.post('api/User/UpdateUser', $scope.data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {                    
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
            $http.post('api/User/AddUser', $scope.data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    console.log(response);
                    $state.go('user');
                }, function (error) {
                    console.log(error);
                });

        }
        $scope.click = function (role) {
            debugger;
            if ($scope.data != null) {
                var p = new Object();
                var exist = false;
                for (var i = 0; i < $scope.data.Roles.length; i++) {
                    if ($scope.data.Roles[i].Role.Id == role.Id) {
                        exist = true;
                        p = $scope.data.Roles[i];
                        $scope.data.Roles.splice(i, 1);
                        console.log(exist);
                    }

                }

                if (exist) {
                    $scope.Deleted.push(p);
                } else {
                    var deleted = false;
                    if ($scope.Deleted.length > 0) {
                        for (var i = 0; i < $scope.Deleted.length; i++) {
                            if ($scope.Deleted[i].Role.Id == role.Id) {
                                p = $scope.Deleted[i];
                                $scope.Deleted.splice(i, 1);
                                deleted = true;
                            }
                        }
                    }
                    if (deleted == false) {
                        p.Id = 0;
                        p.Role = role;
                    }
                    $scope.data.Roles.push(p);
                }

            } else {
                var p = new Object();
                p.Id = 0;
                p.Role = role;
                $scope.data.Roles.push(p);
            }

        }
    });

})();