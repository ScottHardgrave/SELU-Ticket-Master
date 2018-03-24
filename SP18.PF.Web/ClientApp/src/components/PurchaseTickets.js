import React, { Component } from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import Popup from 'reactjs-popup';
import CreditCardInput from 'react-credit-card-input';
import Cards from 'react-credit-cards';
import 'react-credit-cards/es/styles-compiled.css';
import { Route } from 'react-router-dom';
import {
  formatCreditCardNumber,
  formatCVC,
  formatExpirationDate,
  formatFormData,
} from './utils';




const KEYS_TO_FILTER = ['id', 'tourName', 'venueName']

export class PurchaseTickets extends Component {
  displayName = PurchaseTickets.name

  constructor(props) {
    super(props);
    this.state =
      {
        tickets: [],
        loading: true, searchTerm: '',
        venues: [], venuesCity: [],
        venuesState: [], venueZip: [], venueAdd: [],
        number: '',
        name: '',
        expiry: '',
        cvc: '',
        issuer: '',
        focused: '',
        formData: null,
      };
    this.getTicketInfo = this.getTicketInfo.bind(this);
    this.getTicketInfo();
    this.searchUpdated = this.searchUpdated.bind(this)
    this.getVenueInfo = this.getVenueInfo.bind(this);
    this.purchaseTicket = this.purchaseTicket.bind(this);
  }


  getTicketInfo() {
    axios.get('/api/events')
      .then(response => {
        const data = response.data;
        console.log(data);
        this.setState({ tickets: data });
      })
  }


  getVenueInfo(venueId) {
    axios.get('/api/venues/' + venueId)
      .then(response => {
        const venueData = response.data;
        const city = response.data.physicalAddress.city;
        const state = response.data.physicalAddress.state;
        const add = response.data.physicalAddress.addressLine1;
        const zip = response.data.physicalAddress.zipCode;
        this.setState({
          venues: venueData, venuesCity: city, venueZip: zip,
          venuesState: state, venueAdd: add
        });
      })
  }

  purchaseTicket(eventId){
    axios.post('/api/tickets/purchase/' + eventId)
    .then(response => {
      alert('Success!');
    })
  }

  handleCallback = ({ issuer }, isValid) => {
    if (isValid) {
      this.setState({ issuer });
    }
  };

  handleInputFocus = ({ target }) => {
    this.setState({
      focused: target.name,
    });
  };

  handleInputChange = ({ target }) => {
    if (target.name === 'number') {
      target.value = formatCreditCardNumber(target.value);
    } else if (target.name === 'expiry') {
      target.value = formatExpirationDate(target.value);
    } else if (target.name === 'cvc') {
      target.value = formatCVC(target.value);
    }

    this.setState({ [target.name]: target.value });
  };

  handleSubmit = e => {
    e.preventDefault();
    const { issuer } = this.state;
    const formData = [...e.target.elements]
      .filter(d => d.name)
      .reduce((acc, d) => {
        acc[d.name] = d.value;
        return acc;
      }, {});

    this.setState({ formData });
    this.form.reset();
  };



