const mongoose = require("mongoose");

const adminSchema = mongoose.Schema({
    name:{type:String,default:"anonymous"},
    password:{type:Stringtrue,required:true,minLength:5,maxLength:10},
    email:{type:String,required:true},
    
});
module.exports = mongoose.model('AdminSchema',adminSchema)