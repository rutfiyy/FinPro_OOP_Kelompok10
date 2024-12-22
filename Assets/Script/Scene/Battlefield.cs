using UnityEngine;
using UnityEngine.UIElements;

public class Battlefield : MonoBehaviour
{
    private Label timerText;
    private Label nukeText;

    public Nuke nuke;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        timerText = root.Q<Label>("Timer");
        nukeText = root.Q<Label>("Nuke");

        Button mainMenuButton = root.Q<Button>("ToMainMenu");
        mainMenuButton.clicked += () =>
        {
            GameManager.Instance.LoadMainMenu();
        };
    }

    private void Update()
    {
        float timePassed = GameManager.Instance.GetTimePassed();
        timerText.text = $"Time Passed: {timePassed:F2} seconds";

        int nukeAmount = nuke.nukeAmount;
        nukeText.text = $"Nukes: {nukeAmount}";
    }
}
