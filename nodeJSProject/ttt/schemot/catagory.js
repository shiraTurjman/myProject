const mongoose = require("mongoose");

const catagorySchema = mongoose.Schema({
    name:{type:String,default:"anonymous"},
    productId:[{type:mongoose.Schema.Types.ObjectId,ref:"productSchema"}],

    
});

module.exports = mongoose.model('catagurySchema',catagorySchema)