using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    public GameObject[] enemySpawners;

    private float timer;
    private bool isGameActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(playerSpawnPoint.gameObject);
            DontDestroyOnLoad(enemySpawners[0].gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadMainMenu();
    }

    private void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadChooseWeapon()
    {
        SceneManager.LoadScene("ChooseWeapon");
    }

    public void LoadBattlefield()
    {
        SceneManager.LoadScene("Battlefield");
    }

    public void LoadFinalScore()
    {
        SceneManager.LoadScene("FinalScore");
    }

    public void StartGame()
    {
        LoadChooseWeapon();
    }

    public void InitializeGame()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        foreach (var spawner in enemySpawners)
        {
            spawner.SetActive(true);
        }
        isGameActive = true;
        timer = 0;
    }

    public void EndGame()
    {
        isGameActive = false;
        PlayerPrefs.SetFloat("TimePassed", timer);
        LoadFinalScore();
    }

    public float GetTimePassed()
    {
        return timer;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}