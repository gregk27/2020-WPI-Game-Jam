const express = require("express");
var bodyParser = require('body-parser');
const e = require("express");
var app = express();

// create application/x-www-form-urlencoded parser
var urlencodedParser = bodyParser.urlencoded({ extended: false })

var totalScore = 0;
var nextMilestone = 1000;
var leaderboard = new Map()
/** {@type {id:string, name:string, score:number}[]} */
var top = [{id:"0", name:"0", score:0}];

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

    
    // If the score has increased
    if((existing != null ? existing.score : 0)< req.body.score) {
        // Add to leaderboard
        leaderboard.set(id, {id, name:req.body.name, score:req.body.score});
        // Variable to prevent multiple inserts
        let inserted = false;
        // Update top 10 if necessary
        for(en of top){
            console.log(en);
            if(req.body.score > en.score && !inserted){
                top.splice(top.indexOf(en), 0, {id, name:req.body.name, score:req.body.score});
                inserted = true;
            }
            if(en.id == req.body.id){
                top.splice(top.indexOf(en), 1);
            }
        }
        // Keep at 10 entries
        if(top.length > 10){
            top.pop()
        }
    }

    totalScore += req.body.score*1;

    res.json({totalScore, nextMilestone, top});
})


console.log("Hello world!");