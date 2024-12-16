using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;


    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private int bulletPerShot;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;


    private float timer;


    public Transform parentTransform;


    private void Awake()
    {
        Assert.IsNotNull(bulletSpawnPoint);

        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }


    private void Shoot()
    {
        bulletPerShot = Math.Clamp(bulletPerShot, 1, 5);

        float totalSpread = (bulletPerShot - 1) * 10f; // Total spread in degrees
        float halfSpread = totalSpread / 2; // Half spread for symmetrical distribution

        for (int shot = 0; shot < bulletPerShot; shot++)
        {
            // Get a bullet from the object pool
            Bullet bulletObj = objectPool.Get();

            // Calculate the angle offset for this bullet
            float angleOffset = -halfSpread + (shot * (totalSpread / Mathf.Max(bulletPerShot - 1, 1)));

            // Calculate the new rotation with the offset
            float newZRotation = bulletSpawnPoint.rotation.eulerAngles.z + angleOffset;

            // Ensure the angle is valid and normalized
            Quaternion rotation = Quaternion.Euler(0, 0, newZRotation % 360);

            // Set bullet position and rotation
            bulletObj.transform.SetPositionAndRotation(bulletSpawnPoint.position, rotation);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds &&  Input.GetMouseButton(0))
        {
            timer = 0;
            Shoot();
        }
    }

    private Bullet CreateBullet()
    {
        Bullet instance = Instantiate(bullet);
        instance.objectPool = objectPool;
        instance.transform.parent = parentTransform;

        return instance;
    }

    private void OnGetFromPool(Bullet obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet obj)
    {
        Destroy(obj.gameObject);
    }
}