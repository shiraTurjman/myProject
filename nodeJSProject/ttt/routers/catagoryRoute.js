const experss=require('express');

const router=experss.Router();


const admin= require("../controlers/adminController")

router.get("/getAllCatagory",admin.getAllCatagory)

module.exports = router

