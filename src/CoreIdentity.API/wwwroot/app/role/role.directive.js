/// <reference path="../../lib-npm/angular/angular.js" />
"use strict";
(function () {
    angular.module('app').directive('checkrole', [
        '$http', '$q', function ($http, $q) {
            return {
                require: 'ngModel',
                link: function ($scope, element, attrs, ngModel) {
                    ngModel.$asyncValidators.roleExist = function (rolename) {
                        rolename = ($scope.Mode == 'edit') ? '' : rolename;
                        console.log($scope.Mode);

                        return $http.post('api/admin/GetRoleByName', JSON.stringify(rolename), { headers: { 'Content-Type': 'application/json' } })
                            .then(function resolved() {
                                return true;
                            }, function rejected() {
                                return $q.rejected('rolename is exist');
                            });
                    }
                }
            }
        }
    ]);
})();