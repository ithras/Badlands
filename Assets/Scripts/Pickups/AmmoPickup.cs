using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AmmoPickup : MonoBehaviour
{
	[SerializeField] private AmmoManager.AmmoTypes type = AmmoManager.AmmoTypes.Light;
	[SerializeField] private uint ammoAmount = 0;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && col.gameObject.TryGetComponent<AmmoCarrier>(out var ammoCarrier))
		{
			if ((ammoCarrier.IsAtMaxPrimaryAmmo && ammoCarrier.IsAtMaxSecondaryAmmo) || (ammoCarrier.primaryType != type && ammoCarrier.secondaryType != type))
				return;

			ammoCarrier.ApplyAmmo(Convert.ToInt32(ammoAmount), type);
			Destroy(gameObject);
		}
	}
}
