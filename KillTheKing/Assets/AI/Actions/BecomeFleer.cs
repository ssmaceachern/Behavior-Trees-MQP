using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;

[RAINAction]
public class BecomeFleer : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		ai.Body.GetComponentInChildren<EntityRig> ().Entity.GetAspect ("Evil").IsActive = false;
		ai.Body.GetComponentInChildren<EntityRig> ().Entity.GetAspect ("Minion").IsActive = false;

		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}