import { push } from 'connected-react-router';

// Action Types
const receiveCarListType = 'RECEIVE_CAR_LIST';
const requestCarType = 'REQUEST_SINGLE_CAR';
const receiveCarType = 'RECEIVE_SINGLE_CAR';

// Initial state
const initialState = {
    cars: [],
    currentCarId: 0
};

export const actionCreators = {
    requestCarList: () => async (dispatch) => {
        const url = `api/Cars`;
        const response = await fetch(url);
        const carsArray = await response.json();

        // Convert array to object with carId as key for easier state management.
        const cars = carsArray.reduce((result, item) => {
            result[item.id] = item;
            return result;
        }, {});

        dispatch({ type: receiveCarListType, cars });
    },
    requestCar: (carId) => async (dispatch) => {
        dispatch({ type: requestCarType, carId });

        if (carId <= 0) {
            return;
        }

        const url = `api/Cars/${carId}`;
        const response = await fetch(url);
        const car = await response.json();

        dispatch({ type: receiveCarType, carId, car });        
    },
    createCar: (car) => async (dispatch) => {

        const url = `api/Cars`;
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(car)
        });
        const carCreated = await response.json();       

        // redirect to new created car page
        dispatch(push(`/car?id=${carCreated.id}`));
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === receiveCarListType) {
        return {
            ...state,
            cars: action.cars            
        };
    }

    if (action.type === requestCarType) {
        return {
            ...state,
            currentCarId: action.carId
        };
    }

    if (action.type === receiveCarType) {
        return {
            ...state,
            cars: {
                ...state.cars,
                [action.carId]: action.car
            },
            currentCarId: action.carId
        };
    }

    return state;
};
