using UnityEngine;

public abstract class AdvancedWeapon : MonoBehaviour
{
    public int level = 0; // Current weapon level
    protected int maxLevel = 3;

    // Abstract method to fire the weapon
    public abstract void Shoot();
    
    // Abstract method to update the weapon's configuration (e.g., duplicates for levels)
    public abstract void UpgradeWeaponLevel(int amount);
}
