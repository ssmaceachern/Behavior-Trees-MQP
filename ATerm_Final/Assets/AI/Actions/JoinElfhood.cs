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
public class JoinElfhood : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		BasicMind mind = (BasicMind) ai.Mind;
		
		BTAsset elfBT = Resources.Load <BTAsset>("ElfAI");
		mind.SetBehavior (elfBT, null);

		ai.WorkingMemory.SetItem<int> ("MaxCooldown", 500);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}