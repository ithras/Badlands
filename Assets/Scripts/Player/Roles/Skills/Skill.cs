using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public float coolDown { get; private set; }
    public float SkillTimer { get; private set; }
	public float activeTime { get; private set; }
	public void SetCoolDown(float coolDown) => this.coolDown = coolDown;
	public void SkillAddTimer(float amount) => this.SkillTimer += amount;
	public void SetSkillTimer(float timer) => this.SkillTimer = timer;
	public void SetActiveTime(float activeTime) => this.activeTime = activeTime;

	public bool CanBeActivated() => this.SkillTimer >= this.coolDown;
    public virtual void UseSkill() { }
}
