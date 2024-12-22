using UnityEngine;
using System.Collections.Generic;

public class WeaponRocketHolder : MonoBehaviour
{
    [Header("Weapon Rocket Holder Configuration")]
    [SerializeField] private Transform playerTransform; // Reference to the player
    [SerializeField] private RocketWeapon weaponPrefab; // Prefab for the base RocketWeapon

    private List<RocketWeapon> weapons = new List<RocketWeapon>(); // List of active weapons
    public int maxLevel = 3; // Maximum weapon level
    public int level = 0; // Current weapon level

    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    public void UpgradeWeaponLevel(int amount)
    {
        level += amount;
        level = Mathf.Clamp(level, 0, maxLevel);

        // Clear existing weapons
        foreach (var weapon in weapons)
        {
            Destroy(weapon.gameObject);
        }
        weapons.Clear();

        // Instantiate new weapons based on level
        for (int i = 0; i < level; i++)
        {
            Vector3 positionOffset = CalculateWeaponPosition(i, level);
            Vector3 worldPositionOffset = transform.TransformDirection(positionOffset); // Convert local offset to world space
            RocketWeapon weapon = Instantiate(weaponPrefab, transform.position + worldPositionOffset, transform.rotation, transform);
            weapons.Add(weapon);
        }
    }

    private Vector3 CalculateWeaponPosition(int index, int level)
    {
        // Calculate position offsets in local space based on weapon level and index
        float spacing = 0.6f; // Spacing between weapons
        if (level == 1)
        {
            return Vector3.zero; // Centered
        }
        else if (level == 2)
        {
            return new Vector3(index == 0 ? -spacing : spacing, 0, 0); // Left and right
        }
        else if (level == 3)
        {
            return new Vector3(index == 0 ? -spacing : (index == 1 ? 0 : spacing), 0, 0); // Left, center, right
        }

        return Vector3.zero; // Default to centered
    }
}
