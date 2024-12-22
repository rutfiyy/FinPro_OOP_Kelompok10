using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    protected override void ApplyEffect(GameObject player)
    {
        HealthComponent healthComponent = player.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.Subtract(-1);
        }
    }
}
