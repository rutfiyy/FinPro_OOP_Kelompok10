using System.Collections.Generic;
using UnityEngine;

public class Ally : Enemy
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 dir;
    private bool isDropPowerUp = false;
    [SerializeField] List<GameObject> powerUps;

    private void Awake()
    {
        PickRandomPositions();
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * dir);

        Vector3 ePos = Camera.main.WorldToViewportPoint(new(transform.position.x, transform.position.y, transform.position.z));

        if (ePos.x < -0.05f && transform.rotation.y == -180)
        {
            Destroy(gameObject);
        }
        if (ePos.x > 1.05f && transform.rotation.y == 0)
        {
            Destroy(gameObject);
        }
   
        if (ePos.x > 0.4f && ePos.x < 0.6f && !isDropPowerUp)
        {
            isDropPowerUp = true;
            DropPowerUp();
        }
    }

    private void DropPowerUp()
    {
        int random = Random.Range(0, powerUps.Count);
        Instantiate(powerUps[random], transform.position, Quaternion.identity);
    }
    private void PickRandomPositions()
    {
        Vector2 randPos;

        if (Random.Range(-1, 1) >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            randPos = new(1.1f, Random.Range(0.65f, 0.95f));
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            randPos = new(-0.01f, Random.Range(0.65f, 0.95f));
        }

        dir = Vector2.right;

        transform.position = Camera.main.ViewportToWorldPoint(randPos) + new Vector3(0, 0, 10);
    }
}