import React, { Component } from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import SearchInput, { createFilter } from 'react-search-input';
import { Route } from 'react-router-dom';
import { FormattedDate, FormattedRelative } from 'react-intl';
import {Card} from 'reactstrap';

export class MyAccount extends Component {
  displayName = MyAccount.name

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true, userEmail: [], guest: false, theUser: [] };
    this.getUserInfo = this.getUserInfo.bind(this);
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


  render() {
    const { userEmail, theUser } = this.state;
    return (
    
    <form onSubmit={this.handleSubmit}> 
    <Card>
          <div align='center'>
            {theUser.map(user => {
              return (
                <tr key={user.id}>
                  <div><label> Email</label></div>
                  <input type="text" id="email" placeholder={user.email} class="form-control errorInputOutline" />
                  <div><label> Address </label> </div>
                  <input type="text" id="address" placeholder={user.billingAddress.addressLine1} class="form-control errorInputOutline" />
                  <div><label> City</label></div>
                  <input type="text" id="city" placeholder={user.billingAddress.city} class="form-control errorInputOutline" />
                  <div><label> State </label> </div>
                  <input type="text" id="state" placeholder={user.billingAddress.state} class="form-control errorInputOutline" />
                  <div><label> Zipcode</label></div>
                  <input type="text" id="state" placeholder={user.billingAddress.zipCode} class="form-control errorInputOutline" />
                  <Button color='success' onClick={() =>{alert('Are you sure?')}}>Update</Button>
                </tr>
              )

            })}
          </div>
          </Card>
        </form>
    )

  }


}


