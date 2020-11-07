const express = require("express");
var app = express();


var server = app.listen(2708, ()=>{
    console.log(`Running on ${server.address().address}:${server.address().port}`);

});

app.get("/status", (req, res)=>{
    res.send("Alive");
});



console.log("Hello world!");