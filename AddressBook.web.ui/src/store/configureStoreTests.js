import expect from 'expect';
import {createStore} from 'redux';
import rootReducer from '../reducers/rootReducer';
import initialState from '../reducers/initialState';
import * as addressActions from '../actions/addressActions';


describe('Store', function() {
  it('Should handle adding address book entries', function() {
    // arrange
    const store = createStore(rootReducer, initialState);
    const expectedEntry = {
      FirstName: "Larry", LastName: "Brown"
    };

    // act   -- could call multiple actions at once to test everything looks as expected
    const action = addressActions.addAddressSuccess(expectedEntry);
    store.dispatch(action);

    // assert
    const actualEntry = store.getState().addressBookEntries[0];
    expect(actualEntry.FirstName).toBe(expectedEntry.FirstName);
    expect(actualEntry.LastName).toBe(expectedEntry.LastName);
  });
});