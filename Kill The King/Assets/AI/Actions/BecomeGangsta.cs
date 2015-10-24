using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;
using RAIN.BehaviorTrees;

[RAINAction]
public class BecomeGangsta : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		BasicMind mind = (BasicMind) ai.Mind;
		
		BTAsset thugBT = Resources.Load <BTAsset>("ThugAI");
		mind.SetBehavior (thugBT, null);
		
		EntityRig pEnt = ai.Body.GetComponentInChildren<EntityRig> ();
		
		VisualAspect thugspect = new VisualAspect ();
		
		thugspect.AspectName = "Good";
		
		pEnt.Entity.AddAspect (thugspect);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}