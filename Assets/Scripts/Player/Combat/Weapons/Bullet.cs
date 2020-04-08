using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float actionAmount { get; private set; }
	public float speed { get; private set; }
	private Transform target;
	private bool seekTarget;
	public Vector3 shootingDir { get; private set; }
	public Vector3 direction { get; private set; }
	private Rigidbody bulletRB;

	public void SetSpeed(float speed) => this.speed = speed;
	public void SetSeekTarget(bool seekTarget) => this.seekTarget = seekTarget;
	public void SetShootingPoint(Vector3 shootingDir) => this.shootingDir = shootingDir;

	void Start()
	{
		speed = 60f;
	}

	void Update()
	{
		if (bulletRB != null)
			return;

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		float distanceThisFrame = speed * Time.deltaTime;

		if (!seekTarget)
		{
			transform.Translate(shootingDir * distanceThisFrame, Space.World);
			return;
		}
			
		
		direction = target.position - transform.position;

		if (direction.magnitude <= distanceThisFrame)
		{
			Action(target);
			return;
		}

		transform.Translate(direction.normalized * distanceThisFrame, Space.World);
			
	}

	public virtual void Action(Transform target)
	{
		Damageable enemy = target.GetComponent<Damageable>();

		if (enemy != null)
			enemy.DealDamage(actionAmount);

		Destroy(gameObject);
	}

	public void Seek(Transform target, float actionAmount)
	{
		this.target = target;
		this.actionAmount = actionAmount;
	}
}
