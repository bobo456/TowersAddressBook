import React, {PropTypes} from 'react';
import AddressListRow from './AddressListRow';

const AddressList = ({addressBookEntries, deleteAddressBookEntry}) => {
    return(
        <table className="table table-striped my-table-bordered table-hover">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Address</th>
                    <th>City</th>
                    <th>State</th>
                    <th>Zip Code</th>
                    <th>Home Phone</th>
                    <th>Cell Phone</th>
                    <th>Email Address</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                    {addressBookEntries.map(addressBookEntry => 
                        <AddressListRow key={addressBookEntry.Id} addressBookEntry={addressBookEntry} deleteAddressBookEntry={deleteAddressBookEntry}/>
                    )}
                </tbody>
        </table>
    );
};

AddressList.propTypes = {
    addressBookEntries: PropTypes.array.isRequired,
    deleteAddressBookEntry: PropTypes.func.isRequired
};

export default AddressList;