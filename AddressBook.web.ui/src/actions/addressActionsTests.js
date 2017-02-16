import expect from 'expect';
import * as addressActions from './addressActions';
import * as types from './actionTypes';

import thunk from 'redux-thunk';
import nock from 'nock';
import configureMockStore from 'redux-mock-store';

describe('Address Actions', () => {
  describe('addAddressSuccess', () => {
    it('should create an ADD_ADDRESS_SUCCESS action', () => {
      //arrange
      const entry = {FirstName: 'The', LastName: 'Answer'};
      const expectedAction = {
        type: types.ADD_ADDRESS_SUCCESS,
        addressBookEntry: entry
      };

      //act
      const action = addressActions.addAddressSuccess(entry);

      //assert
      expect(action).toEqual(expectedAction);
    });
  });

  describe('loadAddressesSuccess', () => {
    it('should create an LOAD_ADDRESSES_SUCCESS action', () => {
      //arrange
      const entry = {Id: '76', FirstName: 'The', LastName: 'Answer'};
      const expectedAction = {
        type: types.LOAD_ADDRESSES_SUCCESS,
        addressBookEntries: [entry]
      };

      //act
      const action = addressActions.loadAddressesSuccess([entry]);

      //assert
      expect(action).toEqual(expectedAction);
    });
  });

  describe('deleteAddressSuccess', () => {
    it('should create an DELETE_ADDRESS_SUCCESS action', () => {
      //arrange
      const entry = {Id: '76', FirstName: 'The', LastName: 'Answer'};
      const expectedAction = {
        type: types.DELETE_ADDRESS_SUCCESS,
        addressBookEntryId: entry.Id
      };

      //act
      const action = addressActions.deleteAddressSuccess(entry.Id);

      //assert
      expect(action).toEqual(expectedAction);
    });
  });

  describe('updateAddressSuccess', () => {
    it('should create an UPDATE_ADDRESS_SUCCESS action', () => {
      //arrange
      const entry = {Id: '76', FirstName: 'The', LastName: 'Answer'};
      const expectedAction = {
        type: types.UPDATE_ADDRESS_SUCCESS,
        addressBookEntry: entry
      };

      //act
      const action = addressActions.updateAddressSuccess(entry);

      //assert
      expect(action).toEqual(expectedAction);
    });
  });
});
