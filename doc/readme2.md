# 搭建项目

## step 1 domain
第一步我们创建的程序集是一个 Class library 类库程序集 取名为domain
domain -> 领域
这个里面我们做一些提纲掣领的内容放里边 具体放什么 慢慢一步步来
路径切到src下，执行命令创建项目
`dotnet new classlib -o domain`
将新程序集添加到sln中
`dotnet sln add src/domain/domain.csproj`

## step 2 infrastructure
第二步我们继续创建创建一个类库 取名为 infrastructure
infrastructure -> 基础设施
这个里面我们主要放一下基础性的东西 还有工具类的东西 具体放什么 一步步来
路径切到src下，执行命令创建项目
`dotnet new classlib -o infrastructure`
将新程序集添加到sln中
`dotnet sln add src/infrastructure/infrastructure.csproj`

## step 3 application
第三部我们创建的还是一个类库 取名为 application
application -> 应用程序
这里我们主要放 服务的实现 业务逻辑在里面 具体放什么 一步步来
`dotnet new classlib -o application`
将新程序集添加到sln中
`dotnet sln add src/application/application.csproj`

```
./src
├── application
│   ├── application.csproj
│   └── obj
├── domain
│   ├── domain.csproj
│   └── obj
├── infrastructure
│   ├── infrastructure.csproj
│   └── obj
└── web
    ├── ClientApp
    ├── Controllers
    ├── Pages
    ├── Program.cs
    ├── Properties
    ├── Startup.cs
    ├── appsettings.Development.json
    ├── appsettings.json
    ├── bin
    └── web.csproj
```
## 添加项目之间引用
web->application
`dotnet add web/web.csproj reference application/application.csproj`
application->domain
`dotnet add src/application/application.csproj reference src/domain/domain.csproj`
application->infrastructure
`otnet add src/application/application.csproj reference src/infrastructure/infrastructure.csproj`

## 建库加实体


