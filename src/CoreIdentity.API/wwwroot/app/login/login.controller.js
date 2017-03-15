"use strict";
(function () {
    angular.module('authentication').controller('loginController', [
        '$scope', 'authenticate', function ($scope, authenticate) {
            var authData = authenticate.isAuthenticated;            
            $scope.loginData = {
                Username: '',
                Password: ''
            };
            $scope.login = function () {
                authenticate.login($scope.loginData, function (response, $state) {
                    if (response.status == 200) {
                        $state.go('main');
                    }
                });
                authenticate.isAuthenticate();
            }
                
        }
    ]);
})();