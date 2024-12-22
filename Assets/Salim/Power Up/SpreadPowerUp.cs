using UnityEngine;

public class SpreadPowerUp : PowerUp
{
    protected override void ApplyEffect(GameObject player)
    {
        Weapon weapon = player.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.bulletPerShot++;
        }
    }
}
