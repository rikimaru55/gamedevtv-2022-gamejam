# gamedevtv-2022-gamejam

## GDD

### Original concept 
A drink after death. A simple arcade game where you are death's bartender.
You must mix drinks for the people who are waiting at death's door.

### Title 
Death’s Bartender

### Game Summary
You have recently died.
At the gates of Death you’re stopped by Death itself, who asks if you’d be interested in a job? 
You ask what it entails.
Death explains that sometimes people need a drink to reflect upon their lives before crossing the threshold,
your job will be to serve them what they request. Keep customers happy, you don’t have to cross the threshold.
If you fail too much, Death kindly fires you and you get booted to the start of the game.

### Game Outline
The player starts in the middle of a bar, they can move left and right, and stand in front of stations at the top to get ingredients. The customers are on the bottom, and they will each ask for the drink they need. The player must move to choose an appropriate container(pint, tumblr, glass), then the contents,(different colours are different emotions?), and finally decorations?(Optional). The player then needs to go serve the customer, if the order is right the customer is happy, if the order is wrong the customer will leave and the player loses a life. The player can at any time discard a drink and start over. The game is over when the player has lost all their lives.

### MVP
* A scrolling bar where the user can move using LEFT and RIGHT/W and S
* Create drinks and discard drinks(validate that a vessel is chosen before drinks are poured)
* Spawn infinite clients, each client can have different drink complexities. (Levels 1 - 5)
* Death mechanic
* Restart game
* Main Menu
* Music for the menu, the main gameplay loop, and the gameover screen.
* Sound effects for moving, picking a glass, pouring ingredient, serving, happy customer, angry customer, death.
* Clients should say something when they get served, something creepy or memorable.(Writing out full sentences will take took long, see Ghostalk section)
* When clients talk have them make “simlish” sounds.
* Each drink you serve has a randomly generated name

### Ghostalk System TM
Having taken a stab at a simple implementation for this, it will take too long for the ghosts to have full sentences.
An alternative is having Death say that the ghosts speak mostly gibberish and therefore you'll only grasp a few words,
so when they "Talk" we make most of it gibberish, except for a couple of "key words", example:
	"Ghost: BlaBla BlaBlaBla Wife BlaBlaBla Ache BlaBlaBla"
We'll make those "BlaBla" sounds with simlish.
* Each ghost will have between 1 and 3 keywords.
* We'll try and match the kind of keyword with the most predominant type of liquid in their drinks.
* Possible Keywords: Feelings, Places, Names, Months, Events, Colors, etc...
* At the end of a game, we'll let players scroll through all the generated phrases.

### TODO
## Wednesday
Animation Part 1
## Thursday
Dialogue generator and simlish
Sound
## Friday
Make each client have to order several drinks before leaving.
Start and end cinematics
## Saturday
Level Design
Testing & Polishing
## Sunday
Emergency
## Monday
Distribution

### NTHs
* Each client’s appearance is randomized.
* Each drink you served has a randomly generated name based on its ingredients(giving meaning to each colour, research Greek Humours for this)

### Further development
* Controller support?
* Look further into brush effects for paths in Inkscape.
* Move all bottles into a single cabinet, then when the player is in front of it, they can choose which type of container to use with numbers or something?
* Instead of clients telling the player what they want, clients talk about their problems and certain words in their talk are highlighted different colours, the player must then decipher from the client’s words the order and colour of drink to serve the client.This could be possible for the commercial version of this project since this requires much more involved programming, and perhaps even NPL

### References
Straub, J. (1914). Drinks. Marie Straub/Hotel Monthly Press. https://euvs-vintage-cocktail-books.cld.bz/1914-Drinks-by-Jacques-Straub/IV

https://unsplash.com/photos/osuiatBDTww
https://unsplash.com/es/fotos/BPWZ01FtySg

ArtDeco Geometric Patterns from [Used in background for itch.io]: https://pixelbuddha.net/patterns/art-deco-geometric-patterns

Better Minimal WebGL Template:
https://seansleblanc.itch.io/better-minimal-webgl-template/download/eyJpZCI6MTg3NDE2LCJleHBpcmVzIjoxNjUzMzYzMjE5fQ%3d%3d.8pDl6xP8aG0yzfNGSkJg8S96yHI%3d

BlaBlaBla sound used for testing:
https://freesound.org/people/unfa/sounds/165539/