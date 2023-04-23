import { AppBar, BottomNavigation, BottomNavigationAction, Box, Button, FormControl, Grid, InputLabel, Paper, Select, Toolbar } from "@mui/material";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { storeType } from "../../Redux/reducers/rootReducer";
import { Category } from "../../types/category";
import AddCategory2 from "../AddCategory/AddCategory";
import MyDialog from "../dialogTry/dialog";
import { createTheme, styled } from '@mui/material/styles';
import DeleteCategory from "../DeleteCategory/deleteCategory";
import DeleteIcon from '@mui/icons-material/Delete';


export default function Categry() {
  const [category, setCategory] = useState<Category[]>([]);
  // const User = useSelector((store:storeType)=>store.UserReducer);
  const [userId, setId] = useState(() => {
    // getting stored value
    const saved = localStorage.getItem("userId");
    if (saved)
      return JSON.parse(saved);
    return "";
  });

  const _navigate = useNavigate();

  useEffect(() => {
    axios.get(`https://localhost:44323/api/Categories/GetByUserId/${userId}`)
      .then(res => {
        const categories = res.data;
        setCategory(categories)
      })
  }, [])


  // const click=()=>{
  //   _navigate("items/"+1)
  // }
  const Offset = styled('div')(({ theme }) => theme.mixins.toolbar);
  return (
    <>
      <h1>Category</h1>


      <Grid item xs={6}>
        <Box
          sx={{
            p: 2,
            bgcolor: 'background.default',
            display: 'grid',
            gridTemplateColumns: { md: '1fr 1fr 1fr 1fr' },
            gap: 2,
            width: '50%',
            margin: 'auto',
          }}
        >
          <Button variant="outlined" onClick={() => { _navigate("items/-1") }}>
            All Items
            {/* If I go through this button i want to get a list of all items by userId */}
          </Button>
          {category.map((item) => {
            return <div>
              <Button variant="outlined" onClick={() => { _navigate("items/" + item.categoryId) }}>
                {/* href={"items/"+item.categoryId}  */}
                {item.categoryName}
              </Button>
            </div>
          })}
        </Box>
      </Grid>
    </>
  );

}


