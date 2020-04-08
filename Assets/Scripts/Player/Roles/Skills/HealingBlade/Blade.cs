using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Bullet
{
    private GameObject[] targets;
    private float activeTime;
    private float range;
    private Rigidbody bulletRB;

    public void SetActiveTime(float activeTime) => this.activeTime = activeTime;
    public void SetRange(float range) => this.range = range;

    void Start()
    {
        bulletRB = gameObject.AddComponent<Rigidbody>();
        bulletRB.useGravity = false;
        bulletRB.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        bulletRB.AddForce(shootingDir * speed, ForceMode.VelocityChange);
    }

    void OnTriggerEnter()
    {
        Action(null);
    }

    public override void Action(Transform target) 
    {
        targets = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in targets)
        {
            Vector3 dir = player.transform.position - transform.position;
            Healable playerHealable = player.GetComponent<Healable>();

            if (dir.magnitude <= range)
                StartCoroutine(HealingBuff(playerHealable));
        }

        Destroy(gameObject, 6f);
    }

    private IEnumerator HealingBuff(Healable playerHealable)
    {
        int healingTicks = 5;
        float healing = actionAmount / healingTicks;

        for(int i = 0; i < healingTicks; i++)
        {
            playerHealable.ApplyHeal(healing);
            yield return new WaitForSeconds(activeTime / healingTicks);
        }
    }
}
