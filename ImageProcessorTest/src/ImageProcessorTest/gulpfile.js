/// <binding AfterBuild='moveToLibs' />

var gulp = require('gulp');

var paths = {
    npmSrc: "./node_modules/",
    libTarget: "./wwwroot/libs/"
};

var libsToMove = [
    paths.npmSrc + '/angular2/bundles/angular2-polyfills.js',
    paths.npmSrc + '/angular2/bundles/http.dev.js',
    paths.npmSrc + '/angular2/bundles/router.js',
    paths.npmSrc + '/systemjs/dist/system.js',
    paths.npmSrc + '/systemjs/dist/system-polyfills.js',
    paths.npmSrc + '/rxjs/bundles/Rx.js',
    paths.npmSrc + '/angular2/bundles/angular2.dev.js',
    paths.npmSrc + '/es6-shim/es6-shim.min.js',
    paths.npmSrc + '/es6-shim/es6-shim.map',
    paths.npmSrc + '/ng2-bootstrap/ng2-bootstrap.js',
    paths.npmSrc + '/jquery/dist/jquery.min.js'
];

gulp.task('moveToLibs', function () {
    return gulp.src(libsToMove).pipe(gulp.dest(paths.libTarget));
});
