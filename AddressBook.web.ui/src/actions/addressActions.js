import * as types from './actionTypes';
import {beginFetch, fetchError} from './fetchStatusActions';
import fetch from 'isomorphic-fetch';

const _queriesBaseUrl = 'http://localhost:8804/api/v1/addressqueriesapi/';
const _commandsBaseUrl = 'http://localhost:8804/api/v1/addresscommandsapi/';

function getFetchOptions(method, data = null){
    const myHeaders =  {'Accept': 'application/json', 'Content-Type': 'application/json'};
    return { method: method, headers: myHeaders, mode: 'cors', cache: 'default', body: data};
}

function handleErrors(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}

export function loadAddressesSuccess(addressBookEntries){
    return{type: types.LOAD_ADDRESSES_SUCCESS, addressBookEntries};
}

export function updateAddressSuccess(addressBookEntry){
    return{type: types.UPDATE_ADDRESS_SUCCESS, addressBookEntry};
}

export function loadAddresses(){
    return function(dispatch){
        dispatch(beginFetch());
        return fetch(_queriesBaseUrl + 'getalladdresses', getFetchOptions('GET'))
            .then(response => {return response.json();})
            .then(addressBookEntries=>{
                dispatch(loadAddressesSuccess(addressBookEntries));
            })
            .catch((error) => {
                throw(error);
            });
    };
}

export function updateAddress(addressBookEntry){
    return function(dispatch){
        dispatch(beginFetch());
        return fetch(_commandsBaseUrl + 'updateAddressBookEntry', getFetchOptions('PUT', JSON.stringify(addressBookEntry)))
            .then(handleErrors)
            .then(response => {
                dispatch(updateAddressSuccess(addressBookEntry));
            })
            .catch((error) => {
                throw(error);
            });
    };
}