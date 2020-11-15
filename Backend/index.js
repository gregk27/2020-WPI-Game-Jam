const express = require("express");
var bodyParser = require('body-parser');
const mysql = require("mysql");
const util = require("util")
const fs = require("fs");
var app = express();
var secrets = require("./secrets");

// Unlock configuration
const unlocks = JSON.parse(fs.readFileSync("./unlocks.json"));

// Index of the next unlock
var nextUnlock=0;

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

/**
 * Get the current total score and the score needed for the next unlock
 * @type {currentScore:number, nextScore:number}
 */
async function getUnlockScore(){
    let unlock = unlocks.unlocks[nextUnlock];

    // Get total score and number of entries above cutoff score
    let data = (await query("SELECT SUM(score), COUNT(*) from scores WHERE score > ?", [unlock.baseScore * unlock.scaleFactor]))[0];
    
    // Calculate next unlock score based on entry count and scaling factor
    let nextScore = (unlock.baseScore * Math.floor(data["COUNT(*)"] * unlock.scaleFactor + 1));

    // If the next level has been unlocked, update and recurr
    if(data["SUM(score)"] > nextScore){
        nextUnlock ++;
        console.log("Recurring "+nextUnlock);
        return getUnlockScore();
    }

    return {currentScore:data["SUM(score)"], nextScore};
}

app.post("/addScore", urlencodedParser, (req, res) =>{
    // If the id is missing, stop there
    if(req.body.id == undefined){
        console.log("No id");
        res.send("id Undefined");
        return;
    }

    // Get entries with same ID
    connection.query("SELECT * FROM scores WHERE id=?", [req.body.id], async (err, result, fields)=>{
        // Not in table
        if(result.length == 0){
            // Add the row to scores
            await query("INSERT INTO scores (id, name, score) VALUES (?,?,?)", [req.body.id, req.body.name, req.body.score]);
        } else {
            // If the score has increased
            if(req.body.score > result[0].score){
                await query("UPDATE scores SET score=?, name=? WHERE id=?", [req.body.score, req.body.name, req.body.id])
            }
        }
        let data = await getUnlockScore();
        if(data.currentScore > data.nextScore){
            nextUnlock++;
        }
        res.sendStatus(200);
    })
})

app.get("/scoreData", async (req, res)=>{
    let data = await getUnlockScore();

    // Get to 10 scores
    let top = await query("SELECT name, score FROM scores ORDER BY score DESC LIMIT 10");

    res.json({currentScore:data.currentScore, nextUnlock, nextScore:data.nextScore, top})
});

console.log("Hello world!");