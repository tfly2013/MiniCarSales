import React from 'react';
import { Link } from 'react-router-dom';
import { Nav, Navbar } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';

export default props => (
    <Navbar bg="light" expand="lg" sticky="top" collapseOnSelect>
        <Navbar.Brand>
            <Link to={'/'}>Mini-Carsales</Link>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
            <Nav>
                <LinkContainer to={'/'} exact>
                    <Nav.Item>
                        <Nav.Link href="/">Home</Nav.Link>
                    </Nav.Item>
                </LinkContainer>
                <LinkContainer to={'/car'}>
                    <Nav.Item>
                        <Nav.Link href="/car">Car</Nav.Link>
                    </Nav.Item>
                </LinkContainer>
            </Nav>
        </Navbar.Collapse>
    </Navbar>
);
