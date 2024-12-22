using UnityEngine;

public class BulletRocketTargeting : MonoBehaviour
{
    private Enemy targetEnemy; // The enemy to follow
    public float speed; // Rocket speed
    private float rotationSpeed = 180f; // Max rotation speed (degrees per second)
    private float trackingAccuracy = 0.1f; // How close it needs to be to the target to stop rotating
    private float retargetInterval = 0.5f; // Time interval for checking for a new target
    private float lastRetargetTime;

    private void Update()
    {
        // If the target is destroyed (null), retarget
        if (targetEnemy == null)
        {
            targetEnemy = FindClosestEnemy(); // Find a new target
            if (targetEnemy == null)
            {
                Destroy(gameObject); // Destroy the rocket if no enemy is found
                return;
            }
        }

        // Check if it's time to retarget (periodically)
        if (Time.time - lastRetargetTime >= retargetInterval)
        {
            FindNewTarget();
            lastRetargetTime = Time.time;
        }

        // Rotate towards the target enemy
        Vector3 direction = targetEnemy.transform.position - transform.position;
        direction.z = 0; // Keep the movement in the 2D plane
        float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the rocket smoothly towards the target (limited by rotation speed)
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, angleToTarget, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Move the rocket forward
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);

        // Stop rotating if the rocket is facing the target within a tolerance range
        if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, angleToTarget)) < trackingAccuracy)
        {
            Destroy(gameObject); // Destroy rocket after it hits the target
        }
    }

    // Initialize the rocket with speed and optionally with an initial target (if desired)
    public void Initialize(float rocketSpeed)
    {
        speed = rocketSpeed;
        targetEnemy = FindClosestEnemy(); // Find the nearest enemy when the rocket is first fired
    }

    // Find the closest enemy within a specified radius
    private Enemy FindClosestEnemy()
    {
        var enemies = FindObjectsOfType<Enemy>(); // Assuming there's an Enemy class attached to all enemy objects
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    // Periodically check for the closest enemy and retarget if necessary
    private void FindNewTarget()
    {
        Enemy newTarget = FindClosestEnemy();

        if (newTarget != null && newTarget != targetEnemy)
        {
            targetEnemy = newTarget; // Switch target if a new one is found
        }
    }
}
