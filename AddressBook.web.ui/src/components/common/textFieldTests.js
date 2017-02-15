import React from 'react';
import expect from 'expect';
import {mount, shallow} from 'enzyme';
import TextField from './TextField';

function Setup(props){
    return shallow(<TextField {...props} />);
}

describe('TextField', () => {
     const props = {
        name: 'FirstName',
        label: 'First Name',
        value: 'Eric Snow',
        onChange: () => { return "test me"},
        placeHolder: 'First name goes here'
    };
    
    const wrapper = Setup(props);
    
    it('Should have a label', () => {
        expect(wrapper.find('label').length).toBe(1);
        expect(wrapper.find('label').props().children).toBe(props.label);
        expect(wrapper.find('label').props().htmlFor).toBe(props.name);
    });
    
    it('Should have a text input', () => {
        expect(wrapper.find('input').length).toBe(1);
        expect(wrapper.find('input').props().type).toBe("text");
        expect(wrapper.find('input').props().name).toBe(props.name);
        expect(wrapper.find('input').props().onChange()).toBe("test me");
        expect(wrapper.find('input').props().placeholder).toBe(props.placeHolder);
        expect(wrapper.find('input').props().value).toBe(props.value);
    });
});