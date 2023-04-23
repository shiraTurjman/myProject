import * as React from 'react';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';

import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { Box, FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import { Category } from '../../types/category';
import axios from 'axios';
import { useEffect, useState } from 'react';
import { ItemDisplay } from '../../types/ItemDisplay';

export default function DeleteCategory() {


  const [openCat, setOpenCat] = React.useState(false);
  
  const [error, setError] = React.useState<string>('');
  const [category, setCategory] = useState<Category[]>([]);
  const [catg, setcatg] = useState(-1);
  const [uplist,setUplist]=useState(-1);
  const [itemList, setItemList] = useState<ItemDisplay[]>([]);
  // const User = useSelector((store: storeType) => store.UserReducer);

  const [userId, setId] = useState(() => {
    // getting stored value
    const saved = localStorage.getItem("userId");
    if (saved)
      return JSON.parse(saved);
    return "";
  });

  React.useEffect(() => {
    setOpenCat(true)
  }, [])

  useEffect(() => {
    debugger;
   
    axios.get(`https://localhost:44323/api/Categories/GetByUserId/${userId}`)
        .then(res => {
            const categoreis = res.data;
            setCategory(categoreis)
        })
        
   
})

useEffect(() => {
  debugger;
  if(uplist!=-1)
  {
    alert(itemList.length)
    if(itemList.length==0&&catg!=-1)
    {
     axios.delete(`https://localhost:44323/api/Categories/DeleteCategory/${catg}`).then(
       function (response) {
         console.log(response);
       }
     ).catch(
       function (error) {
         console.log(error)
       }
     );
    

     alert("delete category:"+ catg)
     handleClose()
   }
   else
   {alert("You have items from this category delete them or change their category and then delete this category")
   handleClose()
  }
  // מעברלדף ITEM ממוין לפי קטגוריה זאת }
  
  }
}, [uplist]);


  const handleClickOpen = () => {
    setOpenCat(true);
  };

  const handleClose = () => {
    debugger
    setOpenCat(false);

  };

  const saveCategory = () => {
    debugger;
    if (catg == -1) {
      setError('select category');
      return;
    }
    else {
      debugger;
     //check if have item with this category

     axios.get(`https://localhost:44323/api/Items/GetByCategory/${catg}/${userId}`)
     .then(res => {
       const items = res.data;
       console.log(items)
       debugger
       setItemList(items);
       setUplist(1);
     }).catch(
       function (error) {
         console.log(error)
       }
     )
     
    
  }
  };

  return (

<>

<Dialog open={openCat} onClose={handleClose}>
        <DialogTitle>Delete Category</DialogTitle>
       
        <DialogContent>
                    <Box sx={{ minWidth: 120 }}>
                        <FormControl fullWidth>
                            {/* <pre>{JSON.stringify(catg)}</pre> */}
                            <InputLabel id="demo-simple-select-label">Select Category</InputLabel>
                            <Select
                                labelId="demo-simple-select-label"
                                className="basic-single"
                                id="category"
                                value={catg}
                                label="Select Category"
                                onChange={(e: { target: { value: any } }) => { setcatg(Number(e.target.value)) }}
                                
                                >
                                

                                {category.map((item) => (
                                    <MenuItem value={item.categoryId}>{item.categoryName}</MenuItem >
                                ))
                                }
                            </Select>
                        </FormControl>
                    </Box>
                </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={saveCategory}>Delete Category</Button>
        </DialogActions>
      </Dialog>

    </>
  );
}