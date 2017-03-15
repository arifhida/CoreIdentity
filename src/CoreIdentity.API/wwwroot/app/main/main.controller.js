/// <reference path="../../lib/angular/angular.min.js" />
/// <reference path="../../lib/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').controller('mainController',[
        '$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {            
            $scope.username = $rootScope.globals.UserName;            
            $scope.getData = function () {
                $http.get('api/Admin/GetAllRole').then(function (response) {
                    if (response.status != 200) {
                        console.log('error');
                    } else {
                        console.log(response);
                    }                        
                }, function (err) {                   
                    console.log(err);
                });
            }
        }
    ]);
})();