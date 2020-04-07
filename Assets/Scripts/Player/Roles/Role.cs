using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
	
	public Skill MainSkill { get; private set; }
	public Skill UltimateSkill { get; private set; }
	public void setMainSkill(Skill mainSkill, float coolDown, float activeTime) 
	{
		this.MainSkill = mainSkill;
		this.MainSkill.SetCoolDown(coolDown);
		this.MainSkill.SetActiveTime(activeTime);
	}
	public void setUltimateSkill(Skill ultimateSkill, float coolDown)
	{
		this.UltimateSkill = ultimateSkill;
		this.UltimateSkill.SetCoolDown(coolDown);
	}

	public virtual void Start()
	{
		MainSkill.SetSkillTimer(Mathf.Infinity);
		//UltimateSkill.SetSkillTimer(0f);
	}

	public virtual void Update()
	{
		MainSkill.SkillAddTimer(Time.deltaTime);
		//UltimateSkill.SkillAddTimer(Time.deltaTime);
	}
}
