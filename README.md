## Game Design Final Project: _Droppy Bird_
### _By Sebastian Sbirna_
---

### 1. Game main information
The game name for my project is called ___Droppy Bird___, due to the game’s silly main character visual of absurdly dropping constantly with cartoon-like physics.

The logline has been chosen as: _Droppy Bird is a Flappy-Bird-style game which lets the player tap a button to avoid pipe obstacles and winning is done by avoiding all the level obstacles._

### 2. Gameplay description
### 2.1. Objective
The objective is to guide a bird through all pipe obstacles without losing all lives.

### 2.2. Players
The game is meant for one-person interaction, in which the player role is uniform and well-defined: that of the master of the main bird character. Because of that, player interaction patterns resemble a _single-player VS game approach._

Eventually, the game could be upgraded to introduce multiple players in the same game level trying to prevent each other from getting through all the common goals (_á la Mario Kart style_), making the player interactions more oriented towards multilateral competition, however such a change would also need to take into consideration how to make the level difficulty and pipe obstacle distances and spacings manageable in terms of multiple player sprites within the same screen size.

### 2.3. Core gameplay loop
The core gameplay loop takes inspiration from the game Flappy Bird, which was an endless-runner type of 2D game, however Droppy Bird adds a twist to the genre so that a goal and winning condition is actually created for the player.

Droppy Bird’s core gameplay loop involves guiding an absurd gravity-affected bird through a vertical set of obstacles pipes which leave small rooms in between them for passing through, without touching any part of them more times than the player has lives in the game. After every 5 consecutive obstacles passed, the player is granted an extra life. After finishing passing through all the game obstacles with a positive number of lives, the game is won by the player. If the player touches any part of the obstacles or the ground, physics rendering will make the sprite character spin around its own axis (and not vertically or horizontally), which will generate an additional challenge for the player in controlling. The obstacle position relative to each other and the total number of obstacles is randomly selected (within certain bounds) for each level, allowing randomness to constitute part of the ‘fun’ elements of the game.

### 2.4. Player procedures
The player procedures in the game are simple and straight-forward: the player presses a button (in our case, the mouse button) to make the main bird character jump in the air and avoid falling down on the ground. The players must repeat this action numerous times until they have avoided all obstacles, which also means the end goal of the game has been reached.

### 2.5. Conflict elements
The conflict elements in this game are of the classical style of physical objects (both the pipes and the ground) preventing the player from reaching the goal. Moreover, I may argue that avoiding the obstacles is actually the goal in the game, therefore without such conflict element, the player would not be able to achieve its goal at all, as there would be no goal to look forward to.

### 2.6. Outcome
The winning condition happens when the player has successfully passed through the middle of all the randomly-generated number of obstacles in the level while keeping its number of lives above zero.

The losing condition happens anytime the player allows the bird to be hit either by the pipes or by the ground at least as many times as the bird has lives left in the game, before the player has reached to go through all the obstacles. In the situation of a losing condition, a restart of the game is always possible (although with a different randomly-generated set of obstacles, so basically with a new level).

### 3. Dramatic Elements
### 3.1. Premise
Droppy Bird was a small, peaceful little bird, flying around the skies and through forests searching for its brother’s home, until one day she came across an intricate labyrinth made out of protruding pipes coming from all sides of a small hill. Knowing that the only way to continue safely to its brother’s home was to go through the small pathway in between the pipe labyrinth, Droppy Bird has decided to make courage and skillfully move between the obstacles, until it may be able to cross through and continue its search again. At this point, the game itself starts.

### 3.2. Character
Droppy Bird, the eponymous main character of the game, is special due to its absurd flight movements which contain unrealistic, animation-style physics that make him fun to play and look at when he tumbles around in mid-air after being hit, and is amusing to watch getting hit due to its cute, yet representative animations.

### 3.3. Challenge
The challenge of this game is kept through the randomly generated pipe obstacle ‘labyrinth’, in which both horizontal and also vertical relative positions of neighboring pipes to each other are being randomly selected, therefore the game will constantly be refreshing to play from the perspective of the level generation. The challenging aspects of this game have been originally inspired from the game Flappy Bird, however that game was often criticized as being too difficult, and thus this game takes a step away from the central element of difficulty of the mother-game and actually attempts to make it easier to play due to slightly larger spaces between obstacles, constant speed and a life system. However, we have also considered that these changes might make our players lose interest easier if this would be an endless-style game, which is why it has been decided that this game should introduce a winning-goal for the player, in order to increase their engagement and give them a sense of reward when playing our game.

### 3.4. Play
The game was built in such a way that players cannot alter significantly the outcome of the game, since the goal of the game is clear and strictly defined, and so are the win-lose mechanics and how to achieve those mechanics.

### 3.5. Story
There is no definite story, as the gameplay style is casual and reflects visibly what happens to Droppy Bird on its journey: either it is too hurt and needs to start again going through the labyrinth, or it passes well and continues on its journey.
