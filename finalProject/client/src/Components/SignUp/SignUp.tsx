

import { useFormik } from "formik"
import React from "react"
import { Link, useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux"
import axios from "axios";
import { User } from "../../types/user";
// import { useDispatch } from "react-redux"
// import { useNavigate } from "react-router-dom";

export default function SignUp() {
    const _dispatch = useDispatch();
    const _navigate = useNavigate();

    const myCheckValidate = (values: any) => {
        const errors: any = {};
        if (values.email == '' || !values.email)
            errors.email = "Required"
        else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
            errors.email = "Invalid"

        }
        if (values.password == '' || !values.password)
        

            errors.password = "Required"
        else if (values.password.length < 5 || values.password.length > 20)
            errors.password = 'must by 5-20 char'
        if (values.name == '' || !values.name)

            errors.name = "Required"
        else if (values.name.length < 2)
            errors.name = 'must by 2 char'
        return errors;
    }

    const mySubmit = (values: any) => {
        // submit פונקציה שתופעל בלחיצה על //

        const user: User = {
            userName: myFormik.values.name,
            email: myFormik.values.email,
            password: myFormik.values.password,
        }

        _dispatch({
            data: user,
            type: "SET_USER",
        });

        axios.post("https://localhost:44323/api/User/AddUser", user).then(
            function (response) {
                console.log(response);
               
            }
        ).catch(
            function (error) {
                console.log(error)
            }

        );
        alert("signup email:" + myFormik.values.name)

        _navigate("/")
    }

    const myFormik = useFormik(
        {
            initialValues: { name: '', email: '', password: '' },
            validate: myCheckValidate,
            onSubmit: mySubmit,
        }
    )

    return (
        <div style={{width:"70%", margin:"auto"}}>

            <h1> Sign Up</h1>
            <form onSubmit={myFormik.handleSubmit}>


                <div className="form-group">
                    <label htmlFor="name">Name</label>
                    <input className="form-control"
                        id="name"
                        name="name"
                        type="name"
                        onChange={myFormik.handleChange}
                        value={myFormik.values.name} />
                </div>
                {myFormik.errors.name ? <div className="alert alert-danger">{myFormik.errors.name}</div> : ''}


                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input className="form-control"
                        id="email"
                        name="email"
                        type="email"
                        onChange={myFormik.handleChange}
                        value={myFormik.values.email} />
                </div>
                {myFormik.errors.email ? <div className="alert alert-danger">{myFormik.errors.email}</div> : ''}

                <div className="form-group">
                    <label htmlFor="password">Password</label>
                    <input className="form-control"
                        id="password"
                        name="password"
                        type="password"
                        onChange={myFormik.handleChange}
                        value={myFormik.values.password} />
                </div>
                {myFormik.errors.password ? <div className="alert alert-danger">{myFormik.errors.password}</div> : ''}
                <br></br>
                <div className="form-group">
                    <button type="submit" className="btn btn-primary">Sign Up</button>
                </div>

            </form>
            <br></br>
            <div>Already have an account?</div>
            <Link to="/"> Login</Link>
        </div>
    )
}