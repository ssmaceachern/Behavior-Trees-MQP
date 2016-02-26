using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;

// Turn against the king if we are unloyal enough
[RAINAction]
public class BetrayKing : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject newTator = (GameObject)GameObject.Instantiate (Resources.Load ("Traitor"));

		newTator.transform.position = ai.Body.transform.position;

		EntityRig pEnt = newTator.GetComponentInChildren<AIRig>().AI.Body.GetComponentInChildren<EntityRig> ();
		pEnt.Entity.GetAspect("Good").IsActive=true;



		GameObject chars= GameObject.FindGameObjectWithTag ("Characters");
		newTator.transform.parent = chars.transform;//GameObject.FindGameObjectWithTag("Characters");

		newTator.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Health", ai.WorkingMemory.GetItem<int>("Health"));




		ai.Body.SetActive (false);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}