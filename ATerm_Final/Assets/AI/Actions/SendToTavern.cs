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








		GameObject mySlave = ai.WorkingMemory.GetItem<GameObject> ("Slave1");

		if(mySlave==ANull) {
			// this should never reach here, or only for 1 milisecond before slave2 becomes slave1 in update
			return ActionResult.SUCCESS;
		}

		bool isFleeing = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
		
		if(isFleeing) {
			// should also never reach this, or any of the fleeing's, but safety first
			ai.WorkingMemory.SetItem<GameObject> ("Slave1", ANull);
			return ActionResult.SUCCESS;
		}

		mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Target", myTavern);







		GameObject mySlaveTwo = ai.WorkingMemory.GetItem<GameObject> ("Slave2");

		if(mySlaveTwo==ANull) {
			return ActionResult.SUCCESS;
		}

		isFleeing = mySlaveTwo.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
		
		if(isFleeing) {
			ai.WorkingMemory.SetItem<GameObject> ("Slave2", ANull);
			return ActionResult.SUCCESS;
		}

		mySlaveTwo.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Target", myTavern);








		GameObject mySlaveThree = ai.WorkingMemory.GetItem<GameObject> ("Slave3");
		
		if(mySlaveThree==ANull) {
			return ActionResult.SUCCESS;
		}
		
		isFleeing = mySlaveThree.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
		
		if(isFleeing) {
			ai.WorkingMemory.SetItem<GameObject> ("Slave3", ANull);
			return ActionResult.SUCCESS;
		}
		
		mySlaveThree.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Target", myTavern);


        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}