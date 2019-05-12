import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { Form, Button } from 'react-bootstrap';
import FormField from './FormField';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../../store/Cars';

// A form that displays fields of a car and allow user to create or update a car.
class CarForm extends Component {

    componentWillMount() {
        // request the selected car if selected
        const carId = this.props.carId;
        this.props.requestCar(carId);        
    }

    componentWillReceiveProps(nextProps) {
        // request the upcoming car when selection changed
        if (this.props.carId != nextProps.carId) {
            this.props.requestCar(nextProps.carId);
        }
    }

    handleSubmit(values) {
        // handle form submit.
        const car = { type: "Car", ...values }
        if (this.props.isCreate) {
            this.props.createCar(car);
        }
    }

    render() {
        const isCreate = this.props.isCreate;
        return (
            <Form onSubmit={this.props.handleSubmit(this.handleSubmit.bind(this))}>
                <Field
                    name="make"
                    type="text"
                    component={FormField}
                    label="Make"
                    editable={isCreate}
                    required
                />
                <Field
                    name="model"
                    type="text"
                    component={FormField}
                    label="Model"
                    editable={isCreate}
                    required
                />
                <Field
                    name="engine"
                    type="text"
                    component={FormField}
                    label="Engine"
                    editable={isCreate}
                />
                <Field
                    name="doors"
                    type="number"
                    component={FormField}
                    label="Doors"
                    editable={isCreate}
                />
                <Field
                    name="wheels"
                    type="number"
                    component={FormField}
                    label="Wheels"
                    editable={isCreate}
                />
                <Field
                    name="bodyType"
                    type="text"
                    component={FormField}
                    label="BodyType"
                    editable={isCreate}
                />
                {isCreate &&
                    <Button type="submit" disabled={this.props.submitting} variant="primary">Create</Button>
                }
            </Form>
        )
    }
}

// Synchronized form validation
const formValidation = values => {
    const errors = {};
    // make
    if (!values.make) {
        errors.make = 'Required'
    } else if (values.make.length > 50) {
        errors.make = 'Must be 50 characters or less'
    }

    // model
    if (!values.model) {
        errors.model = 'Required'
    } else if (values.model.length > 50) {
        errors.model = 'Must be 50 characters or less'
    }

    // engine
    if (values.engine && values.engine.length > 30) {
        errors.engine = 'Must be 30 characters or less'
    }

    // doors
    if (isNaN(Number(values.doors))) {
        errors.doors = 'Must be a number'
    } else if (values.doors > 10 || values.doors < 0) {
        errors.doors = 'Must be between 0 and 10.'
    }

    // wheels
    if (isNaN(Number(values.wheels))) {
        errors.wheels = 'Must be a number'
    } else if (values.wheels > 10 || values.wheels < 0) {
        errors.wheels = 'Must be between 0 and 10.'
    }

    // engine
    if (!values.bodyType) {
        errors.bodyType = 'Required'
    } else if (values.bodyType.length > 30) {
        errors.bodyType = 'Must be 30 characters or less'
    }

    return errors;
}

const ReduxFormCarForm = reduxForm({
    form: 'carForm',
    enableReinitialize: true,
    validate: formValidation
})(CarForm)

export default connect(
    state => ({ initialValues: state.cars.cars[state.cars.currentCarId] }),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ReduxFormCarForm)
