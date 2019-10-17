import React, { Component } from 'react'
import { Input, Button, Select, Table, Divider, message } from 'antd'
import moment from 'moment'
import GApp from '../../configs/models/ConfigInfoModel';
import { Post } from '../../utils/HttpReq';

const Colors = GApp.color;

export default class AdminList extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isLoading: true,
            adminList: [],
            currentPage: 1,
            pageSize: 10
        }
    }

    componentDidMount() {
        this.getAdminList()
    }

    getAdminList = () => {
        if (!this.state.isLoading) this.setState({ isLoading: true });
        Post('api/User/GetAdminUsers', { pageIndex: this.state.currentPage, pageSize: this.state.pageSize }).then((res) => {
            if (this.state.isLoading) this.setState({ isLoading: false });
            let { data } = res;

            if (data.code === 200) {
                console.log('ok==',data)
                this.setState({
                    adminList: data.data
                })
            } else {
                message.error(data.message)
            }
        })
    }


    renderAdminList() {
        const columns = [{
            title: "用户名",
            dataIndex: 'username',
            key: 'username',
            width: "15%"
        }, {
            title: "用户ID",
            dataIndex: 'userId',
            key: 'userId',
            width: "15%"
        }, {
            title: "昵称",
            dataIndex: 'nickname',
            key: 'nickname',
            width: "15$%"
        }, {
            title: "角色",
            dataIndex: 'roleName',
            key: 'roleName',
            width: "15%"
        }, {
            title: "注册时间",
            dataIndex: 'createTime',
            key: 'createTime',
            width: "20%",
            render: (text, record) => (
                <p style={{ margin: 0 }}>{`${moment(record['createTime']).format('YYYY年MM月DD日 HH时mm分')}`}</p>
            )
        }, {
            title: '操作',
            key: 'action',
            width: "20%",
            render: (text, record) => (
                <span>
                    <a onClick={() => this.props.router.push({ pathname: `/clientinfo/${record['userId']}` })}>详情</a>
                    <Divider type="vertical" />
                    <a onClick={() => { }}>操作</a>
                </span>
            ),
        }];
        return (
            <Table
                columns={columns}
                loading={this.state.isLoading}
                dataSource={this.state.adminList}
                rowKey='id'
                pagination={{ position: 'bottom', current: this.state.currentPage, total: this.state.totalNumber }}
                onChange={(page, pageSize) => this.fetchAdminList(page.current)}
            />
        )
    }
    render() {
        return (
            <div>
                {this.renderAdminList()}
            </div>
        )
    }
}

const styles = {
    formItem: { width: 180 },
    actionFormItem: { width: 260 },
    label: { fontSize: 14, color: Colors.C1 },
};
