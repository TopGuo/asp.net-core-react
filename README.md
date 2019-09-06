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

1. 创建sln项目解决方案「多年经验告诉我 sln 不是必须创建 但是创建了对智能提示友好」
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
![WX20190906-122334](https://i.imgur.com/q2nuf9A.png)
7. 
