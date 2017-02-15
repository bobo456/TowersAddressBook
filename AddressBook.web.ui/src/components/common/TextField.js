import React, {PropTypes} from 'react';

const TextField = ({name, label, value, onChange, placeHolder, error}) => {
    let wrapperClass = "form-group";
    if (error && error.length > 0) {
        wrapperClass += " " + 'has-error';
    }

    return (
        <div className={wrapperClass}>
            <label htmlFor={name}>{label}</label>
            <div className="field">
                <input type="text" className="form-control"
                    name={name} 
                    value={value}
                    onChange={onChange}
                    placeholder={placeHolder}
                />
                {error && <div className="alert alert-danger">{error}</div>}
            </div>
        </div>
    );
};

TextField.propTypes = {
    name: PropTypes.string.isRequired,
    label: PropTypes.string.isRequired,
    value: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired,
    placeHolder: PropTypes.string,
    error: PropTypes.string
};

export default TextField;