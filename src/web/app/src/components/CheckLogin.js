/*
 * @Author: top.brids 
 * @Date: 2019-09-17 09:42:16 
 * @Last Modified by: top.brids
 * @Last Modified time: 2019-09-21 23:25:49
 */
import { Component } from 'react'
import { withRouter } from 'react-router-dom'

const filterCheck = ['/login'];
class CheckLogin extends Component {
    componentDidMount() {
        // console.log(this.props)
        // 登录注册两个页面不需要判断
        if (filterCheck.indexOf(this.props.location.pathname) > -1) {
            return;
        }
        let token = localStorage.getItem("token");
        if (token !== null && token !== '') {
            this.props.history.replace('/main')
        } else {
            this.props.history.replace('/login')
        }
    }

    render() {
        return null;
    }

}
export default withRouter(CheckLogin);
