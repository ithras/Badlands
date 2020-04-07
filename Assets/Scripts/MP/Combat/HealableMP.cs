using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealableMP : MonoBehaviour
{
    [SerializeField] private HealthMP health = null;

    public void ApplyHeal(float amounToHeal)
    {
        health.Add(amounToHeal);
    }

    public bool IsAtMaxHealth => health.IsAtMaxHealth();
}
