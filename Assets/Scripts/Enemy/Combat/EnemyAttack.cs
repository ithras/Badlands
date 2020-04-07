using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 0f;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
            col.gameObject.GetComponent<Damageable>().DealDamage(damage);
    }
}
