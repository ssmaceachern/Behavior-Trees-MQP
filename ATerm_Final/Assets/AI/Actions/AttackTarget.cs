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
		
		GameObject myEnemy = ai.WorkingMemory.GetItem<GameObject> ("Enemy");
		
		if (myEnemy==null || !myEnemy.activeSelf) {
			ai.WorkingMemory.SetItem<GameObject> ("Enemy", null);
			return ActionResult.SUCCESS;
		}

		int cooldown = ai.WorkingMemory.GetItem<int> ("Cooldown");

		if (cooldown>0) {
			ai.WorkingMemory.SetItem<int> ("Cooldown", cooldown-1);
			return ActionResult.SUCCESS;
		}

		int maxCd=ai.WorkingMemory.GetItem<int> ("MaxCooldown");
		ai.WorkingMemory.SetItem<int> ("Cooldown", maxCd);

		int oldHp=myEnemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");

		int myDamage=ai.WorkingMemory.GetItem<int>("Damage");
		if (myDamage != 0) {

			myEnemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Health", oldHp-myDamage);

		} else {

			// default ten damage if normal damage = 0 = null
			myEnemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Health", oldHp-10);

		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}