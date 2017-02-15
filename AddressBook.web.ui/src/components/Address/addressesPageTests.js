import React from 'react';
import expect from 'expect';
import {mount, shallow} from 'enzyme';
import {AddressesPage} from './AddressesPage';
import toastr from 'toastr';
import {browserHistory} from 'react-router';

function Setup(props){
    return mount(<AddressesPage {...props} />);
}

describe('AddressesPage', () => {
     const props = {
         addressBookEntries: [{"id": "guid"}],
         actions: {deleteAddress: () => { return Promise.resolve();}, loadAddresses: () => { return Promise.resolve();}}
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
        browserHistory.push = () => {};
        const wrapper = Setup(props);
        let redirectSpy = expect.spyOn(browserHistory, 'push');
        wrapper.find("input[type='submit']").simulate('click');
        expect(redirectSpy).toHaveBeenCalledWith('/address');
    });

    describe('loadAddresses', () => {
        it('Should be called when addressBookEntries list is empty', () => {
            const emptyProps = {
                addressBookEntries: [],
                actions: {loadAddresses: () => {return Promise.resolve();}}
            };
            let loadAddressesSpy = expect.spyOn(AddressesPage.prototype, 'loadAddresses');
            const wrapper = Setup(emptyProps);
            expect(loadAddressesSpy).toHaveBeenCalled();
        });
        it('Should NOT be called when addressBookEntries are present', () => {
            AddressesPage.prototype.loadAddresses = (id) => {};
            let loadAddressesSpy = expect.spyOn(AddressesPage.prototype, 'loadAddresses');
             const fullProps = {
                addressBookEntries: [{"Id": "", "FirstName": "bob"}, {"Id": "", "FirstName": "tom"}],
                actions: {loadAddresses: () => {return Promise.resolve();}}
            };
            const wrapper = Setup(props);
            expect(loadAddressesSpy).toNotHaveBeenCalled();
        });
    });

    describe('deleteAddress', () => {
        it('Should be called when delete link clicked', () => {
            AddressesPage.prototype.deleteAddress = (id) => {};
            let deleteAddressSpy = expect.spyOn(AddressesPage.prototype, 'deleteAddress');
            const wrapper = Setup(props);
           wrapper.find('#deleteAddressBtn').simulate('click');
           expect(deleteAddressSpy).toHaveBeenCalled();
        });
    });
});