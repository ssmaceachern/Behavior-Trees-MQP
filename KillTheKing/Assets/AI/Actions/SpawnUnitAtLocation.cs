using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SpawnUnitAtLocation : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		string toSpawn= ai.WorkingMemory.GetItem<string> ("unitToSpawn");

		if (toSpawn != "") 
		{
			GameObject newUnit = (GameObject)GameObject.Instantiate (Resources.Load (toSpawn));
			
			newUnit.transform.position = ai.WorkingMemory.GetItem<Vector3> ("spawnLoc");
			
			ai.WorkingMemory.SetItem<GameObject> ("Target", newUnit);
		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}