using UnityEngine;
using UnityEngine.UIElements;

public class Battlefield : MonoBehaviour
{
    private Label timerText;
    private Label nukeText;

    public Nuke nuke;
    private GameObject player;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        GameManager.Instance.InitializeGame();
        
        player = GameObject.FindGameObjectWithTag("Player");

        if (nuke == null && player != null)
        {
            nuke = player.GetComponent<Nuke>();
        }

        timerText = root.Q<Label>("Timer");
        nukeText = root.Q<Label>("Nuke");
    }

    private void Update()
    {
        float timePassed = GameManager.Instance.GetTimePassed();
        timerText.text = $"Time Passed: {timePassed:F2} seconds";

        if (nuke != null)
        {
            int nukeAmount = nuke.nukeAmount;
            nukeText.text = $"Nukes: {nukeAmount}";
        }

        if (player == null)
        {
            GameManager.Instance.EndGame();
        }
    }
}
