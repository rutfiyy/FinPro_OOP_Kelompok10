using UnityEngine;

public class RapidFirePowerUp : PowerUp
{
    [SerializeField] private float fireRateMultiplier = 0.8f; // Decrease shoot interval

    protected override void ApplyEffect(GameObject player)
    {
        Weapon weapon = player.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.shootIntervalInSeconds *= fireRateMultiplier; // Faster shooting
        }
    }
}
