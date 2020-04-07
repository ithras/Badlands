using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : Role
{
    private HealingBladeSkill healingBlade;
	[SerializeField] private Transform shootingPoint;
	[SerializeField] private GameObject bladePrefab;
	[SerializeField] private float healingTotalAmount;
    [SerializeField] private float healingBladeCoolDown;
	[SerializeField] private float healingBuffActiveTime;

	public override void Start()
	{
		healingBlade = ScriptableObject.CreateInstance("HealingBladeSkill") as HealingBladeSkill;
		healingBlade.SetBladePrefab(bladePrefab);
		healingBlade.SetShootingPoint(shootingPoint);
		healingBlade.SetHealingTotalAmount(healingTotalAmount);
		setMainSkill(healingBlade, healingBladeCoolDown, healingBuffActiveTime);

		base.Start();
	}

	public override void Update()
	{
		base.Update();
	}
}
