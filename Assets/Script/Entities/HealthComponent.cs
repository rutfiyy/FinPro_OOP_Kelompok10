using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public int maxHealth;

    private int health;

    InvincibilityComponent invincibilityComponent;

    void Awake()
    {
        health = maxHealth;
        if (audioSource == null)
        {
            audioSource = GameObject.Find("GameAudio").GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        invincibilityComponent = GetComponent<InvincibilityComponent>();
    }

    public void Subtract(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            if (audioSource != null && deathSound != null)
                audioSource.PlayOneShot(deathSound);
            Destroy(gameObject);
        }else if(invincibilityComponent != null)
        {
            if (audioSource != null && hitSound != null)
                audioSource.PlayOneShot(hitSound);
            invincibilityComponent.TriggerInvincibility();
        }else if (audioSource != null){
            if (audioSource != null && hitSound != null)
                audioSource.PlayOneShot(hitSound);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
