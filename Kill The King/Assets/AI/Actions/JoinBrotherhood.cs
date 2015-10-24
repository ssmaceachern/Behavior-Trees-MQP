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
public class JoinBrotherhood : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		BasicMind mind = (BasicMind) ai.Mind;

		BTAsset assBT = Resources.Load <BTAsset>("AssassinAI");
		mind.SetBehavior (assBT, null);

		EntityRig pEnt = ai.Body.GetComponentInChildren<EntityRig> ();

		VisualAspect assAspect = new VisualAspect ();

		assAspect.AspectName = "Assassin";

		pEnt.Entity.AddAspect (assAspect);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}