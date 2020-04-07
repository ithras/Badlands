using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healable : MonoBehaviour
{
    [SerializeField] private Health health = null;

    public void ApplyHeal(float amountToHeal) => health.Add(amountToHeal);

    public bool IsAtMaxHealth => health.IsAtMaxHealth();

}
