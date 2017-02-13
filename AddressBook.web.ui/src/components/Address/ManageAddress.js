import React, {PropTypes} from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as addressActions from '../../actions/addressActions';
import toastr from 'toastr';
import AddressForm from './AddressForm';


export class ManageAddress extends React.Component{
    constructor(props, context){
        super(props, context);

        this.state = {
            addressBookEntry: Object.assign({}, this.props.addressBookEntry)
        };

        this.updateAddressState = this.updateAddressState.bind(this);
        this.updateAddress = this.updateAddress.bind(this);
    }
    
    componentWillReceiveProps(nextProps){
        if(this.props.addressBookEntry.Id != nextProps.addressBookEntry.Id)
            this.setState({addressBookEntry: Object.assign({}, nextProps.addressBookEntry)});
    }

    updateAddressState(event){
        const fieldName = event.target.name;
        let addressBookEntry = this.state.addressBookEntry;
        addressBookEntry[fieldName] = event.target.value;
        return this.setState({addressBookEntry: addressBookEntry});
    }

    saveSuccess(){
        toastr.success('Address book entry saved successfully.');
        this.context.router.push('/addresses');
    }

    updateAddress(event){
        // TODO: Check form validity
        event.preventDefault();

        this.props.actions.updateAddress(this.state.addressBookEntry)
            .then(() => this.saveSuccess())
            .catch((error) => {
                toastr.error(error);
            });
    }

    render(){
        return(
            <AddressForm 
                addressBookEntry={this.state.addressBookEntry} 
                onChange={this.updateAddressState}
                onSave={this.updateAddress}  
            />
        );
    }
}

ManageAddress.propTypes = {
    addressBookEntry: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired
};

ManageAddress.contextTypes = {
    router: PropTypes.object
};

function mapStateToProps(state, ownProps){
    let addressBookEntry = {Id: "", FirstName: "", LastName: "", Street1: "", Street2: "", City: "", State: "", HomePhone: "", MobilePhone:"", Email:""};

    const addressBookEntryId = ownProps.params.id;
    if(addressBookEntryId)
    {
        let foundEntry = state.addressBookEntries.find(a => a.Id == addressBookEntryId);
        addressBookEntry = foundEntry ? foundEntry : addressBookEntry;
    }

    return {
        addressBookEntry: addressBookEntry
    };
}

function mapDispatchToProps(dispatch){
    return {
        actions: bindActionCreators(addressActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(ManageAddress);