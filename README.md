# What you must know before playing escapade

First of all, welcome to our fun mining world! Below are important information for developers and players alike.

## Important note (for developers)

To be able to test the game to its full potential and discover all of its features, consider enabling the spawning of minerals on demand. The game supports a hidden feature, whereby when the key `E` is pressed on the player's keyboard, minerals will be dropped randomly on the map. Pressing the `E` key multiple times will repeat the process, until the map is filled with minerals, which the player can then mine for a vast amount of mineral points.

The purpose of that feature is to allow the player to collect as many mineral points as possible to explore the potential of the Food Exchange Center and purchase more weapons to facilitate the progression to next levels, where some formulas (such as the calculation of food value or energy value) are recomputed.

<<<<<<< HEAD
To enable this mineral feature, simply uncomment `Lines 1097 - 1101` in the file `Escapade.cs`. The code should look like this:

``` if (SwinGame.KeyTyped(KeyCode.EKey)) { GetWorld().PutMinerals(); } ```
=======
To enable that mineral feature, simply uncomment `Lines 1097 - 1101` in the file `Escapade.cs`.

```// Accept the spawning of minerals on demand - Useful for demonstration purposes only (to make it easier to collect mineral points, buy food, weapons, etc.)
			/* if (SwinGame.KeyTyped(KeyCode.EKey))
			{
				GetWorld().PutMinerals();
			}*/```
>>>>>>> 7ed5060cbe47fb9ba5b9deddaee5c18a83a0b414

## Controls for Weapons

### For Player 1

- Press `B` on your keyboard to buy **Normal weapons**
- Press `Shift + B` at the same time to buy **Super weapons**
- Press `W,A,S,D + V` at the same time to fire weapon at the specified direction _(W = up; A = left; S = down; D = right)_.

### Player 2

- Press `P` to buy **Normal weapons**
- Press `J + P` at the same time to buy *Super weapons**
- Press any `Arrow key` + `O` at the same time to fire weapon at the specified direction

## Controls to buy food

- Press `Space` to enable (or clear) the input field in the Food Exchange Center to enter an amount of food (in kilograms) to purchase
- Press `Tab` to effect the transaction
- Click anywhere on the screen to disable the input field

## Controls for player movement

- Player One movement (A, S, D, W = left, down, right, up respectively)
- Player Two movement (Leftkey, Downkey ,Rightkey, Upkey = left, down, right, up respectively)

## Controls for building/mining rocks

- Player's Direction + Mining to mine rocks (Player One = "F", Player Two = "K")
- Player's Direction + Building to build rocks (Player One = "G", Player Two = "L")