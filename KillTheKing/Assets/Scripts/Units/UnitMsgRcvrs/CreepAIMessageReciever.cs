using UnityEngine;
using System.Collections;
using RAIN.Core;

public class CreepAIMessageReciever : MessageReceiver {

	public override void ReceiveMessage (Message msg)
	{
		if (msg.msgType == (int)MessageTypes.MsgType.SetTarget)
		{
			AIRig creepAI = GetComponentInChildren<AIRig>();
			
			creepAI.AI.WorkingMemory.SetItem<GameObject>("Target", (GameObject)msg.info);
		}
		if (msg.msgType == (int)MessageTypes.MsgType.DealDamage)
		{
			AIRig creepAI = GetComponentInChildren<AIRig>();
			
			int oldHealth = creepAI.AI.WorkingMemory.GetItem<int>("Health");
			
			creepAI.AI.WorkingMemory.SetItem<int>("Health", oldHealth - (int)msg.info);

			GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Blood"));
			particle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce.x = (Random.value*200-100);
			nudgeForce.y = 300;
			nudgeForce.z = (Random.value*200-100);
			hisBod.AddForce(nudgeForce);
		}
	}
}
