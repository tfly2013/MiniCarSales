import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import CarDropDown from './CarDropDown';
import CarForm from './CarForm';
import { actionCreators } from '../../store/Cars';
import { Col, Row } from 'react-bootstrap';
import queryString from 'query-string';

class CarPage extends Component {

    render() {
        // Retrieve the query strings attached to url
        const querys = queryString.parse(this.props.location.search);
        const isCreate = "new" in querys;
        const carId = parseInt(querys.id) || 0;
        return (
            <React.Fragment>                
                <Row className="my-3">
                    <Col sm="8">
                        <h1>Car</h1>
                    </Col>
                    <Col sm="4">
                        <CarDropDown />
                    </Col>
                </Row>
                <Row>
                    <Col>
                        {isCreate || carId ?
                            <CarForm submitting="false" carId={carId} isCreate={isCreate} />
                            : <h5>Please select a car or create a new car.</h5>
                        }
                    </Col>
                </Row>
            </React.Fragment>
        );
    }
}

export default connect(
    state => state.cars,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(CarPage);
