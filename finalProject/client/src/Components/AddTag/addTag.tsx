import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, TextField } from '@mui/material'
import axios from 'axios'
import { useFormik } from 'formik'
import { Fragment, useState } from 'react'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { storeType } from '../../Redux/reducers/rootReducer'
import { Tag } from '../../types/tag'
// import { Category } from '../../types/category'
// import './DialogDemo.css';
import './addTag.css'
import * as React from 'react' 

export default function AddTag() {
  const [open, setOpen] = useState(true);
  const [myTagName, setTagName] = useState<string>('');
  const [error, setError] = useState<string>('');
  // const User = useSelector((store: storeType) => store.UserReducer);
  const [userId, setUserId] = useState(() => {
    // getting stored value
    const saved = localStorage.getItem("userId");
    if(saved)
      return JSON.parse(saved);
    return "";
  }); 

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const saveTag = () => {
    debugger;
    if (myTagName == "" || !myTagName) {
      setError('Required');
      return;
    }
    else {
      debugger;
      const tagToAdd: Tag = {
        tagName: myTagName,
        userId: userId
      }
      axios.post("https://localhost:44323/api/Tags/AddTagAsync", tagToAdd).then(
        function (response) {
          console.log(response);
        }
      ).catch(
        function (error) {
          console.log(error)
        }
      );
      handleClose()
    }

  };

  const checkValid = (values: any) => {

    if (values == '' || !values) {
      setError('Required');
      // alert(error)
    }
    else{
      setError('');
    }
  }

  // const mySubmit= (values:any)=>{
  //     // submit פונקציה שתופעל בלחיצה על //

  //     //save in BD
  //     const TagToAdd :Tag={
  //         tagName:.values.tagName,
  //         userId:User.userId

  //     }

  //     axios.post("https://localhost:44323/api/Tags/AddTagAsync", TagToAdd).then(
  //       function (response) {
  //           console.log(response);

  //       }
  //   ).catch(
  //       function (error) {
  //           console.log(error)
  //       }

  //   )

  //    }

  return (
    <>
      {/* <Button variant="outlined" onClick={handleClickOpen}>
        Add Tag
      </Button> */}
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Add Tag</DialogTitle>
        <DialogContent>
          <Box
            component="form"
            sx={{
              '& .MuiTextField-root': { m: 1, width: '25ch' },
            }}
            noValidate
            autoComplete="off"
          >
            <FormControl>
              <TextField
                required
                id="filled-required"
                label="Tag name"
                //   defaultValue=""
                variant="filled"
                value={myTagName}
                onChange={(e: { target: { value: string } }) => { setTagName(e.target.value); checkValid(e.target.value); }}
                helperText={error}
              />
            </FormControl>
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={saveTag}>Add Tag</Button>
        </DialogActions>
      </Dialog>
    </> 
  );
}
