using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCarrier : MonoBehaviour
{
    [SerializeField] private AmmoManager ammoManager = null;

    public void ApplyAmmo(int amount, AmmoManager.AmmoTypes type) =>
        ammoManager.AddAmmo(amount, type);
    
    public bool IsAtMaxPrimaryAmmo => ammoManager.IsAtMaxPrimaryAmmo;
    public bool IsAtMaxSecondaryAmmo => ammoManager.IsAtMaxSecondaryAmmo;

    public AmmoManager.AmmoTypes primaryType => ammoManager.PrimaryType;
    public AmmoManager.AmmoTypes secondaryType => ammoManager.SecondaryType;

}
