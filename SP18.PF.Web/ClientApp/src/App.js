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
import axios from 'axios';



export default class App extends Component {
  displayName = App.name

  constructor(props) {
    super(props);
    this.state = { user: [], loading: true, searchTerm: '', userEmail: [], logged: false };
    this.getUserInfo= this.getUserInfo.bind(this);
    this.getUserInfo();

  }
  
  getUserInfo() {
    axios.get('/api/users/me')
      .then(response => {
        const data = response.data;
        const length = response.data.length;
        console.log(length);
        console.log(data);
        if (length != 0) {

          this.setState({ user: data, logged: true });
        }
      })
  }
  

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        {this.state.logged? <Route path='/mytickets' component={MyTickets}/> : <div></div>}
        {this.state.logged? <Route path='/myaccount' component={MyAccount}/> : <div></div>}
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
