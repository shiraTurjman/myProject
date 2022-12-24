const mongoose = require("mongoose");

const productSchema = mongoose.Schema({
    name:{type:String},
    amount:{type:Number,maxLength:20},
    catagoryId:{type:mongoose.Schema.Types.ObjectId,ref:"catagorySchema"},
    
});
module.exports = mongoose.model('productSchema',productSchema)