using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

// Turn against the king if we are unloyal enough
[RAINAction]
public class BetrayKing : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
		int myHealth = ai.WorkingMemory.GetItem<int> ("Health");
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		/* Kill the king along with ourselves */
		// Try to kill the king
		dispatch.SendMsg (0.0f,
		                  ai.Body,
		                  myKing,
		                  (int)MessageTypes.MsgType.DealDamage,
		                  myHealth);

		// Kill ourselves
		ai.WorkingMemory.SetItem<int> ("Health", -1);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}