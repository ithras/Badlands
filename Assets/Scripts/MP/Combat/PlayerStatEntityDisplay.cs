using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class PlayerStatEntityDisplay : MonoBehaviour
{
    [SerializeField] private Image characterIconImage = null;
    [SerializeField] private TMP_Text playerNameText = null;
    [SerializeField] private TMP_Text scoreText = null;
    [SerializeField] private Image healthBarImage = null;

    public uint PlayerNetId { get; private set; }

    public string PlayerName { get; private set; }

    public void SetUp(PlayerMP player)
    {
        PlayerNetId = player.OwnerId;

        if (NetworkIdentity.spawned[player.OwnerId].TryGetComponent<NetworkGamePlayerPablo>(out var gamePlayer))
        {
            playerNameText.text = gamePlayer.DisplayName;
            PlayerName = gamePlayer.DisplayName;
            scoreText.text = gamePlayer.Score.ToString();
        }

        if (!player.TryGetComponent<HealthMP>(out var health))
            return;

        health.OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(object sender, HealthChangedEventArgsMP e)
    {
        healthBarImage.fillAmount = e.Health / e.MaxHealth;
    }

}
