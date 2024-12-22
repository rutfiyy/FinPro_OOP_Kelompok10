using UnityEngine;

public class NukePowerUp : PowerUp
{

    protected override void ApplyEffect(GameObject player)
    {
        Nuke nuke = player.GetComponentInChildren<Nuke>();
        if (nuke != null)
        {
            nuke.nukeAmount++;
        }
    }
}
