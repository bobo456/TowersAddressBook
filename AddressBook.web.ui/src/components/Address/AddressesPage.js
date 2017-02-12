import React, {PropTypes} from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as addressActions from '../../actions/addressActions';
import AddressList from './AddressList';

class AddressesPage extends React.Component{
    constructor(props, context){
        super(props, context);
    }

    render(){
        const addressBookEntries = this.props.addressBookEntries;

        return(
            <div>
                <AddressList addressBookEntries={addressBookEntries} />
            </div>
        );
    }
}

AddressesPage.PropTypes = {
    addressBookEntries: PropTypes.array.isRequired
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