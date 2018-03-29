import React, { Component } from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import { Route } from 'react-router-dom';
import { FormattedDate, FormattedRelative } from 'react-intl';
import { Card } from 'reactstrap';
import {Image, Col} from "react-bootstrap";
var music = require('../../src/music.jpg');
var background = require('../../src/background.jpg');
var logo = require('../../src/ticketslogo.jpg');


export class MyAccount extends Component {
  displayName = MyAccount.name

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true, userEmail: [], guest: false, theUser: [], value:'', send: false, itsOkay: false };
    this.getUserInfo = this.getUserInfo.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.updateInfo = this.updateInfo.bind(this);
    this.isValidform = this.isValidform.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.getUserInfo();
  }

  getUserInfo() {
    axios.get('/api/users/me')
      .then(response => {
        const data = response.data;
        const userData = response.data[0].email;
        const length = response.data.length;
        console.log(length);
        console.log(data);
        console.log(userData);
        if (length != 0) {

          this.setState({ userEmail: userData, theUser: data });
        }
      })
  }

    updateInfo() {
      return axios.put('/api/users/billing-info', 
      {addressLine1: this.state.address, 
      zipCode: this.state.zipcode, city: this.state.city,
      state: this.state.state}
    ).then(response => {
          console.log(response)
      })
      .then(
      alert("Updated Information! Some things may not change based on what you put in."),
      window.location.reload(),
      )

    }

  handleChange(event) {
    const target = event.target;
    const value =  target.type === 'checkbox' ? target.checked : target.value;
    const name = target.name;
    this.setState({[name]: value});
  }

  handleSubmit(event){
    this.setState({
      itsOkay: true
    })
  }


  render() {
    const { userEmail, theUser } = this.state;
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
  <div class='container'>
  <div class='row' style={{ color: 'black', backgroundColor: 'white', opacity: '0.9' }}>
      <form onSubmit={this.handleSubmit}>
        <Card>
          <div class='form-group' align='center'>
            {theUser.map(user => {
              return (
                <tr key={user.id}>
                  <div><h1> My Account </h1></div>
                    <Image src={music} width='100px' height='100px' circle />
           
                  <div><label> </label></div>
                  <div><h2>{user.email}</h2></div>
                 
                  <div><label> Address </label> </div>
                  <input type="text" name="address" placeholder={user.billingAddress.addressLine1} 
                  class="form-control errorInputOutline" 
                  value={this.state.address} 
                  onChange={this.handleChange}/>
                  <div><label> City</label></div>
                  <input type="text" name="city" placeholder={user.billingAddress.city} 
                  class="form-control errorInputOutline" 
                  value={this.state.city} 
                  onChange={this.handleChange}/>
                  <div><label> State </label> </div>
                  <input type="text" pattern="^[A-Za-z]{2}$" maxLength="2" name="state" 
                  placeholder={user.billingAddress.state} 
                  class="form-control errorInputOutline" 
                  value={this.state.state} 
                  onChange={this.handleChange}/>
                  <div><label> Zipcode</label></div>
                  <input type="tel" maxLength="5" minLength="5" name="zipcode" 
                  placeholder={user.billingAddress.zipCode} 
                  class="form-control errorInputOutline"
                  value={this.state.zipcode} 
                  onChange={this.handleChange} />
                  {this.state.send ? <Button color='success' onClick={() => {this.updateInfo(this.state.address) }}>Update
                  </Button> : <Button color='success' disabled ={this.isValidform()}> Update </Button>}
                </tr>
              )

            })}
             <br />
        <br />
        <br />
        <br />
          </div>
        </Card>
      </form>
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
    )

  }
  isValidform() {
    if(this.state.itsOkay == true){
      this.setState({
        send: true
      })
    }
  }



}


