using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class GiveOrders : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject mySlave = ai.WorkingMemory.GetItem<GameObject> ("Slave1");
		bool isFleeing = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");

		if(isFleeing) {
			GameObject ANull = ai.WorkingMemory.GetItem<GameObject> ("ANull");
			ai.WorkingMemory.SetItem<GameObject> ("Slave1", ANull);
			return ActionResult.SUCCESS;
		}
		
		GameObject myTrap = ai.WorkingMemory.GetItem<GameObject> ("Target");
		mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Target", myTrap);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}