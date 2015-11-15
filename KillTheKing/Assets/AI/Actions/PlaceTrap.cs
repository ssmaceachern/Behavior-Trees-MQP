using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class PlaceTrap : RAINAction
{
	public Expression trapRotation = new Expression();

	private string pathTo;

    public override void Start(RAIN.Core.AI ai)
    {
		// Get the path to the prefab
		pathTo = ai.WorkingMemory.GetItem<string> ("trapToLay");

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		Quaternion rotation = Quaternion.Euler (trapRotation.Evaluate <Vector3> (ai.DeltaTime, ai.WorkingMemory));
		Vector3 position = ai.WorkingMemory.GetItem<Vector3> ("trapPlacement");

		GameObject newTrap = (GameObject)GameObject.Instantiate(Resources.Load(pathTo), position, rotation);

		newTrap.transform.position = ai.WorkingMemory.GetItem<Vector3> ("trapPlacement");

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}