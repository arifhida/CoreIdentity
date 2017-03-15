/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
/// <binding BeforeBuild='inject:index' />
"use strict";

var gulp = require('gulp'),
    series = require('stream-series'),
    inject = require('gulp-inject'),
    wiredep = require('wiredep').stream;

var webroot = "./wwwroot/";
var paths = {
    ngModule: webroot + "app/**/*.module.js",
    ngRoute: webroot + "app/**/*.route.js",
    ngController: webroot + "app/**/*.controller.js",
    script: webroot + "scripts/**/*.js",
    style: webroot + "styles/**/*.css",
    npm: webroot + "lib-npm/*.js"
};
var ngroutepath = "./node_modules/";
var libnpmPath = webroot + 'lib-npm/';
gulp.task('copy-angular-route', function () {
    gulp.src(ngroutepath + 'angular-ui-router/release/*.js')
        .pipe(gulp.dest(libnpmPath));
});
gulp.task('copy-checklist', function () {
    gulp.src(ngroutepath + 'checklist-model/*.js')
       .pipe(gulp.dest(libnpmPath));
});
gulp.task('inject:Index', function () {
    var moduleSrc = gulp.src(paths.ngModule, { read: false });
    var routeSrc = gulp.src(paths.ngRoute, { read: false });
    var controllerSrc = gulp.src(paths.ngController, { read: false });
    var scriptSrc = gulp.src(paths.script, { read: false });
    var styleSrc = gulp.src(paths.style, { read: false });
    var npmSrc = gulp.src(paths.npm, { read: false })
    gulp.src(webroot + 'app/Index.html')
        .pipe(wiredep({
            optional: 'configuration',
            goes: 'here',
            ignorePath: '..'
        }))
        .pipe(inject(series(npmSrc, scriptSrc, moduleSrc, controllerSrc, routeSrc), { ignorePath: '/wwwroot' }))
        .pipe(inject(series(styleSrc), { ignorePath: '/wwwroot' }))
        .pipe(gulp.dest(webroot + 'app'));
});
