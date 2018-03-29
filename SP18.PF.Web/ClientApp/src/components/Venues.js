import React, { Component } from 'react';
import axios from 'axios';
import { Button } from 'reactstrap';
import { Route } from 'react-router-dom';
var musicPicture = require('../../src/music.jpg');
var background = require('../../src/background.jpg');
var logo = require('../../src/ticketslogo.jpg');

export class Venues extends Component {
    displayName = Venues.name

    constructor(props) {
        super(props);
        this.state = { venues: [], loading: true };
        this.getVenueInfo = this.getVenueInfo.bind(this);
        this.getVenueInfo();

    }

    getVenueInfo() {
        axios.get('/api/venues')
            .then(response => {
                const data = response.data;
                console.log(response.data);
                this.setState({ venues: data });
            })
    }
    render() {
        const RouteButton = ({ venueId }) => (
            <Route render={({ history }) => (
                <Button
                    color='success'
                    onClick={() => { history.push(`/Venue/${venueId}`) }}
                >
                    Events
    </Button>
            )} />
        )

        return (
            <div style={{ backgroundImage: `url(${background})`, backgroundSize: '1400px 1400px' }}>
            <h1 align="center"><img src={logo} height='200px' width='500px' /></h1>
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
                <h1>Venues</h1>
                <p>All of the venues</p>         
                <table className='table'>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Venue name</th>
                            <th>Address</th>
                            <th>Description</th>
                            <th></th>
                            
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.venues.map(venues => {
                            return (
                                <tr key={venues.name}>
                                <td><img src={musicPicture} width="25" height="25"/></td>
                                    <td>{venues.name}</td>
                                    <td>{venues.physicalAddress.addressLine1}{" "}{venues.physicalAddress.city}{" "}{venues.physicalAddress.state}{" "}{venues.physicalAddress.zipCode}</td>
                                    <td>{venues.description}</td>
                                    <td><RouteButton venueId={venues.id} /></td>

                                </tr>
                            )
                        }

                        )}
                    </tbody>
                </table>
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
}