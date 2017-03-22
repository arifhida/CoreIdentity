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
    npm: webroot + "lib-npm/*.js",
    jquery: webroot + "lib-npm/jquery/*.js",
    bootstrapcss: webroot + "lib-npm/bootstrap/**/*.min.css",
    bootstrapjs: webroot + "lib-npm/bootstrap/**/*.min.js",
    angular: webroot + "lib-npm/angular/*.min.js",
    localstorage: webroot + "lib-npm/angular-local-storage/*.min.js",
    fontawesomecss: webroot + 'lib-npm/font-awesome/css/*.css'    
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

gulp.task('copy-npm', function () {
    gulp.src(ngroutepath + 'angular/*.js')
    .pipe(gulp.dest(libnpmPath + 'angular/'));
    gulp.src(ngroutepath + 'angular-local-storage/dist/*.js')
    .pipe(gulp.dest(libnpmPath + 'angular-local-storage/'));
    gulp.src(ngroutepath + 'bootstrap/dist/**/*.*')
    .pipe(gulp.dest(libnpmPath + 'bootstrap/'));
    gulp.src(ngroutepath + 'jquery/dist/*.js')
    .pipe(gulp.dest(libnpmPath + 'jquery/'));
    gulp.src(ngroutepath + 'font-awesome/fonts/**.*')
    .pipe(gulp.dest(libnpmPath + 'font-awesome/fonts/'));
    gulp.src(ngroutepath + 'font-awesome/css/**.*')
    .pipe(gulp.dest(libnpmPath + 'font-awesome/css/'));
    gulp.src(ngroutepath + 'admin-lte/dist/**/*.*')
    .pipe(gulp.dest(libnpmPath + 'admin-lte/'));    
});
gulp.task('inject:Index', function () {
    var moduleSrc = gulp.src(paths.ngModule, { read: false });
    var routeSrc = gulp.src(paths.ngRoute, { read: false });
    var controllerSrc = gulp.src(paths.ngController, { read: false });
    var scriptSrc = gulp.src(paths.script, { read: false });
    var styleSrc = gulp.src(paths.style, { read: false });
    var bootstrapcssSrc = gulp.src(paths.bootstrapcss, { read: false });
    var bootjsSrc = gulp.src(paths.bootstrapjs, { read: false });
    var npmSrc = gulp.src(paths.npm, { read: false });
    var jquerySrc = gulp.src(paths.jquery, { read: false });
    var ngSrc = gulp.src(paths.angular, { read: false });
    var ngStorage = gulp.src(paths.localstorage, { read: false });
    var faSrc = gulp.src(paths.fontawesomecss, { read: false });
    gulp.src(webroot + 'app/Index.html')
        .pipe(wiredep({
            optional: 'configuration',
            goes: 'here',
            ignorePath: '..'
        }))
        .pipe(inject(series(jquerySrc, bootjsSrc, ngSrc, npmSrc, ngStorage, scriptSrc, moduleSrc, controllerSrc, routeSrc), { ignorePath: '/wwwroot' }))
        .pipe(inject(series(styleSrc, bootstrapcssSrc, faSrc), { ignorePath: '/wwwroot' }))
        .pipe(gulp.dest(webroot + 'app'));
});
