using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;
using RAIN.BehaviorTrees;

[RAINAction]
public class GiveRallyLoc : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject MyMerc = ai.WorkingMemory.GetItem<GameObject> ("Merc");
		GameObject aNull = ai.WorkingMemory.GetItem<GameObject> ("ANull");

		if (MyMerc == aNull) {
			
			ai.WorkingMemory.SetItem<GameObject> ("Merc", aNull);
			return ActionResult.SUCCESS;
			
		} else {

			int hisHp = MyMerc.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");
			
			if (hisHp<=0) {
				
				ai.WorkingMemory.SetItem<GameObject> ("Merc", aNull);
				return ActionResult.SUCCESS;
			}
		}

		Vector3 rallyPoint =  ai.WorkingMemory.GetItem<Vector3> ("Location");
		rallyPoint.x += (Random.value * 4 - 2);
		rallyPoint.z += (Random.value * 4 - 2);

		string command= ai.WorkingMemory.GetItem<string> ("Command");

		MyMerc.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<Vector3> ("Location", rallyPoint);
		MyMerc.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("Command", command);

		EntityRig pEnt = MyMerc.GetComponentInChildren<AIRig>().AI.Body.GetComponentInChildren<EntityRig> ();
		
		pEnt.Entity.GetAspect("Good").IsActive=true;

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}