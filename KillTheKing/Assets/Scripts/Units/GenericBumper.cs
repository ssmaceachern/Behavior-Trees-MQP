using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;

public class GenericBumper : MonoBehaviour
{
	
	// If the king reaches the end, the player loses
	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("we got fight collisions");
		//timesHit++;
	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.GetComponentInChildren<AIRig>()==null)
		{
			Debug.Log ("hit a non-ai");
			return;
		}
		
		AIRig ai = this.gameObject.GetComponentInChildren <AIRig> ();
		string hisTeam = col.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("Team");

		Vector3 nudgeForce = new Vector3 ();
		nudgeForce.x = (col.gameObject.transform.position.x-this.gameObject.transform.position.x)*3 + (Random.value*10-5);
		nudgeForce.z = (col.gameObject.transform.position.z-this.gameObject.transform.position.z)*3 + (Random.value*10-5);
		col.gameObject.GetComponentInChildren<Rigidbody> ().AddForce (nudgeForce);

		if(hisTeam==ai.AI.WorkingMemory.GetItem("Team"))
		{
			Debug.Log ("hit a non-enemy");
			return;
		}







		/* We don't want an attack every frame, so keep a cooldown value */
		int cooldown = ai.AI.WorkingMemory.GetItem<int> ("Cooldown");
		Debug.Log (cooldown);
		
		// If we shouldn't attack this frame, decrement our cooldown value 
		if (cooldown > 0) 
		{
			ai.AI.WorkingMemory.SetItem<int> ("Cooldown", cooldown-1);
			return;
		}
		
		/* Attack the enemy and reset the cool down, so we don't attack next frame as well */
		int maxCd = ai.AI.WorkingMemory.GetItem<int> ("MaxCooldown");
		ai.AI.WorkingMemory.SetItem<int> ("Cooldown", maxCd);
		
		/* Decrease the enemy's health by the amount of damage we deal */
		int myDamage = ai.AI.WorkingMemory.GetItem<int>("Damage");
		
		// Default to 10 damage if no damage amount specified
		if (myDamage == 0) 
		{
			myDamage = 10;
		}




		Vector3 pushForce = new Vector3 ();
		pushForce.x = (col.gameObject.transform.position.x-this.gameObject.transform.position.x)*100;
		pushForce.z = (col.gameObject.transform.position.z-this.gameObject.transform.position.z)*100;
		col.gameObject.GetComponentInChildren<Rigidbody> ().AddForce (pushForce);



		
		MessageDispatcher dispatch = this.gameObject.GetComponent<MessageDispatcher> ();
		// Tell the enemy how much damage we are dealing to them
		dispatch.SendMsg (0.0f,
		                  this.gameObject,
		                  col.gameObject,
		                  (int)MessageTypes.MsgType.DealDamage,
		                  myDamage);
	}

	void OnCollisionExit(Collision collisionInfo)
	{
		Debug.Log("no more col");
	}

	void onTriggerEnter(Collision col)
	{

		Debug.Log ("we got sumo triggers");
	}
}
