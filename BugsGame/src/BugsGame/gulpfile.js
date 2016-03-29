/// <binding AfterBuild='moveToLibs' />

var gulp = require('gulp');
var sass = require('gulp-sass');

var paths = {
    npmSrc: "./node_modules/",
    libTarget: "./wwwroot/libs/",
    styleTarget: "./wwwroot/style",
    styleSrc: "./Style/*.scss"
};

var libsToMove = [
   paths.npmSrc + '/angular2/bundles/angular2-polyfills.js',
   paths.npmSrc + '/angular2/bundles/http.dev.js',
   paths.npmSrc + '/angular2/bundles/router.js',
   paths.npmSrc + '/bootstrap/dist/js/bootstrap.js',
   paths.npmSrc + '/systemjs/dist/system.js',
   paths.npmSrc + '/systemjs/dist/system-polyfills.js',
   paths.npmSrc + '/rxjs/bundles/Rx.js',
   paths.npmSrc + '/angular2/bundles/angular2.dev.js',
   paths.npmSrc + '/es6-shim/es6-shim.min.js',
   paths.npmSrc + '/es6-shim/es6-shim.map',
   paths.npmSrc + '/ng2-bootstrap/ng2-bootstrap.js',
   paths.npmSrc + '/jquery/dist/jquery.min.js',
   paths.npmSrc + '/autotrack/autotrack.js',
   paths.npmSrc + '/ng2-material/dist/ng2-material.js'
];
gulp.task('moveToLibs', function () {
    return gulp.src(libsToMove).pipe(gulp.dest(paths.libTarget));
});

gulp.task('sass-compile', function () {
    gulp.src(paths.styleSrc).pipe(sass()).pipe(gulp.dest(paths.styleTarget));
});

gulp.task('watch-sass', function () {
    gulp.watch(paths.styleSrc, ['sass-compile']);
});