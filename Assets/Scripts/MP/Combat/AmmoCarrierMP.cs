using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCarrierMP : MonoBehaviour
{
    [SerializeField] private AmmoMP ammoManager = null;

    public void ApplyAmmo(int amount, AmmoMP.ammoTypes type)
    {
        ammoManager.UpdateAmmo(amount, type);
    }

    public bool IsAtMaxPrimaryAmmo => ammoManager.IsAtPrimaryMaxAmmo();

    public bool IsAtMaxSecondaryAmmo => ammoManager.IsAtSecondaryMaxAmmo();

    public bool IsAtMaxGranades => ammoManager.IsAtMaxGranades();
}
