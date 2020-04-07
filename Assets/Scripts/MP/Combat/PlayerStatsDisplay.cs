using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerStatEntityDisplay statEntityDisplay = null;
    [SerializeField] private Transform statEntityHolderTransform = null;

    private readonly List<PlayerStatEntityDisplay> statEntityDisplays = new List<PlayerStatEntityDisplay>();

    private void Awake()
    {
        PlayerMP.OnPlayerSpawned += HandlePlayerSpawned;
        PlayerMP.OnPlayerDespawned += HandlePlayerDespawned;
    }

    private void OnDestroy()
    {
        PlayerMP.OnPlayerSpawned -= HandlePlayerSpawned;
        PlayerMP.OnPlayerDespawned -= HandlePlayerDespawned;
    }

    private void HandlePlayerSpawned(PlayerMP player)
    {
        PlayerStatEntityDisplay displayInstance = Instantiate(statEntityDisplay, statEntityHolderTransform);
        displayInstance.SetUp(player);
        statEntityDisplays.Add(displayInstance);
    }

    private void HandlePlayerDespawned(PlayerMP player)
    {
        PlayerStatEntityDisplay displayInstance = statEntityDisplays.FirstOrDefault(x => x.PlayerNetId == player.netId);

        if (displayInstance == null)
            return;

        statEntityDisplays.Remove(displayInstance);

        Destroy(displayInstance.gameObject);
    }
}
