using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technician : Role
{
	private TurretSkill turretSkill;
	[SerializeField] private GameObject turretPrefab;
	[SerializeField] private Transform shootingPoint;
	[SerializeField] private float turretSkillCoolDown;
	[SerializeField] private float turretActiveTime;

	public override void Start()
	{
		turretSkill = ScriptableObject.CreateInstance("TurretSkill") as TurretSkill;
		turretSkill.SetTurretPrefab(turretPrefab);
		turretSkill.SetShootingPoint(shootingPoint);
		setMainSkill(turretSkill, turretSkillCoolDown, turretActiveTime);

		base.Start();
	}

	public override void Update()
	{
		base.Update();
	}

}
