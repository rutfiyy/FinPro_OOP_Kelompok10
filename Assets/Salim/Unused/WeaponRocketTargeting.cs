/*
using UnityEngine;

public class TargetingRocketWeapon : AdvancedWeapon
{
    [Header("Rocket Stats")]
    [SerializeField] private float rocketSpeed = 5f; // Speed of the rocket
    [SerializeField] private BulletRocketTargeting rocketPrefab; // Prefab of the rocket

    [Header("Weapon Configuration")]
    [SerializeField] private Transform firePoint; // The point from where the rocket will fire
    [SerializeField] private SpriteRenderer weaponSpriteRenderer; // Reference to the weapon's SpriteRenderer

    [Header("Weapon Sprites")]
    [SerializeField] private Sprite level1Sprite; // Sprite for level 1
    [SerializeField] private Sprite level2Sprite; // Sprite for level 2
    [SerializeField] private Sprite level3Sprite; // Sprite for level 3

    private float shootTimer = 0f;
    private float shootInterval = 3f; // Time interval between each shot (firing rate)

    private void FixedUpdate()
    {
        shootTimer += Time.deltaTime;

        // Shoot if the interval is reached and the player holds the shoot button
        if (shootTimer >= shootInterval && Input.GetMouseButton(0))
        {
            shootTimer = 0f;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateWeaponLevel(1);
        } else if (Input.GetKeyDown(KeyCode.I))
        {
            UpdateWeaponLevel(2);
        } else if (Input.GetKeyDown(KeyCode.O))
        {
            UpdateWeaponLevel(3);
        }
    }

    public override void Shoot()
    {
        // Instantiate a new rocket
        BulletRocketTargeting rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);

        // Initialize the rocket with speed
        rocket.speed = rocketSpeed;

        // Optionally, you can also add a velocity to the rocket if you want it to move on its own path
        // rocket.GetComponent<Rigidbody2D>().velocity = rocket.transform.right * rocketSpeed;
    }

    public override void UpdateWeaponLevel(int newLevel)
    {
        level = newLevel;

        // Adjust stats based on the level
        switch (level)
        {
            case 1:
                rocketSpeed = 5f; // Default speed for level 1
                shootInterval = 3f; // Firing rate for level 1
                weaponSpriteRenderer.sprite = level1Sprite; // Update sprite for level 1
                break;
            case 2:
                rocketSpeed = 7f; // Faster rockets for level 2
                shootInterval = 2f; // Faster firing rate for level 2
                weaponSpriteRenderer.sprite = level2Sprite; // Update sprite for level 2
                break;
            case 3:
                rocketSpeed = 9f; // Even faster rockets for level 3
                shootInterval = 1f; // Fastest firing rate for level 3
                weaponSpriteRenderer.sprite = level3Sprite; // Update sprite for level 3
                break;
        }

        // Optionally, other stats like rotation speed or retargeting frequency could be adjusted
    }
}
*/