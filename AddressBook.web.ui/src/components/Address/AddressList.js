import React, {PropTypes} from 'react';
import AddressListRow from './AddressListRow';

const AddressList = ({addressBookEntries}) => {
    return(
        <table className="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Address</th>
                    <th>City</th>
                    <th>State</th>
                    <th>Home Phone</th>
                    <th>Cell Phone</th>
                    <th>Email Address</th>
                </tr>
                </thead>
                <tbody>
                    {addressBookEntries.map(addressBookEntry => 
                        <AddressListRow key={addressBookEntry.Id} addressBookEntry={addressBookEntry} />
                    )}
                </tbody>
        </table>
    );
};

AddressList.propTypes = {
    addressBookEntries: PropTypes.array.isRequired
};

export default AddressList;