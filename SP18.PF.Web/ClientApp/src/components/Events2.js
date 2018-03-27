﻿import React, { Component } from 'react';
import axios from 'axios';
import { Button } from 'reactstrap';
import { FormattedDate, FormattedRelative } from 'react-intl';
import { withRouter } from 'react-router-dom';
import Popup from 'reactjs-popup';
import Cards from 'react-credit-cards';
import 'react-credit-cards/es/styles-compiled.css';
import {
    formatCreditCardNumber,
    formatCVC,
    formatExpirationDate,
} from './utils';

import { Link, DirectLink, Element, animateScroll as scroll, scrollSpy, scroller } from 'react-scroll';


export class Events2 extends Component {
    displayName = Events2.name

    constructor(props) {
        super(props);
        this.state = {
            events: [], venue: { name: "" }, loading: true, tour: { name: "" }, loading: true,
            venuesCity: [],
            venuesState: [], venueZip: [], venueAdd: [],
            number: '',
            name: '',
            tourname: '',
            expiry: '',
            cvc: '',
            issuer: '',
            focused: '',
            formData: null,
            send: false,
        };
        this.getEventInfo = this.getEventInfo.bind(this);
        this.getVenueInfo = this.getVenueInfo.bind(this);
        this.getTourInfo = this.getTourInfo.bind(this);
        this.isValidform = this.isValidform.bind(this);
    }

    componentDidMount() {
        this.getEventInfo();
        this.getVenueInfo();
        this.getTourInfo();
    }

    getEventInfo() {
        console.log("Props for events.")
        console.log(this.props);
        //this.props.match.params.id;

        axios.get('/api/events',
            {
                params: {
                    tourId: this.props.tourId,
                    venueId: this.props.venueId
                }
            })
            .then(response => {
                const data = response.data;
                console.log(response.data);
                this.setState({ events: data });
            })
    }


    getTourInfo() {
        console.log("Props for events.")
        console.log(this.props);
        //this.props.match.params.id;

        axios.get('/api/tours/' + this.props.tourId)
            .then(response => {
                const data = response.data;
                console.log(response.data);
                this.setState({ tour: data });
            })
    }

    getVenueInfo(venueId) {
        console.log("Props for events.")
        console.log(this.props);
        //this.props.match.params.id;

        axios.get('/api/venues/' + venueId)
            .then(response => {
                const data = response.data;
                const city = response.data.physicalAddress.city;
                const state = response.data.physicalAddress.state;
                const add = response.data.physicalAddress.addressLine1;
                const zip = response.data.physicalAddress.zipCode;
                console.log(response.data);
                this.setState({
                    venue: data, venuesCity: city, venueZip: zip,
                    venuesState: state, venueAdd: add
                });
            })
    }




    purchaseTicket(eventId) {
        axios.post('/api/tickets/purchase/' + eventId)
            .then(response => {
                console.log(response)
                this.setState({
                    send: false,
                    formData: null,
                })
            }
            )
            .then(
            alert("Congrats on the Ticket Purchase! Enjoy the show!"),
            window.location.reload(),
            window.location = '/mytickets'

            )
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
        const formData = [...e.target.elements]
            .filter(d => d.name)
            .reduce((acc, d) => {
                acc[d.name] = d.value;
                return acc;
            }, {});

        this.setState({ formData: formData });
        this.isValidform();
        
    };

    render() {
        const {  tour, venue, venuesCity, venueAdd, venuesState, venueZip,
            name, number, expiry, cvc, focused, issuer } = this.state;


        return (

            <div>
                
                        <h1>{this.state.tour.name}</h1>





                <table className='table'>
                    <thead>
                        <tr>
                            <th>Venue name</th>
                            <th>Tour name</th>
                            <th>Ticket price</th>
                            <th>Event time</th>
                            <th></th>

                        </tr>
                    </thead>

                    <tbody>
                        {this.state.events.map(events => {
                            var newDate = Date.parse(events.eventStart);
                            var parsed = new Date(newDate);
                            return (
                                <tr key={events.tourId}>
                                    <td>{events.venueName}</td>
                                    <td>{events.tourName}</td>
                                    <td>{events.ticketPrice}</td>
                                    <td>{
                                        <FormattedDate value={parsed}
                                            month="numeric"
                                            day="numeric"
                                            year="numeric"
                                            hour="numeric"
                                            minute="numeric"


                                        />}

                                    </td>
                                    <td><Popup trigger={<Button color='success' type='submit' active>Purchase Ticket</Button>}
                                        modal
                                        lockScroll={false}
                                        closeOnDocumentClick>
                                        {close => (
                                            <Element id="containerElement" style={{
                                                position: 'relative',
                                                height: '600px',
                                                overflow: 'scroll'
                                            }}>
                                                <form ref={c => (this.form = c)} onSubmit={this.handleSubmit}>
                                                   {this.getVenueInfo(events.venueId)}
                                                    <div class="col-sm-9"><label>Ticket Information:</label>
                                                        <div class="form-group text-left">
                                                            <label class="col-sm-3">
                                                                Ticket:
                           </label>
                                                            <label class="col-sm-9">
                                                                <Popup trigger={<Button color='success' size="sm">
                                                                    {events.tourName} at  {events.venueName}
                                                                </Button>}>
                                                                    <div> Address: {venueAdd}{"  "}{venuesCity},{venuesState}{"  "}{venueZip}</div>
                                                                    <div> Capacity: {venue.capacity} </div>
                                                                    <div> Description: {venue.description}</div>
                                                                </Popup>
                                                            </label>
                                                        </div>
                                                        <div class="form-group text-left">
                                                            <label class="col-sm-3">
                                                                Subtotal:
                            </label>
                                                            <label class="col-sm-9">
                                                                $ {events.ticketPrice}
                                                            </label>
                                                        </div>
                                                        <div class="form-group text-left">
                                                            <label class="col-sm-3">
                                                                Tax:
                            </label>
                                                            <label class="col-sm-9">
                                                                $ {(.0825 * events.ticketPrice).toFixed(2)}
                                                            </label>
                                                        </div>
                                                        <div class="form-group text-left">
                                                            <label class="col-sm-3">
                                                                Total:
                            </label>
                                                            <label class="col-sm-9">
                                                                $ {(events.ticketPrice + (.0825 * events.ticketPrice)).toFixed(2)}
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
                                                                <select required id="dropdown" class="form-control">
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
                                                                            pattern="[A-Z][\w]* [A-Z][\w]*"
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
                                                                            acceptedCards={['visa', 'mastercard', 'americanexpress']}
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
                                                                        {this.state.send ? <Button color="success" size='sm' onClick={() => { this.purchaseTicket(events.id) }}>PAY</Button> :
                                                                            <Button color='success' size='sm' disabled={this.isValidform()}>PAY</Button>}
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </form>
                                            </Element>
                                        )}
                                    </Popup></td>
                                </tr>


                            )
                        }

                        )}
                    </tbody>
                </table>

            </div>
        );
    }
    isValidform() {
        if (this.state.formData != null) {
            this.setState({
                send: true
            })
        }
    }
}