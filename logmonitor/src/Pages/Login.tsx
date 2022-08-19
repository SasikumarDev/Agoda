import React from "react";
import { Button } from "primereact/button";
import globalContext from "../Context/LogMonitorContext";
import { InputText } from 'primereact/inputtext';
import './Login.css';
import * as yup from 'yup';
import { useFormik } from "formik";
import LogMonitorServices from "../Services/LogMonitorServices";

const Login = () => {
    const ctx = React.useContext(globalContext);

    const loginValidation = yup.object({
        Username: yup.string().email('Invalid Email Format').required('Username is Required'),
        Password: yup.string().required('Password is Required')
    });

    const formik = useFormik({
        initialValues: {
            Username: '',
            Password: ''
        },
        validationSchema: loginValidation,
        onSubmit: (values) => {
            loginClick(values);
        },
    })

    const loginClick = (data: any) => {
        LogMonitorServices.login(data).then((x) => {
            console.log(x);
            let token = x.data.token.access_token;
            ctx?.setTokenValue(token);
        }).catch((err) => {
            console.log(err);
        });
    }

    return (
        <div className="loginWrapper">
            <form className="card" onSubmit={formik.handleSubmit} >
                <div className="p-inputgroup">
                    <InputText placeholder="Username" name="Username" value={formik.values.Username} onChange={formik.handleChange} />
                    <span className="p-inputgroup-addon">
                        <i className="pi pi-user"></i>
                    </span>
                </div>
                <div>
                    {
                        formik.errors.Username && formik.touched.Username ? <p style={{ color: 'red' }} >{formik.errors.Username}</p> : null
                    }
                </div>
                <div className="p-inputgroup">
                    <InputText type="password" placeholder="Password" name="Password" value={formik.values.Password} onChange={formik.handleChange} />
                    <span className="p-inputgroup-addon">
                        <i className="pi pi-key"></i>
                    </span>
                </div>
                <div>
                    <Button label="Login" type="submit" ></Button>
                </div>
            </form>
        </div>
    )
}

export default Login;