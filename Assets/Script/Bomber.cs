using UnityEngine;

public class Bomber : Enemy
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 dir;

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
