import React, { Component } from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import { Route } from 'react-router-dom';
import { FormattedDate, FormattedRelative } from 'react-intl';
import { Link, DirectLink, Element, Events, animateScroll as scroll, scrollSpy, scroller } from 'react-scroll';
var musicPicture = require('../../src/music.jpg');
var background = require('../../src/background.jpg');
var logo = require('../../src/ticketslogo.jpg');
const KEYS_TO_FILTER = [ 'event.tourName', 'event.venueName']

export class MyTickets extends Component {
  displayName = MyTickets.name


  constructor(props) {
    super(props);
    this.state = { tickets: [], loading: true, searchTerm: '', userEmail: [], logged: false, Guest:'Guest' };
    this.getTicketInfo = this.getTicketInfo.bind(this);
    this.getTicketInfo();
    this.searchUpdated = this.searchUpdated.bind(this);
    this.getUserInfo= this.getUserInfo.bind(this);
    this.getUserInfo();

  }
  
  getUserInfo() {
    axios.get('/api/users/me')
      .then(response => {
        const data = response.data;
        const userData = response.data[0].email;
        const length = response.data.length;
        console.log(length);
        console.log(data);
        console.log(userData);
        if (length != 0) {

          this.setState({ userEmail: userData, logged: true });
        }
      })
  }

  getTicketInfo() {
    axios.get('/api/tickets')
      .then(response => {
        const data = response.data;
        const user = response.data[0].user.email;
        console.log(data);
        this.setState({ tickets: data });
      })
  }


  render() {
    const { tickets, userEmail, Guest, logged } = this.state;
    const filterTickets = tickets.filter(createFilter(this.state.searchTerm, KEYS_TO_FILTER))
    const RouteButton = () => (
      <Route render={({ history }) => (
        <Button
          color='success'
          onClick={() => { history.push('/purchasetickets') }}
        >
          Purchase a New Ticket
    </Button>
      )} />
    )
    return (
      <div style={{ backgroundImage: `url(${background})`, backgroundSize: '1400px 1400px' }}>
      <h1 align="center"><img src={logo} height='200px' width='400px' /></h1>
      <div>
      <br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <div class='container-fluid'>
  <div class='row' style={{ color: 'black', backgroundColor: 'white', opacity: '0.9' }}>
          <Element id="containerElement" style={{
            position: 'relative',
            height: '600px',
            overflow: 'scroll'
          }}>
        <h1 align='center'>My Tickets</h1>
        <p>Logged in as: {this.state.logged ? userEmail : Guest }</p>
        <SearchInput className="search-input" onChange={this.searchUpdated} />
        <RouteButton />
        <table className='table' >
          <thead>
            <tr>
              <th></th>
              <th>Venue Name</th>
              <th>Tour Name</th>
              <th>Purchase Price</th>
              <th>Event Date</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {filterTickets.map(ticket => {
               var newDate = Date.parse(ticket.event.eventStart);
               var parsed = new Date(newDate);
              return (

                <tr key={ticket.id}>
                  <td><img src={musicPicture} width="25" height="25"/></td>
                  <td>{ticket.event.venueName}</td>
                  <td>{ticket.event.tourName}</td>
                  <td>{ticket.purchasePrice}</td>
                  <td> <FormattedDate value={parsed}
                      month="numeric"
                      day="numeric"
                      year="numeric"
                      hour="numeric"
                      minute="numeric"
                    /></td>
                </tr>
              )

            })}
          </tbody>
        </table>
        </Element>
        </div>
        </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        </div>
    );
  }

  searchUpdated(term) {
    this.setState({ searchTerm: term })
  }
}
