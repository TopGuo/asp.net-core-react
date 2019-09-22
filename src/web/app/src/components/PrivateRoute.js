import React from 'react'
import { Redirect, Route } from 'react-router-dom';

export default function PrivateRoute({ component: Component, ...rest }) {
    let token = localStorage.getItem('token') !== null && localStorage.getItem('token') !== '';
    return (
        <Route
            {...rest}
            render={props => token ? <Component {...props} /> : <Redirect to={{
                pathname: '/login',
                state: { from: props.location }
            }} />}
        >
        </Route>
    )
}
