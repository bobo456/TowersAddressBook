import React, {PropTypes} from 'react';
import {Link} from 'react-router';

const AddressListRow = ({addressBookEntry}) => {
    return (
        <tr>
            <td><Link to={'/address/' + addressBookEntry.id}> {addressBookEntry.FirstName + ' ' + addressBookEntry.LastName}</Link></td>
            <td>{addressBookEntry.Street1 + addressBookEntry.Street2}</td>
            <td>{addressBookEntry.City}</td>
            <td>{addressBookEntry.State}</td>
            <td>{addressBookEntry.HomePhone}</td>
            <td>{addressBookEntry.MobilePhone}</td>
            <td>{addressBookEntry.Email}</td>
        </tr>
    );
};

AddressListRow.propTypes = {
    addressBookEntry: PropTypes.object.isRequired
};

export default AddressListRow;