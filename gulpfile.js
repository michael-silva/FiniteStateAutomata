var gulp  = require('gulp'),
    shell = require('gulp-shell'),
    process = require('process');

var options = {
    cwd: './',
    env: process.env
};

gulp.task('console', shell.task('dnx -p src/Automata.Console run', options));
gulp.task('core', shell.task('dnx -p src/Automata.Core build', options));
gulp.task('test', shell.task('dnx -p test/Automata.Test test', options));