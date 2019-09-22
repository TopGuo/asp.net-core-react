import React, { Component } from 'react'
import { HashRouter as Router, Route, Switch } from 'react-router-dom'
import MainLayout from './MainLayout'
import Login from './admin/Login';
import PrivateRoute from '../components/PrivateRoute';
import Error from './Error';

export default class Routes extends Component {
    render() {
        return (
            <Router>
                <Switch>
                    <PrivateRoute path="/" exact component={MainLayout} ></PrivateRoute>
                    <PrivateRoute path="/main" component={MainLayout} ></PrivateRoute>
                    <Route path="/login" exact component={Login}></Route>
                    <Route component={Error}></Route>
                </Switch>
            </Router>
        )
    }
}
