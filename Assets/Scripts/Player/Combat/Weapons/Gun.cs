using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : Weapon
{
	public enum BulletTypes { Projectile, HitScan, Explosive }
	public enum FireModes { Single, Auto, Semi }
	[Header("Gun")]
	[SerializeField] private BulletTypes BulletType = BulletTypes.Projectile;
	[SerializeField] private AmmoManager.AmmoTypes type = AmmoManager.AmmoTypes.Light;
	[SerializeField] private FireModes fireMode = FireModes.Auto;
	[SerializeField] private bool moreFireModes = false;
	[SerializeField] private FireModes[] fireModes = new FireModes[2];
	[SerializeField] private int magAmmo = 0;
	[SerializeField] private int magMaxAmmo = 0;
	[SerializeField] private bool isPrimary = true;
	[SerializeField] private float reloadSpeed = 0f;
	[SerializeField] private AmmoManager ammoManager = null;
	[SerializeField] private GameObject hitScanShotParticle = null;

	private float reloadTimer;
	private float shootTimer = 0f;
	private bool isFiring = false;

	public AmmoManager.AmmoTypes Type => type;
	public int GetMagAmmo => magAmmo;
	public FireModes GetFireMode => fireMode;
	public bool IsPrimary => isPrimary;

	public void SetBulletType(BulletTypes type) => this.BulletType = type;
	
	public void SetChamberMaxAmmo(int chamberMaxAmmo) => this.magMaxAmmo = chamberMaxAmmo;

	public bool Reload()
	{
		if (magAmmo >= magMaxAmmo || ammoManager.GetAmmo(isPrimary) <= 0)
			return false;

		int reloadChamberAmmo = magMaxAmmo - magAmmo;
		int reloadAmmo = ammoManager.GetAmmo(isPrimary) - reloadChamberAmmo;

		if (reloadAmmo < 0)
		{
			reloadChamberAmmo -= reloadAmmo;
		}
			
		magAmmo += reloadChamberAmmo;
		ammoManager.SubstractAmmo(reloadChamberAmmo, type);
		ammoManager.HandleAmmoUpdate();

		return true;
	}

	public void ManageShoot(bool startedShooting)
	{
		if (magAmmo <= 0)
			return;

		shootTimer += Time.deltaTime;

		if (startedShooting)
			shootTimer += GetFireRate;

		ManageFireMode();
	}

	private void ManageFireMode()
	{
		
		switch (fireMode)
		{
			case FireModes.Auto:
				if (shootTimer < GetFireRate)
					return;

				shootTimer = 0f;
				StartCoroutine(Shoot());
				break;

			case FireModes.Semi:
				if (magAmmo < 3 || shootTimer < GetFireRate || isFiring)
					return;

				shootTimer = 0f;
				StartCoroutine(Shoot());
				break;

			case FireModes.Single:
				StartCoroutine(Shoot());
				break;
		}
	}

	private IEnumerator Shoot()
	{
		int semi = fireMode == FireModes.Semi ? 3 : 1;
		int i = 0;
		isFiring = true;

		while (i < semi)
		{
			switch (BulletType)
			{
				case BulletTypes.HitScan:
					HitScanShot();
					break;

				case BulletTypes.Projectile:
					ProjectileShot();
					break;

				case BulletTypes.Explosive:
					ExplosiveShot();
					break;
			}
			GetAnimation.ShootAnimation();
			magAmmo--;
			ammoManager.HandleAmmoUpdate();
			i++;

			if(fireMode == FireModes.Semi)
				yield return new WaitForSeconds(GetFireRate / 3);
		}
		isFiring = false;
	}

	private void HitScanShot()
	{
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var target, GetRange))
		{
			Debug.Log(target.collider.name);

			if (target.collider.tag == "Enemy")
				target.collider.GetComponent<Damageable>().DealDamage(GetDamage);

			//Instantiate(hitScanShotParticle, target.transform, false);
		}
	}

	private void ProjectileShot()
	{

	}

	private void ExplosiveShot()
	{
		//Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

		//foreach(var col in colliders)
		//{
		//    Debug.Log(col.name);

		//    if (col.tag == "Enemy")
		//        col.GetComponent<Damageable>().DealDamage(GetDamage);
		//}
	}
}
