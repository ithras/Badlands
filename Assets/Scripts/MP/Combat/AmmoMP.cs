using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AmmoMP : NetworkBehaviour
{
    public enum ammoTypes { LightBullets, HeavyBullets, SupportBullets, Granades, SecondaryBullets }
    [SerializeField] private ammoTypes ammoType = ammoTypes.LightBullets;

    [Header("Primary Weapon")]
    [SerializeField] private uint primaryDamage = 10;
    [SerializeField] private uint primaryMaxAmmo = 100;

    [SyncVar(hook = nameof(HandleAmmoUpdate))]
    private uint primaryAmmo;
    public uint GetPrimaryAmmo => primaryAmmo;
    public uint GetPrimaryDamage => primaryDamage;

    [Header("Secondary Weapon")]
    [SerializeField] private uint secondaryDamage = 10;
    [SerializeField] private uint secondaryMaxAmmo = 100;

    [SyncVar(hook = nameof(HandleAmmoUpdate))]
    private uint secondaryAmmo;
    public uint GetSecondaryAmmo => secondaryAmmo;
    public uint GetSecondaryDamage => secondaryDamage;

    [Header("Granades")]
    [SerializeField] private uint maxGranades = 3;

    [SyncVar(hook = nameof(HandleAmmoUpdate))]
    private uint granades;
    private uint granadeDamage;
    public uint GetGranades => granades;
    public uint GetGranadeDamage => granadeDamage;

    public event EventHandler<AmmoChangedEventArgsMP> OnAmmoChanged;

    private void HandleAmmoUpdate(uint oldValue, uint newValue)
    {
        OnAmmoChanged?.Invoke(this, new AmmoChangedEventArgsMP
        {
            type = ammoType,
            ammo = primaryAmmo,
            maxAmmo = primaryMaxAmmo
        });
    }

    [Server]
    public void UpdateAmmo(int amount, ammoTypes type)
    {
        switch(type)
        {
            case ammoTypes.LightBullets:
                primaryAmmo = Convert.ToUInt32(Mathf.Clamp(primaryAmmo + amount, 0, primaryMaxAmmo));
                break;

            case ammoTypes.SupportBullets:
                primaryAmmo = Convert.ToUInt32(Mathf.Clamp(primaryAmmo + amount, 0, primaryMaxAmmo));
                break;

            case ammoTypes.HeavyBullets:
                primaryAmmo = Convert.ToUInt32(Mathf.Clamp(primaryAmmo + amount, 0, primaryMaxAmmo));
                break;

            case ammoTypes.Granades:
                granades = Convert.ToUInt32(Mathf.Clamp(granades + amount, 0, maxGranades));
                break;

            case ammoTypes.SecondaryBullets:
                secondaryAmmo = Convert.ToUInt32(Mathf.Clamp(secondaryAmmo + amount, 0, secondaryMaxAmmo));
                break;
        }
    }

    [Server]
    public bool IsAtPrimaryMaxAmmo()
    {
        return primaryAmmo == primaryMaxAmmo;
    }

    [Server]
    public bool IsAtSecondaryMaxAmmo()
    {
        return primaryAmmo == primaryMaxAmmo;
    }

    [Server]
    public bool IsAtMaxGranades()
    {
        return granades == maxGranades;
    }
}
