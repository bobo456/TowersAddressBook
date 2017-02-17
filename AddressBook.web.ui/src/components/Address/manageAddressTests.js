import React from 'react';
import expect from 'expect';
import {mount, shallow} from 'enzyme';
import {ManageAddress} from './ManageAddress';
import * as addressActions from '../../actions/addressActions';

describe('Manage Address', ()=>{
    it('sets error messages when trying to save empty address book entry', ()=>{
        const props = {
            actions: {},
            addressBookEntry: {Id: "", FirstName: "", LastName: "", Street1: "", Street2: "", City: "", State: "",ZipCode: "", HomePhone: "", MobilePhone:"", Email:""}
        };
        
        const wrapper = mount(<ManageAddress {...props} />);
        const saveButton = wrapper.find("input[type='submit']").last();
        expect(saveButton.prop('type')).toBe('submit');
        saveButton.simulate('click');
        expect(wrapper.state().errors.FirstName).toBe('First name must between 2 and 35 characters.');
        expect(wrapper.state().errors.LastName).toBe('Last name must between 2 and 35 characters.');
        expect(wrapper.state().errors.HomePhone).toBe('One contact method must be supplied (phone/email).');
    });
    
    it('sets error messages appropriately if required fields are populated with invalid data', ()=>{
        const props = {
            actions: {},
            addressBookEntry: {Id: "123", FirstName: "bob", LastName: "barker", Street1: "", Street2: "", City: "", State: "",ZipCode: "123", HomePhone: "456", MobilePhone:"", Email:"a@b"}
        };
        
        const wrapper = mount(<ManageAddress {...props} />);
        const saveButton = wrapper.find("input[type='submit']").last();
        expect(saveButton.prop('type')).toBe('submit');
        saveButton.simulate('click');
        expect(wrapper.state().errors.Email).toBe('Incorrect email format.  ex: abc@123.com');
        expect(wrapper.state().errors.HomePhone).toBe('Incorrect phone format. ex: 801-123-4567');
        expect(wrapper.state().errors.ZipCode).toBe('Incorrect Zip Code format. ex: 84101 or 84101-8404');
    });
});