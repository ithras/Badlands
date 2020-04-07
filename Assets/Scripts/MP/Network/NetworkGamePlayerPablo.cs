using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetworkGamePlayerPablo : NetworkBehaviour
{
    [SyncVar]
    public string DisplayName = "Loading...";
    [SyncVar]
    public int Score = 0;

    private NetworkManagerPablo room;

    private NetworkManagerPablo Room
    {
        get
        {
            if (room != null)
                return room;

            return room = NetworkManager.singleton as NetworkManagerPablo;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.GamePlayers.Add(this);
    }

    public override void OnNetworkDestroy()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.DisplayName = displayName;
    }
}
