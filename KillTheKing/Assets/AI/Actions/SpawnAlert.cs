using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SpawnAlert : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{

        return ActionResult.SUCCESS;
    }
}