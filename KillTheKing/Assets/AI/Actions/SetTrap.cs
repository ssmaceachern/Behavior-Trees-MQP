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
public class SetTrap : RAINAction
{
	public Expression trapPosition = new Expression();
	public Expression trapRotation = new Expression ();
	public Expression trapToLay = new Expression ();

	private string trap;
	private Vector3 trapPos;

    public override void Start(RAIN.Core.AI ai)
    {
		trap = trapToLay.Evaluate<string> (ai.DeltaTime, ai.WorkingMemory);
		trapPos = trapPosition.Evaluate<Vector3> (ai.DeltaTime, ai.WorkingMemory);

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		//Quaternion rotation = Quaternion.Euler (trapRotation.Evaluate <Vector3> (ai.DeltaTime, ai.WorkingMemory));

		GameObject newTrap = (GameObject)GameObject.Instantiate (Resources.Load (trap));
		
        GameObject charPar = GameObject.FindGameObjectWithTag("Characters");
		newTrap.transform.parent = charPar.transform;

		newTrap.transform.position = trapPos;
		//newTrap.transform.rotation = rotation;
		
		//EntityRig pEnt = ai.Body.GetComponentInChildren<EntityRig> ();
		//pEnt.Entity.GetAspect("Good").IsActive=false;

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}