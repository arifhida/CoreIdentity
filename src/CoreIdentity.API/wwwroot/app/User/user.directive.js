/// <reference path="../../lib-npm/angular/angular.min.js" />
/// <reference path="../../lib-npm/angular/angular.js" />

"use strict";
(function () {
    angular.module('app').directive('useravailable', [
        '$http', '$q', function ($http, $q) {
            return {
                require: 'ngModel',
                link: function ($scope, element, attrs, ngModel) {
                    ngModel.$asyncValidators.useravailable = function (username) {
                        return $http.post('api/admin/GetUserName', JSON.stringify(username), { headers: { 'Content-Type': 'application/json' } })
                        .then(function resolved() {
                            return true;
                        }, function rejected() {
                            return $q.rejected('username is exist');
                        });
                    }

                }
            }
        }
    ]).directive('emailexist', [
        '$http', '$q', function ($http, $q) {
            return {
                require: 'ngModel',
                link: function ($scope,element, attrs, ngModel) {
                    ngModel.$asyncValidators.emailexist = function (email) {
                        return $http.post('api/admin/GetUserEmail', JSON.stringify(email), { headers: { 'Content-Type': 'application/json' } })
                        .then(function resolved() {
                            return true;
                        }, function rejected() {
                            return $q.rejected('email is exist');
                        });
                    }
                }
            }
        }
    ]);
})();