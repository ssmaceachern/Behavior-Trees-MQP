using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class BetrayKing : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
		int hisHealth = myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");
		int myHealth = ai.WorkingMemory.GetItem<int> ("Health");

		myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Health", hisHealth-myHealth);
		
		ai.WorkingMemory.SetItem<int> ("Health", -1);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}