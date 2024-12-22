using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("StartButton");
        Button exitButton = root.Q<Button>("ExitButton");

        startButton.clicked += GameManager.Instance.StartGame;
        exitButton.clicked += GameManager.Instance.QuitGame;
    }
}
