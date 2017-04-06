/// <reference path="../../lib/angular/angular.min.js" />
/// <reference path="../../lib/angular/angular.js" />
/// <reference path="../../lib-npm/angular-local-storage/angular-local-storage.min.js" />
/// <reference path="../../lib-npm/angular-local-storage/angular-local-storage.js" />

"use strict";
(function () {
    angular.module('authentication')
        .factory('authenticate', ['$http', '$q', 'localStorageService', '$state', function ($http, $q, localStorageService, $state) {
            var service = {};
            var authenticationData = {
                isAuth: false,
                UserName: ''
            };

            var _login = function (loginData, callback) {
                $http.post('api/Token', loginData, { headers: { 'Content-Type': 'application/json' } }).
                then(function successCallback(response) {                    
                    console.log(response);
                    var result = response.data;
                    localStorageService.set('authentication', {
                        token: result.access_token,
                        UserName: loginData.Username
                    });
                    
                    callback(response,$state);
                }, function errorCallback(err) {                    
                    console.log(err);
                    callback(err, $state);
                }
                );                
            }
            service.isAuthenticate = function () {
                var tokenData = localStorageService.get('authentication');
                return (tokenData !== null);
            }

            var _logout = function () {
                localStorageService.clearAll();
            }
            service.login = _login;
            service.isAuthenticated = authenticationData;
            service.logout = _logout;
            return service;
        }]);
    
})();