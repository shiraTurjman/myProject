import React from 'react';
import logo from './logo.svg';
import './App.css';
import Login from './Components/Login/Login';
import { Route, Routes } from 'react-router-dom';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';

import Manager from './Components/Manager/Manager';
import User from './Components/User/User';
import NotFound from './Components/NotFound/NotFount';
import About from './Components/About/About';
import Message from './Components/MessageCom/MessageCom';


function App() {
  return (
    <div className="App">
       <header className="App-header">
         <Routes>
          <Route path="/" element={<Login></Login>}> </Route>
            <Route path={'header'} element={<Header></Header>}>
              <Route path={'home'} element={<Home></Home>}>
                <Route path={":user_id"} element={<Message></Message>}></Route>
              </Route>
              <Route path={'manager'} element={<Manager></Manager>}></Route>
              <Route path={'user'} element={<User></User>}></Route>
              <Route path={'about'} element={<About></About>}></Route>
              
              <Route path={'message'} element={<Message></Message>}></Route>

            </Route>
            <Route path="*" element={<NotFound></NotFound>} ></Route>
         </Routes>
      </header>
    </div>
  );
}

export default App;
