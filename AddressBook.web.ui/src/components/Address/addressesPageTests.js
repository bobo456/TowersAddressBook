import React from 'react';
import expect from 'expect';
import {mount, shallow} from 'enzyme';
import {AddressesPage} from './Addressespage';
import toastr from 'toastr';

function Setup(props){
    return mount(<AddressesPage {...props} />);
}

describe('AddressesPage', () => {
     const props = {
         addressBookEntries: [{"id": "guid"}],
         actions: {deleteAddress: () => { return Promise.resolve();}}
    };
    
    it('Should render an address list', () => {
        const wrapper = Setup(props);
        expect(wrapper.find('AddressList').length).toBe(1);
        expect(wrapper.find('AddressList').props().addressBookEntries[0].id).toBe('guid');
    });

    it('Should render a Add button', () => {
        const wrapper = Setup(props);
        expect(wrapper.find('input').length).toBe(1);
        expect(wrapper.find('input').props().value).toBe('Add Address');
        expect(wrapper.find('input').props().type).toBe('submit');
    });
    
    it('Should redirect to add address form when add button is clicked', () => {
        // const wrapper = Setup(props);
        // let redirectSpy = expect.spyOn(AddressesPage.prototype, 'redirectToAdd');
        // wrapper.find('input').simulate('click');
        // expect(redirectSpy).toHaveBeenCalled();
    });

    describe('deleteAddress', () => {
        it('Should be called when delete link clicked', () => {
            const wrapper = Setup(props);
            let toastrSpy = expect.spyOn(AddressesPage.prototype, 'deleteAddress');
           wrapper.find('#deleteAddressBtn').simulate('click');
           expect(toastrSpy).toHaveBeenCalled();
        });
    });
});