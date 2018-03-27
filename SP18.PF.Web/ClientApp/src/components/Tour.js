import React, { Component } from 'react';
import axios from 'axios';
import request from 'request';
import { Button } from 'reactstrap';
import { FormattedDate, FormattedRelative } from 'react-intl';
import { Events2 } from './Events2';
import { PurchaseTickets } from './PurchaseTickets';




export class Tour extends Component {
    displayName = Tour.name

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
            <Events2 tourId={this.props.match.params.id}></Events2>



        );
    }
}