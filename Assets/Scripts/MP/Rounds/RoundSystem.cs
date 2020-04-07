using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoundSystem : NetworkBehaviour
{
    private List<PlayerMP> remainingPlayers = new List<PlayerMP>();

    [Server]
    private void HandleDeath(object sender, DeathEventArgs e)
    {
        if (remainingPlayers.Count == 1)
            return;

        foreach(var player in remainingPlayers) 
        {
            if(player == null || player.connectionToClient == e.ConnectionToClient)
            {
                remainingPlayers.Remove(player);
                break;
            }
        }
    }
}
