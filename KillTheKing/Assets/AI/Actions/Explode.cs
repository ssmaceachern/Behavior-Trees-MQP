using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;

[RAINAction]
public class Explode : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		string myType = ai.WorkingMemory.GetItem<string> ("TrapType");
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		if (myType == "SpikeTrap") 
		{
			for (int i=0; i<40; i++) 
			{
				GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Bile"));
				particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
				Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
				Vector3 nudgeForce = new Vector3 ();
				nudgeForce.x = (Random.value*500-250);
				nudgeForce.y = 300;
				nudgeForce.z = (Random.value*500-250);
				hisBod.AddForce(nudgeForce);
			}
			
			dispatch.BroadcastMsg (0.0f,
			                       ai.Body,
			                       ai.Body.transform.position,
			                       5,
			                       (int)MessageTypes.MsgType.SpikeTrap,
			                       35);
		} 
		else if (myType == "SnareTrap") 
		{
			GameObject myVictim = ai.WorkingMemory.GetItem<GameObject> ("Victim");

			myVictim.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Rooted", 15);

			Vector3 pullForce = new Vector3 ();
			pullForce.x = (myVictim.transform.position.x-ai.Body.transform.position.x)*-150;
			pullForce.z = (myVictim.transform.position.z-ai.Body.gameObject.transform.position.z)*-150;
			
			Rigidbody hisBod = myVictim.GetComponent<Rigidbody> ();
			hisBod.AddForce(pullForce);

			if(myVictim.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("UnitType")=="King")
			{
				dispatch.SendMsg(0.0f,
				                 ai.Body,
				                 myVictim,
				                 (int)MessageTypes.MsgType.UpdateFear,
				                 10);
			}

		} 
		else 
		{
			Debug.Log ("Unknown trap exploded?????????");
		}
		
		ai.Body.GetComponentInChildren<EntityRig> ().Entity.GetAspect ("Trap").IsActive = false;

		//ai.Body.SetActive (false);

        return ActionResult.SUCCESS;
    }
}