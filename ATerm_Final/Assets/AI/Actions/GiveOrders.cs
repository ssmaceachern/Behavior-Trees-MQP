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
		GameObject workSlave = ai.WorkingMemory.GetItem<GameObject> ("WorkingSlave");
		GameObject aNull = ai.WorkingMemory.GetItem<GameObject> ("ANull");

		bool alreadyGiven = true;

		if (workSlave == aNull) {

			alreadyGiven=false;

		} else {

			bool isFleeing = workSlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
			int hisHp = workSlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");

			if (isFleeing || hisHp<=0 || !workSlave.activeSelf) {

				ai.WorkingMemory.SetItem<GameObject> ("WorkingSlave", aNull);
				alreadyGiven=false;
			}
		}

		if (alreadyGiven) {

			return ActionResult.SUCCESS;

		}

		GameObject mySlave = ai.WorkingMemory.GetItem<GameObject> ("PossibleSlave");

		bool Fleeing = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");

		int Hp = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");
		
		if (Fleeing || Hp<=0 || !mySlave.activeSelf) {
			
			ai.WorkingMemory.SetItem<GameObject> ("PossibleSlave", aNull);
			return ActionResult.SUCCESS;
		}

		ai.WorkingMemory.SetItem<GameObject> ("WorkingSlave", mySlave);

		GameObject myTrap = ai.WorkingMemory.GetItem<GameObject> ("Target");
		mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Target", myTrap);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}