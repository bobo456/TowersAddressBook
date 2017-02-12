import * as types from './actionTypes';
import {beginFetch, fetchError} from './fetchStatusActions';
import fetch from 'isomorphic-fetch';

const _queriesBaseUrl = 'http://localhost:8804/api/v1/addressqueriesapi/';
const _commandsBaseUrl = 'http://localhost:8804/api/v1/addresscommandsapi/';

function getFetchOptions(method){
    const myHeaders = new Headers();
    return { method: method, headers: myHeaders, mode: 'cors', cache: 'default' };
    // return { method: method, headers: { "Access-Control-Allow-Origin": "*" }, mode: 'no-cors', cache: 'default' };
}

export function loadAddressesSuccess(addressBookEntries){
    return{type: types.LOAD_ADDRESSES_SUCCESS, addressBookEntries};
}

export function loadAddresses(){
    return function(dispatch){
        dispatch(beginFetch());
        return fetch(_queriesBaseUrl + 'getalladdresses', getFetchOptions('GET'))
            .then(
                response => {
                    return response.json();}
            )
            .then((addressBookEntries)=>{
                dispatch(loadAddressesSuccess(addressBookEntries));
            })
            .catch((error) => {
                throw(error);
            });
    };
}
