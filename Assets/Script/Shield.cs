using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent; // Reference to the HealthComponent
    [SerializeField] private Renderer shieldRenderer; // Renderer for the shield object
    [SerializeField] private Color greenColor = Color.green;  // Full health color
    [SerializeField] private Color yellowColor = Color.yellow; // Medium health color
    [SerializeField] private Color redColor = Color.red;     // Low health color

    private int previousHealth; // To track health changes

    void Start()
    {
        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent is not assigned to the PlayerShield script.");
            enabled = false;
            return;
        }

        if (shieldRenderer == null)
        {
            Debug.LogError("Shield Renderer is not assigned to the PlayerShield script.");
            enabled = false;
            return;
        }

        // Initialize shield color
        previousHealth = healthComponent.GetHealth();
        UpdateShieldColor();
    }

    void Update()
    {
        int currentHealth = healthComponent.GetHealth();

        // Update shield color only if health changes
        if (currentHealth != previousHealth)
        {
            UpdateShieldColor();
            previousHealth = currentHealth;
        }
    }

    private void UpdateShieldColor()
    {
        int currentHealth = healthComponent.GetHealth();
        int maxHealth = healthComponent.maxHealth;

        // Determine the shield color based on health
        if (currentHealth == maxHealth)
        {
            shieldRenderer.material.color = greenColor;
        }
        else if (currentHealth == 3)
        {
            shieldRenderer.material.color = yellowColor;
        }
        else if (currentHealth == 2)
        {
            shieldRenderer.material.color = redColor;
        }
        else
        {
            shieldRenderer.enabled = false; // Disable shield if health is 0
            return;
        }

        shieldRenderer.enabled = true; // Ensure shield is visible for non-zero health
    }
}
