// import logo from './logo.svg';
import './App.css';
import React from 'react';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';


import Login from './Login';
import Register from './Register';
import MyAccount from './MyAccount';

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path='/' element={<Login />}></Route>
          <Route path='/login' element={<Login />}></Route>
          <Route path='/register' element={<Register />}></Route>
          <Route path='/myaccount' element={<MyAccount />}></Route>
        </Routes>
      </Router>
    </div>
  );
}

export default App;