using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableMP : MonoBehaviour
{
    [SerializeField] private HealthMP health = null;

    public void DealDamage(float damageToDeal)
    {
        health.Remove(damageToDeal);
    }
}
