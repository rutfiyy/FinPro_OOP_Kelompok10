using UnityEngine;
using UnityEngine.Pool;

public class RocketWeapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Weapon Configuration")]
    [SerializeField] private Bullet bulletPrefab; // Bullet prefab to spawn
    [SerializeField] private Transform firePoint; // Shoot point for the weapon
    [SerializeField] private SpriteRenderer spriteRenderer; // Weapon sprite

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    private float timer;
    private Transform parentTransform; // Parent for bullets

    private void Awake()
    {
        // Initialize the object pool
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);

        // Ensure parentTransform is assigned
        if (parentTransform == null)
        {
            GameObject parentObject = GameObject.Find("BulletPool");
            if (parentObject == null)
            {
                parentObject = new GameObject("BulletPool");
            }
            parentTransform = parentObject.transform;
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds && Input.GetMouseButton(0))
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        // Get a bullet from the pool and set its position/rotation
        Bullet bullet = objectPool.Get();
        bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.objectPool = objectPool;
        bulletInstance.gameObject.SetActive(false);
        bulletInstance.transform.parent = parentTransform;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
