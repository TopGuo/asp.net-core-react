# 重头开始修react 最后载入antd 加到asp.net core 中 实现spa

## 基础知识预备
1. 熟悉.net core
2. 熟悉react
3. 熟悉nodejs
4. 了解npm yarn webpack
5. 了解create-react-app 脚手架
6. 了解antd 蚂蚁金服前中后台框架设计 [https://ant.design/docs/react/introduce-cn 地址](https://ant.design/docs/react/introduce-cn)

## 创建
`使用npm方式常见项目`
1. npm install -g create-react-app //npm 方式安装cra 脚手架
2. npx create-react-app xx「clientapp」

`使用yarn方式创建项目`
1. 安装yarn //yarn
2. yarn create react-app projectname //创建项目

注意命令执行路径·

## 修改startup.cs
将UseSpa中SourcePath指定为你的react 项目名称
```
app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "rapp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
```
## 引入antd
`cd rapp`
`yarn add antd`

## 引入 react-router-dom
`yarn add react-router-dom`

## 引入axiox 一个封装很好的网络请求库
`yarn add axios`

## 对create-react-app 项目的默认配置进行自定义
这个是非常有必要的，如果不配置，默认加载了全部的 antd 组件的样式（gzipped 后一共大约 60kb)
配置后我们只需要加载器用到的css
1. 添加两个库
`yarn add react-app-rewired customize-cra`

2. 修改package.json 文件的script节点
将
```
"scripts": {
    "start": "react-scripts start",
    "build": "react-scripts build",
    "test": "react-scripts test",
    "eject": "react-scripts eject"
  },

```
替换为
```
"scripts": {
    "start": "react-app-rewired start",
    "build": "react-app-rewired build",
    "test": "react-app-rewired test",
    "eject": "react-scripts eject"
  },

```
3. 引入babel-plugin-import babel-plugin-import 是一个用于按需加载组件代码和样式的 babel 插件（原理），现在我们尝试安装它并修改 config-overrides.js 文件
`yarn add babel-plugin-import`

4. 然后在项目根目录创建一个 config-overrides.js 用于修改默认配置
修改内容如下：

```
const { override, fixBabelImports } = require('customize-cra');

module.exports=override(
    fixBabelImports('import',{
        libraryName:'antd',
        libraryDirectory:'es',
        style:'css'
    })
);
```
## 自定义主题
按照 配置主题 的要求，自定义主题需要用到 less 变量覆盖功能。我们可以引入 customize-cra 中提供的 less 相关的函数 addLessLoader 来帮助加载 less 样式，同时修改 config-overrides.js 文件如下
`yarn add less less-loader`

```
- const { override, fixBabelImports } = require('customize-cra');
+ const { override, fixBabelImports, addLessLoader } = require('customize-cra');

module.exports = override(
  fixBabelImports('import', {
    libraryName: 'antd',
    libraryDirectory: 'es',
-   style: 'css',
+   style: true,
  }),
+ addLessLoader({
+   javascriptEnabled: true,
+   modifyVars: { '@primary-color': '#1DA57A' },
+ }),
);
```
这里利用了 less-loader 的 modifyVars 来进行主题配置，变量和其他配置方式可以参考 配置主题 文档
注意:
这个自定义主题一般用到会很少 antd 默认样式就很不错 除非有特俗需求 需要定制样式 然后可以采用这种方式去修改antd默认样式！
![WX20190916-111053_blue](/assets/WX20190916-111053_blue.png)
antd 默认主题样式
![WX20190916-111125_green](/assets/WX20190916-111125_green.png)

自定义后的样式

## 总结
以上主要介绍了react+antd的一套流程 是我们.net core和react的一个衔接 这些东西 我介绍的比较简单 但是需要下一番功夫研究！
.net core+react 封神！
