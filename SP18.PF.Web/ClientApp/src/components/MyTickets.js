import React, { Component } from 'react';
import request from 'request';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import { Route } from 'react-router-dom';

const KEYS_TO_FILTER = ['event.id']

export class MyTickets extends Component {
  displayName = MyTickets.name


  constructor(props) {
    super(props);
    this.state = { tickets: [], loading: true, searchTerm: '', userEmail: [] };
    this.getTicketInfo = this.getTicketInfo.bind(this);
    this.getTicketInfo();
    this.searchUpdated = this.searchUpdated.bind(this)

  }

  getTicketInfo() {
    axios.get('/api/tickets')
      .then(response => {
        const data = response.data;
        const user = response.data[0].user.email;
        this.setState({ tickets: data, userEmail: user });
      })
  }


  render() {
    const { tickets, userEmail } = this.state;
    const filterTickets = tickets.filter(createFilter(this.state.searchTerm, KEYS_TO_FILTER))
    const RouteButton = () => (
      <Route render={({ history }) => (
        <Button
          class='button button-blue'
          color='primary'
          onClick={() => { history.push('/purchasetickets') }}
        >
          Purchase a New Ticket
    </Button>
      )} />
    )
    return (

      <div>
        <h1>My Tickets</h1>
        <p>These are the tickets that {userEmail} has purchased.</p>
        <SearchInput className="search-input" onChange={this.searchUpdated} />
        <RouteButton />
        <table className='table'>
          <thead>
            <tr>
              <th>Event Name</th>
              <th>Purchase Price</th>
              <th>Event Date</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {filterTickets.map(ticket => {
              return (

                <tr key={ticket.id}>
                  <td>{ticket.event.id}</td>
                  <td>{ticket.purchasePrice}</td>
                  <td>{ticket.event.eventStart.substring(0, 16)}</td>
                </tr>
              )

            })}
          </tbody>
        </table>
      </div>
    );
  }

  searchUpdated(term) {
    this.setState({ searchTerm: term })
  }
}
