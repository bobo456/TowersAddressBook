import React from 'react';
import {Link} from 'react-router';

class Home extends React.Component{
    render(){
        return (
        <div className="jumbotron">
            <h1>Address Book</h1>
            <p>For all your address needs!</p>
            <Link to="/" className="btn btn-primary btn-lg">Proceed</Link>
        </div>
        );
    }
}

export default Home;