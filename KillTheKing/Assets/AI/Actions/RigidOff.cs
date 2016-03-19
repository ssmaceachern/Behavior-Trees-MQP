using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RigidOff : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		ai.Body.layer=11;
		ai.Body.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        return ActionResult.SUCCESS;
    }
}