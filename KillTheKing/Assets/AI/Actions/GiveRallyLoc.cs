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
		MessageDispatcher dispatch = ai.Body.GetComponent <MessageDispatcher> ();

		// Ensure we have a merc to give a location to.
		if (MyMerc == null) 
		{	
			ai.WorkingMemory.SetItem<GameObject> ("Merc", null);
			return ActionResult.SUCCESS;	
		} 
		else 
		{
			int hisHp = MyMerc.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Health");

			// Ensure the merc is still alive
			if (hisHp<=0) 
			{	
				ai.WorkingMemory.SetItem<GameObject> ("Merc", null);
				return ActionResult.FAILURE;
			}
		}

		// Add some randomness to the selected point, so the mercenaries don't pile up on the same spot.
		Vector3 rallyPoint =  ai.WorkingMemory.GetItem<Vector3> ("Location");
		rallyPoint.x += (Random.value * 4 - 2);
		rallyPoint.z += (Random.value * 4 - 2);

		string command= ai.WorkingMemory.GetItem<string> ("Command");

		// Send messages indicating the point to go to, and the command given
		dispatch.SendMsg (0.0f,
		                  ai.Body,
		                  MyMerc,
		                  (int)MessageTypes.MsgType.MoveTo,
		                  rallyPoint);

		dispatch.SendMsg (0.0f,
		                  ai.Body,
		                  MyMerc,
		                  (int)MessageTypes.MsgType.GiveCommand,
		                  command);

		// Allow the merc to be detected now that he is hired.
		dispatch.SendMsg (0.0f,
		                  ai.Body,
		                  MyMerc,
		                  (int)MessageTypes.MsgType.ActivateEntity,
		                  null);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}