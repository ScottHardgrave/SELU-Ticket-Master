import React, { Component } from 'react';
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
    this.state = { user: [], loading: true, searchTerm: '', userEmail: [], logged: false };
    this.getUserInfo = this.getUserInfo.bind(this);
    this.getUserInfo();

  }


  getUserInfo() {
    axios.get('/api/users/me')
      .then(response => {
        const data = response.data;
        const length = response.data.length;
        console.log(length);
        console.log(data);
        if (length != 0) {

          this.setState({ user: data, logged: true });
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
               <LinkContainer to={'/Tours'}>
               <NavItem>
               <Glyphicon glyph='tags' /> Tours
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
