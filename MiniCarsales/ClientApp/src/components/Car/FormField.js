import React, { Component } from 'react';
import { Row, Col, Form } from 'react-bootstrap';

// A simple component that represents one field in the form.
export default class extends Component {

    render() {
        const { input, label, type, editable, required, meta } = this.props;
        const invalid = meta.touched && meta.error;
        return (
            <Form.Group as={Row}>
                <Form.Label column sm="2">{label}</Form.Label>
                <Col sm="10">
                    <Form.Control {...input} placeholder={editable ? label : ""} type={type} plaintext={!editable} readOnly={!editable} required={required} isInvalid={invalid} />
                    {invalid &&
                        <Form.Control.Feedback type="invalid">
                            {meta.error}
                        </Form.Control.Feedback>                        
                    }
                </Col>
            </Form.Group>
        )
    }
}