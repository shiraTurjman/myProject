import { Backdrop, Box, SpeedDial, SpeedDialAction, SpeedDialIcon } from "@mui/material";
import Add from "../Add/Add";
import AddTag from "../AddTag/addTag";
import FileCopyIcon from '@mui/icons-material/FileCopyOutlined';
import React from "react";
import EditIcon from '@mui/icons-material/Edit';
import AddCategory2 from "../AddCategory/AddCategory";
//import "../home/Home.css"

export default function Home() {

  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [addcat,setAddcat] = React.useState(false);
  const addCategory=()=>{
    setAddcat(true)
    handleClose();
  }
  return (
    <>
      <h1>Home</h1>
    </>
  );
}