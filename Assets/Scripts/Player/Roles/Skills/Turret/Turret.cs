using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float Range;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    private float activeTime;
    private Transform target;
    private float fireTimer = 0;

    public float SetActiveTime(float activeTime) => this.activeTime = activeTime;

    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
        Destroy(gameObject, activeTime);
    }

    private void updateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); ;
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
            target = nearestEnemy.transform;

        else
            target = null;
    }

    void Update() 
    {
        if (target == null)
            return;

        partToRotate.LookAt(target);

        if(fireTimer >= (1 / fireRate))
        {
            Shoot();
            fireTimer = 0;
        }

        fireTimer += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bulletGO.transform.parent = transform;

        if (bullet != null)
        {
            bullet.SetSeekTarget(true);
            bullet.Seek(target, damage);
        }
            
    }
    
}
