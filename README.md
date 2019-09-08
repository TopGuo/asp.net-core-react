# asp.net-core-react
asp.net core react
## 简介
1. 开发依赖环境

```
.NET Core SDK (reflecting any global.json): 
Version:   2.2.300

Runtime Environment:
OS Name:     Mac OS X

Host (useful for support):
Version: 2.2.5

node -v
v10.16.0
```
2. 开发语言
asp.net core
react

3. 开发工具
vscode

## 开发流程

1. 创建sln项目解决方案「sln 不是必须创建 但是创建了对智能提示友好」
`dotnet new sln`

2. 组织项目
为了时髦一些我在和sln并列层级目录里床架了一个src文件夹 以此来管理源代码
`mkdir src`
3. 进入src 目录创建一个react web应用 取名为web
`cd src`
`dotnet new react -o web`
4. 回到项目根目录 将新添加的 web项目 添加到sln 项目解决文件中
`dotnet sln add src/web/web.csproj`
「这个一定要➕加」
我们当前目录结构是这样的
```
.
├── LICENSE
├── README.md
├── asp.net-core-react.sln
└── src
    └── web
        ├── ClientApp
        ├── Controllers
        ├── Pages
        ├── Program.cs
        ├── Properties
        ├── Startup.cs
        ├── appsettings.Development.json
        ├── appsettings.json
        ├── obj
        └── web.csproj

7 directories, 8 files

```
5. 启动项目 -p 指定项目启动文件 src/web 里面有Program.cs 致我们的启动项目文件
`dotnet run -p src/web/`
6. 项目启动ok
![WX20190906-122334](/assets/WX20190906-122334.png)
7. 浏览web/ClientApp/package.json
```
"scripts": {
    "start": "rimraf ./build && react-scripts start",
    "build": "react-scripts build",
    "test": "cross-env CI=true react-scripts test --env=jsdom",
    "eject": "react-scripts eject",
    "lint": "eslint ./src/"
  }
```
scripts里集成了几个命令 用来启动 react 这个项目 或者build这个项目 启动这个项目之前需要加上npm
`npm start`
`npm build`
`npm test`
...
执行目录要在ClinetApp文件夹下

8. build react 项目
![WX20190906-132807](/assets/WX20190906-132807.png)
9. 发布项目
在sln同一个层次创建release文件夹用来存放发布文件
`dotnet publish -c release -o ../../release/`
将项目发布出去
![WX20190906-133251](/assets/WX20190906-133251.png)
10. 执行发布文件
`dotnet release/web.dll`
11. 关于环境变量设置推文
[https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/environments?view=aspnetcore-2.2](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/environments?view=aspnetcore-2.2)

## 项目已经推送到GitHub
地址:
[https://github.com/TopGuo/asp.net-core-react](https://github.com/TopGuo/asp.net-core-react)

[第二章节](https://github.com/TopGuo/asp.net-core-react/blob/master/doc/readme2.md)


