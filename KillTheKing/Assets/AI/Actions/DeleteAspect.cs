using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Entities.Aspects;
using RAIN.Entities;

// Delete the given aspect
[RAINAction]
public class DeleteAspect : RAINAction
{
	public Expression aspectToDelete = new Expression();
	private RAINAspect toDelete;

    public override void Start(RAIN.Core.AI ai)
    {
		toDelete = aspectToDelete.Evaluate<RAINAspect> (ai.DeltaTime, ai.WorkingMemory);
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		toDelete.Entity.RemoveAspect (toDelete);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}