import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';

function Login() {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    
    function GetLoginDetails(e) {
        // e.preventDefault();

        if (userName.length == 0) {
            toast.warning('Please insert Username.')
            return
        }
        if (password.length == 0) {
            toast.warning('Please insert Password.')
            return
        }

        let items = { userName, password }

        fetch(process.env.REACT_APP_API_URL + 'ValidateUser',
            {
                method: "POST",
                headers:
                {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(items)
            }).then((response) => {
                response.json().then((resp) => {
                    if (resp.accountNumber > 0) {
                        localStorage.setItem('AccountNumber', resp.accountNumber);
                        localStorage.setItem('Token', resp.token);
                        navigate("/myaccount");
                    }
                    else {
                        toast.error("Incorrect username or password.")
                    }
                })
            }).catch((err) => {
                toast.error("Login failed:" + err);
            });
    }
    return (
        <div className="bg-gradient-primary">
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-xl-10 col-lg-12 col-md-9">
                        <div className="card o-hidden border-0 shadow-lg my-5">
                            <div className="card-body p-0">
                                <div className="row">
                                    <div className="col-lg-6">
                                        <ToastContainer></ToastContainer>
                                        <div className="p-5">
                                            <div className="text-center">
                                                <h1 className="h4 text-gray-900 mb-4">Welcome!</h1>
                                            </div>
                                            <div className="user">
                                                <div className="form-group">
                                                    <input type="text" className="form-control form-control-user"
                                                        value={userName} onChange={(e) => { setUserName(e.target.value) }}
                                                        placeholder="User Name" />
                                                </div>
                                                <div className="form-group">
                                                    <input type="password" className="form-control form-control-user"
                                                        value={password} onChange={(e) => { setPassword(e.target.value) }}
                                                        placeholder="Password" />
                                                </div>
                                                <button className="btn btn-primary btn-user btn-block" onClick={GetLoginDetails}>
                                                    Login
                                                </button>
                                            </div>
                                            <hr />
                                            <div className="text-center">
                                                <a className="small" href="register">Create an Account!</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>)
}

export default Login