using UnityEngine;

public class OrbWeaponPowerUp : PowerUp
{
    protected override void ApplyEffect(GameObject player)
    {
        OrbWeapon weapon = player.GetComponentInChildren<OrbWeapon>();
        if (weapon != null)
        {
            weapon.UpgradeWeaponLevel(1);
        }
    }
}
