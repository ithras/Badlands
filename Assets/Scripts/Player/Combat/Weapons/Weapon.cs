using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Weapon")]
	[SerializeField] private float damage;
	[SerializeField] private float range;
	[SerializeField] private float fireRate;
	[SerializeField] private WeaponsAnimation weaponAnimation = null;

	public float GetDamage => damage;
	public float GetRange => range;
	public float GetFireRate => 1 / fireRate;
	public WeaponsAnimation GetAnimation => weaponAnimation;
}
