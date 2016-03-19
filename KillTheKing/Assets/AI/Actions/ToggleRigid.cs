using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;

[RAINAction]
public class ToggleRigid : RAINAction
{
	public Expression becomeSolid = new Expression();


    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }
}