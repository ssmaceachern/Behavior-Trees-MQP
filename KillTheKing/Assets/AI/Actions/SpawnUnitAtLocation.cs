using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SpawnUnitAtLocation : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		string toSpawn= ai.WorkingMemory.GetItem<string> ("unitToSpawn");

        GameObject charPar = GameObject.FindGameObjectWithTag("Characters");

        if (toSpawn != "") 
		{
			GameObject newUnit = (GameObject)GameObject.Instantiate (Resources.Load (toSpawn));

			newUnit.transform.position = ai.Body.transform.position;
            newUnit.transform.parent = charPar.transform;

            ai.WorkingMemory.SetItem<GameObject> ("Target", newUnit);

            if (charPar.GetComponent<FreezeGameplay>().IsFrozen())
            {
                newUnit.GetComponentInChildren<AIRig>().AI.IsActive = false;
            }
        }

        return ActionResult.SUCCESS;
    }
}