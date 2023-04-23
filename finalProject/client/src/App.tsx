import React from 'react';
import logo from './logo.svg';
import Login from './Components/Login/Login'
import SignUp from './Components/SignUp/SignUp'
import './App.css';
import { Route, Routes } from 'react-router-dom';
import Header from './Components/Header/Header';
import Home from './Components/home/Home';
import Categry from './Components/Category/Category';
import NotFound from './Components/NotFound/NotFound';
import Outfit from './Components/Outfit/Outfit';
import Items from './Components/Items/Items';
import AddCategory2 from './Components/AddCategory/AddCategory';
import ShoppingList from './Components/ShoppingList/ShoppingList';
import Calendar from './Components/Calendar/Calendar';
import Messages from './Components/Messages/Messages';

function App() {
  return (
    <div className="App">
      <Routes>

      <Route path="/" element={<Login></Login>}> </Route>
      <Route path="SignUp" element={<SignUp></SignUp>}></Route>
      <Route path="header" element={<Header></Header>}>
      
        <Route path="home" element={<Home></Home>}></Route>
        
        <Route path="category" element={<Categry></Categry>}>
       
        <Route path="addCategory" element={<AddCategory2></AddCategory2>}></Route>
        </Route>
        <Route path="category/items/:id" element={<Items></Items>}></Route>
        <Route path="items/:id" element={<Items></Items>}></Route>
        
        <Route path="outfit" element={<Outfit></Outfit>}></Route>
        <Route path="ShoppingList" element={<ShoppingList></ShoppingList>}></Route>
        <Route path="Calendar" element={<Calendar></Calendar>}></Route>
        <Route path="Messages" element={<Messages></Messages>}></Route>
      </Route>
      <Route path="*" element={<NotFound></NotFound>} ></Route>
      </Routes>
      
    </div>
  );
}

export default App;
