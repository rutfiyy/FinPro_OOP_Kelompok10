using UnityEngine;

public class RocketWeaponPowerUp : PowerUp
{
    protected override void ApplyEffect(GameObject player)
    {
        WeaponRocketHolder weapon = player.GetComponentInChildren<WeaponRocketHolder>();
        if (weapon != null)
        {
            weapon.UpgradeWeaponLevel(1);
        }
        Debug.Log(weapon);
    }
}
