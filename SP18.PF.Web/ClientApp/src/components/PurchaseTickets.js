import React, { Component } from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import Popup from 'reactjs-popup';

const KEYS_TO_FILTER = ['event.id', 'event.tourName', 'event.venueName']

export class PurchaseTickets extends Component {
  displayName = PurchaseTickets.name

  constructor(props) {
    super(props);
    this.state = { tickets: [], loading: true, searchTerm: '' };
    this.getTicketInfo = this.getTicketInfo.bind(this);
    this.getTicketInfo();
    this.searchUpdated = this.searchUpdated.bind(this)
  }


  getTicketInfo() {
    axios.get('/allTickets')
      .then(response => {
        const data = response.data;
        console.log(data)
        this.setState({ tickets: data });
      })
  }



  render() {
    const { tickets } = this.state;
    const filterTickets = tickets.filter(createFilter(this.state.searchTerm, KEYS_TO_FILTER))
    return (

      <div>
        <h1>Purchase Tickets</h1>
        <p>Choose From Tickets Below</p>
        <SearchInput className="search-input" onChange={this.searchUpdated} />
        <table className='table'>
          <thead>
            <tr>
              <th>Event Name</th>
              <th>Venue Name</th>
              <th>Tour Name</th>
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
                  <td>{ticket.event.venueName}</td>
                  <td>{ticket.event.tourName}</td>
                  <td>{ticket.purchasePrice}</td>
                  <td>{ticket.event.eventStart.substring(0, 16)}</td>
                  <td><Popup trigger={<Button color='primary'>Purchase Ticket</Button>}
                    modal
                    closeOnDocumentClick>
                    {close => (
                      <div>
                        <h1>Finish Purchase Order</h1>
                        <form>
                          <label>
                            Name:
                           <input type="text" name="name" />
                            Billing Address:
                           <input type="text" name="billingAddress" />
                           Credit/Debit Card Info:
                           <input type="text" name="CreditCard" />
                          </label>
                        </form>
                        <a className="close" onClick={close}>
                          &times;
                      </a>
                      </div>
                    )}
                  </Popup></td>
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

