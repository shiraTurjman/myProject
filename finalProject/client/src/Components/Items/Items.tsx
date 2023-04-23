import axios from "axios";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { storeType } from "../../Redux/reducers/rootReducer";
import './item.css'
import { ItemDisplay } from "../../types/ItemDisplay";
import { Box, Button, Chip, FormControl, Grid, IconButton, InputLabel, MenuItem, OutlinedInput, Paper, Select, SelectChangeEvent, ThemeProvider } from "@mui/material";
import { styled } from '@mui/material/styles';
import SettingsOutlinedIcon from '@mui/icons-material/SettingsOutlined';
import { Category } from "../../types/category";
import { MyColor } from "../../types/color";
import SortIcon from '@mui/icons-material/Sort';
import { Tag } from "../../types/tag";
import EditIcon from '@mui/icons-material/Edit';

export default function Items() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [colors, setColor] = useState<MyColor[]>([]);
  const [tags, setTag] = useState<Tag[]>([]);
  const [sortListItem,setSortListItem]=useState<ItemDisplay[]>([]);
  const [sortListItem2,setSortListItem2]=useState<ItemDisplay[]>([]);

  const [tagSelected, setTagSelected] = useState<string[]>([]);//tag selected
  const [selectedCategory, setSelectedCategory] = useState(-1);//category selected
  const [selectedColor, setColorSelect] = useState(-1);//color selected
  const params = useParams();
  const prams = useParams();
  //var imgData:string;

  const [itemList, setItemList] = useState<ItemDisplay[]>([]);
  // const User = useSelector((store:storeType)=>store.UserReducer);
  const [userId, setId] = useState(() => {
    // getting stored value
    const saved = localStorage.getItem("userId");
    if (saved)
      return JSON.parse(saved);
    return "";
  });


  useEffect(() => {
    debugger;
    console.log(prams.id);
    
    setSelectedCategory(Number(prams.id))
    // axios.get(`https://localhost:44323/api/Items/GetByCategory/${prams.id}/${userId}`)
    axios.get(`https://localhost:44323/api/Categories/GetByUserId/${userId}`)
      .then(res => {
        const categories = res.data;
        setCategories(categories)
      })
    axios.get(`https://localhost:44323/api/Colors/GetAll`)
      .then(res => {
        const color = res.data;
        setColor(color)
      })
    axios.get(`https://localhost:44323/api/Tags/GetAllByUserId/${userId}`)
      .then(res => {
        const tag = res.data;
        setTag(tag)
      })
      

    // axios.get(`https://localhost:44323/api/Items/GetByCategory/${prams.id}/${userId}`)
    //   .then(res => {
    //     const items = res.data;
    //     console.log(items)
    //     debugger
    //     setItemList(items);
    //   }).catch(
    //     function (error) {
    //       console.log(error)
    //     }
    //   )

      axios.get(`https://localhost:44323/api/Items/GetByUser/${userId}`)
      .then(res => {
        const items = res.data;
        console.log(items)
        debugger
        setItemList(items);
      }).catch(
        function (error) {
          console.log(error)
        }
      )
      

  }, [])
    // useEffect(() => {

    //   listItemSort();
    // }, [itemList]);

  const listItemSort=()=>{
    itemList.map((myitem:ItemDisplay)=>(
     
      selectedCategory==-1?(
       setSortListItem([...sortListItem,myitem])
      ):
      (selectedCategory==myitem.categoryId ?(
        setSortListItem([...sortListItem,myitem])
      ):null)


    ))
  }
  const editItem=()=>{
     alert("edit");
  }
  const resetSort=()=>{
    setColorSelect(-1);
    setSelectedCategory(-1);
  }
  //handle change for tags
  const handleChange = (event: SelectChangeEvent<typeof tagSelected>) => {
    debugger;
    const {
      target: { value },

    } = event;
    setTagSelected(
      typeof value === 'string' ? value.split(',') : value,
    );

  };
  // https://localhost:44323/api/Items/GetByCategory/${prams}/${User.userId}

  const Item = styled(Paper)(({ theme }) => ({
    ...theme.typography.body2,
    textAlign: 'center',
    color: theme.palette.text.secondary,
    lineHeight: '60px',
  }));


  return (
    <>
      <h1>Item</h1>
      <div>
        <Grid item xs={6}>
          <Box
            sx={{
              p: 2,
              bgcolor: 'background.default',
              display: 'grid',
              gridTemplateColumns: { md: '1fr 1fr 1fr 1fr 1fr' },
              gap: 2,
              width: '50%',
              margin: 'auto',

            }}
          >
            <SortIcon>
              //on click open an option to sort oldest to newest or newest to oldest
            </SortIcon>
            <Box sx={{ minWidth: 120 }}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">Category</InputLabel>
                <Select
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  // value={}
                  label="Category"
                  onChange={(e: { target: { value: any } }) => { setSelectedCategory(Number(e.target.value)) }}
                >
                  <MenuItem value={-1}>all Category</MenuItem>
                  {categories.map((category) => {
                    return (
                      <MenuItem value={category.categoryId}>{category.categoryName}</MenuItem>
                    )

                  })}

                </Select>
              </FormControl>
            </Box>
            <Box sx={{ minWidth: 120 }}>
              <FormControl fullWidth>
                {/* <pre>{JSON.stringify(catg)}</pre> */}
                <InputLabel id="demo-simple-select-label">Select Color</InputLabel>
                <Select
                  label="Select Color"
                  labelId="demo-simple-select-label"
                  className="basic-single"
                  id="color"
                // value={colorSelect}
                 onChange={(e) => { setColorSelect(Number(e.target.value)) }}
                >
                  <MenuItem value={-1}>Select Color</MenuItem>
                  {colors.map((item) => (
                    <MenuItem style={{ color: item.colorCode }} value={item.colorId}>{item.colorName}</MenuItem>
                  ))
                  }

                </Select>
              </FormControl>
            </Box>
            <FormControl fullWidth >
              <InputLabel id="demo-multiple-chip-label">Select Tags</InputLabel>
              <Select
                labelId="demo-multiple-chip-label"
                id="demo-multiple-chip"
                multiple
                value={tagSelected}
                onChange={handleChange}
                input={<OutlinedInput id="select-multiple-chip" label="Chip" />}
                renderValue={(selected) => (
                  <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                    {selected.map((value) => (
                      <Chip key={value} label={value} />
                    ))}
                  </Box>
                )}
              //MenuProps={MenuProps}
              >
                {tags.map((tag) => (
                  <MenuItem
                    key={tag.tagId}
                    value={tag.tagId}
                  //   style={getStyles(name, personName, theme)}
                  >
                    {tag.tagName}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Box>
          {/* <Box sx={{ minWidth: 120 }}> */}
              
              <Button onClick={resetSort}>reset</Button>
              
           
        </Grid>
      </div>

      {/* <img src={`data:image/jpeg;base64,${image.fileData}`} width="100" /> */}

      <Grid item xs={6}>
        <Box
          sx={{
            p: 2,
            bgcolor: 'background.default',
            display: 'grid',
            gridTemplateColumns: { md: '1fr 1fr 1fr 1fr' },
            gap: 2,
          }}
        >

          

          {itemList.map((item:ItemDisplay) => (
             selectedCategory==-1 ?(
             selectedColor==-1 ?(
             <> 
            {/* {setSortListItem([...sortListItem,item])} */}
             
             <Item key={item.itemId} elevation={item.itemId}>
            <h4>{item.itemId}</h4>
            {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
            <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
            <SettingsOutlinedIcon></SettingsOutlinedIcon>
            <IconButton onClick={editItem}><EditIcon /></IconButton>

          </Item>
             </>
             ):(
              selectedColor==item.colorId ?(
                <> 
                {/* {setSortListItem([...sortListItem,item])} */}
                 
                 <Item key={item.itemId} elevation={item.itemId}>
                <h4>{item.itemId}</h4>
                {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
                <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
                <SettingsOutlinedIcon></SettingsOutlinedIcon>
                <IconButton onClick={editItem}><EditIcon /></IconButton>
    
              </Item>
                 </>

              ):null
             )
             
             ):
             (selectedCategory==item.categoryId ? (

            // <>
             
            //  {/* {setSortListItem([...sortListItem,item])} */}
            //   {/* {console.log(item.imgId)}
            //   {getImageData(item.imgId)} */}
            //   <Item key={item.itemId} elevation={item.itemId}>
            //     <h4>{item.itemId}</h4>
            //     {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
            //     <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
            //     <SettingsOutlinedIcon></SettingsOutlinedIcon>
            //   </Item>
            //   </>
            selectedColor==-1 ?(
              <> 
             {/* {setSortListItem([...sortListItem,item])} */}
              
              <Item key={item.itemId} elevation={item.itemId}>
             <h4>{item.itemId}</h4>
             {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
             <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
             <SettingsOutlinedIcon></SettingsOutlinedIcon>
             <IconButton onClick={editItem}><EditIcon /></IconButton>
 
           </Item>
              </>
              ):(
               selectedColor==item.colorId ?(
                 <> 
                 {/* {setSortListItem([...sortListItem,item])} */}
                  
                  <Item key={item.itemId} elevation={item.itemId}>
                 <h4>{item.itemId}</h4>
                 {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
                 <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
                 <SettingsOutlinedIcon></SettingsOutlinedIcon>
                 <IconButton onClick={editItem}><EditIcon /></IconButton>
     
               </Item>
                  </>
 
               ):null
              )


             ):null)

              
                ))}
                
              
                </Box>
              </Grid>
        
     </>
          )
        }
            
          //  /* selectedCategory==-1&&selectedCategory==item.categoryId ? (
          //     selectedColor==-1 ?(
          //       tagSelected.length==0 ? 
          //       (
          //         <>
          //         {/* {console.log(item.imgId)}
          //         {getImageData(item.imgId)} */}
          //         <Item key={item.itemId} elevation={item.itemId}>
          //           <h4>{item.itemId}</h4>
          //           {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //           <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //           <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //         </Item>
          //         </>
          //       ):
          //       <>
          //       {/* {console.log(item.imgId)}
          //       {getImageData(item.imgId)} */}
          //       <Item key={item.itemId} elevation={item.itemId}>
          //         <h4>{item.itemId}</h4>
          //         {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //         <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //         <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //       </Item>
          //       </>
                

          //     ):
          //     selectedColor==item.colorId ?
          //     (
          //       tagSelected.length==0 ? 
          //       (
          //         <>
          //         {/* {console.log(item.imgId)}
          //         {getImageData(item.imgId)} */}
          //         <Item key={item.itemId} elevation={item.itemId}>
          //           <h4>{item.itemId}</h4>
          //           {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //           <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //           <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //         </Item>
          //         </>
          //       ):
          //       <>
          //       {/* {console.log(item.imgId)}
          //       {getImageData(item.imgId)} */}
          //       <Item key={item.itemId} elevation={item.itemId}>
          //         <h4>{item.itemId}</h4>
          //         {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //         <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //         <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //       </Item>
          //       </>
          //     )
          //     :null
            
          //   ):
          //   selectedCategory==item.categoryId ?
          //   (
          //     selectedColor==-1 ?(
          //       tagSelected.length==0 ? 
          //       (
          //         <>
          //         {/* {console.log(item.imgId)}
          //         {getImageData(item.imgId)} */}
          //         <Item key={item.itemId} elevation={item.itemId}>
          //           <h4>{item.itemId}</h4>
          //           {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //           <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //           <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //         </Item>
          //         </>
          //       ):
          //       <>
          //       {/* {console.log(item.imgId)}
          //       {getImageData(item.imgId)} */}
          //       <Item key={item.itemId} elevation={item.itemId}>
          //         <h4>{item.itemId}</h4>
          //         {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //         <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //         <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //       </Item>
          //       </>
                

          //     ):
          //     selectedColor==item.colorId ?
          //     (
          //       tagSelected.length==0 ? 
          //       (
          //         <>
          //         {/* {console.log(item.imgId)}
          //         {getImageData(item.imgId)} */}
          //         <Item key={item.itemId} elevation={item.itemId}>
          //           <h4>{item.itemId}</h4>
          //           {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //           <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //           <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //         </Item>
          //         </>
          //       ):
          //       <>
          //       {/* {console.log(item.imgId)}
          //       {getImageData(item.imgId)} */}
          //       <Item key={item.itemId} elevation={item.itemId}>
          //         <h4>{item.itemId}</h4>
          //         {/* <img src={`data:image/jpeg;base64,${binary_data}`} /> */}
          //         <img src={`data:image/jpeg;base64,${item.imgData}`} width="100" /><br></br>
          //         <SettingsOutlinedIcon></SettingsOutlinedIcon>
          //       </Item>
          //       </>
          //     )
          //     :null
          //   )
          //   : null
            
            
      