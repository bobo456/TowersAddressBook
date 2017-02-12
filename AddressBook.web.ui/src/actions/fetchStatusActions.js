import * as types from './actionTypes';

export function beginFetch(){
    return { type: types.FETCH_BEGAN};
}

export function fetchError(){
    return { type: types.FETCH_ERROR};
}