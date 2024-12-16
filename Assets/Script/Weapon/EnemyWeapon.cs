using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("EnemyBullets")]
    public EnemyBullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("EnemyBullet Pool")]
    private static IObjectPool<EnemyBullet> objectPool;  // Static pool shared across all enemies

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    private float timer;

    public Transform parentTransform; // Parent for bullets

    private void Awake()
    {
        Assert.IsNotNull(bulletSpawnPoint);

        // Ensure parentTransform is assigned
        if (parentTransform == null)
        {
            GameObject parentObject = GameObject.Find("EnemyBulletPool");
            if (parentObject == null)
            {
                parentObject = new GameObject("EnemyBulletPool");
            }
            parentTransform = parentObject.transform;
        }

        // Initialize the object pool only if it's not already initialized
        if (objectPool == null)
        {
            objectPool = new ObjectPool<EnemyBullet>(
                CreateEnemyBullet,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyPooledObject,
                collectionCheck,
                defaultCapacity,
                maxSize
            );
        }
    }

    private void Shoot()
    {
        EnemyBullet bulletObj = objectPool.Get();
        bulletObj.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            timer = 0;
            Shoot();
        }
    }

    private EnemyBullet CreateEnemyBullet()
    {
        EnemyBullet instance = Instantiate(bullet);

        // Parent the bullet to parentTransform
        instance.transform.SetParent(parentTransform);

        // Ensure the objectPool is assigned to the instance
        instance.objectPool = objectPool;

        return instance;
    }

    private void OnGetFromPool(EnemyBullet obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(EnemyBullet obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(EnemyBullet obj)
    {
        Destroy(obj.gameObject);
    }
}
