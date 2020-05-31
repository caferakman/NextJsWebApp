import {useEffect} from 'react';
import Router from 'next/router';
import {isAuth} from '../../actions/auth';

//check for the user login
const Private = ({children}) => {
    useEffect(() => {
        if(!isAuth()) {
            Router.push(`/signin`)
        }
    }, [])

    return <React.Fragment>{children}</React.Fragment>
}

export default Private;