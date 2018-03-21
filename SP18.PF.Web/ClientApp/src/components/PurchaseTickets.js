import React, { Component } from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import Popup from 'reactjs-popup';
import CreditCardInput from 'react-credit-card-input';



const KEYS_TO_FILTER = ['event.id', 'event.tourName', 'event.venueName']

export class PurchaseTickets extends Component {
  displayName = PurchaseTickets.name

  constructor(props) {
    super(props);
    this.state = { tickets: [], loading: true, searchTerm: '', cardNumber: [], expiry: [], cvc: [], };
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
                      <div class="col-sm-9"> Billing Information
                      <div class="form-group text-left">
                          <label>Address</label>
                          <input type="text" id="purchaseFormAddressLine1" placeholder="Address Line 1" class="form-control errorInputOutline" />
                          <input type="text" id="purchaseFormAddressLine2" placeholder="Address Line 2" class="form-control validInputOutline" />
                        </div>
                        <div class="form-group text-left">
                          <label for="billingCity">City</label>
                          <input type="text" id="billingCity" placeholder="City" class="form-control errorInputOutline" value="" />
                        </div>
                        <div class="form-group text-left">
                          <label for="billingState">State</label>
                          <select id="dropdown" class="form-control">
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
                          <input type="text" id="billingZipCode" placeholder="70706" maxLength="5" class="form-control errorInputOutline" />
                          </div>
                          <div class="form-group text-left">
                          <label for="billingCardHolderName">Card Holder Name</label>
                          <input type="text" id="billingCardHolderName" placeholder="Name Here" class="form-control errorInputOutline" />
                          </div>
                        
                        <CreditCardInput fieldStyle='wrapper'
                          cardNumberInputProps={{ input: this.state.cardNumber, onChange: this.handleCardNumberChange }} 
                          cardExpiryInputProps={{ input: this.state.expiry, onChange: this.handleCardExpiryChange }}
                          cardCVCInputProps={{ input: this.state.cvc, onChange: this.handleCardCVCChange }}
                          fieldClassName="input"
                        />
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

