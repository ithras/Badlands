using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] private float maxHealthPoints = 100f;
	public float HealthPoints { get; private set; }
	public float MaxHealthPoints => maxHealthPoints;

	public bool IsDead => HealthPoints == 0;

	 void Start()
	 {
		HealthPoints = maxHealthPoints;
	 }

	public virtual void Add(float value)
	{
		value = Mathf.Max(value, 0);
		HealthPoints = Mathf.Min(HealthPoints + value, maxHealthPoints);
	}

	public virtual void Remove(float value)
	{
		value = Math.Max(value, 0);
		HealthPoints = Mathf.Max(HealthPoints - value, 0);
	}

	public bool IsAtMaxHealth()
	{
		return HealthPoints == maxHealthPoints;
	}
}
