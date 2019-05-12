import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Cars';
import Dropdown from 'react-bootstrap/Dropdown';
import { LinkContainer } from 'react-router-bootstrap';

// A dropdown that allows user to view a specific car or create new car.
class CarDropDown extends Component {

    componentWillMount() {
        // load car list when mount.
        this.props.requestCarList();
    }

    render() {
        const cars = this.props.cars;
        return (
            <Dropdown>
                <Dropdown.Toggle>
                    View Cars
                </Dropdown.Toggle>
                <Dropdown.Menu>
                    {Object.keys(cars).map(carId =>
                        <LinkContainer key={carId} to={`/car?id=${carId}`}>
                            <Dropdown.Item eventKey={carId}>{cars[carId].make} {cars[carId].model}</Dropdown.Item>
                        </LinkContainer>                        
                    )}
                    <Dropdown.Divider />
                    <LinkContainer to={`/car?new`}>
                        <Dropdown.Item eventKey="4">Create A New Car...</Dropdown.Item>
                    </LinkContainer>
                </Dropdown.Menu>
            </Dropdown>
        );
    }
}

export default connect(
    state => state.cars,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(CarDropDown);
