
const experss = require('express');
const app = experss();
const user = require('./routers/userRoute')
const admin = require('./routers/adminRoute')
const catagory = require('./routers/catagoryRoute')

const mongoose = require('mongoose')
// const dotenv = require('dotenv')
// dotenv.config();
const bodyParser = require("body-parser");

app.listen(4000, () => {
    console.log("listening on port 4000");
})
const connectionParams = {
    useNewUrlParser: true,
    useCreateIndex: true,
    useUnifiedTopology: true
}


mongoose.connect(

    `mongodb+srv://shira:shira8808@cluster1.daium.mongodb.net/myFirstDatabase?retryWrites=true&w=majority`,
    {
        useNewUrlParser: true,

        useUnifiedTopology: true
    }
).then(() => { console.log("the DB connecting") })

app.use(bodyParser.json())
app.use("/user", user)
app.use("/admin", admin)
app.use("/catagory", catagory)