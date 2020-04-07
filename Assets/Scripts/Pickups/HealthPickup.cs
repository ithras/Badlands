using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.TryGetComponent<Healable>(out var healable))
        {
            if (healable.IsAtMaxHealth)
                return;

            healable.ApplyHeal(healAmount);
            Destroy(gameObject);
        }
    }
}
