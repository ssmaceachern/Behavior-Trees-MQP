using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class BlowBridge : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject.Destroy (ai.WorkingMemory.GetItem<GameObject> ("Bridge"));

		ai.WorkingMemory.GetItem<GameObject> ("King").GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<bool> ("Longpath", true);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}