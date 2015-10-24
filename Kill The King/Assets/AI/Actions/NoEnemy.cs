using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class NoEnemy : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject myEnemy = ai.WorkingMemory.GetItem<GameObject> ("Enemy");
		GameObject aNull = ai.WorkingMemory.GetItem<GameObject> ("ANull");

		if (myEnemy==null || myEnemy == aNull || !myEnemy.activeSelf) {
			ai.WorkingMemory.SetItem<GameObject> ("Enemy", aNull);
			return ActionResult.SUCCESS;
		}

		int hisHp = myEnemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");

		if ( hisHp <= 0) {
			ai.WorkingMemory.SetItem<GameObject> ("Enemy", aNull);
			return ActionResult.SUCCESS;
		}

        return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}