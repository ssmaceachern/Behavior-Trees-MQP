using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class BecomeGhost : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		ai.Body.layer=15;
		ai.Body.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezePositionY;

        return ActionResult.SUCCESS;
    }
}