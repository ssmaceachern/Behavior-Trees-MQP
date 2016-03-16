using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class PushFriend : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		dispatch.BroadcastMsg (0.0f,
		                       ai.Body,
		                       ai.Body.transform.position,
		                       2,
		                       (int)MessageTypes.MsgType.PushFriends,
		                       35);

        return ActionResult.SUCCESS;
    }
}