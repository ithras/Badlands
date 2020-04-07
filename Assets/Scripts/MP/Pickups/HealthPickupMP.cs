using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthPickupMP : NetworkBehaviour
{
    [SerializeField] private float healAmount;

    [Server]
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.TryGetComponent<HealableMP>(out var healable))
        {
            if (healable.IsAtMaxHealth)
                return;

            healable.ApplyHeal(healAmount);
            Destroy(gameObject);
        }
    }
}
