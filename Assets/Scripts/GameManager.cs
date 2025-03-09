using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public BowlingBall bowlingBall;
    public Transform pinParent;  // Parent object containing all pins
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text frameText;

    private List<BowlingPin> pins = new List<BowlingPin>();
    private int currentFrame = 1;
    private int currentRoll = 1;
    private int[] frameScores = new int[10];
    private int[] rollScores = new int[21]; // Max 21 rolls in a game (10 frames, with 2 rolls each + potential bonus in 10th)
    private int rollIndex = 0;

    [SerializeField] private PinSetupHelper pinSetupHelper;

    private bool uiInitialized = false;

    void Start()
    {
        // Set full screen mode at startup
        Screen.fullScreen = true;

        // Setup pins if we have a helper
        if (pinSetupHelper != null)
        {
            pinSetupHelper.SetupPins();
        }

        // Find all pins in the pin parent
        foreach (Transform child in pinParent)
        {
            BowlingPin pin = child.GetComponent<BowlingPin>();
            if (pin != null)
                pins.Add(pin);
        }

        // Initialize UI elements if not assigned
        if (scoreText == null || frameText == null)
        {
            Debug.LogWarning("UI Text elements not assigned. Attempting to find them in scene.");
            Canvas canvas = FindAnyObjectByType<Canvas>();
            if (canvas != null)
            {
                if (scoreText == null)
                    scoreText = canvas.transform.Find("ScoreText")?.GetComponent<Text>();

                if (frameText == null)
                    frameText = canvas.transform.Find("FrameText")?.GetComponent<Text>();
            }
        }

        uiInitialized = (scoreText != null && frameText != null);
        if (!uiInitialized)
        {
            Debug.LogError("Failed to initialize UI. Score and frame will not be displayed.");
        }

        // Reset all scores
        for (int i = 0; i < rollScores.Length; i++)
            rollScores[i] = 0;

        for (int i = 0; i < frameScores.Length; i++)
            frameScores[i] = 0;

        UpdateUI();
    }

    public void OnBallRolled()
    {
        // Count fallen pins
        int fallenPins = 0;
        foreach (BowlingPin pin in pins)
        {
            if (pin.IsFallen())
            {
                fallenPins++;
            }
        }

        // Record score for this roll
        rollScores[rollIndex] = fallenPins;
        rollIndex++;

        // Handle spare/strike logic
        if (currentRoll == 1)
        {
            if (fallenPins == 10) // Strike
            {
                currentRoll = 1;
                currentFrame++;
                StartCoroutine(ResetPins());
            }
            else
            {
                currentRoll = 2;
                StartCoroutine(ClearFallenPins());
            }
        }
        else // Second roll in frame
        {
            currentRoll = 1;
            currentFrame++;
            StartCoroutine(ResetPins());
        }

        // Calculate and update score
        CalculateScore();
        UpdateUI();

        // Check if game is over
        if (currentFrame > 10)
        {
            Debug.Log("Game Over! Final Score: " + GetTotalScore());
            // Show game over UI
        }
        else
        {
            // Return ball for next roll
            bowlingBall.ResetBall();
        }
    }

    IEnumerator ClearFallenPins()
    {
        // Wait for pins to settle
        yield return new WaitForSeconds(2f);

        // Remove fallen pins
        foreach (BowlingPin pin in pins)
        {
            if (pin.IsFallen())
            {
                pin.gameObject.SetActive(false);
            }
        }

        // Return the ball
        bowlingBall.ResetBall();
    }

    IEnumerator ResetPins()
    {
        // Wait for pins to settle
        yield return new WaitForSeconds(2f);

        // Reset all pins
        foreach (BowlingPin pin in pins)
        {
            pin.gameObject.SetActive(true);
            pin.AnimateReset();
        }

        yield return new WaitForSeconds(1f); // Wait for animation to complete

        // Return the ball
        bowlingBall.ResetBall();
    }

    void CalculateScore()
    {
        int roll = 0;
        int totalScore = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (IsStrike(roll)) // Strike
            {
                totalScore += 10 + rollScores[roll + 1] + rollScores[roll + 2];
                roll++;
            }
            else if (IsSpare(roll)) // Spare
            {
                totalScore += 10 + rollScores[roll + 2];
                roll += 2;
            }
            else // Open frame
            {
                totalScore += rollScores[roll] + rollScores[roll + 1];
                roll += 2;
            }

            frameScores[frame] = totalScore;
        }
    }

    bool IsStrike(int roll)
    {
        return rollScores[roll] == 10;
    }

    bool IsSpare(int roll)
    {
        return rollScores[roll] + rollScores[roll + 1] == 10;
    }

    int GetTotalScore()
    {
        return frameScores[9];
    }

    void UpdateUI()
    {
        if (!uiInitialized) return;

        // Debug information
        Debug.Log("Updating UI - Frame: " + currentFrame + ", Roll: " + currentRoll +
                 ", Score: " + (currentFrame > 1 ? frameScores[currentFrame - 2] : 0));

        if (scoreText != null)
        {
            string scoreString = "Score: " + (currentFrame > 1 ? frameScores[currentFrame - 2].ToString() : "0");
            scoreText.text = scoreString;
            Debug.Log("Setting score text: " + scoreString);
        }

        if (frameText != null)
        {
            string frameString = "Frame: " + currentFrame + " Roll: " + currentRoll;
            frameText.text = frameString;
            Debug.Log("Setting frame text: " + frameString);
        }
    }
}
