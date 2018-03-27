import React, { Component } from 'react';
import axios from 'axios';
import { Button } from 'reactstrap';
import { Route } from 'react-router-dom';

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

            <div>
                <h1>Tours</h1>
                <p>All of the Tours</p>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Tour name</th>
                            <th>Description</th>
                            <th></th>

                        </tr>
                    </thead>
                    <tbody>
                        {this.state.tours.map(tours => {
                            return (
                                <tr key={tours.name}>
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
        );
    }
}