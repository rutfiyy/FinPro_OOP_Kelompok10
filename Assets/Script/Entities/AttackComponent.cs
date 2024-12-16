using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag)) return;

        if (other.GetComponent<HitboxComponent>() != null)
        {
            HitboxComponent hitbox = other.GetComponent<HitboxComponent>();

            hitbox.Damage(damage);
        }

        if (other.GetComponent<InvincibilityComponent>() != null)
        {
            other.GetComponent<InvincibilityComponent>().TriggerInvincibility();
        }
    }
}
