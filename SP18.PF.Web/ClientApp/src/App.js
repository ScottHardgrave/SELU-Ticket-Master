import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { MyTickets } from './components/MyTickets';
import {PurchaseTickets} from './components/PurchaseTickets';
import { Venues } from './components/Venues';
import { Events } from './components/Events';
import { Venue } from './components/Venue';
import { MyAccount } from './components/MyAccount';
import { Tour } from './components/Tour';
import { Events2 } from './components/Events2';
import { Tours } from './components/Tours';



export default class App extends Component {
  displayName = App.name

  

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/mytickets' component={MyTickets}/>
        <Route path='/myaccount' component={MyAccount}/>
        <Route path='/purchasetickets' component={PurchaseTickets}/>
        <Route path='/Venues' component={Venues} />
        {/*<Route path='/Events' component={Events} />*/}
        <Route path='/Venue/:id' component={Venue} />
        <Route path='/Tour/:id' component={Tour} />
        <Route path='/Tours/' component={Tours} />
      </Layout>
    );
  }
}
