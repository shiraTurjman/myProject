import { useFormik } from "formik"
import React, { useState } from "react"
import { Link, useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux"
import axios from "axios";
import {User} from "../../types/user"
import { LoginDto } from "../../types/loginDto";
import background from './logo.jpg'

import './Login.css'
import LocalTry from "../try/localTry";

// import { useDispatch } from "react-redux"
// import { useNavigate } from "react-router-dom";

export default function Login()
{
    const _dispatch=useDispatch();
    const _navigate=useNavigate();
    
    let [passError, setpassError] = useState(false)
    const myCheckValidate=(values:any)=>{
        const errors:any={};
        if(values.email=='' || !values.email)
        errors.email="Required"
        else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
        errors.email="Invalid"

        }
        if(values.password=='' || !values.password)

         errors.password="Required"
        return errors;
        }

    const mySubmit= (values:any)=>{
        // submit פונקציה שתופעל בלחיצה על //
        // debugger;

        const loginUser : LoginDto = {
            email: myFormik.values.email,
            password: myFormik.values.password,
          }

          axios.post("https://localhost:44323/api/User/Login", loginUser ).then(
            function (response) {
                console.log(response.data);
               const myuser=response.data;
                
               localStorage.setItem("userName", JSON.stringify( myuser.userName));
               localStorage.setItem("userId",JSON.stringify(myuser.userId));
               
                    _dispatch({
                        data: myuser,
                        type: "SET_USER",
                      });
            
                      alert("hello :" + myuser.userName)
                    _navigate("header")
    
                    setpassError(true);
            }
        ).catch(
            function (error) {
                console.log(error)
                setpassError(true);
            }
        );
                       

    //     _navigate("header")
    //     axios.post("https://localhost:44323/api/User/Login",  loginUser).then(
    //         function (response) {
    //             console.log(response);
    //         }
    //     ).catch(
    //         function (error) {
    //             console.log(error)
    //         }

    //     );
         
       }

    const myFormik=useFormik(
    {
    initialValues:{email:'',password:''},
    validate: myCheckValidate,
    onSubmit: mySubmit,
    }
    )
       
    return (
        <>
        <div className="logo">
            <div className="login">
                <br></br>
           <h1>Login</h1>
           <br></br>
           <form onSubmit={myFormik.handleSubmit}>
           <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input className="form-control"
                        id="email"
                        name="email"
                        type="email"
                        onChange={myFormik.handleChange}
                        value={myFormik.values.email} />
                </div>
                {myFormik.errors.email ? <div className="alert alert-danger">{myFormik.errors.email}</div>:''}
                <br></br>
                 <div className="form-group">
                    <label htmlFor="password">Password</label>
                    <input className="form-control"
                        id="password"
                        name="password"
                        type="password"
                        onChange={myFormik.handleChange}
                        value={myFormik.values.password} />
                </div>
                {myFormik.errors.password ? <div className="alert alert-danger">{myFormik.errors.password}</div>:''}
                <br></br>
                <div className="form-group">
                    <button type="submit" className="btn btn-primary">Login</button>
                </div>
                
     
           </form>
           {passError ? <div>Incorrect password</div>: ''}
           <br></br>
           <Link to="SignUp">Don't have an account? Sign Up </Link>
           </div>
           {/* <LocalTry></LocalTry> */}
           </div>
           
        </>
    )
}