using System.Collections;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    [SerializeField] private int maxNuke = 3;
    [SerializeField] public int nukeAmount;
    [SerializeField] private float cooldown = 3f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip nukeSound;
    [SerializeField] AudioClip noNukeSound;
    private float timer;
    public CombatManager combatManager;
    public GameObject enemyBulletPool;

    private void Start()
    {
        nukeAmount = maxNuke;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= cooldown && Input.GetMouseButtonDown(1) && nukeAmount > 0) // Right mouse button
        {
            timer = 0;
            audioSource.PlayOneShot(nukeSound);
            ActivateNuke();
            nukeAmount--;
        }else if (Input.GetMouseButtonDown(1))
        {
            audioSource.PlayOneShot(noNukeSound);
        }
    }

    private void ActivateNuke()
    {
        // Loop through all enemy spawners
        foreach (EnemySpawner spawner in combatManager.GetActiveSpawners())
        {
            // Destroy all child objects (enemies) in the spawner
            foreach (Transform child in spawner.transform)
            {
                Enemy enemy = child.GetComponent<Enemy>();
                if (enemy != null)
                {
                    HitboxComponent hitbox = enemy.GetComponent<HitboxComponent>();
                    if (hitbox != null)
                    {
                        hitbox.Damage(1000); // Apply damage to destroy the enemy
                    }
                }
            }
        }

        // Destroy all enemy bullets
        foreach (Transform child in enemyBulletPool.transform)
        {
            EnemyBullet enemyBullet = child.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
            {
                HitboxComponent hitbox = enemyBullet.GetComponent<HitboxComponent>();
                if (hitbox != null)
                {
                    hitbox.Damage(1000); // Apply damage to destroy the bullet
                }
            }
        }
    }
}
