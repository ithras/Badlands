using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBladeSkill : Skill
{
	private GameObject bladePrefab;
	public Transform shootingPoint { get; private set; }
	private float healingTotalAmount;
	private bool isAiming = false;
	private Blade blade;

	public void SetBladePrefab(GameObject bladePrefab) => this.bladePrefab = bladePrefab;
	public void SetShootingPoint(Transform shootingPoint) => this.shootingPoint = shootingPoint;
	public void SetHealingTotalAmount(float amount) => this.healingTotalAmount = amount;

	public override void UseSkill()
	{
		if (!isAiming)
		{
			GameObject bladeObj = Instantiate(bladePrefab, shootingPoint);
			blade = bladeObj.GetComponent<Blade>();
		}
		else if(isAiming && blade != null)
		{
			blade.transform.parent = null;
			Debug.Log(blade.transform.parent);
			blade.SetActiveTime(activeTime);
			blade.SetSeekTarget(false);
			blade.SetSpeed(10f);
			blade.SetRange(20f);
			blade.SetShootingPoint(shootingPoint.forward);
			blade.Seek(shootingPoint, healingTotalAmount);
			blade.enabled = true;
		}

		isAiming = !isAiming;
	}
}
