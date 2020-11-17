# 2020-WPI-Game-Jam
2708's Entry for the 2020 WPI Game Jam.

**NOTE:** Game information in the readme reflects a rough target for the game. features followed by [0.1] are included in the 0.1 version.

# What is it?
Our entry is a hybrid platformer and tower defense game. The objective is to prevent the malware from getting past your defenses.

The malware starts on the left side of the screen, and works it's way to the right. You can run and jump to various platforms where towers can be placed to prevent them from reaching the end

## Malware
There are 5 different kinds of malware, with abilities inspried by their real-life counterparts:
### Virus
The virus is the standard malware, it simply moves across the screen with medium health. [0.1]
### Worm
Worms can replicate exponentially. While they are relatively easy to stop, they can quickly become overwhelming in large numbers. [0.1]
### Spyware
Spyware will follow you around until defeated. Thankfully it's more annoying than harmful. [0.1]
### Trojan
Trojans are full of other malware. They have higher health, and once defeated will release more malware. [0.1]
### Ransomware
Ransomware will seek out towers and disable them until it is defeated.

## Towers
There are 2 main towers
### Basic
This is your standard tower, it lauches simple projectiles at enemies at medium range. Projectiles cannot travel through walls [0.1]
### Firewall
The firewall is Plan B. It can defeat 5-6 enemies (depending on health) before failing.
### Teleporter
When placed, teleporters can be used to easily navigate around the map.

## Progression
Progression is based on the gloabal leaderboard. Each time a player sets a personal high score, their respective leaderboard entry will be updated. Each unlock occurs at a specific total score, which is calculated based on the number of players.

Unlocks include:
 - New towers
 - Tower upgrades
 - Faster player
 - More jumps
 - More health

The top 10 leaderboard scores are displayed in-game.

Information about the various backend endpoints can be found in `/Backend/readme.md` [0.1]

# Controls [0.1]
 - Left/right: A/D or ←/→
 - Jump: W or ↑ (Hold in air to slow-fall)
 - Fast-fall: S or ↓
 - Place tower: E
 
 
 # Progress logs
 While no firm progress logs were kept, various screen recordings made throughout development and are linked below
  - [12/11/2020: Basic platforming and malware](https://www.youtube.com/watch?v=Fd5wVAr-5hE&feature=youtu.be)
  - [12/11/2020: A* algorithm implemented using external package](https://www.youtube.com/watch?v=iYKF-N0swYk&feature=youtu.be)
  - [15/11/2020: Turrets (almost) working](https://www.youtube.com/watch?v=N9ZfVxSodAg&feature=youtu.be)
 
