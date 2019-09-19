import React, { Component } from 'react'
import { HashRouter as Router, Route, Switch } from 'react-router-dom'
import MainLayout from './MainLayout'
import Login from './admin/Login';
import CheckLogin from '../components/CheckLogin';
import Error from './Error';

export default class Routes extends Component {
    render() {
        return (
            <Router>
                <CheckLogin></CheckLogin>
                <Switch>
                    <Route path="/main" render={(props) => {
                        return <MainLayout {...props}></MainLayout>
                    }}></Route>
                    <Route path="/login" exact component={Login}></Route>
                    <Route component={Error}></Route>
                </Switch>
            </Router>
        )
    }
}
