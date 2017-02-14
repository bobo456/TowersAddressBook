import * as types from './actionTypes';
import {beginFetch, fetchError} from './fetchStatusActions';
import fetch from 'isomorphic-fetch';

const _queriesBaseUrl = 'http://localhost:8804/api/v1/addressqueriesapi/';
const _commandsBaseUrl = 'http://localhost:8804/api/v1/addresscommandsapi/';

function getFetchOptions(method, data = null){
    const myHeaders =  {'Accept': 'application/json', 'Content-Type': 'application/json'};
    return { method: method, headers: myHeaders, mode: 'cors', cache: 'default', body: data};
}

function handleErrors(response, shouldHaveData) {
    if (!response.ok) {
        throw Error(response.statusText);
    }

    if(shouldHaveData)
        return response.json();

    return response;
}

export function loadAddressesSuccess(addressBookEntries){
    return{type: types.LOAD_ADDRESSES_SUCCESS, addressBookEntries};
}

export function addAddressSuccess(addressBookEntry){
    return{type: types.ADD_ADDRESS_SUCCESS, addressBookEntry};
}

export function deleteAddressSuccess(addressBookEntryId){
    return{type: types.DELETE_ADDRESS_SUCCESS, addressBookEntryId};
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

export function addAddress(addressBookEntry){
    return function(dispatch){
        dispatch(beginFetch());
        return fetch(_commandsBaseUrl + 'addAddressBookEntry', getFetchOptions('POST', JSON.stringify(addressBookEntry)))
            .then(response => handleErrors(response, true))
            .then(response => {
                dispatch(addAddressSuccess(response));
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

export function deleteAddress(addressBookEntryId){
    return function(dispatch){
        dispatch(beginFetch());
        return fetch(_commandsBaseUrl + 'deleteAddressBookEntry?id=' + addressBookEntryId, getFetchOptions('DELETE'))
            .then(handleErrors)
            .then(response => {
                dispatch(deleteAddressSuccess(addressBookEntryId));
            })
            .catch((error) => {
                throw(error);
            });
    };
}

