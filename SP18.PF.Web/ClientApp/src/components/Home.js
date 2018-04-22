import React, { Component } from 'react';
import { Card, CardColumns, CardDeck, CardBody, CardTitle, CardImg, Button, CardSubtitle, CardText } from 'reactstrap';
import { Route } from 'react-router-dom';
import { Image } from 'react-bootstrap';
var ticketsrus = require('../../src/ticketsrus.gif');
var logo = require('../../src/ticketslogo.jpg');
var background = require('../../src/background.jpg');

export class Home extends Component {
  displayName = Home.name

  render() {

    const RouteButton1 = () => (
      <Route render={({ history }) => (
        <Button
          color='success'
          onClick={() => { history.push(`/Venues`) }}
        >
          Venues
    </Button>
      )} />
    )


    const RouteButton2 = () => (
      <Route render={({ history }) => (
        <Button
          color='success'
          onClick={() => { history.push(`/tours`) }}
        >
          Tours
    </Button>
      )} />
    )


    const RouteButton3 = () => (
      <Route render={({ history }) => (
        <Button
          color='success'
          onClick={() => { history.push(`/PurchaseTickets`) }}
        >
          Buy Tickets
    </Button>
      )} />
    )

    return (
      <div style={{ backgroundImage: `url(${background})`, backgroundSize: '1400px 1400px' }}>
        <h1 align="center"><img src={logo} height='200px' width='400px' /></h1>

        <h1 style={{ textAlign: 'center' }}>Welcome to Tickets R' Us SELU! </h1>
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
            <br />
            <div class="col-sm-2" />
            <div class="col-sm-3">
              <Card>
                <a href="/venues">
                  <CardImg top width="200px" src="http://www.dw.com/image/18920718_303.jpg" alt="Card image cap" />
                </a>
                <CardBody>
                  <CardTitle>Check out Venues!</CardTitle>
                  <CardSubtitle>See what is playing at venues near you!</CardSubtitle>
                  <Button size='lg'><RouteButton1 /></Button>
                </CardBody>
              </Card>
              <br />
            </div>
            <div class="col-sm-3">
              <Card>
                <a href="/tours">
                  <CardImg top width="200px" src="https://media.npr.org/assets/img/2017/08/04/20161020_herbie_concert_banner_wide-a9527ea7e2350182ce144909739f5e224c7cbb0f-s900-c85.jpg" alt="Card image cap" />
                </a>
                <CardBody>
                  <CardTitle> Check out Tours!</CardTitle>
                  <CardSubtitle>See where your favorite bands are going next!</CardSubtitle>
                  <Button size='lg'><RouteButton2 /></Button>
                </CardBody>
              </Card>
              <br />
            </div>
            <div class="col-sm-3">
              <Card>
                <a href="/purchasetickets">
                  <CardImg top width="200px" height="115px" src="https://mactemplates.com/media/blog-concert_tickets_template-1024x384.jpg" alt="Card image cap" />
                </a>
                <CardBody>
                  <CardTitle>Buy Tickets!</CardTitle>
                  <CardSubtitle>Lets go!</CardSubtitle>
                  <Button size='lg'><RouteButton3 /></Button>
                </CardBody>
              </Card>
              <br />
            </div>
            <br />
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
