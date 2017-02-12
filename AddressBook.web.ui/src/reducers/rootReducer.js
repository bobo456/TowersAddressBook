import {combineReducers} from 'redux';
import addressBookEntries from './addressReducer';

const rootReducer = combineReducers({
    addressBookEntries
});

export default rootReducer;