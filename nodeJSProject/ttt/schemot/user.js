const mongoose = require("mongoose");

const userSchema = mongoose.Schema({
    name:{type:String,default:"anonymous"},
    password:{type:String,required:true,minLength:5,maxLength:10},
    email:{type:String,required:true},
    productId:[{type:mongoose.Schema.Types.ObjectId,ref:"productSchema"}],

    
});
module.exports = mongoose.model('userSchema',userSchema)
//משתמש- USER   שמכיל name, password, email ,products (products הוא מערך של  product)
