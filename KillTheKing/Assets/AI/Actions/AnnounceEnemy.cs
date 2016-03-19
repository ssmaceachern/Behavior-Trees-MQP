using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class AnnounceEnemy : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		//Debug.Log("telling of enemy");

		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();
			
		dispatch.BroadcastMsg (0.0f,
		                       ai.Body,
		                       ai.Body.transform.position,
		                       20,
		                       (int)MessageTypes.MsgType.SeenEnemy,
		                       ai.WorkingMemory.GetItem<GameObject>("Enemy"));

        return ActionResult.SUCCESS;
    }
}