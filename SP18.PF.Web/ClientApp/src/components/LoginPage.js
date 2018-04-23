import React, { Component } from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import { Route, withRouter } from 'react-router-dom';
import { FormattedDate, FormattedRelative } from 'react-intl';
import { Card } from 'reactstrap';
import {Image, Col} from "react-bootstrap";
var music = require('../../src/music.jpg');
var background = require('../../src/background.jpg');
var logo = require('../../src/ticketslogo.jpg');


export class LoginPage extends Component {
    displayName = LoginPage

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true, userEmail: [], guest: false, theUser: [], value: '', send: false, itsOkay: false };
    this.handleChange = this.handleChange.bind(this);
    this.updateInfo = this.updateInfo.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);

  }

    handleChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;
        this.setState({ [name]: value });
    }




    updateInfo() {
        return axios.post('/api/users/login',
            {
                email: this.state.email,
                password: this.state.password,
                remeberMe: true
            })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            
            });

    }



    handleSubmit() {
        this.setState({
            itsOkay: true
        })

        this.updateInfo();

    }


    render() {
        var email;
        var password;
    
    return (
      <div style={{ backgroundImage: `url(${background})`, backgroundSize: '1400px 1400px' }}>
      <h1 align="center"><img src={logo} height='200px' width='400px' /></h1>
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
                <form method="post" onSubmit={this.handleSubmit}  >
      <div class='container'>
  <div class='row' style={{ color: 'black', backgroundColor: 'white', opacity: '0.9' }}>
        <Card>
          <div class='form-group' align='center'>
            
              
                <tr>
                  <div><h1> Login </h1></div>
                    <Image src={music} width='100px' height='100px' circle />
           
                  <div><label> </label></div>
                  
                 
                  <div><label> Address </label> </div>
                  <input type="text" name="email" placeholder={"Email"} 
                                            class="form-control errorInputOutline" value={this.state.email}
                                            onChange={this.handleChange}/>
                  <div><label> City</label></div>
                                        <input type="Password" name="password" placeholder={"Password"}
                                            class="form-control errorInputOutline" value={this.state.password}
                                            onChange={this.handleChange}/>
                  <br></br>
                   <Button  color='success' type='submit' value='Submit'> Login </Button>
                </tr>
              

            
             <br />
        <br />
        <br />
        <br />
          </div>
        </Card>
        </div>
      </div>
      </form>
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
    )

  }



}