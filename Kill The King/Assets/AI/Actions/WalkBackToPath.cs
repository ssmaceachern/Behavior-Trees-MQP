using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Motion;

[RAINAction]
public class WalkBackToPath : RAINAction
{
	MoveLookTarget mlt;

    public override void Start(RAIN.Core.AI ai)
    {
		mlt = ai.WorkingMemory.GetItem<MoveLookTarget> ("moveTo");

		if (mlt != null)
			ai.WorkingMemory.SetItem<MoveLookTarget> ("newMoveTo", mlt);

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}