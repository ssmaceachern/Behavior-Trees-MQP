using UnityEngine;
using System.Collections;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;

public class LessLessLaggyBumper : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.GetComponentInChildren<AIRig>()==null)
		{
			//	Debug.Log ("hit a non-ai");
			return;
		}
		
		
		
		Rigidbody hisBod = col.gameObject.GetComponent<Rigidbody> ();
		
		if (hisBod == null) 
		{
			//	Debug.Log ("hit a non-rigid ai");
			return;
		}
		
		
		AIRig ai = this.gameObject.GetComponentInChildren <AIRig> ();
		string hisTeam = col.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("Team");
		
		
		if (col.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("UnitType") == "Assassin" && ai.AI.WorkingMemory.GetItem<string> ("UnitType") == "King") {
			Debug.Log ("This king is about to die");
			
			return;
		}
		
		
		if(hisTeam==ai.AI.WorkingMemory.GetItem<string>("Team"))
		{
			//	Debug.Log ("hit a non-enemy");
			
			if(col.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("UnitType") == "King")
			{
				return;
			}
			
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce.x = (col.gameObject.transform.position.x-this.gameObject.transform.position.x)*80 + 120;
			nudgeForce.z = (col.gameObject.transform.position.z-this.gameObject.transform.position.z)*80;
			
			hisBod.AddForce (nudgeForce);
			
			return;
		}
		
		//Debug.Log (ai.AI.WorkingMemory.GetItem<string>("Team"));
		
		
		
		
		/* We don't want an attack every frame, so keep a cooldown value */
		//	int cooldown = ai.AI.WorkingMemory.GetItem<int> ("Cooldown");
		//Debug.Log (cooldown);
		
		// If we shouldn't attack this frame, decrement our cooldown value 
		//	if (cooldown > 0) 
		//	{
		//		ai.AI.WorkingMemory.SetItem<int> ("Cooldown", cooldown-1);
		//		return;
		//	}
		
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
		pushForce.x = (col.gameObject.transform.position.x-this.gameObject.transform.position.x)*250;
		pushForce.z = (col.gameObject.transform.position.z-this.gameObject.transform.position.z)*250;
		hisBod.AddForce (pushForce);
		
		
		
		
		MessageDispatcher dispatch = this.gameObject.GetComponent<MessageDispatcher> ();
		// Tell the enemy how much damage we are dealing to them
		dispatch.SendMsg (0.0f,
		                  this.gameObject,
		                  col.gameObject,
		                  (int)MessageTypes.MsgType.DealDamage,
		                  myDamage);
	}
}
