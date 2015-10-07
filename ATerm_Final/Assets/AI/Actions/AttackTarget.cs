using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class AttackTarget : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		int cooldown = ai.WorkingMemory.GetItem<int> ("Cooldown");

		if (cooldown>0) {
			ai.WorkingMemory.SetItem<int> ("Cooldown", cooldown-1);
			return ActionResult.SUCCESS;
		}

		int maxCd=ai.WorkingMemory.GetItem<int> ("MaxCooldown");
		ai.WorkingMemory.SetItem<int> ("Cooldown", maxCd);

		GameObject myEnemy = ai.WorkingMemory.GetItem<GameObject> ("Enemy");
		int oldHp=myEnemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");
		myEnemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Health", oldHp-10);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}