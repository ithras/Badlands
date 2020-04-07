using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
	public override void Remove(float value)
	{
		base.Remove(value);

		if(IsDead)
			Destroy(gameObject);
	}
}
