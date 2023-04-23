import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { Backdrop, Box, Chip, CircularProgress, FormControl, InputLabel, MenuItem, OutlinedInput, Select, SelectChangeEvent } from '@mui/material';
import { useEffect, useState } from 'react';
import { Category } from '../../types/category';
import { MyColor } from '../../types/color';
import { Tag } from '../../types/tag';
import { storeType } from '../../Redux/reducers/rootReducer';
import axios from 'axios';
import { Item } from '../../types/item';
import { TagItem } from '../../types/tagItem';
import { Link, useNavigate } from "react-router-dom";

export default function FormDialog() {
    const [open, setOpen] = React.useState(true);
    const [category, setCategory] = useState<Category[]>([]);
    const [tags, setTag] = useState<Tag[]>([]);
    const [colors, setColor] = useState<MyColor[]>([]);
    const [openLoading, setOpenLoading] = React.useState(false);
    const [tagSelected, setSelectedTag] = useState<string[]>([]);//tag selected
    const [fileSelected, setFileSelected] = useState<any>();
    const [imgId, setImgId] = useState<Number>();
    const [catg, setcatg] = useState(-1);
    const [colorSelect, setColorSelect] = useState(-1);
    const _navigate = useNavigate();
    const [userName, setUserName] = useState(() => {
        // getting stored value
        const saved = localStorage.getItem("userName");
        if (saved)
            return JSON.parse(saved);
        return "";
    });
    const [userId, setUserId] = useState(() => {
        // getting stored value
        const saved = localStorage.getItem("userId");
        if (saved)
            return JSON.parse(saved);
        return "";
    });


    useEffect(() => {
       
        axios.get(`https://localhost:44323/api/Categories/GetByUserId/${userId}`)
            .then(res => {
                const categoreis = res.data;
                setCategory(categoreis)
            })
        axios.get(`https://localhost:44323/api/Tags/GetAllByUserId/${userId}`)
            .then(res => {
                const tag = res.data;
                setTag(tag)
            })

        axios.get(`https://localhost:44323/api/Colors/GetAll`)
            .then(res => {
                const color = res.data;
                setColor(color)
            })
    })

    // useEffect(() => {

    //     if (tagOption.length == 0) {
    //         tags.map((item) => (
    //             add(item)
    //         ))
    //         setLoadTag(false)
    //     }
    // }, [tags]);



    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        setcatg(-1);
        setColorSelect(-1);
        setSelectedTag([]);
        


    };

    // const add = (item: Tag) => {

    //     const op: Option = {
    //         value: item.tagName,
    //         label: item.tagName,
    //         id: item.tagId,
    //         color: ''
    //     }
    //     tagOption.unshift(op);
    //     setTagOption([...tagOption])
    // }
    const handleChange = (event: SelectChangeEvent<typeof tagSelected>) => {
        debugger;
        const {
            target: { value },
        } = event;
        setSelectedTag(
            typeof value === 'string' ? value.split(',') : value,
        );

    };

    const myCheckValidate = (values: any) => {
        const errors: any = {};
        if (values.itemName == '' || !values.itemName)
            errors.itemName = "Required"
        if (!catg || catg == -1)
            errors.category = "select category"


        return errors;
    }
    const [newItemId, setNewItemId] = useState();


    const importFile = async () => {
        const formData = new FormData();
        formData.append("file", fileSelected);
        try {
            debugger
            var res = await axios.post("https://localhost:44323/api/Image/AddImage", formData);
            setImgId(res.data)
        } catch (ex) {
            console.log(ex);
        }
    };

    const addItem = async () => {
        const itemToAdd: Item = {
            categoryId: catg,
            userId: userId,
            colorId: colorSelect,
            imgId: imgId,
            entryDate: new Date()
        }
        debugger;
        await axios.post("https://localhost:44323/api/Items/AddItem", itemToAdd).then(

            function (response) {
                debugger
                const itemId = response.data;
                setNewItemId(itemId);
                console.log(response);
            }
        ).catch(
            function (error) {
                debugger
                console.log(error)
            }
        )

        handleClose();
        setOpenLoading(false);
        
    }
    useEffect(() => {
        debugger;
        if (imgId) {
            addItem();
        }

    }, [imgId]);

    const saveItem = () => {
        // add item פונקציה שתופעל בלחיצה על //
        setOpenLoading(!openLoading);
        importFile();
        //save image 


    };

    useEffect(() => {
        debugger;
        if (newItemId) {
            // tagSelected.map((item: any) => (
            //     // saveTagItem(Number(item))
            // ))
            alert("add item:" + newItemId) // This will always use latest value of count
        }

    }, [newItemId]);

    const saveTagItem = (tagToAdd: number) => {

        const tagItemToAdd: TagItem = {
            tagId: tagToAdd,
            itemId: newItemId
        }
        debugger;
        axios.post("https://localhost:44323/api/TagItem/AddTagItem", tagItemToAdd).then(
            function (response) {

                console.log(response);

            }
        ).catch(
            function (error) {
                console.log(error)
            }

        );
    }

    const saveFileSelected = (e: any) => {
        setFileSelected(e.target.files[0]);
    };


    return (
        <div>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Add Item</DialogTitle>
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
                                onChange={(e: { target: { value: any } }) => { setcatg(Number(e.target.value)) }}>

                                {category.map((item) => (
                                    <MenuItem value={item.categoryId}>{item.categoryName}</MenuItem >
                                ))
                                }
                            </Select>
                        </FormControl>
                    </Box>
                </DialogContent>
                <DialogContent>
                    <div>
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
                    </div>
                </DialogContent>
                <DialogContent>
                    <Box sx={{ minWidth: 120 }}>
                        <FormControl fullWidth>
                            {/* <pre>{JSON.stringify(catg)}</pre> */}
                            <InputLabel id="demo-simple-select-label">Select Color</InputLabel>
                            <Select
                                label="Select Color"
                                labelId="demo-simple-select-label"
                                className="basic-single"
                                id="color"
                                value={colorSelect}
                                onChange={(e) => { setColorSelect(Number(e.target.value)) }}>

                                {colors.map((item) => (
                                    <MenuItem style={{ color: item.colorCode }} value={item.colorId}>{item.colorName}</MenuItem>
                                ))
                                }

                            </Select>
                        </FormControl>
                    </Box>
                </DialogContent>
                <DialogContent>
                    <input accept="image/*" type="file" onChange={saveFileSelected} />
                    {/* hidden accept="image/*" */}
                    {/* <input type="button" value="upload" onClick={importFile} /> */}

                </DialogContent>

                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={saveItem}>Add Item</Button>
                </DialogActions>

                <Backdrop
                    sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                    open={openLoading}
                >
                    <CircularProgress color="inherit" />
                </Backdrop>
            </Dialog>


        </div>
    );
}
