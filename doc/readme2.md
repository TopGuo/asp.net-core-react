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

```
CREATE TABLE `admin_action` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '权限ID',
  `action_name` varchar(30) DEFAULT NULL COMMENT '权限名称',
  `code` varchar(50) NOT NULL COMMENT '权限代码，用于快速区分权限',
  `info` varchar(100) DEFAULT NULL COMMENT '权限备注信息',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `enable` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COMMENT='权限表';

CREATE TABLE `admin_role_action` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '岗位权限、关联ID',
  `role_id` int(11) NOT NULL COMMENT '岗位ID',
  `action_id` int(11) NOT NULL COMMENT '权限ID',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '关联时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `rright` (`role_id`,`action_id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COMMENT='权限关联表';

CREATE TABLE `admin_roles` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '岗位ID',
  `role_name` varchar(50) DEFAULT NULL COMMENT '岗位名称',
  `info` varchar(100) DEFAULT NULL COMMENT '备注',
  `is_deleted` tinyint(1) DEFAULT '0' COMMENT '是否删除 0:未删除，1：已经删除',
  `create_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COMMENT='岗位表';

CREATE TABLE `admin_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(11) NOT NULL,
  `password` varchar(32) NOT NULL,
  `nickname` varchar(30) DEFAULT NULL,
  `role_id` int(11) NOT NULL,
  `create_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8 COMMENT='管理用户表';
```
建库 创建这几个表 然后采用dbfrist 的方式 生成实体entity 参考如下链接
[https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/dotnet#dotnet-ef-dbcontext-scaffold](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/dotnet#dotnet-ef-dbcontext-scaffold)
为数据库的DbContext和实体类型生成代码。 为了使此命令生成实体类型, 数据库表必须具有主键
`EF Core 提供两种主要方法来保持 EF Core 模型和数据库架构同步。至于我们应该选用哪个方法，请确定你是希望以 EF Core 模型为准还是以数据库为准。`两种方式各有优点 不一定 code frist 就牛逼！例如我就喜欢设计好数据库 然后生成实体 这种方式属于db frist！

这里我们使用mysql 需要安装mysql provider for ef core

`dotnet add package Pomelo.EntityFrameworkCore.MySql --version 2.2.0`

强调1 这里的Design 必须安装！！！

`dotnet add package Microsoft.EntityFrameworkCore.Design`

下面这两个命令是拓展参数

`dotnet ef dbcontext scaffold "Server=localhost;Database=ef;User=root;Password=123456;" "Pomelo.EntityFrameworkCore.MySql"`

`dotnet ef dbcontext scaffold "Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -t Blog -t Post --context-dir Context -c BlogContext`

强调2 修改你的csproj项目TargetFramework 不能是netstandard 要换成netcoreapp

![WX20190906-213359@2x](/assets/WX20190906-213359@2x.png)

我最终执行命令如下：

```
dotnet ef dbcontext scaffold "Server=***;Database=****;User=****;Password=***...;"
 "Pomelo.EntityFrameworkCore.MySql" -o entitys --context-dir context
 ```
![WX20190906-213615@2x](/assets/WX20190906-213615@2x.png)

## repositorys
在domain下创建 repository 这里主要放服务接口

```

├── LICENSE
├── README.md
├── asp.net-core-react.sln
├── assets
│   ├── WX20190906-122334.png
│   ├── WX20190906-132807.png
│   ├── WX20190906-133251.png
│   ├── WX20190906-213359@2x.png
│   └── WX20190906-213615@2x.png
├── doc
│   ├── certs
│   │   └── 1.txt
│   ├── img
│   │   ├── WX20190906-122334.png
│   │   ├── WX20190906-132807.png
│   │   ├── WX20190906-133251.png
│   │   ├── WX20190906-213359@2x.png
│   │   └── WX20190906-213615@2x.png
│   └── readme2.md
├── release
│   ├── ClientApp
│   │   └── build
│   │       ├── asset-manifest.json
│   │       ├── favicon.ico
│   │       ├── index.html
│   │       ├── manifest.json
│   │       ├── service-worker.js
│   │       └── static
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── web.Views.dll
│   ├── web.Views.pdb
│   ├── web.config
│   ├── web.deps.json
│   ├── web.dll
│   ├── web.pdb
│   └── web.runtimeconfig.json
└── src
    ├── application
    │   ├── application.csproj
    │   ├── bin
    │   │   └── Debug
    │   ├── obj
    │   │   ├── Debug
    │   │   ├── application.csproj.nuget.cache
    │   │   ├── application.csproj.nuget.dgspec.json
    │   │   ├── application.csproj.nuget.g.props
    │   │   ├── application.csproj.nuget.g.targets
    │   │   └── project.assets.json
    │   └── services
    │       ├── AccountService.cs
    │       ├── DbRepository.cs
    │       ├── OrderService.cs
    │       └── bases
    ├── domain
    │   ├── bin
    │   │   └── Debug
    │   ├── context
    │   │   └── baixiaosheng_1Context.cs
    │   ├── domain.csproj
    │   ├── entitys
    │   │   ├── AdminActions.cs
    │   │   ├── AdminRoleAction.cs
    │   │   ├── AdminRoles.cs
    │   │   └── AdminUsers.cs
    │   ├── obj
    │   │   ├── Debug
    │   │   ├── domain.csproj.EntityFrameworkCore.targets
    │   │   ├── domain.csproj.nuget.cache
    │   │   ├── domain.csproj.nuget.dgspec.json
    │   │   ├── domain.csproj.nuget.g.props
    │   │   ├── domain.csproj.nuget.g.targets
    │   │   └── project.assets.json
    │   └── repository
    │       ├── IAccountService.cs
    │       ├── IDbRepository.cs
    │       └── IOrderService.cs
    ├── infrastructure
    │   ├── Extensions
    │   │   └── ServiceExtension.cs
    │   ├── bin
    │   │   └── Debug
    │   ├── infrastructure.csproj
    │   └── obj
    │       ├── Debug
    │       ├── infrastructure.csproj.nuget.cache
    │       ├── infrastructure.csproj.nuget.dgspec.json
    │       ├── infrastructure.csproj.nuget.g.props
    │       ├── infrastructure.csproj.nuget.g.targets
    │       └── project.assets.json
    └── web
        ├── ClientApp
        │   └── package-lock.json
        ├── Controllers
        │   └── SampleDataController.cs
        ├── Pages
        │   ├── Error.cshtml
        │   ├── Error.cshtml.cs
        │   └── _ViewImports.cshtml
        ├── Program.cs
        ├── Properties
        │   └── launchSettings.json
        ├── Startup.cs
        ├── appsettings.Development.json
        ├── appsettings.json
        ├── bin
        │   ├── Debug
        │   └── Release
        ├── obj
        │   ├── Debug
        │   ├── project.assets.json
        │   ├── web.csproj.EntityFrameworkCore.targets
        │   ├── web.csproj.nuget.cache
        │   ├── web.csproj.nuget.dgspec.json
        │   ├── web.csproj.nuget.g.props
        │   └── web.csproj.nuget.g.targets
        └── web.csproj

40 directories, 77 files
```
接下来写react！


