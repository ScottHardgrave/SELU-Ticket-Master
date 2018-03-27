﻿import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';
var logo = require('../../src/icon.png');


export class NavMenu extends Component {
  displayName = NavMenu.name
  constructor(props) {
    super(props);
    this.state = { tickets: [], loading: true, searchTerm: '', userEmail: [], logged: false };
    this.getTicketInfo = this.getTicketInfo.bind(this);
    this.getTicketInfo();

  }


  getTicketInfo() {
    axios.get('/api/tickets')
      .then(response => {
        const data = response.data;
        const user = response.data[0].user.email;
        if (user != null) {

          this.setState({ tickets: data, userEmail: user, logged: true });
        }
      })
  }

  render() {
    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={'/'}><img src={logo} height='30px' width='75px' align="center"/></Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
          <Nav>
            <LinkContainer to={'/'} exact>
              <NavItem>
                <Glyphicon glyph='home' /> Home
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/purchasetickets'}>
              <NavItem>
                <Glyphicon glyph='tags' /> Purchase Tickets
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/Venues'}>
              <NavItem>
                <Glyphicon glyph='cd' /> Venues
              </NavItem>
            </LinkContainer>
            {this.state.logged ?
              <LinkContainer to={'/mytickets'}  >
                <NavItem>
                  <Glyphicon glyph='music' /> My Tickets
              </NavItem>
              </LinkContainer>
               :
              <div> </div>
            }
            {this.state.logged ?
               <LinkContainer to={'/myaccount'}  >
               <NavItem>
                 <Glyphicon glyph='user' /> My Account
             </NavItem>
             </LinkContainer> :
            <div> </div> }
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}
