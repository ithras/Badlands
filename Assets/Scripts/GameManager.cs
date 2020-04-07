using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public GameObject player;
	[SerializeField] private PlayerUI playerUI;
	private PlayerHealth playerHealth;
	private Gun playerGun;
	private AmmoManager ammoManager;
	private WeaponManager weaponManager;

	void Start()
	{
		playerHealth = player.GetComponent<PlayerHealth>();
		weaponManager = player.GetComponent<WeaponManager>();
		playerGun = weaponManager.GetActiveWeapon;
		ammoManager = player.GetComponent<AmmoManager>();

		playerHealth.OnHealthChanged += HandleHealthUpdate;
		ammoManager.OnAmmoChanged += HandleAmmoUpdate;
		weaponManager.OnWeaponChanged += HandleWeaponSwap;

		playerUI.GetAmmoText.text = ammoManager.GetAmmo(playerGun.IsPrimary).ToString();
		playerUI.GetChamberAmmoText.text = playerGun.GetMagAmmo.ToString();

		if (gm == null)
			gm = this.gameObject.GetComponent<GameManager>();
	}

	private void HandleHealthUpdate(HealthChangedEventArgs e) =>
		playerUI.GetHealthBarImage.fillAmount = e.Health / e.MaxHealth;

	public void HandleAmmoUpdate()
	{
		playerUI.GetChamberAmmoText.text = playerGun.GetMagAmmo.ToString();
		playerUI.GetAmmoText.text = ammoManager.GetAmmo(playerGun.IsPrimary).ToString();
	}

	public void HandleWeaponSwap(Gun weapon) {
		playerGun = weapon;
		HandleAmmoUpdate();
	}
}
