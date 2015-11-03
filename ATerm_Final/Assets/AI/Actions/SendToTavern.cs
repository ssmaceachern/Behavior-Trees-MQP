using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SendToTavern : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject ANull = ai.WorkingMemory.GetItem<GameObject> ("ANull");
		GameObject myTavern = ai.WorkingMemory.GetItem<GameObject> ("Tavern");
		GameObject mySlave = ai.WorkingMemory.GetItem<GameObject> ("PossibleSlave");

		if(mySlave==ANull) {

			return ActionResult.SUCCESS;
		}

		bool isFleeing = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
		int hisHp = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");
		
		if(isFleeing || hisHp<=0) {

			ai.WorkingMemory.SetItem<GameObject> ("PossibleSlave", ANull);
			return ActionResult.SUCCESS;

		}

		mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Target", myTavern);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}