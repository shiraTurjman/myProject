import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { User } from "../../types/user";
import userService from "../../services/User.service";
import Box from '@mui/material/Box';
import { DataGrid, GridColDef } from  '@mui/x-data-grid';



export default function Manager() {

    const [listUsers, setListUsers] = useState<User[]>([]);
    const _navigate=useNavigate();
  
  

    useEffect(() => {
      userService.getAllUserList().then((response) => {
        setListUsers(response)
      })
    }, [])




    const MessagesList=(userId:number)=>{
        _navigate(`/header/home/${userId}`)
    }

    
const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 90 },
  {
    field: 'name',
    headerName: 'Name',
    width: 150,
    editable: true,
    
  },
  {
    field: 'name',
    headerName: 'name',
    width: 150,
    editable: true,
  },
  {
    field: 'email',
    headerName: 'Email',
    type: 'number',
    width: 110,
    editable: true,
  }
];

const rows = listUsers;

  return (<div className="Admin">

    <Box sx={{ height: 400, width: '100%'}}>
      <DataGrid
        rows={rows}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        disableSelectionOnClick
        onCellClick={(params)=>{MessagesList(params.value)}}
        sx={{
          boxShadow: 3,
          border: 5,
          borderColor: 'primary.light',
          '& .MuiDataGrid-cell:hover': {
            color: 'primary.main',
          },
          color: 'white'
        }}
      />
    </Box>
    </div>
  );
}
