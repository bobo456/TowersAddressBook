import React from 'react';
import expect from 'expect';
import {mount, shallow} from 'enzyme';
import Header from './Header';

describe('Header', () => {
    const wrapper = shallow(<Header />);
    
    it('Should display nav', () => {
        expect(wrapper.find('nav').length).toBe(1);
    });

    it('Should contain a indexlink for Home', () => {
        expect(wrapper.find('IndexLink').length).toBe(1);
        expect(wrapper.find('IndexLink').props().to).toBe('/');
        expect(wrapper.find('IndexLink').props().children).toBe('Home');
    });

    it('Should contain a Link to Addresses', () => {
        expect(wrapper.find('Link').length).toBe(1);
        expect(wrapper.find('Link').props().to).toBe('/addresses');
        expect(wrapper.find('Link').props().children).toBe('Addresses');
    });
});