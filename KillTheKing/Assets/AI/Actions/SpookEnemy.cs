using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SpookEnemy : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject enemy = ai.WorkingMemory.GetItem<GameObject> ("Enemy");

		if (enemy != null)
		{
			ai.Body.GetComponent<MessageDispatcher>().SendMsg (0.0f,
			                                                   ai.Body,
			                                                   enemy,
			                                                   (int)MessageTypes.MsgType.GetSpooked,
			                                                   50);

		}

		ai.Body.SetActive (false);

		GameObject.Destroy (ai.Body);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}