/// <reference path="../../lib-npm/angular/angular.min.js" />
/// <reference path="../../lib-npm/angular/angular.js" />


"use strict";
(function () {
    angular.module('app').directive('categorySelect', function () {
        return {
            require: 'ngModel',
            transclude: true,
            replace: true,
            scope: {
                autocomplete: '='
            },
            link: function (scope, element, attr, ngModel) {
                element.on('keypress', function () {
                    console.log(ngModel.$modelValue);
                    scope.$apply(function () {
                        ngModel.$setViewValue('test');
                    });
                    console.log(ngModel.$modelValue);
                    scope.autocomplete = true;
                });
            }
        }
    });

})();