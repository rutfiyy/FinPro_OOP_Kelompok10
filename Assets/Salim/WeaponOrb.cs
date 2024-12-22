using UnityEngine;
using System.Collections.Generic;

public class OrbWeapon : AdvancedWeapon
{
    [Header("Weapon Configuration")]
    [SerializeField] private GameObject orbPrefab; // Prefab for the orb
    [SerializeField] private float orbitRadius = 1.5f; // Distance of the orb from the player
    [SerializeField] private float orbitSpeed = 50f; // Speed of the orb rotation (degrees per second)

    private List<GameObject> activeOrbs = new List<GameObject>(); // List of spawned orbs
    private List<float> currentAngles = new List<float>(); // Current angles for each orb
    private Transform playerTransform; // Reference to the player

    private void Awake()
    {
        playerTransform = transform; // Assume the script is attached to the player
    }

    private void Update()
    {
        UpdateOrbPositions();
    }

    public override void Shoot()
    {
        // No shooting functionality for the orb weapon
    }

    public override void UpgradeWeaponLevel(int amount)
    {
        level += amount;
        level = Mathf.Clamp(level, 0, maxLevel);

        // Destroy existing orbs
        foreach (GameObject orb in activeOrbs)
        {
            Destroy(orb);
        }
        activeOrbs.Clear();
        currentAngles.Clear();

        // Spawn new orbs based on the level
        for (int i = 0; i < level; i++)
        {
            GameObject orb = Instantiate(orbPrefab, playerTransform.position, Quaternion.identity, playerTransform);
            activeOrbs.Add(orb);

            // Initialize angles for even distribution
            float initialAngle = i * (360f / level);
            currentAngles.Add(initialAngle);
        }
    }

    private void UpdateOrbPositions()
    {
        if (activeOrbs.Count == 0) return;

        // Update the angle of each orb based on orbitSpeed
        for (int i = 0; i < activeOrbs.Count; i++)
        {
            // Increment the angle based on orbitSpeed and time
            currentAngles[i] += orbitSpeed * Time.deltaTime;
            if (currentAngles[i] >= 360f) currentAngles[i] -= 360f; // Keep the angle within 0-360 degrees

            // Calculate the orb's position using trigonometry
            float radian = Mathf.Deg2Rad * currentAngles[i];
            Vector3 orbPosition = new Vector3(
                Mathf.Cos(radian) * orbitRadius,
                Mathf.Sin(radian) * orbitRadius,
                0
            );

            // Update orb position
            activeOrbs[i].transform.localPosition = orbPosition;
        }
    }
}
