# Flappy_Bird: Online Multiplayer Flappy Bird

This project is an **online real-time multiplayer version** of the iconic Flappy Bird game. Two players compete in a shared game room, and the player who hits the pipes first loses.

---

## Gameplay Overview

- **Objective**: Players navigate their birds through a series of pipes without colliding.  
- **Multiplayer**: Two players are matched in a room, competing in real-time.
- **Game Flow**:
  1. Players join a room with their usernames.
  2. Once two participants are in the room, the match begins.
  3. Each player’s actions are synchronized across the network, allowing both to observe each other’s gameplay.

---

## Networking

The game leverages the **Photon Engine** for real-time networking:
- **Room Management**:
  - Players connect to a room using their usernames.
  - The match starts automatically when the room has two participants.
- **Photon View**:
  - Each client has a Photon View, enabling real-time synchronization of their bird’s movement for the other player.

---

This project offers a fun and competitive multiplayer experience while showcasing robust real-time networking capabilities. Let me know if you have feedback or ideas for improvement!

