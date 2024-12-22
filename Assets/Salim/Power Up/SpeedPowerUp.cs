using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    [SerializeField] private float speedIncrease = 2f;

    protected override void ApplyEffect(GameObject player)
    {
        CharacterFollowMouse movement = player.GetComponent<CharacterFollowMouse>();
        if (movement != null)
        {
            movement.maxSpeed += speedIncrease;
        }
    }
}
