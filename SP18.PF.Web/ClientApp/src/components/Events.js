import React, { Component } from 'react';
import axios from 'axios';
import request from 'request';
import { Button } from 'reactstrap';
import { FormattedDate, FormattedRelative } from 'react-intl';


export class Events extends Component {
    displayName = Events.name

    constructor(props) {
        super(props);
        this.state = {
            events: [], venue: { name:"" }, loading: true };
        this.getEventInfo = this.getEventInfo.bind(this);
        this.getVenueInfo = this.getVenueInfo.bind(this);
    }

    componentDidMount() {
        this.getEventInfo();
        this.getVenueInfo();
    }

    getEventInfo() {
        console.log("Props for events.")
        console.log(this.props);
        //this.props.match.params.id;

        axios.get('/api/events',
            {
                params: {
                    venueId: this.props.venueId
                }
            })
            .then(response => {
                const data = response.data;
                console.log(response.data);
                this.setState({ events: data });
            })
    }

    getVenueInfo() {
        console.log("Props for events.")
        console.log(this.props);
        //this.props.match.params.id;

        axios.get('/api/venues/' + this.props.venueId)
            .then(response => {
                const data = response.data;
                console.log(response.data);
                this.setState({ venue: data });
            })
    }

    render() {


        return (

            <div>
                <h1>{this.state.venue.name} events</h1>
                


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