const experss=require('express');


const router=experss.Router();
const user= require("../controlers/userController")

// router.post("/createCategory", admin.createCategory);
// router.post("/createProduct", admin.createProduct);
// router.put("/updateCategory/:id", admin.updateCategory);
// router.put("/updateProduct/:id", admin.updateProduct);


// router.delete("/deleteUser/:id",order.deleteOrder);

// router.delete("/deleteProduct/:id",order.deleteProduct);

// router.delete("/deleteCatagory/:id",order.deleteCatagory);
router.put("/addProduct/:idU/:idP",user.addProduct)
router.delete("/deleteProductFromShopping/:idU/:idP",user.deleteProductFromShopping)
router.put("/updateUser/:id",user.updateUser)
router.get("/getShoppungCart/:id",user.getShoppungCart);
router.get("/getProductsByCategory/:id",admin.getProductsByCategory)
//router.get("/getProductsByCategory/:name",user.getProductsByCategory)
router.post("/createUser", user.createUser)

module.exports = router

      