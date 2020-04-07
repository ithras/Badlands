using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : Health
{
	public event Action<HealthChangedEventArgs> OnHealthChanged;

	public override void Add(float value)
	{
		base.Add(value);
		HandleHealthUpdated();
	}

	public override void Remove(float value)
	{
		base.Remove(value);
		HandleHealthUpdated();
	}

	private void HandleHealthUpdated()
	{
		OnHealthChanged?.Invoke(new HealthChangedEventArgs
		{
			Health = HealthPoints,
			MaxHealth = MaxHealthPoints
		});
	}
}
