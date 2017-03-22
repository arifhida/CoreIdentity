"use strict";
(function () {
    angular.module('authentication').controller('loginController', [
        '$scope', 'authenticate', function ($scope, authenticate) {
            var authData = authenticate.isAuthenticated;            
            $scope.loginData = {
                Username: '',
                Password: ''
            };
            $scope.loading = false;
            $scope.login = function () {
                $scope.loading = true;
                authenticate.login($scope.loginData, function (response, $state) {
                    if (response.status == 200) {
                        $scope.status == response.status;
                        $state.go('main');
                    } else {
                        $scope.status == response.status;
                        $scope.error = response.data;
                    }
                    $scope.loading = false;
                });
                authenticate.isAuthenticate();
            }
                
        }
    ]);
})();