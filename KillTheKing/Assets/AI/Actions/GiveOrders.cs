using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class GiveOrders : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject workSlave = ai.WorkingMemory.GetItem<GameObject> ("WorkingSlave");
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		bool alreadyGiven = true;

		// Ensure we have someone to give orders to.
		if (workSlave == null) 
		{
			alreadyGiven = false;
		} 
		else 
		{
			bool isFleeing = workSlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
			int hisHp = workSlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");

			// Ensure our slave is alive and able to take an order
			if (isFleeing || hisHp <= 0 || !workSlave.activeSelf) 
			{
				ai.WorkingMemory.SetItem<GameObject> ("WorkingSlave", null);
				alreadyGiven=false;
			}
		}

		if (alreadyGiven) 
		{
			return ActionResult.FAILURE;
		}

		GameObject mySlave = ai.WorkingMemory.GetItem<GameObject> ("PossibleSlave");

		bool Fleeing = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
		int Hp = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");
		
		if (Fleeing || Hp<=0 || !mySlave.activeSelf) 
		{	
			ai.WorkingMemory.SetItem<GameObject> ("PossibleSlave", null);
			return ActionResult.SUCCESS;
		}

		ai.WorkingMemory.SetItem<GameObject> ("WorkingSlave", mySlave);

		GameObject myTrap = ai.WorkingMemory.GetItem<GameObject> ("Target");

		mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Rooted", 0);

		// Send a message to the slave to check out the trap.
		dispatch.SendMsg (0.0f,
		                  ai.Body,
		                  mySlave,
		                  (int)MessageTypes.MsgType.SetTarget,
		                  myTrap);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}