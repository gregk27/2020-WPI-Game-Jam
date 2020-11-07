const express = require("express");
var bodyParser = require('body-parser')
var app = express();

// create application/x-www-form-urlencoded parser
var urlencodedParser = bodyParser.urlencoded({ extended: false })


var totalScore = 0;
var nextMilestone = 1000;
var leaderboard = new Map()

var server = app.listen(2708, ()=>{
    console.log(`Running on ${server.address().address}:${server.address().port}`);
});

app.get("/status", (req, res)=>{
    res.send("Alive");
});

app.post("/addScore", urlencodedParser, (req, res) =>{
    console.log(req.body);
    let id = req.body.id;
    if(req.body.id == undefined){
        console.log("No id");
        res.send("id Undefined");
        return;
    }

    let existing;
    console.log(leaderboard.get(id));
    if((existing = leaderboard.get(id)) != null){
        totalScore -= existing.score*1;
    }

    leaderboard.set(id, {id, name:req.body.name, score:req.body.score})


    totalScore += req.body.score*1;

    res.json({totalScore, nextMilestone});
})


console.log("Hello world!");