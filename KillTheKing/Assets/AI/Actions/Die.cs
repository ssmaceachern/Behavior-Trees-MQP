using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Die : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		//Debug.Log(ai.WorkingMemory.GetItem<string> ("UnitType"));

		if (ai.WorkingMemory.GetItem<string> ("UnitType") == "") { // if you're something that just dies with no other tweaking needed

			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;
			
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "King") { // if you're a king

			ai.WorkingMemory.SetItem<int> ("Health", -1);

			ai.Body.SetActive (false);

			Application.LoadLevel (2);

			return ActionResult.SUCCESS;

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Knight") { // if you're a guard

			ai.WorkingMemory.SetItem<bool> ("Fleeing", true);

			ai.WorkingMemory.SetItem<int> ("Health", -1);

			ai.Body.SetActive (false);

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Dragon") { // if you're a Dragon
			
			ai.WorkingMemory.SetItem<int> ("Health", -1);
			
			ai.Body.SetActive (false);

			//TODO: send a message to everyone around you to take x damage


		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Peasant") { // if you're a Peasant
			
			ai.WorkingMemory.SetItem<int> ("Health", -1);
			
			ai.Body.SetActive (false);

			//TODO: care more if a peasant dies that can be put here, or should that be a per-level script attached to them?

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "unitSpawner") { // if you're a unitSpawner
			
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Ghost") { // if you're a ghost

			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

			
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Ghoul") { // if you're an explosive ghoul
			
			ai.Body.SetActive (false);
		
			MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

			dispatch.BroadcastMsg (0.0f,
			                  		ai.Body,
			                  		ai.Body.transform.position,
			                       	15,
			                  		(int)MessageTypes.MsgType.GhoulBomb,
			                  		20);

			for (int i=0; i<40; i++) {
				GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Bile"));
				particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
				Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
				Vector3 nudgeForce = new Vector3 ();
				nudgeForce.x = (Random.value*500-250);
				nudgeForce.y = 300;
				nudgeForce.z = (Random.value*500-250);
				hisBod.AddForce(nudgeForce);
			}

			return ActionResult.SUCCESS;

		} else { // If you're something that dies with a generic blood spout

			ai.Body.SetActive (false);
		}


		for (int i=0; i<6; i++) {
			GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Blood"));
			particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
			Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce.x = (Random.value*300-150);
			nudgeForce.y = 400;
			nudgeForce.z = (Random.value*300-150);
			hisBod.AddForce(nudgeForce);
		}
	
		ai.Body.SetActive (false);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}