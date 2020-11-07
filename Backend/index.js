const express = require("express");
var bodyParser = require('body-parser');
const mysql = require("mysql");
const util = require("util")
var app = express();
var secrets = require("./secrets");


var connection = mysql.createConnection({
    host: secrets.hostname,
    user: secrets.user,
    password: secrets.password,
    database: secrets.database
})

// node native promisify
const query = util.promisify(connection.query).bind(connection);

connection.connect((err)=>{
    if(err){
        console.error("Error connecting: "+err.stack);
        return;
    }

    console.log("Database connected successfully")
});

// create application/x-www-form-urlencoded parser
var urlencodedParser = bodyParser.urlencoded({ extended: false })

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

    let totalScore, nextMilestone, top;
    
    connection.query("SELECT * FROM scores WHERE id=?", [req.body.id], async (err, result, fields)=>{
        console.log(result);
        // Not in table
        if(result.length == 0){
            await query("INSERT INTO scores (id, name, score) VALUES (?,?,?)", [req.body.id, req.body.name, req.body.score]);
        } else {
            // If the score has increased
            if(req.body.score > result[0].score){
                query("UPDATE scores SET score=?, name=? WHERE id=?", [req.body.score, req.body.name, req.body.id], (err, result, fields)=>{
                    console.log("updated")
                    console.log(err);
                    console.log(result);
                });
            }
        }
        res.sendStatus(200);
    })


    // res.json({totalScore, nextMilestone, top});
})


console.log("Hello world!");