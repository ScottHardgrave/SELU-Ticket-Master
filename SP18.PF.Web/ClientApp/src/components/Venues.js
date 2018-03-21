import React, { Component } from 'react';
import axios from 'axios';
import request from 'request';
import { Button } from 'reactstrap';
import { Route } from 'react-router-dom';

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
        const RouteButton = () => (
            <Route render={({ history }) => (
                <Button
                    color='primary'
                    onClick={() => { history.push('/Events') }}
                >
                    Events
    </Button>
            )} />
        )

        return (

            <div>
                <h1>Venues</h1>
                <p>All of the venues</p>
                <table className='table'>
                    <thead>
                        <tr>
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
                                    <td>{venues.name}</td>
                                    <td>{venues.physicalAddress.addressLine1}{" "}{venues.physicalAddress.city}{" "}{venues.physicalAddress.state}{" "}{venues.physicalAddress.zipCode}</td>
                                    <td>{venues.description}</td>
                                    <td><RouteButton /></td>

                                </tr>
                            )
                        }

                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}