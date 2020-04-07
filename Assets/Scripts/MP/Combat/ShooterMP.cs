using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMP : MonoBehaviour
{
    public void Shoot(AmmoMP playerAmmo)
    {
		if (playerAmmo.GetPrimaryAmmo == 0)
			return;

		playerAmmo.UpdateAmmo(-1, AmmoMP.ammoTypes.LightBullets);

		RaycastHit whatHit;
		if (Physics.Raycast(transform.position, transform.forward, out whatHit, Mathf.Infinity))
		{
			Debug.Log(whatHit.collider.name);

			if (whatHit.collider.gameObject.GetComponent<Damageable>())
				whatHit.collider.gameObject.GetComponent<Damageable>().DealDamage(playerAmmo.GetPrimaryDamage);
		}
	}
}
