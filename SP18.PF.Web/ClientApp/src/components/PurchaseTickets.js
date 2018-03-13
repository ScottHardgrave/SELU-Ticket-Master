import React, { Component } from 'react';
import { Button } from 'reactstrap';

export class PurchaseTickets extends Component {
  displayName = PurchaseTickets.name

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };

 
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table'>
        <thead>
          <tr>
            <th>Ticket Name</th>
            <th>Purchase Price</th>
            <th>Event Date</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.id}>
              <td>{forecast.id}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {


    return (
     
      <div>
        <h1>Purchase Tickets</h1>
        <p>This component will be used to purchase tickets.</p>
        <Button color='primary'
        onClick ={()=> {alert('Hello')}}>
        Purchase a New Ticket </Button>

      </div>
    );
  }
}
