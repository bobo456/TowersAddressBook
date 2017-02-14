import * as types from '../actions/actionTypes';
import initialState from './initialState';

export default function addressReducer(state = initialState.addressBookEntries, action){
    switch(action.type){
        case types.LOAD_ADDRESSES_SUCCESS:
            return action.addressBookEntries;

        case types.DELETE_ADDRESS_SUCCESS:
             return [
                ...state.filter(s => s.Id != action.addressBookEntryId)
            ];
        
        case types.ADD_ADDRESS_SUCCESS:
            return [
                ...state,
                Object.assign({}, action.addressBookEntry) 
            ];
        
        case types.UPDATE_ADDRESS_SUCCESS:
            return [
                ...state.filter(s => s.Id != action.addressBookEntry.Id),
                Object.assign({}, action.addressBookEntry) 
            ];

        default:
            return state;
    }
}