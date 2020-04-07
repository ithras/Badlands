using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkill : Skill
{
	private GameObject turretPrefab;
	private Transform shootingPoint;
	private GameObject turretObj = null;
	private bool isPlacingTurret = false;

	public void SetTurretPrefab(GameObject turretPrefab) => this.turretPrefab = turretPrefab;
	public void SetShootingPoint(Transform shootingPoint) => this.shootingPoint = shootingPoint;

	public override void UseSkill()
	{
		isPlacingTurret = !isPlacingTurret;

		if (isPlacingTurret)
			CreateTurret();

		else
			PlaceTurret();

	}

	public void CreateTurret()
	{
		turretObj = Instantiate(turretPrefab, shootingPoint, false);
	}

	public void PlaceTurret()
	{
		if (turretObj == null)
			return;

		Turret turret = turretObj.GetComponent<Turret>();
		turret.SetActiveTime(activeTime);
		turret.enabled = true;

		turretObj.transform.parent = null;
		turretObj = null;

		SetSkillTimer(0f);
	
	}
}
