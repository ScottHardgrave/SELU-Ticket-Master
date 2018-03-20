import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { MyTickets } from './components/MyTickets';
import {PurchaseTickets} from './components/PurchaseTickets';
import {Venues} from './components/Venues';



export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} />
        <Route path='/mytickets' component={MyTickets}/>
        <Route path='/purchasetickets' component={PurchaseTickets}/>
        <Route path='/Venues' component={Venues} />
      </Layout>
    );
  }
}
