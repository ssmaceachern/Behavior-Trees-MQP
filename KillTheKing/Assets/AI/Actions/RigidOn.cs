using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RigidOn : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{	
		string myType = ai.WorkingMemory.GetItem<string> ("UnitType");

		if(myType=="King")
		{
			ai.Body.layer=8;
			ai.Body.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |  RigidbodyConstraints.FreezePositionY ;
		}
		else if(myType=="Knight")
		{
			ai.Body.layer=10;
			ai.Body.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |  RigidbodyConstraints.FreezePositionY;
		}
		else if(myType=="Thug")
		{
			ai.Body.layer=9;
			ai.Body.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
		}
		else if(myType=="Archer")
		{
			ai.Body.layer=16;
			ai.Body.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
		}

        return ActionResult.SUCCESS;
    }
}