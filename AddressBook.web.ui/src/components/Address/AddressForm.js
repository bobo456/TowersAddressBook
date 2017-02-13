import React, {PropTypes} from 'react';
import TextField from '../common/TextField';
import {Link} from 'react-router';

const AddressForm = ({addressBookEntry, onChange, onSave}) => {
    for(let propertyName in addressBookEntry)
    {
        if(addressBookEntry.hasOwnProperty(propertyName))
        {
            addressBookEntry[propertyName] = addressBookEntry[propertyName] ? addressBookEntry[propertyName] : "";
        }
    }
    
    return (
        <form>
            <h1>Manage Address</h1>
            <TextField 
                name="FirstName"
                label="First Name"
                value={addressBookEntry.FirstName}
                onChange={onChange}
            />
            <TextField 
                name="LastName"
                label="Last Name"
                value={addressBookEntry.LastName}
                onChange={onChange}
            />
            <TextField 
                name="Street1"
                label="Street"
                value={addressBookEntry.Street1}
                onChange={onChange}
            />
            <TextField 
                name="Street2"
                label="Street 2"
                value={addressBookEntry.Street2}
                onChange={onChange}
            />
            <TextField 
                name="City"
                label="City"
                value={addressBookEntry.City}
                onChange={onChange}
            />
            <TextField 
                name="State"
                label="State"
                value={addressBookEntry.State}
                onChange={onChange}
            />
            <TextField 
                name="HomePhone"
                label="Home Phone"
                value={addressBookEntry.HomePhone}
                onChange={onChange}
            />
            <TextField 
                name="MobilePhone"
                label="Cell Phone"
                value={addressBookEntry.MobilePhone}
                onChange={onChange}
            />
            <TextField 
                name="Email"
                label="Email Address"
                value={addressBookEntry.Email}
                onChange={onChange}
            />
            <input
                type="submit"
                value="Save"
                className="btn btn-primary"
                onClick={onSave}
            />
            <Link to="/addresses" className="btn btn-default">Cancel</Link>
        </form>
    );
};

AddressForm.propTypes ={
    addressBookEntry: PropTypes.object.isRequired,
    onChange: PropTypes.func.isRequired,
    onSave: PropTypes.func.isRequired
};

export default AddressForm;