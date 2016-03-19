﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;

public class GenericBumper : MonoBehaviour
{

	void OnCollisionEnter(Collision col)
	{
	//	Debug.Log ("staying");
	}

	void OnCollisionStay(Collision col)
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

		if (col.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("UnitType") == "Assassin" && ai.AI.WorkingMemory.GetItem<string> ("Team") == "Evil") {
			Debug.Log ("Smacked an assassin");

			MessageDispatcher dispatch = ai.AI.Body.GetComponent<MessageDispatcher> ();

			dispatch.SendMsg (0.0f,
			                  ai.AI.Body,
			                  col.gameObject,
			                  (int)MessageTypes.MsgType.DealDamage,
			                  100); 
			return;
		}


		if(hisTeam==ai.AI.WorkingMemory.GetItem<string>("Team"))
		{
			//Debug.Log ("hit a non-enemy?");

			if(col.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("UnitType") == "King")
			{
				Debug.Log ("i touched my king?");

				Vector3 stepBackForce = new Vector3 ();
				stepBackForce = this.gameObject.transform.forward*-100;


				Rigidbody myBod = this.gameObject.GetComponent<Rigidbody> ();
				myBod.AddForce(stepBackForce);

				return;
			}

			if(col.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("Opponent") !=null)
			{
			//	Debug.Log ("iThis guy is fightin");
				hisBod.velocity=new Vector3(0,0,0);
				return;
			}
			
		//	Debug.Log ("iThis guy is bumpy");
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce.x = (col.gameObject.transform.position.x-this.gameObject.transform.position.x)*3;
			nudgeForce.z = (col.gameObject.transform.position.z-this.gameObject.transform.position.z)*3;

			if(Mathf.Abs(nudgeForce.x)>=Mathf.Abs(nudgeForce.z))
			{
				nudgeForce.z+=nudgeForce.x;
			}
			else
			{
				nudgeForce.x+=nudgeForce.z;
			}

			if(this.gameObject.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("Opponent") !=null)
			{
				nudgeForce.x *=3;
				nudgeForce.z *=3;
			}

			hisBod.AddForce(nudgeForce);

			int lastAnnoy=ai.AI.WorkingMemory.GetItem<int> ("Annoyance");
			ai.AI.WorkingMemory.SetItem<int> ("Annoyance", lastAnnoy+1);

	//		Debug.Log(lastAnnoy);

			return;
		}
	}

	void OnCollisionExit(Collision collisionInfo)
	{
		//Debug.Log("no more col");
	}

	void onTriggerEnter(Collision col)
	{

		Debug.Log ("we got sumo triggers?");
	}
}
