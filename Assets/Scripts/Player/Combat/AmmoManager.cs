using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AmmoManager : MonoBehaviour
{
    public enum AmmoTypes { Light, Heavy, Support, Explosive }
    [SerializeField] private AmmoTypes primaryType;
    [SerializeField] private AmmoTypes secondaryType;
    [SerializeField] private int primaryAmmo;
    [SerializeField] private int primaryMaxAmmo;
    [SerializeField] private int secondaryAmmo;
    [SerializeField] private int secondaryMaxAmmo;

    public event Action OnAmmoChanged;

    public AmmoTypes PrimaryType => primaryType;
    public AmmoTypes SecondaryType => secondaryType;
    public bool IsAtMaxPrimaryAmmo => primaryAmmo == primaryMaxAmmo;
    public bool IsAtMaxSecondaryAmmo => secondaryAmmo == secondaryMaxAmmo;

    public int GetAmmo(bool isPrimary)
    {
        if (isPrimary)
            return primaryAmmo;

        return secondaryAmmo;
    }

    public void AddAmmo(int amount, AmmoTypes type)
    {
        if(primaryType == type)
        {
            primaryAmmo = Mathf.Min(primaryAmmo + amount, primaryMaxAmmo);
            HandleAmmoUpdate();
        }
        else if( secondaryType == type)
        {
            primaryAmmo = Mathf.Min(primaryAmmo + amount, primaryMaxAmmo);
            HandleAmmoUpdate();
        }
    }
    public void SubstractAmmo(int amount, AmmoTypes type)
    {
        if (primaryType == type)
        {
            primaryAmmo = Mathf.Min(primaryAmmo - amount, primaryMaxAmmo);
            HandleAmmoUpdate();
        }
        else if (secondaryType == type)
        {
            primaryAmmo = Mathf.Min(primaryAmmo - amount, primaryMaxAmmo);
            HandleAmmoUpdate();
        }
    }

    public void HandleAmmoUpdate()
    {
        OnAmmoChanged?.Invoke();
    }
}
