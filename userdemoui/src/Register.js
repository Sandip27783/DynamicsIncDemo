import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

function Register() {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    function CreateAccount() {
        if (userName.length === 0) {
            toast.warning('Please insert Username.')
            return
        }
        if (password.length === 0) {
            toast.warning('Please insert Password.')
            return
        }
        else if (password.length < 8) {
            toast.warning('Password length must be 8 to 16 characters.');
            return;
        }
        let items = { userName, password }

        fetch(process.env.REACT_APP_API_URL + 'RegisterUser',
            {
                method: "POST",
                headers:
                {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(items)
            }).then((response) => {
                if (response.status === 200) {
                    response.json().then((resp) => {
                        if (resp > 0) {
                            confirmAlert({
                                title: 'User created.',
                                message: 'User created successfully.Please login.',
                                buttons: [
                                    {
                                        label: 'Ok',
                                        onClick: () => {
                                            toast.error("")
                                            navigate("/");
                                        }
                                    }
                                ]
                            });
                        }
                        else
                            toast.error("The username already in use.");
                    })
                }
                else {
                    response.json().then((resp) => {
                        toast.info(resp);
                    })
                }
            }).catch((err) => {
                toast.error("Registration failed:" + err);
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
                                                <h1 className="h4 text-gray-900 mb-4">Create an Account!</h1>
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
                                                <button className="btn btn-primary btn-user btn-block" onClick={CreateAccount}>
                                                    Create Account
                                                </button>
                                            </div>
                                            <hr />
                                            <div className="text-center">
                                                <a className="small" href="/">Already have an account? Login!</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Register
