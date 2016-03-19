using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;

[RAINAction]
public class RearmTrap : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{

		if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Chest") 
		{
			ai.Body.GetComponentInChildren<EntityRig> ().Entity.GetAspect ("Chest").IsActive = true;

		} 
		else if (ai.WorkingMemory.GetItem<string> ("ChestType") == "Trap") 
		{
			ai.Body.GetComponentInChildren<EntityRig> ().Entity.GetAspect ("Trap").IsActive = true;
		} 
		else 
		{
			Debug.Log("UNKNOWN UNIT TYPE IS USING REARM?");
		}

		GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load("EvilAura"));
		particle.GetComponent<ParticleFade>().followTarget = ai.Body;

		ai.WorkingMemory.SetItem<bool> ("Used", false);

        return ActionResult.SUCCESS;
    }

}