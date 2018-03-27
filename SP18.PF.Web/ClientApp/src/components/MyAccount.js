import React, { Component } from 'react';
import axios from 'axios';

export class MyAccount extends Component {
  displayName = MyAccount.name

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true, userEmail:[], guest: false, theUser:[] };
    this.getUserInfo=this.getUserInfo.bind(this);
    this.getUserInfo();
  }

    getUserInfo() {
      axios.get('/api/tickets')
        .then(response => {
          const data = response.data[0];
          const user = response.data[0].user.email;
          console.log(data);
          this.setState({ theUser: data, userEmail: user, guest: true });
        })
    }
  

  render(){
    const {  userEmail, guest, user } = this.state;
    return(
      <tr key={user}>
      <td>{userEmail}</td>
    </tr>

    )
  }

 

}
