using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class EditTarget : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject myTarget = ai.WorkingMemory.GetItem<GameObject> ("Target");
		string thisEdit = ai.WorkingMemory.GetItem<string> ("editCommand");

		if (thisEdit=="giveLoc") 
		{
			Vector3 newLoc=ai.WorkingMemory.GetItem<Vector3> ("newVec");
			myTarget.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<Vector3> ("Location", newLoc);
		}
		else if (thisEdit=="giveMaster") 
		{
			GameObject newMaster=ai.WorkingMemory.GetItem<GameObject> ("newObj");
			myTarget.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Master", newMaster);
		}
		else if (thisEdit=="giveMoveTarget") 
		{
			Vector3 newLoc=ai.WorkingMemory.GetItem<Vector3> ("newVec");
			myTarget.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<Vector3> ("MoveTarget", newLoc);
		}
		
		int oldUnitsLeft = ai.WorkingMemory.GetItem<int> ("unitsLeft");
		ai.WorkingMemory.SetItem<int> ("unitsLeft", oldUnitsLeft-1);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}