 const User=require("../schemot/user");
const product = require("../schemot/product");
const catagory = require("../schemot/catagory");
//const user = require("../schemot/user");


//work
const createUser = async (req, res) => {
    try {
        
        const newUser =await new User(req.body);
    
        
      
        const u = await newUser.save();
        res.status(200).json({ mess: "user save success", User: u })
    }
    catch (err) {
        console.log(err);
        res.status(400).json(err)
    }
}
//work
const updateUser = async (req, res) => {
    const { id } = req.params;
    try {
        const user = await User.findByIdAndUpdate(id, req.body, { new: true })
        res.status(200).json({ message: "success update", User: user })
    }
    catch (err) {
        res.status(400).json(err)
    }
}


//not
// const getProductsByCategory = async (req, res)=>{

//     catagory.findOne({name:req.params.name}).then(cat=>{
//         if(cat){
//             console.log(cat.product)
//             res.status(200)
//         }
//         res.status(404).send("catagory not found")
//     }).catch(err=>{
//         console.log(err)
//         res.status(400).json(err)
//     })
    

// }
//work
const addProduct=async(req,res)=>{
    
    try{
    const pro=await product.findById(req.params.idP);
    console.log(pro+"ccccccccccccccccccccc")
    if(pro)
    {
        const user=await User.findById(req.params.idU)
        console.log(user)
            if(user)

            {user.productId.push(pro.id)
                user.save();
                res.status(200).send(" product saved")
                Console.log(pro)
                
            }
            else{
                res.status(404).send("user not found")

            }
        
    }
    else
    res.status(404).send("product not found")
    }
    catch(err){
        res.status(400).json(err)
    }
    // product.findById({id:req.params.idP}).then(pro=>{
    //     if(pro){
    //         User.findById({id:req.params.idU}).then(user=>{
    //             if(user)
    //             {user.product.push(pro)
    //                 user.save();
    //                 res.status(200).send(" product saved")
    //                 Console.log(pro)
    //             }
    //             res.status(404).send("user not found")
    //         }
    //         )

    //     }res.status(404).send("product not found")
    // }).catch(err)
    // res.status(400).json(err)
}

// const addProduct1=async(req,res)=>{
//     product.findById({id:req.params.idP}).then(pro=>{
//         if(pro){
//             User.findById({id:req.params.idU}).then(user=>{
//                 if(user)
//                 {const newProduct=new product(user.product)
//                     newProduct.push(pro)
//                     user.product=newProduct
//                     res.status(200).send(" product saved")
//                     Console.log(pro)
//                 }
//                 res.status(404).send("user not found")
//             }
//             )

//         }res.status(404).send("product not found")
//     }).catch(err)
//     res.status(400).json(err)
// }



const deleteProductFromShopping=async(req,res)=>{
    
   let user=await User.findById(req.params.idU)
        if(user){
            if(user.productId){
               
            user.productId.findByIdAndDelete(req.params.idP, (err,p) => {
                if (err) {
                    res.json(err);}
                else{
                 res.status(200).json({message: "delete", product: p})
                }
           
            
    
        })
        
    }else{res.status(400).send("product not found")}
    }
    else{
        res.status(400).send("user not found")
    }
}

  
    //work
const getShoppungCart = async (req, res)=>{
   try{ 
   let user1=await User.findById(req.params.id)

   if(user1){
    console.log(user1)
    
   // console.log(product1)
       res.status(200).json({message: "shoppung cart",product: user1.productId})
   }
   res.status(404).send("user not found")
    
   }
   catch(err){
       res.status(400).sent(err)
   }
}

module.exports={getShoppungCart,deleteProductFromShopping,addProduct,getProductsByCategory,updateUser,createUser};