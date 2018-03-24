import React, { Component } from 'react';
import axios from 'axios';
import request from 'request';
import { Button } from 'reactstrap';
import { FormattedDate, FormattedRelative } from 'react-intl';
import { Events } from './Events';
import { PurchaseTickets } from './PurchaseTickets';
import { Route } from 'react-router-dom';



export class Venue extends Component {
    displayName = Venue.name

    constructor(props) {
        super(props);
        this.state = { events: [], loading: true };
    }

    componentDidMount() {
        console.log("Find me")
        console.log(this.props);
    }



    render() {


        return (
            <Events venueId={this.props.match.params.id}></Events>


            
        );
    }
}