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
            addressBookEntry: Object.assign({}, this.props.addressBookEntry),
            errors: {},
            isSaving: false
        };

        this.updateAddressState = this.updateAddressState.bind(this);
        this.saveAddress = this.saveAddress.bind(this);
    }
    
    componentWillReceiveProps(nextProps){
        if(this.props.addressBookEntry.Id != nextProps.addressBookEntry.Id)
            this.setState({addressBookEntry: Object.assign({}, nextProps.addressBookEntry)});
    }

    isFormValid(){
        let isValid = true;
        let entry = this.state.addressBookEntry;
        let errors = {};

        // We are requiring first name, last name and at least one contact method
        if(!entry.FirstName || entry.FirstName.length < 2){
            errors.FirstName = 'First name must be at least two characters.';
            isValid = false;    
        }
            
        if(!entry.LastName || entry.LastName.length < 2){
            errors.LastName = 'Last name must be at least two characters.';
            isValid = false;    
        }

        if(!entry.HomePhone && !entry.MobilePhone && !entry.Email)
        {
            errors.HomePhone = 'One contact method must be supplied (phone/email).';
            errors.MobilePhone = 'One contact method must be supplied (phone/email).';
            errors.Email = 'One contact method must be supplied (phone/email).';
            isValid = false;    
        }

        this.setState({errors: errors});
        return isValid;
    }

    updateAddressState(event){
        const fieldName = event.target.name;
        let addressBookEntry = this.state.addressBookEntry;
        addressBookEntry[fieldName] = event.target.value;
        return this.setState({addressBookEntry: addressBookEntry});
    }

    saveSuccess(){
        this.setState({isSaving:false});
        toastr.success('Address book entry saved successfully.');
        this.context.router.push('/addresses');
    }

    saveAddress(event){
        event.preventDefault();
        
        if(!this.isFormValid())
            return;

        this.setState({isSaving:true});
        if(!this.state.addressBookEntry.Id)
        {
            this.props.actions.addAddress(this.state.addressBookEntry)
            .then(() => this.saveSuccess())
            .catch((error) => {
                this.setState({isSaving:false});
                toastr.error(error);
            });
        }
        else{
            this.props.actions.updateAddress(this.state.addressBookEntry)
            .then(() => this.saveSuccess())
            .catch((error) => {
                this.setState({isSaving:false});
                toastr.error(error);
            });
        }
    }

    render(){
        return(
            <AddressForm 
                addressBookEntry={this.state.addressBookEntry} 
                onChange={this.updateAddressState}
                onSave={this.saveAddress}  
                errors={this.state.errors}
                isSaving={this.state.isSaving}
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
    let addressBookEntry = {Id: "", FirstName: "", LastName: "", Street1: "", Street2: "", City: "", State: "",ZipCode: "", HomePhone: "", MobilePhone:"", Email:""};

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