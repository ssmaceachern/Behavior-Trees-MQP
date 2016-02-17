using UnityEngine;
using System.Collections;
using RAIN.Core;

public class KnightMessageReceiver : MessageReceiver 
{
	public override void ReceiveMessage (Message msg)
	{
		if (msg.msgType == (int)MessageTypes.MsgType.SetTarget)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();

			knightAI.AI.WorkingMemory.SetItem<GameObject>("Target", (GameObject)msg.info);
		}

		if (msg.msgType == (int)MessageTypes.MsgType.DealDamage || msg.msgType == (int)MessageTypes.MsgType.GhoulBomb || msg.msgType == (int)MessageTypes.MsgType.SpikeTrap)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();

			int oldHealth = knightAI.AI.WorkingMemory.GetItem<int>("Health");

			knightAI.AI.WorkingMemory.SetItem<int>("Health", oldHealth - (int)msg.info);

			GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Blood"));
			particle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce=(transform.position-msg.sender.transform.position)*50;
			nudgeForce.x += (Random.value*100-50);
			nudgeForce.y = 300;
			nudgeForce.z += (Random.value*100-50);
			hisBod.AddForce(nudgeForce);
		}

		if (msg.msgType == (int)MessageTypes.MsgType.PriestHeal)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();
			
			int oldHealth = knightAI.AI.WorkingMemory.GetItem<int>("Health");

			if(oldHealth<100)
			{
				knightAI.AI.WorkingMemory.SetItem<int>("Health", oldHealth + 10);

				int oldLoyal = knightAI.AI.WorkingMemory.GetItem<int>("Loyalty");
				knightAI.AI.WorkingMemory.SetItem<int>("Loyalty", oldLoyal - 10);
				
				GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Heart"));
				particle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
				Vector3 nudgeForce = new Vector3 ();
				nudgeForce.x = (Random.value*100-50);
				nudgeForce.y = 300;
				nudgeForce.z = (Random.value*100-50);
				hisBod.AddForce(nudgeForce);

				GameObject myMaster = knightAI.AI.WorkingMemory.GetItem<GameObject>("Master");
				GameObject ANull = knightAI.AI.WorkingMemory.GetItem<GameObject>("ANull");

				if(myMaster!=ANull)
				{
					int oldSus=myMaster.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<int>("Paranoia");
					myMaster.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<int>("Paranoia", oldSus+5);
				}
			}
			
		}

		if (msg.msgType == (int)MessageTypes.MsgType.BlueSong)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();
			
			int oldLoyal = knightAI.AI.WorkingMemory.GetItem<int>("Loyalty");
			
			knightAI.AI.WorkingMemory.SetItem<int>("Loyalty", oldLoyal - 5);

			GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Heart"));
			particle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce.x = (Random.value*100-50);
			nudgeForce.y = 300;
			nudgeForce.z = (Random.value*100-50);
			hisBod.AddForce(nudgeForce);

			GameObject myMaster = knightAI.AI.WorkingMemory.GetItem<GameObject>("Master");
			GameObject ANull = knightAI.AI.WorkingMemory.GetItem<GameObject>("ANull");

			if(myMaster!=ANull)
			{
				int oldSus=myMaster.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<int>("Paranoia");
				myMaster.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<int>("Paranoia", oldSus+5);
			}
		}

		if (msg.msgType == (int)MessageTypes.MsgType.GreenSong)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();
			
			knightAI.AI.WorkingMemory.SetItem<int>("WalkSpeed", 2);

		}
		
		if (msg.msgType == (int)MessageTypes.MsgType.GetSpooked)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();

			int oldParanoia = knightAI.AI.WorkingMemory.GetItem<int>("Paranoia");

			knightAI.AI.WorkingMemory.SetItem<int>("Paranoia", oldParanoia + (int)msg.info);
		}
	}
}
