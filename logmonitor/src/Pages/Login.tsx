import React from "react";
import { Card } from 'primereact/card';
import { Button } from "primereact/button";
import globalContext from "../Context/LogMonitorContext";



const Login = () => {
    const ctx = React.useContext(globalContext);

    const loginClick = () => {
        let token = 'abcdefshjshdjsdhjsdjsjdhjshdjsjdsasa';
        ctx?.setTokenValue(token);
    }
    return (
        <Card title="Login">
            <Button label="Login" onClick={loginClick} ></Button>
            <p>{ctx?.token}</p>
            <p>{ctx?.isLoggedIn?'True':'False'}</p>
        </Card>
    )
}

export default Login;