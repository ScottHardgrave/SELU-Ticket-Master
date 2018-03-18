import React, { Component } from 'react';
import request from 'request';
import { Button } from 'reactstrap';
import axios from 'axios';

export class PurchaseTickets extends Component {
  displayName = PurchaseTickets.name

  constructor(props) {
    super(props);
    this.state = { tickets: [], loading: true };
    this.getTicketInfo = this.getTicketInfo.bind(this) ;
    this.getTicketInfo();
 
  }


 getTicketInfo(){
  axios.get('/api/tickets')
  .then(response => {
    const data = response.data;
    console.log(response.data);
    this.setState({tickets: data});
  })
  }
  /*getTicketInfo(){
    let self =this;    
    var options = { method: 'GET',
    url: '/api/allTickets',
    headers: 
     { 
       'cache-control': 'no-cache'
       } };
  
  request(options, function (error, response, body) {
    if (error) throw new Error(error);
    var hey = body;

    self.setState({tickets: JSON.parse(body), loading: false});
    console.log(body);
    console.log(JSON.parse(body));
  })
  }
  */

  render() {

    return (
     
      <div>
        <h1>Purchase Tickets</h1>
        <p>This component will be used to purchase tickets.</p>
        <table className='table'>
        <thead>
          <tr>
            <th>Ticket Name</th>
            <th>Purchase Price</th>
            <th>Event Date</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {this.state.tickets.map(ticket =>{
            return (
              <tr key={ticket.id}>
              <td>{ticket.id}</td>
              <td>{ticket.purchasePrice}</td>
              <td>{ticket.event.eventStart}</td>
              <td><Button color='primary'
              onClick ={()=> {alert('Hello')}}>
               Purchase a New Ticket </Button></td>
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
