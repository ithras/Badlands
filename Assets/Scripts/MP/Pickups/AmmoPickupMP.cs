using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class AmmoPickupMP : NetworkBehaviour
{
	[SerializeField] private AmmoMP.ammoTypes ammoType = AmmoMP.ammoTypes.LightBullets;
	[SerializeField] private uint ammoAmount = 0;
	
	[Server]
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && col.gameObject.TryGetComponent<AmmoCarrierMP>(out var ammoCarrier))
		{
			if (ammoCarrier.IsAtMaxPrimaryAmmo)
				return;

			ammoCarrier.ApplyAmmo(Convert.ToInt32(ammoAmount), ammoType);
			Destroy(gameObject);
		}
	}
}
