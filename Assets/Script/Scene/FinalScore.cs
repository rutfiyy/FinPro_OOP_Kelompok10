using UnityEngine;
using UnityEngine.UIElements;

public class FinalScore : MonoBehaviour
{
    private Label scoreText;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        scoreText = root.Q<Label>("FinalScore");
        float timePassed = PlayerPrefs.GetFloat("TimePassed");
        scoreText.text = $"Time Passed: {timePassed:F2} seconds";

        Button mainMenuButton = root.Q<Button>("ToMainMenu");
        mainMenuButton.clicked += () =>
        {
            GameManager.Instance.LoadMainMenu();
        };
    }
}