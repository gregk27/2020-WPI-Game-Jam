# Backend API
The backend API is used to interface with the leaderboard system and check which features have been unlocked.

The API has 4 endpoints
- `GET /status`
- `GET /progression`
- `POST /addScore`
- `GET /scoreData`

## Status (GET)
The status endpoint can be accessed at `/status`. It can be used to check that the server is alive.
### Output
"Alive" if the server is alive.

## Progression (GET)
The progression endpoint can be accessed at `/progression`. It returns values pertaining to game progression.
### Output
JSON representation of progression unlocks.


## Add Score (POST)
The add score endpoint can be accessed at `/addScore`. It is the method for adding a new score to the leaderboard. It compares the ID against the database and if the player is found, their score is updated. Otherwise a new entry is created. Input is provided in the POST body.
### Input
- id (string): The unique ID of the player. Method of calculation TBD.
- name (string): The name associated with the score
- score (int): The player's score
### Output
HTTP 200 on success.

## Score Data (GET)
The score data endpoint can be accessed at `/scoreData`. It returns information about the high scores.

### Output
The following JSON object is returned:
```
    {
        // The current cumulative score
        "currentScore":int,
        // The index of the next unlock
        "nextUnlock":int,
        // The cumulative score needed for the next unlock
        "nextScore":int,
        // The top 10 scores
        "top": [
            {
                // Name of the player
                "name":string,
                // Score achieved
                "score":int
            }
        ]
    }

```
