/*
 * @Author: top.brids 
 * @Date: 2019-09-17 09:42:16 
 * @Last Modified by: top.brids
 * @Last Modified time: 2019-09-17 10:02:39
 */

import { Component } from 'react'
import { withRouter } from 'react-router-dom'
import AppGlobal from '../configs/models/ConfigInfoModel';

const filterCheck = ['/login'];
class CheckLogin extends Component {
    componentDidMount() {
        // 登录注册两个页面不需要判断
        if (filterCheck.indexOf(this.props.location.pathname) > -1) {
            return;
        }
        if (AppGlobal.userInfo.isLogin) {
            this.props.history.push('/main')
        } else {
            this.props.history.push('/login')
        }
    }
    render() {
        return null;
    }

}
export default withRouter(CheckLogin);
