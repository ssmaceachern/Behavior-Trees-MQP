using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class KingPushMe : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{

		GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");

		if(ai.WorkingMemory.GetItem<bool> ("PathWalker") || (myKing.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<GameObject> ("Enemy") != null && myKing.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<GameObject> ("Opponent") == null))
		{
			return ActionResult.SUCCESS;
		}

		Vector3 pushForce = new Vector3 ();
		pushForce.x = (ai.Body.transform.position.x-myKing.transform.position.x)*50 +  20*(Random.value-0.5f);
		pushForce.z = (ai.Body.gameObject.transform.position.z-myKing.transform.position.z) * 50 + 20*(Random.value-0.5f);
		
		Rigidbody myBod = ai.Body.GetComponent<Rigidbody> ();
		myBod.AddForce(pushForce);

        return ActionResult.SUCCESS;
    }

}