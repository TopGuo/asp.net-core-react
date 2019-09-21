import React, { Component } from 'react'
import { Form, Icon, Input, Button, message } from 'antd'
import Gapp from '../../configs/models/ConfigInfoModel';
import { Post } from '../../utils/HttpReq';
export default class Login extends Component {
    constructor(props) {
        super(props)
        this.state = {
            username: '',
            password: ''
        }
    }
    handleSubmit = () => {
        let { username, password } = this.state;
        if (username === '') {
            message.warn('用户名不能为空');
            return;
        }
        if (password === '') {
            message.warn('密码不能为空');
            return;
        }
        Post('api/User/Login', { username: this.state.username, password: this.state.password }).then(res => {
            let { data } = res;
            if (data.code === 200) {
                Gapp.userInfo.userName = data.data.userName;
                Gapp.userInfo.token = data.data.token;
                Gapp.userInfo.isLogin = true;
                localStorage.setItem("token", data.data.token);
                this.props.history.replace("/main");
            } else {
                message.error(data.message)
            }
        })
    }
    render() {
        return (
            <div style={styles.container}>
                <div style={{ margin: 100 }}>
                    <p style={{ fontSize: 45, color: Gapp.color.C8 }}>{`${Gapp.appSetting.loginTitle}`}</p>
                </div>
                <div style={styles.tag}>
                    <Form style={styles.tagFrom}>
                        <Form.Item>
                            <Input autoComplete="new-password" onChange={(e) => this.setState({ username: e.target.value })} value={this.state.username}
                                prefix={<Icon type="user" style={{ fontSize: 13 }} />}
                                placeholder='请输入用户名'
                            />
                        </Form.Item>
                        <Form.Item>
                            <Input autoComplete="new-password" onChange={(e) => this.setState({ password: e.target.value })} value={this.state.password}
                                prefix={<Icon type="lock" style={{ fontSize: 13 }} />}
                                placeholder='请输入密码'
                                type="password"
                                onPressEnter={() => this.handleSubmit()}
                            />
                        </Form.Item>
                        <Form.Item>
                            <Button type="primary" style={{ width: '100%' }} onClick={() => this.handleSubmit()}>登陆</Button>
                        </Form.Item>
                    </Form>
                </div>
            </div>
        )
    }
}
const styles = {
    container: { backgroundSize: '100%', display: 'flex', flex: 1, height: document.querySelector('body').offsetHeight, background: `url(${require("../../sources/img/loginBackground.png")}) center center / cover no-repeat` },
    tag: { marginRight: 100, display: 'flex', flex: 1, justifyContent: 'flex-end', alignItems: 'center' },
    tagFrom: { width: "400px", padding: '60px 80px 40px 80px', borderRadius: 4, background: '#00000055' }
}
