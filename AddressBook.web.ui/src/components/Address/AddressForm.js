import React, {PropTypes} from 'react';
import TextField from '../common/TextField';
import {Link} from 'react-router';

const AddressForm = ({addressBookEntry, onChange, onSave, isSaving, errors}) => {
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
                error={errors.FirstName}
            />
            <TextField 
                name="LastName"
                label="Last Name"
                value={addressBookEntry.LastName}
                onChange={onChange}
                error={errors.LastName}
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
                name="ZipCode"
                label="ZipCode"
                value={addressBookEntry.ZipCode}
                onChange={onChange}
                placeHolder="84101"
                error={errors.ZipCode}
            />
            <TextField 
                name="HomePhone"
                label="Home Phone"
                value={addressBookEntry.HomePhone}
                onChange={onChange}
                placeHolder="801-123-1234"
                error={errors.HomePhone}
            />
            <TextField 
                name="MobilePhone"
                label="Cell Phone"
                value={addressBookEntry.MobilePhone}
                onChange={onChange}
                placeHolder="801-123-1234"
                error={errors.MobilePhone}
            />
            <TextField 
                name="Email"
                label="Email Address"
                value={addressBookEntry.Email}
                onChange={onChange}
                placeHolder="MyNameIs@FindMe.com"
                error={errors.Email}
            />
            <input
                type="submit"
                value={isSaving ? "Saving..." : "Save"}
                className="btn btn-primary"
                onClick={onSave}
                disabled={isSaving}                
            />
            <Link to="/addresses" className="btn btn-default myColor">Cancel</Link>
        </form>
    );
};

AddressForm.propTypes ={
    addressBookEntry: PropTypes.object.isRequired,
    onChange: PropTypes.func.isRequired,
    onSave: PropTypes.func.isRequired,
    isSaving: PropTypes.bool.isRequired,
    errors: PropTypes.object.isRequired
};

export default AddressForm;