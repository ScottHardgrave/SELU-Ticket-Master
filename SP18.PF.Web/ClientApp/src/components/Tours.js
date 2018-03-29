import React, { Component } from 'react';
import axios from 'axios';
import { Button } from 'reactstrap';
import { Route } from 'react-router-dom';
var musicPicture = require('../../src/music.jpg');
var background = require('../../src/background.jpg');
var logo = require('../../src/ticketslogo.jpg');

export class Tours extends Component {
    displayName = Tours.name

    constructor(props) {
        super(props);
        this.state = { tours: [], loading: true };
        this.getTourInfo = this.getTourInfo.bind(this);
        this.getTourInfo();

    }

    getTourInfo() {
        axios.get('/api/tours')
            .then(response => {
                const data = response.data;
                console.log(response.data);
                this.setState({ tours: data });
            })
    }
    render() {
        const RouteButton = ({ tourId }) => (
            <Route render={({ history }) => (
                <Button
                    color='success'
                    onClick={() => { history.push(`/Tour/${tourId}`) }}
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
                <h1 align='center'>Tours</h1>
                <p>All of the Tours</p>
                <table className='table'>
                    <thead>
                        <tr>
                            <th> </th>
                            <th>Tour name</th>
                            <th>Description</th>
                            <th></th>

                        </tr>
                    </thead>
                    <tbody>
                        {this.state.tours.map(tours => {
                            return (
                                <tr key={tours.name}>
                                <td><img src={musicPicture} width="25" height="25"/></td>
                                    <td>{tours.name}</td>
                                    <td>{tours.description}</td>
                                    <td><RouteButton tourId={tours.id} /></td>

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