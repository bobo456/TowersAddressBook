import expect from 'expect';
import AddressReducer from './addressReducer';
import * as actions from '../actions/addressActions';

describe('Address Reducer', () => {
    it('should add address book entry when passed ADD_ADDRESS_SUCCESS', () => {
        // arrange
        const initialState = [
            {FirstName: 'Kyle', LastName: 'Korver', Email: 'kk@kooky.com'},
            {FirstName: 'Matt', LastName: 'Harpbring', Email: 'mh@sturdy.com'}
        ];

        const newEntry = {FirstName: 'Raja', LastName: 'Bell', Email: 'rb@toocoo.com'};
        const action = actions.addAddressSuccess(newEntry);

        // act
        const newState = AddressReducer(initialState, action);
        
        // assert
        expect(newState.length).toEqual(3);
        expect(newState[0].LastName).toEqual('Korver');
        expect(newState[1].LastName).toEqual('Harpbring');
        expect(newState[2].LastName).toEqual('Bell');
    });

    it('should update address book entry when passed UPDATE_ADDRESS_SUCCESS', () => {
        // arrange
        const initialState = [
            {Id: "1234", FirstName: 'Kyle', LastName: 'Korver', Email: 'kk@kooky.com'},
            {Id: "2345", FirstName: 'Matt', LastName: 'Harpbring', Email: 'mh@sturdy.com'}
        ];

        const updatedEntry = {Id: "2345", FirstName: 'Raja', LastName: 'Bell', Email: 'rb@toocoo.com'};
        const action = actions.updateAddressSuccess(updatedEntry);

        // act
        const newState = AddressReducer(initialState, action);
        
        // assert
        expect(newState.length).toEqual(2);
        expect(newState[0].LastName).toEqual('Korver');
        expect(newState[1].LastName).toEqual('Bell');
    });
});