  render() {
    const { tickets, venues, venuesCity, venueAdd, venuesState, venueZip,
      name, number, expiry, cvc, focused, issuer, formData } = this.state;
    const filterTickets = tickets.filter(createFilter(this.state.searchTerm, KEYS_TO_FILTER))
    const RouteButton = () => (
      <Route render={({ history }) => (
        <Button
          color='primary'
          onClick={() => { history.push('/') }}
        >
          Purchase
    </Button>
      )} />
    )
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
                  <td>{ticket.id}</td>
                  <td>{ticket.venueName}</td>
                  <td>{ticket.tourName}</td>
                  <td>{ticket.ticketPrice}</td>
                  <td>{ticket.eventStart.substring(5, 7)}{"/"}{ticket.eventStart.substring(8, 10)}{"/"}{ticket.eventStart.substring(0, 4)}{"  "}{ticket.eventStart.substring(21, 25)}{" PM"}</td>
                  <td><Popup trigger={<Button color='primary' type='submit' active>Purchase Ticket</Button>}
                    modal
                    lockScroll={false}
                    closeOnDocumentClick>
                    {close => (
                      <form ref={c => (this.form = c)} onSubmit={this.handleSubmit}>
                        {this.getVenueInfo(ticket.venueId)}
                        <div class="col-sm-9"><label>Ticket Information:</label>
                          <div class="form-group text-left">
                            <label class="col-sm-3">
                              Ticket:
                           </label>
                            <label class="col-sm-9">
                              <Popup trigger={<Button color='primary' size="sm">
                                {ticket.tourName} at  {ticket.venueName}
                              </Button>}>
                                <div> Address: {venueAdd}{"  "}{venuesCity},{venuesState}{"  "}{venueZip}</div>
                                <div> Capacity: {venues.capacity} </div>
                                <div> Description: {venues.description}</div>
                              </Popup>
                            </label>
                          </div>
                          <div class="form-group text-left">
                            <label class="col-sm-3">
                              Subtotal:
                            </label>
                            <label class="col-sm-9">
                              $ {ticket.ticketPrice}
                            </label>
                          </div>
                          <div class="form-group text-left">
                            <label class="col-sm-3">
                              Tax:
                            </label>
                            <label class="col-sm-9">
                              $ {(.0825 * ticket.ticketPrice).toFixed(2)}
                            </label>
                          </div>
                          <div class="form-group text-left">
                            <label class="col-sm-3">
                              Total:
                            </label>
                            <label class="col-sm-9">
                              $ {(ticket.ticketPrice + (.0825 * ticket.ticketPrice)).toFixed(2)}
                            </label>
                          </div>

                          <div><label>Billing Information:</label>
                            <div class="form-group text-left">
                              <label>Address</label>
                              <input type="text" id="purchaseFormAddressLine1" placeholder="Address Line 1" class="form-control errorInputOutline" required />
                              <input type="text" id="purchaseFormAddressLine2" placeholder="Address Line 2" class="form-control validInputOutline" />
                            </div>
                            <div class="form-group text-left">
                              <label for="billingCity">City</label>
                              <input type="text" id="billingCity" placeholder="City" class="form-control errorInputOutline" required />
                            </div>
                            <div class="form-group text-left">
                              <label for="billingState">State</label>
                              <select  required id="dropdown" class="form-control">
                                <option value="AL">Alabama</option>
                                <option value="AK">Alaska</option>
                                <option value="AZ">Arizona</option>
                                <option value="AR">Arkansas</option>
                                <option value="CA">California</option>
                                <option value="CO">Colorado</option>
                                <option value="CT">Connecticut</option>
                                <option value="DE">Delaware</option>
                                <option value="DC">District Of Columbia</option>
                                <option value="FL">Florida</option>
                                <option value="GA">Georgia</option>
                                <option value="HI">Hawaii</option>
                                <option value="ID">Idaho</option>
                                <option value="IL">Illinois</option>
                                <option value="IN">Indiana</option>
                                <option value="IA">Iowa</option>
                                <option value="KS">Kansas</option>
                                <option value="KY">Kentucky</option>
                                <option value="LA">Louisiana</option>
                                <option value="ME">Maine</option>
                                <option value="MD">Maryland</option>
                                <option value="MA">Massachusetts</option>
                                <option value="MI">Michigan</option>
                                <option value="MN">Minnesota</option>
                                <option value="MS">Mississippi</option>
                                <option value="MO">Missouri</option>
                                <option value="MT">Montana</option>
                                <option value="NE">Nebraska</option>
                                <option value="NV">Nevada</option>
                                <option value="NH">New Hampshire</option>
                                <option value="NJ">New Jersey</option>
                                <option value="NM">New Mexico</option>
                                <option value="NY">New York</option>
                                <option value="NC">North Carolina</option>
                                <option value="ND">North Dakota</option>
                                <option value="OH">Ohio</option>
                                <option value="OK">Oklahoma</option>
                                <option value="OR">Oregon</option>
                                <option value="PA">Pennsylvania</option>
                                <option value="RI">Rhode Island</option>
                                <option value="SC">South Carolina</option>
                                <option value="SD">South Dakota</option>
                                <option value="TN">Tennessee</option>
                                <option value="TX">Texas</option>
                                <option value="UT">Utah</option>
                                <option value="VT">Vermont</option>
                                <option value="VA">Virginia</option>
                                <option value="WA">Washington</option>
                                <option value="WV">West Virginia</option>
                                <option value="WI">Wisconsin</option>
                                <option value="WY">Wyoming</option>
                              </select>
                            </div>
                            <div class="form-group text-left">
                              <label for="billingZipCode">ZipCode</label>
                              <input required type="text" id="billingZipCode" placeholder="70706" maxLength="5" class="form-control errorInputOutline" />
                            </div>
                            <div key="Payment">
                              <div className="App-payment">
                                <Cards
                                  number={number}
                                  name={name}
                                  expiry={expiry}
                                  cvc={cvc}
                                  focused={focused}
                                  callback={this.handleCallback}
                                />
                                <div className="col-6"><label>Name</label>
                                    <input
                                      type="text"
                                      name="name"
                                      className="form-control"
                                      placeholder="Name"
                                      required
                                      onChange={this.handleInputChange}
                                      onFocus={this.handleInputFocus}
                                    />
                                  </div>
                                  <div className="col-6"><label>Card Number</label>
                                    <input
                                      type="tel"
                                      name="number"
                                      className="form-control"
                                      placeholder="Card Number"
                                      pattern="[\d| ]{16,22}"
                                      required
                                      onChange={this.handleInputChange}
                                      onFocus={this.handleInputFocus}
                                    />
                                  </div>
                                  <div className="row">
                                    <div className="col-sm-3"><label>Exp</label>
                                      <input
                                        type="tel"
                                        name="expiry"
                                        className="form-control"
                                        placeholder="Valid Thru"
                                        pattern="\d\d/\d\d"
                                        required
                                        onChange={this.handleInputChange}
                                        onFocus={this.handleInputFocus}
                                      />
                                    </div>
                                    <div className="col-sm-3"><label> CVC </label>
                                      <input
                                        type="tel"
                                        name="cvc"
                                        className="form-control"
                                        placeholder="CVC"
                                        pattern="\d{3,4}"
                                        required
                                        onChange={this.handleInputChange}
                                        onFocus={this.handleInputFocus}
                                      />
                                    </div>
                                    </div>
                                  <input type="hidden" name="issuer" value={issuer} />
                                  <div className="form-actions">
                                  <Button color="primary">PAY</Button>
                                  </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </form>
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

