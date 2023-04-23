import { useEffect, useState } from "react";
import { Button, Container, Nav, Navbar } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { Link, Outlet, useNavigate } from "react-router-dom";
import { storeType } from "../../Redux/reducers/rootReducer";
import SvgIcon, { SvgIconProps } from '@mui/material/SvgIcon';
import AccountCircle from '@mui/icons-material/AccountCircle';

import './Header.css'
import { createSvgIcon } from '@mui/material/utils';
import LocalTry from "../try/localTry";

import { Box, IconButton, Menu, MenuItem, SpeedDial, SpeedDialAction, SpeedDialIcon, Tooltip } from "@mui/material";
import { User } from "../../types/user";
import FileCopyIcon from '@mui/icons-material/FileCopyOutlined';
import React from "react";
import EditIcon from '@mui/icons-material/Edit';
import AddCategory2 from "../AddCategory/AddCategory";
import Add from "../Add/Add";
import AddTag from "../AddTag/addTag";
import DeleteIcon from '@mui/icons-material/Delete';
import DeleteCategory from "../DeleteCategory/deleteCategory";

const HomeIcon = createSvgIcon(
  <path d="M10 20v-6h4v6h5v-8h3L12 3 2 12h3v8z" />,
  'Home',
);



export default function Header() {
  //  const myUser = useSelector((store:storeType)=>store.UserReducer);
  const [openadd, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const [addCategory, setAddCategory] = React.useState(false);
  const [ifAddItem, setIfAddItem] = React.useState(false);
  const [addTag, setAddTag] = React.useState(false);
  const [deleteCategory,setDeleteCaregory] = React.useState(false);
  const handleCloseAdd = () => { setOpen(false) };

  const addCategoryFunc = () => {
    setAddCategory(true)
    handleCloseAdd();
  }

  const addItem = () => {
    setIfAddItem(true)
    handleCloseAdd();
  }

  const addTagF = () => {
    { openadd }
    setAddTag(true)
    handleCloseAdd();
  }
  
  const deleteCatF = () => {
    
    setDeleteCaregory(true)
   
  }


  const _dispatch = useDispatch();

  const _navigate = useNavigate();
  const [userName, setName] = useState(() => {
    // getting stored value
    const saved = localStorage.getItem("userName");
    if (saved)
      return JSON.parse(saved);
    return "";
  });

  const [userId, setId] = useState(() => {
    // getting stored value
    const saved = localStorage.getItem("userId");
    if (saved)
      return JSON.parse(saved);
    return "";
  });

  // useEffect(() => {
  //     debugger;
  //     const name = localStorage.getItem("userName");
  //     if(name)
  //     setUserName(JSON.parse(name));

  //     const id = localStorage.getItem("userId");
  //     if(id)
  //     setUserId(JSON.parse(id));

  // //     if(!myUser.userName||myUser.userName=='')
  // //     _navigate("/");
  // //     else
  // //   _navigate("home");
  // }, []);



  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const toLogout = () => {

    const name = userName;
    // debugger;
    // const user: User={
    //     userName: "",
    //     email: "",
    //     password: ""
    // };
    // _dispatch({
    //     data: user,
    //     type: "SET_USER",
    //   });
    localStorage.setItem("userName", "");
    localStorage.setItem("userId", "");

    handleClose();
    alert("logout:" + name)
    _navigate("/");
  };


  return (

    <div className="Header">
      {/* <h1 className="hello">Hello {myUser.userName}</h1> */}
      {/* <h2>Hello {userName}</h2> */}

      <div className="topnev" >
        <Navbar bg="dark" variant="dark" className="nev" >
          <Container >
            <Nav className="me-auto">

              <Nav.Link ><Link to="home">   <HomeIcon /> </Link></Nav.Link>
              <Nav.Link ><Link to="category" className="nev">   Category   </Link></Nav.Link>
              <Nav.Link ><Link to="outfit" className="nev">   Outfit   </Link></Nav.Link>
              <Nav.Link ><Link to="ShoppingList" className="nev">   Shopping List   </Link></Nav.Link>
              <Nav.Link ><Link to="Calendar" className="nev">   Calendar  </Link></Nav.Link>
            </Nav>
            <div>
              <IconButton
                id="basic-button"
                aria-controls={open ? 'basic-menu' : undefined}
                aria-haspopup="true"
                aria-expanded={open ? 'true' : undefined}
                onClick={handleClick}
              >
                <AccountCircle color="primary" />

              </IconButton>

              <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                MenuListProps={{
                  'aria-labelledby': 'basic-button',
                }}
              >
                <MenuItem onClick={handleClose}>{userName}</MenuItem>
                <MenuItem onClick={toLogout}>Logout</MenuItem>
              </Menu>
            </div>
          </Container>


        </Navbar>
        <Outlet></Outlet>
        {addCategory &&
          <AddCategory2 ></AddCategory2>
        }
        {ifAddItem && <Add></Add>}
        {addTag && <AddTag></AddTag>}
        {deleteCategory && <DeleteCategory></DeleteCategory>}

        <Box sx={{ height: 320, transform: 'translateZ(0px)', flexGrow: 1 }}>
          <SpeedDial
            ariaLabel="SpeedDial openIcon example"
            sx={{ position: 'absolute', bottom: 16, right: 16 }}
            icon={<SpeedDialIcon openIcon={<EditIcon />} />}
          >

            <SpeedDialAction
              key={"add category"}
              icon={<FileCopyIcon />}
              tooltipTitle={"addCategory"}
              tooltipOpen
              onClick={addCategoryFunc}
            />

            <SpeedDialAction
              key={"add item"}
              icon={<FileCopyIcon />}
              tooltipTitle={"addItem"}
              tooltipOpen
              onClick={addItem}
            />

            <SpeedDialAction
              key={"add tag"}
              icon={<FileCopyIcon />}
              tooltipTitle={"addTag"}
              tooltipOpen
              onClick={addTagF}
            />
          </SpeedDial>
        </Box>

        <Tooltip title="Delete">
          <IconButton onClick={deleteCatF}>
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      </div>
    </div>


  )

}