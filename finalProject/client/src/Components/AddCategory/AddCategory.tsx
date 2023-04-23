import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { Box, FormControl } from '@mui/material';
import { Category } from '../../types/category';
import axios from 'axios';
import { useState } from 'react';

export default function AddCategory2() {
  const [openCat, setOpenCat] = React.useState(false);
  const [myCategoryName, setcategoryName] = React.useState<string>('');
  const [error, setError] = React.useState<string>('');
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

  const handleClickOpen = () => {
    setOpenCat(true);
  };

  const handleClose = () => {
    debugger
    setOpenCat(false);

  };

  const saveCategory = () => {
    debugger;
    if (myCategoryName == "" || !myCategoryName) {
      setError('Required');
      return;
    }
    else {
      debugger;


      const categoryToAdd: Category = {
        categoryName: myCategoryName,
        userId: userId

      }

      axios.post("https://localhost:44323/api/Categories/AddCategory", categoryToAdd).then(
        function (response) {
          console.log(response);
        }
      ).catch(
        function (error) {
          console.log(error)
        }
      );

      alert("add category:" + myCategoryName)
      handleClose()
    }
  };



  const checkValid = (value: any) => {
    debugger;
    if (value == '' || !value) { setError('Required'); }
    else { setError(''); }
  };

  return (
    <>
      <Dialog open={openCat} onClose={handleClose}>
        <DialogTitle>Add Category</DialogTitle>
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
                label="category name"
                //   defaultValue=""
                variant="filled"
                value={myCategoryName}
                onChange={(e: { target: { value: string } }) => { setcategoryName(e.target.value); checkValid(e.target.value); }}
                helperText={error}

              />

              {/* {myCategoryName=='' && <DialogContentText id="alert-dialog-slide-description" >
              Required
        </DialogContentText>} */}
              {/* </TextField> */}
            </FormControl>
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={saveCategory}>Add Category</Button>
        </DialogActions>
      </Dialog>
    </>
  );
}