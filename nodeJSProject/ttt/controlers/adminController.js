//not work getProductsByCategory

// admin=require("../schemot/admin");
const catagory = require("../schemot/catagory");

const product = require("../schemot/product");
//work
const createCategory = async (req, res) => {
    try {
        const newCatagory = new catagory(req.body);
        console.log("new catagory" + newCatagory);
        const saveCatagory = await newCatagory.save();
        res.status(200).json({ mess: "you just added a new catagory!!!", catagory: saveCatagory });
    }
    catch (err) {
        console.log(err);
        res.status(400).json(err)
    }
}
//work
const createProduct = async (req, res) => {
    try {
        const newProduct = new product(req.body);
        console.log("new product" + newProduct);
        const u = await newProduct.save();
        console.log()
        const c= await  catagory.findById(u.catagoryId);
        console.log("c",c)
        c.productId.push(u);
        c.save();
        res.status(200).json({ mess: "product save success", user: u })
    }
    catch (err) {
        console.log(err);
        res.status(400).json(err)

    }
}
//work
const updateProduct = async (req, res) => {
    const { id } = req.params;
    try {
        const product1 = await product.findByIdAndUpdate(id, req.body, { new: true })
        res.status(200).json({ message: "success update", product: product1 })
    }
    catch (err) {
        res.status(400).json(err)
    }
}
//work
const updateCategory = async (req, res) => {
    const { id } = req.params;
    try {
        const catagory1 = await catagory.findByIdAndUpdate(id, req.body, { new: true })
        res.status(200).json({ message: "success update", catagory: catagory1 })
    }
    catch (err) {
        res.status(400).json(err)
    }
}


const deleteUser = (req, res) => {
    User.findByIdAndDelete(req.params.id, (err, u) => {
        if (err) {
            res.json(err);
        }
        console.log("success delete");
        res.status(200).json({ message: "success delete", user: u })
    })
}
 //work
const deleteCatagory = (req, res) => {
    catagory.findByIdAndDelete(req.params.id, (err, u) => {
        if (err) {
            res.json(err);
        }
        console.log("success delete");
        res.status(200).json({ message: "success delete", catagoty: u })
    })
}
 //work
const deleteProduct = (req, res) => {
    product.findByIdAndDelete(req.params.id, (err, u) => {
        if (err) {
            res.json(err);
        }
        console.log("success delete");
        res.status(200).json({ message: "success delete", product: u })
    })
}
 //not .populate(catagory.productId)
const getProductsByCategory = async (req, res)=>{

    try{
    
        console.log("gh")
        let catagory1=await catagory.findById(req.params.id)
        console.log(catagory1)
         res.status(200).json(catagory1.productId)
     }
     catch(err){
  
         res.status(400).json(err)
     }
//    try{
//        console.log(req.params.id);
//     const c= await catagory.findById(req.params.id);
//     console.log(c)
//     if(c){
//        console.log(c.productId);
//        res.status(200)
      
//     }
//     else{
//         res.status(404).send("catagory not found");
//       } 
//    }
//    catch(err){
//     res.status(400).json(err)
//    }
}
    // catagory.findById(req.params.id).then(cat=>{
    //     if(cat){
    //         console.log(cat.productId)
    //         res.status(200)
    //     }
    
    //     res.status(404).send("catagory not found")
    // }).catch(err=>{
    //     console.log(err)
    //     res.status(400).json(err)
    // })
    



//work
const getAllCatagory = async(req, res) => {
   
   try{
    
      console.log("gh")
      let catagory1=await catagory.find()
      console.log(catagory1)
       res.status(200).json(catagory1)
   }
   catch(err){

       res.status(400).json(err)
   }
};

const sss= async(req,res)=>{
    console.log("sdfghjk");
   }
module.exports={sss,getAllCatagory,getProductsByCategory,deleteProduct,deleteCatagory,deleteUser,updateCategory,updateProduct,createProduct,createCategory}