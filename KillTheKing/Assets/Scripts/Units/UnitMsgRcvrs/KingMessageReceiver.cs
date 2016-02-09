using UnityEngine;
using System.Collections;
using RAIN.Core;

public class KingMessageReceiver : MessageReceiver 
{
	public override void ReceiveMessage (Message msg)
	{
		// Subtract from the king's health by some amount
		if (msg.msgType == (int) MessageTypes.MsgType.DealDamage || msg.msgType == (int)MessageTypes.MsgType.GhoulBomb)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();

			int currentHealth = kingAI.AI.WorkingMemory.GetItem<int>("Health");
			kingAI.AI.WorkingMemory.SetItem<int>("Health", (currentHealth - ((int) msg.info)));

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
		// Add to the king's greed by some amount
		if (msg.msgType == (int) MessageTypes.MsgType.MakeGreedy)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();

			int currentGreed = kingAI.AI.WorkingMemory.GetItem<int>("Greed");
			int newGreed = currentGreed + ((int) msg.info);

			// The king can't become less than not greedy at all.
			if (newGreed < 0)
				newGreed = 0;

			kingAI.AI.WorkingMemory.SetItem<int>("Greed", newGreed);
		}
		// Confirm that a guard has handled a trap
		if (msg.msgType == (int) MessageTypes.MsgType.CheckTrap)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();

			kingAI.AI.WorkingMemory.SetItem<GameObject> ("Target", null);
			kingAI.AI.WorkingMemory.SetItem<GameObject> ("WorkingSlave", null);
		}
		if (msg.msgType == (int) MessageTypes.MsgType.ResetAI)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();

			kingAI.AI.Mind.AIInit ();
		}
		if (msg.msgType == (int)MessageTypes.MsgType.GetSpooked)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();
			
			int oldParanoia = kingAI.AI.WorkingMemory.GetItem<int>("Paranoia");
			
			kingAI.AI.WorkingMemory.SetItem<int>("Paranoia", oldParanoia + (int)msg.info);
		}

		if (msg.msgType == (int)MessageTypes.MsgType.PriestHeal)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();
			
			int oldHealth = kingAI.AI.WorkingMemory.GetItem<int>("Health");

			//TODO suspicion?
			
		}

		if (msg.msgType == (int)MessageTypes.MsgType.BlueSong)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();
			
			int oldHealth = kingAI.AI.WorkingMemory.GetItem<int>("Health");
			
			//TODO suspicion?
			
		}

		if (msg.msgType == (int)MessageTypes.MsgType.GreenSong)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();
			
			int oldHealth = kingAI.AI.WorkingMemory.GetItem<int>("Health");
			
			//TODO suspicion?
			
		}

		if (msg.msgType == (int) MessageTypes.MsgType.SpikeTrap)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();
			
			int currentHealth = kingAI.AI.WorkingMemory.GetItem<int>("Health");
			kingAI.AI.WorkingMemory.SetItem<int>("Health", (currentHealth - ((int) msg.info)));

		//	int currentFear = kingAI.AI.WorkingMemory.GetItem<int>("Fear");
		//	kingAI.AI.WorkingMemory.SetItem<int>("Fear", (currentFear + 35));

			for(int i=0;i<3;i++)
			{
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
			
		}
	}
}
