using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

// Give an order for the guards to go to the tavern.
[RAINAction]
public class SendToTavern : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject myTavern = ai.WorkingMemory.GetItem<GameObject> ("Tavern");
		GameObject mySlave = ai.WorkingMemory.GetItem<GameObject> ("PossibleSlave");
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		// Ensure we have a slave to order around.
		if(mySlave == null) 
		{
			return ActionResult.FAILURE;
		}

		/* Check that the guard is capable of going to the tavern */
		bool isFleeing = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<bool> ("Fleeing");
		int hisHp = mySlave.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");
		
		if(isFleeing || hisHp<=0) 
		{
			ai.WorkingMemory.SetItem<GameObject> ("PossibleSlave", null);
			return ActionResult.FAILURE;
		}

		/* Send a message to the guard to set his target */
		// Ensure the guard is capable of receiving messages.
		if (mySlave.GetComponent<MessageReceiver>() != null)
		{
			dispatch.SendMsg (0.0f,
		    	              ai.Body,
		        	          mySlave,
		            	      (int)MessageTypes.MsgType.SetTarget,
		                	  myTavern);
		}

		ai.WorkingMemory.SetItem<GameObject> ("PossibleSlave", null);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}