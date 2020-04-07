using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Gun primary = null;
    [SerializeField] private Gun secondary = null;
    [SerializeField] private Explosive explosive = null;

    public Gun Primary => primary;
    public Gun Secondary => secondary;
    public Explosive Explosive => explosive;
    public Gun GetActiveWeapon => Primary.isActiveAndEnabled ? Primary : Secondary;

    public event Action<Gun> OnWeaponChanged;

    [SerializeField] private Transform shootingPoint = null;

    private bool isSwapping = false;

    void OnEnable() => InitializeWeapons();

    public void InitializeWeapons()
    {
        Gun[] weapons = GetComponentsInChildren<Gun>(true);

        foreach (Gun weapon in weapons)
        {
            AddWeapon(weapon);
        }
    }

    public void AddWeapon(Gun weapon)
    {
        if (weapon.IsPrimary)
        {
            primary = weapon;
            //Primary.transform.parent = shootingPoint;
        }
        else
        {
            secondary = weapon;
            //Secondary.transform.parent = shootingPoint;
        }
    }

    public void AddExplosive(Explosive explosive) => this.explosive = explosive;

    public void StartWeaponSwap() {
        if (isSwapping)
            return;

        StartCoroutine(WeaponSwap());
    }

    private IEnumerator WeaponSwap()
    {
        isSwapping = true;
        
        if (Primary.isActiveAndEnabled)
        {
            Primary.gameObject.SetActive(false);
            Secondary.gameObject.SetActive(true);
            OnWeaponChanged?.Invoke(Secondary);
        }
        else
        {
            Secondary.gameObject.SetActive(false);
            Primary.gameObject.SetActive(true);
            OnWeaponChanged?.Invoke(Primary);
        }

        yield return new WaitForSeconds(1f);

        isSwapping = false;
    }
}
