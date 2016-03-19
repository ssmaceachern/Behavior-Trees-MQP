using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class KillKing : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("King");

        MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher>();

        // Deal a lot of damage to the king to immediately kill him.
        dispatch.SendMsg(0.0f,
                         ai.Body,
                         myKing,
                         (int)MessageTypes.MsgType.DealDamage,
                         1000);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}