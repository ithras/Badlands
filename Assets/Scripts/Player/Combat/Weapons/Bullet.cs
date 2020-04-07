using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float actionAmount { get; private set; }
	private float speed = 60f;
	private Transform target;
	private bool seekTarget;
	private Vector3 shootingDir;
	public Vector3 direction { get; private set; }
	private Rigidbody bulletRB;

	public void SetSpeed(float speed) => this.speed = speed;
	public void SetSeekTarget(bool seekTarget) => this.seekTarget = seekTarget;
	public void SetShootingPoint(Vector3 shootingDir) => this.shootingDir = shootingDir;

	void Start() 
	{
		bulletRB = GetComponent<Rigidbody>();
		if (bulletRB == null)
			return;

		bulletRB.AddForce(shootingDir * speed, ForceMode.VelocityChange);
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
