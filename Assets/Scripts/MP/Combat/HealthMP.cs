using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthMP : NetworkBehaviour
{
	[Header("Health")]
	[SerializeField] private float maxHealthPoints = 100f;
	[SyncVar(hook = nameof(HandleHealthUpdated))]
	private float healthPoints;

	[Header("Lives")]
	public int lives;
	[SerializeField] private int maxLives = 1;
	[SerializeField] private bool canRespawn = true;
	private Vector3 respawnPosition;
	private Quaternion respawnRotation;

	public static event EventHandler<DeathEventArgs> OnDeath;
	public event EventHandler<HealthChangedEventArgsMP> OnHealthChanged;

	public bool IsDead => healthPoints == 0;

	public override void OnStartServer()
	{
		healthPoints = maxHealthPoints;
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;
	}

	[ServerCallback]
	private void OnDestroy()
	{
		OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
	}

	[Server]
	public void Add(float value)
	{
		value = Mathf.Max(value, 0);
		healthPoints = Mathf.Min(healthPoints + value, maxHealthPoints);
	}

	[Server]
	public void Remove(float value)
	{
		value = Math.Max(value, 0);

		healthPoints = Mathf.Max(healthPoints - value, 0);

		if(healthPoints == 0)
		{
			OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
			RpcHandleDeath();
		}
	}

	private void HandleHealthUpdated(float oldValue, float newValue)
	{
		OnHealthChanged?.Invoke(this, new HealthChangedEventArgsMP
		{
			Health = healthPoints,
			MaxHealth = maxHealthPoints
		});
	}

	[ClientRpc]
	private void RpcHandleDeath()
	{
		gameObject.SetActive(false);
	}

	public bool IsAtMaxHealth()
	{
		return healthPoints == maxHealthPoints;
	}
}
