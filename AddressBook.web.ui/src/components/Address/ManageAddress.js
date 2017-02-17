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
        if(this.props.addressBookEntry.Id !== nextProps.addressBookEntry.Id)
            this.setState({addressBookEntry: Object.assign({}, nextProps.addressBookEntry)});
    }

    isFormValid(){
        let isValid = true;
        let entry = this.state.addressBookEntry;
        let errors = {};
        
        // We are requiring first name, last name and at least one contact method
        if(!entry.FirstName || entry.FirstName.length < 2 || entry.FirstName.length > 35){
            errors.FirstName = 'First name must between 2 and 35 characters.';
            isValid = false;    
        }
            
        if(!entry.LastName || entry.LastName.length < 2 || entry.LastName.length > 35){
            errors.LastName = 'Last name must between 2 and 35 characters.';
            isValid = false;    
        }

        if(entry.Street1.length > 40){
            errors.Street1 = 'Street address must be less than 40 characters.';
            isValid = false;    
        }

        if(entry.Street2.length > 40){
            errors.Street2 = 'Street 2 address must be less than 40 characters.';
            isValid = false;    
        }

        if(entry.City.length > 35){
            errors.City = 'City must be less than 35 characters.';
            isValid = false;    
        }

        if(entry.State.length > 25){
            errors.State = 'State must be less than 25 characters.';
            isValid = false;    
        }

        const emailRegex = '[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$';
        if(entry.Email && entry.Email.match(emailRegex) === null)
        {
            errors.Email = 'Incorrect email format.  ex: abc@123.com';
            isValid = false;
        }

        if(!entry.HomePhone && !entry.MobilePhone && !entry.Email)
        {
            errors.HomePhone = 'One contact method must be supplied (phone/email).';
            errors.MobilePhone = 'One contact method must be supplied (phone/email).';
            errors.Email = 'One contact method must be supplied (phone/email).';
            isValid = false;    
        }
        
        const phoneRegex = '^[0-9]{3}-[0-9]{3}-[0-9]{4}$';
        const phoneFormatError = 'Incorrect phone format. ex: 801-123-4567';
        if(entry.HomePhone && entry.HomePhone.match(phoneRegex) === null)
        {
            errors.HomePhone = phoneFormatError;
            isValid = false;
        }
        
        if(entry.MobilePhone && entry.MobilePhone.match(phoneRegex) === null)
        {
            errors.MobilePhone = phoneFormatError;
            isValid = false;
        }
                              
        const zipCodeRegex = '^[0-9]{5}(?:-[0-9]{4})?$';
        if(entry.ZipCode && entry.ZipCode.match(zipCodeRegex) === null)
        {
            errors.ZipCode = 'Incorrect Zip Code format. ex: 84101 or 84101-8404';
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
    let addressBookEntry = {Id: "", FirstName: "", LastName: "", Street1: "", Street2: "", City: "", State: "", ZipCode: "", HomePhone: "", MobilePhone: "", Email: ""};

    const addressBookEntryId = ownProps.params.id;
    if(addressBookEntryId)
    {
        let foundEntry = state.addressBookEntries.find(a => a.Id === addressBookEntryId);
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