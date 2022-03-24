# UnityMultiplayer
Testing Unity multiplayer networking using Unity Netcode, Facepunch, and netcode transports for Facepunch. I am hoping to get a functional mulitplayer game running on Steam with Steamworks.

Facepunch: https://github.com/Facepunch/Facepunch.Steamworks
Facepunch Netcode: https://github.com/Unity-Technologies/multiplayer-community-contributions?path=%2FTransports%2Fcom.community.netcode.transport.facepunch
Netcode: https://unity.com/products/netcode

## Log
2022/0/24:
  I have implemented a simple pong game with two players. One player starts the game as the host and then the other joins as a client. Once 2 players have joined the server the ball starts moving in a random direction. The scoring UI works and adds a point to the correct player when the ball goes past the player. No menu has been implemented yet, but the UI dissapears when the player connects to the server.
