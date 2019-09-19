export const Menu_List = [
    {
        key: '1',
        title: '首页',
        icon: 'home',
        path: '/'
    },
    {
        key: '2',
        title: '管理员',
        icon: 'solution',
        submenu: [
            {
                key: '2-1',
                title: '管理员列表',
                icon: 'team',
                path: '/admin'
            },
            {
                key: '2-2',
                title: '角色',
                icon: 'bulb',
                path: '/role'
            },
            {
                key: '2-3',
                title: '操作日志',
                icon: 'red-envelope',
                path: '/risklog'
            },
            {
                key:'2-4',
                title:'三代联动',
                icon:'fire',
                submenu:[
                    {
                        key: '2-4-1',
                        title: '操作日志1',
                        icon: 'red-envelope',
                        path: '/risklog'
                    },
                    {
                        key: '2-4-2',
                        title: '操作日志1',
                        icon: 'red-envelope',
                        path: '/risklog'
                    }
                ]
            }
        ]
    }
]