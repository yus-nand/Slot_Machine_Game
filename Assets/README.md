# Unity Slot Machine

## Game Overview

This project is a simple 3-reel slot machine game developed in Unity. The player starts with $1000 and can place one of four available bets:

- $100
- $250
- $500
- $1000

After placing a bet, the player can pull the lever to spin the reels. Each reel spins independently and gradually slows down before stopping on a randomly selected symbol.

## Win Conditions

A player wins when all three reels display the same symbol:

- 7 - 7 - 7
- Bar - Bar - Bar
- Bell - Bell - Bell
- Cherry - Cherry - Cherry

## Payouts

| Combination | Prize |
|-------------|-------:|
| Cherry - Cherry - Cherry | Bet + (Bet × 2) |
| Bell - Bell - Bell       | Bet + (Bet × 3.5) |
| Bar - Bar - Bar         | Bet + (Bet × 5) |
| 7 - 7 - 7               | Bet + (Bet × 10) |

Example:

If the player bets $100 and lands 7 - 7 - 7, the prize is:

$100 + ($100 × 10) = $1100

If the reels do not match, the player loses the amount they bet.

---

## Running the WebGL Build

### Option 1: Play the WebGL Build

1. Navigate to the `Build/WebGL` folder.
2. Serve the contents with a local web server (browsers block some WebGL features when opened directly from disk).
3. Open the game in your browser.

Example (Python 3):

```bash
python -m http.server 8000
```

Then open `http://localhost:8000` in your browser.

### Option 2: Open the Project in Unity

1. Clone the repository.
2. Open the project with the Unity Editor.
3. Open the main game scene.
4. Press Play in the Unity Editor.

## How to Play

1. Select a bet amount:
	- $100
	- $250
	- $500
	- $1000
2. Pull the lever.
3. Wait until all reels finish spinning.
4. The result will be displayed automatically.
5. Continue playing until you either increase your balance or run out of funds.

---

## Bonus Features

### Sound & Audio

The game includes:

- Lever pull sound effect
- Reel spinning sounds
- Reel stop sound effects
- Win sound effect
- Background music

These audio cues improve player feedback and overall game feel.

### Retry System

If the player runs out of money, a Retry button appears in the bottom-left corner of the screen. Pressing it restarts the game and resets the session.

---

## Architecture & Implementation Notes

The project is organized into separate gameplay systems:

- `ReelController` — Handles reel movement, stopping logic, and symbol selection.
- `SlotMachineController` — Manages game flow, spin execution, win detection, and payouts.
- `ScoreManager` — Tracks and updates the player’s balance.
- `GameManager` — Handles betting, messages, and overall game state.
- `AudioManager` — Handles background music and sound effects.

### Reel Animation

Reels use a continuous scrolling animation where symbols move while spinning. Each reel starts and stops independently for a natural feel. Acceleration and deceleration were added for visual polish.

### Randomization

Each reel independently determines its final symbol using Unity's random number generator. The final outcome is decided before the reel settles.

### Development Goals

Focus areas:

- Clean code structure
- Readable and maintainable scripts
- Responsive UI
- Smooth reel animations
- Clear win/loss feedback

The project showcases Unity programming fundamentals, object-oriented design, UI integration, animation handling, and game logic.

---

If you'd like, I can also convert this to `README.md` in the repository root and add a short contributing section.
