using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerMP : NetworkBehaviour
{
    public static event Action<PlayerMP> OnPlayerSpawned;
    public static event Action<PlayerMP> OnPlayerDespawned;

    [SyncVar(hook = nameof(HandleOwnerSet))]
    public uint ownerId;

    public uint OwnerId => ownerId;

    private void OnDestroy()
    {
        OnPlayerDespawned?.Invoke(this);
    }

    private void HandleOwnerSet(uint oldValue, uint newValue)
    {
        OnPlayerSpawned?.Invoke(this);
    }

    [Server]
    public void SetOwner(uint ownerId)
    {
        this.ownerId = ownerId;
    }
}
