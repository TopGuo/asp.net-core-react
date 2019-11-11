const gulp = require('gulp');//1. 引用gulp
var path = require('path');//2. 引用path
var del = require('del');//3.引用del

//定义路径
const paths = {
    src: 'wwwroot/plugins/',
    dest: 'wwwroot/lib/'
};

//定义需要完整复制的Bower文件夹
const copyFolders = [
    "bootstrap",
];

//定义项目中需要引用的bower包中的js、css文件
const copyFiles = [
    "jquery/jquery.min.js",
    "bootstrap/dist/js/bootstrap.min.js"
];

//在复制之前先清空生成目录
gulp.task('clean:all', cb => {
    del([paths.dest]);
    cb()
});

//复制文件
gulp.task('copy:file', cb => {
    //循环遍历文件列表
    copyFiles.map(function (file) {
        //拼接文件完整路径
        var scrFullPath = path.join(`${paths.src}`, file);
        //拼接完整目标路径
        var index = file.lastIndexOf('/');
        var destPath = file.substring(0, index);
        var destFullPath = path.join(`${paths.dest}`, destPath);
        return gulp.src(scrFullPath)
            .pipe(gulp.dest(destFullPath));
    });
    cb();

});

//复制文件夹
gulp.task('copy:folder', cb => {
    var tasks = copyFolders.map(function (folder) {
        //拼接完整目标路径
        var destFullPath = path.join(`${paths.dest}`, folder);
        return gulp.src(path.join(`${paths.src}`, folder + '/**/*'))
            .pipe(gulp.dest(destFullPath));
    });
    cb();

});

//将三个任务组装在一起
// gulp.task('default', ['clean:all', 'copy:file', 'copy:folder']);
gulp.task('default', gulp.series('clean:all', 'copy:file', 'copy:folder'), cb => {
    cb();
});