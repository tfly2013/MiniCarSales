import React from 'react';
import { Route } from 'react-router-dom';
import { Container } from 'react-bootstrap';
import NavMenu from './components/NavMenu';
import CarPage from './components/Car/CarPage';
import Home from './components/Home';

export default () => (
    <React.Fragment>
        <NavMenu />
        <Container>
            <Route exact path='/' component={Home} />
            <Route path='/car' component={CarPage} />
        </Container>
    </React.Fragment>
);
