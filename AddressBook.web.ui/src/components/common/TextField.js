import React, {PropTypes} from 'react';

const TextField = ({name, label, value, onChange}) => {
    let wrapperClass = "form-group";

    return (
        <div className={wrapperClass}>
            <label htmlFor={name}>{label}</label>
            <div className="field">
                <input type="text" className="form-control"
                    name={name} 
                    value={value}
                    onChange={onChange}
                     />
            </div>
        </div>
    );
};

TextField.propTypes = {
    name: PropTypes.string.isRequired,
    label: PropTypes.string.isRequired,
    value: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired
};

export default TextField;