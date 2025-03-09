using UnityEngine;
using UnityEngine.UI;

public class UISetup : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private Canvas canvas;
    private Text scoreText;
    private Text frameText;

    // Context menu for right-click access
    [ContextMenu("Setup UI")]
    public void SetupUI()
    {
        // Find or create Canvas
        canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Add required components
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        // Create Score Text if it doesn't exist
        scoreText = canvas.transform.Find("ScoreText")?.GetComponent<Text>();
        if (scoreText == null)
        {
            GameObject scoreObj = new GameObject("ScoreText");
            scoreObj.transform.SetParent(canvas.transform, false);

            scoreText = scoreObj.AddComponent<Text>();
            scoreText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            scoreText.text = "Score: 0";
            scoreText.fontSize = 24;
            scoreText.color = Color.white;

            RectTransform rectTransform = scoreText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);
            rectTransform.anchoredPosition = new Vector2(20, -20);
            rectTransform.sizeDelta = new Vector2(200, 30);
        }

        // Create Frame Text if it doesn't exist
        frameText = canvas.transform.Find("FrameText")?.GetComponent<Text>();
        if (frameText == null)
        {
            GameObject frameObj = new GameObject("FrameText");
            frameObj.transform.SetParent(canvas.transform, false);

            frameText = frameObj.AddComponent<Text>();
            frameText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            frameText.text = "Frame: 1 Roll: 1";
            frameText.fontSize = 24;
            frameText.color = Color.white;

            RectTransform rectTransform = frameText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);
            rectTransform.anchoredPosition = new Vector2(20, -50);
            rectTransform.sizeDelta = new Vector2(200, 30);
        }

        // Create Exit Button if it doesn't exist
        Button exitButton = canvas.transform.Find("ExitButton")?.GetComponent<Button>();
        if (exitButton == null)
        {
            GameObject exitObj = new GameObject("ExitButton");
            exitObj.transform.SetParent(canvas.transform, false);

            // Add Image component for button background
            Image buttonImage = exitObj.AddComponent<Image>();
            buttonImage.color = new Color(0.8f, 0.2f, 0.2f);

            // Add Button component
            exitButton = exitObj.AddComponent<Button>();
            ColorBlock colors = exitButton.colors;
            colors.normalColor = new Color(0.8f, 0.2f, 0.2f);
            colors.highlightedColor = new Color(0.9f, 0.3f, 0.3f);
            colors.pressedColor = new Color(0.7f, 0.1f, 0.1f);
            exitButton.colors = colors;

            // Create text for the button
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(exitObj.transform, false);
            Text buttonText = textObj.AddComponent<Text>();
            buttonText.text = "EXIT";
            buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            buttonText.fontSize = 20;
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.color = Color.white;

            // Set text RectTransform to fill the button
            RectTransform textRT = buttonText.GetComponent<RectTransform>();
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            textRT.offsetMin = Vector2.zero;
            textRT.offsetMax = Vector2.zero;

            // Position the button in bottom right corner
            RectTransform buttonRT = exitButton.GetComponent<RectTransform>();
            buttonRT.anchorMin = new Vector2(1, 0);
            buttonRT.anchorMax = new Vector2(1, 0);
            buttonRT.pivot = new Vector2(1, 0);
            buttonRT.anchoredPosition = new Vector2(-20, 20);
            buttonRT.sizeDelta = new Vector2(100, 50);

            // Add onClick listener to exit the game
            exitButton.onClick.AddListener(() =>
            {
                Debug.Log("Exit button clicked - Application will quit");
                Application.Quit();
            });
        }

        // Assign to GameManager if needed
        if (gameManager != null)
        {
            gameManager.scoreText = scoreText;
            gameManager.frameText = frameText;
        }
        else
        {
            Debug.LogWarning("GameManager not assigned to UISetup. UI elements will not be linked automatically.");
        }
    }

    // This will appear as a button in the Inspector
    public void SetupUIButton()
    {
        SetupUI();
    }

    // For the Inspector button attribute
    [InspectorButton("Setup UI")]
    public bool setupUIButton;

    // Add this class to create a button in the Inspector
    public class InspectorButtonAttribute : PropertyAttribute
    {
        public readonly string MethodName;

        public InspectorButtonAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
