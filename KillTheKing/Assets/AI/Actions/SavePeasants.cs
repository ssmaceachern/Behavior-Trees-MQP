using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class SavePeasants : RAINAction
{
	public Expression shoutRadius;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		float shoutRad = shoutRadius.Evaluate<float> (ai.DeltaTime, ai.WorkingMemory);

		ai.Body.GetComponent<MessageDispatcher> ().BroadcastMsg (0.0f,
		                                                       	 ai.Body,
		                                                       	 ai.Body.transform.position,
		                                                         shoutRad,
		                                                         (int)MessageTypes.MsgType.FollowMe,
		                                                         null);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}