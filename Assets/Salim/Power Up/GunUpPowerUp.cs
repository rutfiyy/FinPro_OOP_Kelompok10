using UnityEngine;

public class GunUpPowerUp : PowerUp
{
    [SerializeField] private int damageIncrease = 5;

    protected override void ApplyEffect(GameObject player)
    {
        Weapon weapon = player.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.bullet.damage += damageIncrease;
        }
    }
}
