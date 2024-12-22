using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pickupSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (audioSource == null)
        {
            audioSource = GameObject.Find("GameAudio").GetComponent<AudioSource>();
        }
    } 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero; // Stop vertical movement
            rb.isKinematic = true;     // Make the power-up static
        }
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
            if (audioSource != null && pickupSound != null) audioSource.PlayOneShot(pickupSound);
            Destroy(gameObject); // Destroy the power-up after pickup
        }
    }

    /// <summary>
    /// Applies the specific power-up effect to the player.
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    protected abstract void ApplyEffect(GameObject player);
}
