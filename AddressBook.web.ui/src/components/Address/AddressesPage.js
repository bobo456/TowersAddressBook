import React, {PropTypes} from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as addressActions from '../../actions/addressActions';
import {browserHistory} from 'react-router';
import toastr from 'toastr';
import AddressList from './AddressList';


export class AddressesPage extends React.Component{
    constructor(props, context){
        super(props, context);

        this.deleteAddress = this.deleteAddress.bind(this);
    }

    redirectToAdd(){
        browserHistory.push('/address');
    }

    deleteAddress(addressBookEntryId){
        this.props.actions.deleteAddress(addressBookEntryId)
        .then(() => toastr.success('Address book entry deleted successfully.'))
        .catch((error) => {
            toastr.error(error);
        });
    }

    render(){
        const addressBookEntries = this.props.addressBookEntries;

        return(
            <div>
                <AddressList addressBookEntries={addressBookEntries} deleteAddressBookEntry={this.deleteAddress} />
                <input type="submit" value="Add Address" className="btn btn-primary" onClick={this.redirectToAdd} />
            </div>
        );
    }
}

AddressesPage.propTypes = {
    addressBookEntries: PropTypes.array.isRequired,
    actions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps){
    return {
        addressBookEntries: state.addressBookEntries
    };
}

function mapDispatchToProps(dispatch){
    return {
        actions: bindActionCreators(addressActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(AddressesPage);