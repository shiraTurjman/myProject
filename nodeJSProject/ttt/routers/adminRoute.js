const experss=require('express');


const router=experss.Router();
const admin= require("../controlers/adminController")

router.post("/createCategory", admin.createCategory);
router.post("/createProduct", admin.createProduct);
router.put("/updateCategory/:id", admin.updateCategory);
router.put("/updateProduct/:id", admin.updateProduct);
router.get("/getProductsByCategory/:id",admin.getProductsByCategory)
router.get("/sss",admin.sss)
router.delete("/deleteUser/:id",admin.deleteUser);

router.delete("/deleteProduct/:id",admin.deleteProduct);

router.delete("/deleteCatagory/:id",admin.deleteCatagory);

module.exports = router