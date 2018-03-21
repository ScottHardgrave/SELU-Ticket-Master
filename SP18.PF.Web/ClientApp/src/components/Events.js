import React, { Component } from 'react';
import axios from 'axios';
import request from 'request';
import { Button } from 'reactstrap';


export class Events extends Component {
    displayName = Events.name

    constructor(props) {
        super(props);
        this.state = { events: [], loading: true };
        this.getEventInfo = this.getEventInfo.bind(this);
        this.getEventInfo();

    }

    getEventInfo() {
        axios.get('/api/events')
            .then(response => {
                const data = response.data;
                console.log(response.data);
                this.setState({ events: data });
            })
    }
    render() {

        return (

            <div>
                <h1>Events</h1>
                

                <p>All of the venues</p>
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
                            return (
                                <tr key={events.tourId}>
                                    <td>{events.venueName}</td>
                                    <td>{events.tourName}</td>
                                    <td>{events.ticketPrice}</td>
                                    <td>{events.eventStart.substring(5, 7)}{"/"}{events.eventStart.substring(8, 10)}{"/"}{events.eventStart.substring(0, 4)}{"  "}{events.eventStart.substring(21, 25)}{" PM"}</td>
                                    <td>{<Button color='primary'>Purchase</Button>}</td>

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