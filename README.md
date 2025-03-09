# 3D Bowling Game

A 3D bowling game with 4 lanes, physics-based ball and pin mechanics, and proper scoring.

## Setup Instructions

1. Install Unity 2021.3 LTS or newer
2. Open Unity Hub and click "Add" to add this project folder
3. Open the project in Unity
4. Create a new scene (File > New Scene) if no scene exists, or open an existing scene from `Assets/Scenes/`
5. Set up your bowling alley by adding the required prefabs to the scene
6. Press Play to test the game

## Game Controls

- **A/D or Left/Right Arrow Keys**: Move the ball sideways
- **Mouse Movement**: Aim the ball (changes throwing angle)
- **Left Mouse Button or Spacebar**: Roll the ball
- **Tab**: Switch camera views

## Project Structure

- `Assets/Scenes`: Contains game scenes
- `Assets/Scripts`: All C# scripts for game mechanics
- `Assets/Prefabs`: Reusable game objects (pins, ball, lane)
- `Assets/Models`: 3D models for the bowling alley
- `Assets/Materials`: Materials for the 3D models
- `Assets/Audio`: Sound effects and music

## Implementation Details

The game uses Unity's physics system for realistic pin and ball interaction. Scoring follows standard bowling rules with strikes, spares, and open frames. The game tracks 10 frames per game with appropriate bonus rolls in the 10th frame.

## Adding More Features

Consider adding these features to enhance the game:

- Multiple ball designs
- Multiplayer support
- Different lane conditions affecting physics
- Realistic sound effects
- Performance statistics
