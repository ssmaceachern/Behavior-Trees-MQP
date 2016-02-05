using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class PriestHeal : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		
		GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Halo"));
		particle.GetComponent<ParticleFade> ().followTarget = ai.Body;


		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();
		
		dispatch.BroadcastMsg (0.0f,
		                       ai.Body,
		                       ai.Body.transform.position,
		                       10,
		                       (int)MessageTypes.MsgType.PriestHeal,
		                       10);

        return ActionResult.SUCCESS;
    }

}