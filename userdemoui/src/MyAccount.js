import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

function MyAccount() {
    const [userName, setUserName] = useState('');
    const [AccountNumber, setAccountNumber] = useState('');
    const [DynamicsCredits, setDynamicsCredits] = useState('');
    const navigate = useNavigate();
    var accNo = localStorage.getItem('AccountNumber');
    var token = localStorage.getItem('Token');

    useEffect(() => {
        if (accNo == null) {
            confirmAlert({
                title: 'Not authorized',
                message: 'Not authorized.',
                buttons: [
                    {
                        label: 'Ok',
                        onClick: () => {
                            navigate("/");
                        }
                    }
                ]
            });
        }
        else {
            fetch(process.env.REACT_APP_API_URL + 'GetUserByAccountNumber?AccountNumber=' + accNo,
                {
                    method: "POST",
                    headers:
                    {
                        "Accept": "application/json",
                        "Content-type": "application/json",
                        "Authorization": "bearer " + token
                    },
                    // body: JSON.stringify(items)
                }).then((response) => {
                    if (response.status === 200) {
                        response.json().then((resp) => {
                            if (resp.accountNumber > 0 && resp.isLoggedIn == true) {
                                setAccountNumber(accNo)
                                setUserName(resp.userName)
                                setDynamicsCredits(resp.dynamicsCredits);
                            }
                            else {
                                confirmAlert({
                                    title: 'Invalid Session',
                                    message: 'Session expired.',
                                    buttons: [
                                        {
                                            label: 'Ok',
                                            onClick: () => {
                                                localStorage.removeItem('AccountNumber');
                                                navigate("/")
                                            }
                                        }
                                    ]
                                });
                            }
                        })
                    }
                    else if (response.status === 401) {
                        confirmAlert({
                            title: 'Unauthorized',
                            message: 'Session Expired, please login again.',
                            buttons: [
                                {
                                    label: 'Ok',
                                    onClick: () => {
                                        navigate("/");
                                    }
                                }
                            ]
                        });
                    }
                }).catch((err) => {
                    toast.error("Error getting data:" + err);
                });
        }
    }, []);

    function Logout() {
        confirmAlert({
            title: 'Confirm to logout',
            message: 'Are you sure to logout.',
            buttons: [
                {
                    label: 'Yes',
                    onClick: () => {
                        
                        fetch(process.env.REACT_APP_API_URL + 'LogoutUser?AccountNumber=' + accNo,
                            {
                                method: "POST",
                                headers:
                                {
                                    "Accept": "application/json",
                                    "Content-type": "application/json",
                                    "Authorization": "bearer " + token
                                },
                                // body: JSON.stringify(items)
                            }).then((response) => {
                                if (response.status === 200) {
                                    response.json().then((resp) => {
                                        if (resp > 0) {
                                            localStorage.removeItem('AccountNumber');
                                            localStorage.removeItem('Token');
                                            navigate("/")
                                        }
                                        else {
                                            toast.warn("Some issue with adding credits.");
                                        }
                                    })
                                }
                            }).catch((err) => {
                                toast.error("Error getting data:" + err);
                            });
                        
                    }
                },
                {
                    label: 'No',
                    onClick: () => {

                    }
                }
            ]
        });

    }

    function AddDynamicsCredit() {
        var accNo = localStorage.getItem('AccountNumber');
        fetch(process.env.REACT_APP_API_URL + 'AddDynamicsCredit?AccountNumber=' + accNo,
            {
                method: "POST",
                headers:
                {
                    "Accept": "application/json",
                    "Content-type": "application/json",
                    "Authorization": "bearer " + token
                },
                // body: JSON.stringify(items)
            }).then((response) => {
                if (response.status === 200) {
                    response.json().then((resp) => {
                        if (resp > 0) {
                            setDynamicsCredits(resp);
                        }
                        else {
                            toast.warn("Some issue with adding credits.");
                        }
                    })
                }
            }).catch((err) => {
                toast.error("Error getting data:" + err);
            });
    }

    return (
        <div className="bg-gradient-primary">
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-xl-10 col-lg-12 col-md-9">
                        <div className="card o-hidden border-0 shadow-lg my-5">
                            <div className="card-body p-0">
                                <ToastContainer></ToastContainer>
                                <nav className="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">
                                    <ul className="navbar-nav ml-auto">
                                        <li className="nav-item dropdown no-arrow">
                                            <a className="nav-link dropdown-toggle" href="#" id="userDropdown" role="button"
                                                data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <span className="mr-2 d-none d-lg-inline text-gray-600 small">{userName}</span>
                                                <img className="img-profile rounded-circle"
                                                    src="img/undraw_profile.svg" />
                                            </a>
                                            <div className="dropdown-menu dropdown-menu-right shadow animated--grow-in"
                                                aria-labelledby="userDropdown">
                                                <a className="dropdown-item" href="#">
                                                    <i className="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>
                                                    Profile
                                                </a>
                                                <div className="dropdown-divider"></div>
                                                <a className="dropdown-item" onClick={Logout} href="#" data-toggle="modal" data-target="#logoutModal">
                                                    <i className="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                                                    Logout
                                                </a>
                                            </div>
                                        </li>
                                    </ul>
                                </nav>
                                <div className="row">
                                    <div className="col-lg-6">
                                        <div className="p-5">
                                            <div className="text-center">
                                                <h1 className="h4 text-gray-900 mb-4">Welcome {userName}!</h1>
                                            </div>
                                            <div className="user">
                                                <div className="form-group">
                                                    <p>Account Number: {AccountNumber}</p>
                                                </div>
                                                <div className="form-group">
                                                    <p>Dynamics Credits: {DynamicsCredits}</p>
                                                    <p>
                                                        <button className="btn btn-primary btn-user btn-block" onClick={AddDynamicsCredit}>Add 1 Dynamic Credit</button>
                                                    </p>
                                                </div>
                                            </div>
                                            <hr />
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

export default MyAccount;
