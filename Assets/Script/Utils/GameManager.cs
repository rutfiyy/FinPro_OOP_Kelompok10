using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Scene MainMenu;
    Scene BattleField;
    Scene ChooseWeapon;
    Scene FinalScore;
    Scene CurrentScene = SceneManager.GetActiveScene();
   
    int playerHP;
    int killCount;
    public EnemySpawner enemySpawner;

    public void transitionToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void transitionToChooseWeapon()
    {
        SceneManager.LoadScene("ChooseWeapon");
    }

    public void transitionToBattleField()
    {
        SceneManager.LoadScene("BattleField");
    }

    public void transitionToFinalScore()
    {
        SceneManager.LoadScene("FinalScore");
    }

    public void onKill()
    {
        killCount++;
    }

    public void onEnemyHit()
    {

    }

    public void onPlayerHit()
    {
        playerHP--;
    }

    void Start()
    {
        playerHP  = 3;
        killCount = 0;
    }

    void Loop()
    {
        while (CurrentScene == BattleField)
        {
            enemySpawner.SpawnEnemy();

            if (playerHP == 0)
            {
                transitionToFinalScore();
            }
        }
    }
}