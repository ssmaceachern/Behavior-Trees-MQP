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

		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		dispatch.BroadcastMsg (0.0f,
	    	              		ai.Body,
		                       	ai.Body.transform.position,
	        	          		900,
	            	      		(int)MessageTypes.MsgType.SetTarget,
	                	  		 myTavern);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}