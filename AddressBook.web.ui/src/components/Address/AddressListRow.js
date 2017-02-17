import React, {PropTypes} from 'react';
import {Link} from 'react-router';

const AddressListRow = ({addressBookEntry, deleteAddressBookEntry}) => {
    function handleClick(){
        deleteAddressBookEntry(addressBookEntry.Id);
    }

    return (
            <tr>
                <td><Link to={'/address/' + addressBookEntry.Id}> {addressBookEntry.FirstName + ' ' + addressBookEntry.LastName}</Link></td>
                <td>{addressBookEntry.Street1 + ' ' + addressBookEntry.Street2}</td>
                <td>{addressBookEntry.City}</td>
                <td>{addressBookEntry.State}</td>
                <td>{addressBookEntry.ZipCode}</td>
                <td>{addressBookEntry.HomePhone}</td>
                <td>{addressBookEntry.MobilePhone}</td>
                <td>{addressBookEntry.Email}</td>
                <td>
                    <a id="deleteAddressBtn" href="#" className="btn btn-danger btn-sm" onClick={handleClick}>
                        <span className="glyphicon glyphicon-remove"></span> Remove 
                    </a>
                </td>
            </tr>
    );
};

AddressListRow.propTypes = {
    addressBookEntry: PropTypes.object.isRequired,
    deleteAddressBookEntry: PropTypes.func.isRequired
};

export default AddressListRow;