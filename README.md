# Mystery Dungeon Engine
This project aims to be a game engine that other developers can use as a starting point for creating their own Mystery Dungeon like game within the Unity Game Engine environment. It have most of the back-end systems in place that provides an API for developers to plug-in their own features.

This doesn't supply visual assets, but merely a codebase to minimize the development time over systems shared amongst Mystery Dungeon games.

This project is still in active development, so this document will be updated as it progresses.

## Features

The main goal of this is to provide the codebase for the systems shared amongst all Mystery Dungeon games. So the following are what is planned to receive an API for future developers to connect with to create their own features:
* Creatures
* Moves
* Abilities
* Items
* Status Effects
* NPC Interaction
* Dungeon Generation Algorithms
* Cutscenes
* And possibly others that I cannot think of currently.

## Usage

At the current state of the project, it's not close to actually build a full game with. However, if you still want to poke around with the project, you can!

First, you'll need to install Unity. The most recent version should be sufficient to load the project, but I current use Unity 2021.1.12f1 at the time of writing this.

Once installed, clone the repo to somewhere on your machine. Then it's as simple as opening the `Mystery-Dungeon-Engine` folder created within Unity. Upon loading the project, it might yell about a version difference, but that didn't seem to affect the loading of the project.

Once Unity loads the project, everything should be available within Unity! There are some development scenes in `Assets/Scenes/Development` that I've been using to work on the engine itself, but they're fairly rudimentary.