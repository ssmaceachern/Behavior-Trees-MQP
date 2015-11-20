using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class BreakBuilding : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject target = ai.WorkingMemory.GetItem<GameObject> ("BuildTarget");

		if (target != null && target.GetComponent<MessageReceiver>() != null)
		{
			ai.Body.GetComponent<MessageDispatcher>().SendMsg (0.0f,
			                                                   ai.Body,
			                                                   target,
			                                                   (int)MessageTypes.MsgType.DestroyBuilding,
			                                                   null);
		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}