import React, { Component } from 'react';
import { Card, CardColumns, CardDeck, CardBody, CardTitle, CardImg, Button, CardSubtitle, CardText } from 'reactstrap';
import { Route } from 'react-router-dom';
var ticketsrus = require('../../src/ticketsrus.gif')

export class Home extends Component {
  displayName = Home.name

  render() {

      const RouteButton1 = () => (
          <Route render={({ history }) => (
              <Button
                  color='primary'
                  onClick={() => { history.push(`/Venues`) }}
              >
                  Venues
    </Button>
          )} />
      )


      const RouteButton2 = () => (
          <Route render={({ history }) => (
              <Button
                  color='primary'
                  onClick={() => { history.push(`/Venues`) }}
              >
                  Tours
    </Button>
          )} />
      )


      const RouteButton3 = () => (
          <Route render={({ history }) => (
              <Button
                  color='primary'
                  onClick={() => { history.push(`/PurchaseTickets`) }}
              >
                  Buy Tickets
    </Button>
          )} />
      )

    return (
      <div>
        <h1 align="center">Welcome to Tickets R' Us!</h1>
        <img src={ticketsrus} width="1375px" height="300px"/>

        <h3>Toys R' Us has decided to rebrand as a Music App called Tickets R' Us! Feel free to search through our music options and have fun! </h3>
      <br/>
      <br/>
      <br/>
      <div class="col-sm-4">       
        <Card>
        <CardImg top width="200px" src="http://www.dw.com/image/18920718_303.jpg" alt="Card image cap" />
        <CardBody>
          <CardTitle>Check out Venues!</CardTitle>
          <CardSubtitle>See what is playing at venues near you!</CardSubtitle>
          <Button size='lg'><RouteButton1 /></Button>
        </CardBody>
      </Card>
      </div>
      <div class="col-sm-4">
      <Card>
        <CardImg top width="200px" src="https://media.npr.org/assets/img/2017/08/04/20161020_herbie_concert_banner_wide-a9527ea7e2350182ce144909739f5e224c7cbb0f-s900-c85.jpg" alt="Card image cap" />
        <CardBody>
          <CardTitle> Check out Tours!</CardTitle>
          <CardSubtitle>See where your favorite bands are going next!</CardSubtitle>
          <Button size='lg'><RouteButton2 /></Button>
        </CardBody>
      </Card>
      </div>
      <div class="col-sm-4">
      <Card>
        <CardImg top width="200px" height="115px" src="https://mactemplates.com/media/blog-concert_tickets_template-1024x384.jpg" alt="Card image cap" />
        <CardBody>
          <CardTitle>Buy Tickets!</CardTitle>
          <CardSubtitle>Lets go!</CardSubtitle>
          <Button size='lg'><RouteButton3 /></Button>
        </CardBody>
      </Card>
      </div>
     </div>
    );
  }
}
