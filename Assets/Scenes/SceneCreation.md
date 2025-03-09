MainScene.unity

1. Create the Game Manager
- Create an empty GameObject (Right-click in Hierarchy > Create Empty)
- Rename it to "GameManager"
- Add the GameManager.cs script component

2. Create the Bowling Lane
- Create a 3D Cube (Right-click in Hierarchy > 3D Object > Cube)
- Rename it to "BowlingLane"
- Scale it to (2, 0.1, 20) in the Inspector
- Position it at (0, 0, 10)
- Add a Box Collider component

3. Create the Bowling Ball
- Create a 3D Cube (Right-click in Hierarchy > 3D Object > Cube)
- Rename it to "BowlingLane"
- Scale it to (2, 0.1, 20) in the Inspector
- Position it at (0, 0, 10)
- Add a Box Collider component

4. Create the Aim Arrow
- Create a 3D Cube (Right-click in Hierarchy > 3D Object > Cube)
- Rename it to "AimArrow"
- Scale it to (0.1, 0.1, 2)
- Position it at (0, 0.5, 1)
- Make it a child of the BowlingBall
- Assign it to the "aimArrow" field in the BowlingBall component

5. Create the Pin Setup 
- Create an empty GameObject (Right-click in Hierarchy > Create Empty)
- Rename it to "PinParent"
- Position it at (0, 0, 18)

For each of the 10 pins will be a prefab: (PinSetupHelper.cs will set up the pins)
- Create a 3D Capsule (Right-click in Hierarchy > 3D Object > Capsule)
- Rename it to "Pin"
- Scale it to (0.3, 0.5, 0.3)
- Position them in the standard bowling triangle formation
- Make them children of PinParent
- Add the BowlingPin.cs script component
- Add a Rigidbody component
- Add a Capsule Collider component
- Drag this to the Prefabs folder

6. Set Up Camera
- Select the Main Camera in the Hierarchy
- Position it at (0, 3, -5)
- Rotate it to (30, 0, 0)
- Add the CameraController.cs script component
- Assign the BowlingBall to the "bowlingBall" field

- Create empty GameObjects for camera positions:
  - "CamPos_Default" at (0, 3, -5) with rotation (30, 0, 0)
  - "CamPos_BallFollow" at (0, 1, -1) with rotation (10, 0, 0)
  - "CamPos_PinView" at (0, 2, 20) with rotation (30, 180, 0)

- Assign these positions to the "cameraPositions" array in the CameraController

7. Create the Pin Area Trigger
- Create an empty GameObject (Right-click in Hierarchy > Create Empty)
- Rename it to "PinAreaTrigger"
- Position it at (0, 0.5, 16)
- Add a Box Collider component
  - Set the Size to (4, 1, 4)
  - Check the "Is Trigger" box
  - Set its Tag to "PinArea" (create this tag if it doesn't exist)

8. Set Up UI
- Create a Canvas (Right-click in Hierarchy > UI > Canvas)
- Create a Text element under Canvas (Right-click on Canvas > UI > Text (legacy))
  - Rename to "ScoreText"
  - Position it in the top-left corner
  - Set the text to "Score: 0"
- Create another Text element for the frame information
  - Rename to "FrameText" 
  - Position it below ScoreText
  - Set the text to "Frame: 1 Roll: 1"
- Assign these Text elements to the corresponding fields in the GameManager component

9. Connect Everything
- Select the GameManager object
- Assign the BowlingBall object to the "bowlingBall" field
- Assign the PinParent object to the "pinParent" field
- Assign the ScoreText and FrameText to their respective fields

10. Save the Scene
Finally, save the scene again (File > Save) to ensure all your changes are preserved.

Additional Notes:
You may need to adjust positions and scales based on how your game feels during play testing
Consider adding materials to your objects for better visual appearance
Make sure all colliders are properly sized to ensure realistic physics interactions
Test the game by clicking the Play button in Unity