import React from 'react';
import {Route, IndexRoute} from 'react-router';
import App from './components/App';
import Home from './components/Home';
import AddressesPage from './components/Address/AddressesPage';  //eslint-disable-line import/no-named-as-default
import ManageAddress from './components/Address/ManageAddress';

export default(
    <Route path="/" component={App}>
        <IndexRoute component={Home} />
        <Route path="addresses" component={AddressesPage} />
        <Route path="address" component={ManageAddress} />
        <Route path="address/:id" component={ManageAddress} />
        <Route path="*" component={Home}/>
    </Route>
);