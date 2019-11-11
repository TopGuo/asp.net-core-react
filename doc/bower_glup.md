### 一个新的web项目开始，我们总是很自然地去下载需要用到的js类库文件，比如jQuery，去官网下载名为jquery-1.10.2.min.js文件，放到我们的项目里。当项目又需要bootstrap的时候，我们会重复刚才的工作，去bootstrap官网下载对应的类库。如果bootstrap所依赖的jQuery并不是1.10.2，而是2.0.3时，我们会再重新下载一个对应版本的jQuery替换原来的。

### 包管理是个复杂的问题，我们要知道谁依赖谁，还要明确哪个版本依赖哪个版本。这些对于开发人员来说，负担过重了。bower作为一个js依赖管理的工具，提供一种理想包管理方式，借助了npm的一些思想，为我们提供一个舒服的开发环境。

Bower 是 twitter 推出的一款包管理工具，基于nodejs的模块化思想，把功能分散到各个模块中，让模块和模块之间存在联系，通过 Bower 来管理模块间的这种联系

1. 安装
npm install -g bower

2. 初始化
bower init
创建bower.json

3. 创建.bowerrc

### 使用gulp 给依赖包做减法

1. 全局安装
npm install --global gulp-cli
2. 安装开发依赖
npm install --save-dev gulp
npm install --save-dev path
npm install --save-dev del
3. 创建gulpfile.js
touch gulpfile.js

```
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
    "font-awesome"
];

//定义项目中需要引用的bower包中的js、css文件
const copyFiles = [
    "Ionicons/css/ionicons.css",
    "jquery/dist/jquery.min.js",
    "bootstrap/dist/js/bootstrap.min.js"
];

//在复制之前先清空生成目录
gulp.task('clean:all', function (cb) {
    del([paths.dest], cb);
});

//复制文件
gulp.task('copy:file', () => {
    //循环遍历文件列表
    var tasks = copyFiles.map(function (file) {
        //拼接文件完整路径
        var scrFullPath = path.join(`${paths.src}`, file);
        //拼接完整目标路径
        var index = file.lastIndexOf('/');
        var destPath = file.substring(0, index);
        var destFullPath=path.join(`${paths.dest}`, destPath);
        return gulp.src(scrFullPath)
            .pipe(gulp.dest(destFullPath));

    });

});

//复制文件夹
gulp.task('copy:folder', () => {
    var tasks = copyFolders.map(function (folder) {
        //拼接完整目标路径
        var destFullPath = path.join(`${paths.dest}`, folder);
        return gulp.src(path.join(`${paths.src}`, folder + '/**/*'))
            .pipe(gulp.dest(destFullPath));
    });

});

//将三个任务组装在一起
gulp.task('default', ['clean:all', 'copy:file', 'copy:folder']);

```
bundleconfig.json
