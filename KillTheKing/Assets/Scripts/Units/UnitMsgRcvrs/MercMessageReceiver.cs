using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Entities;

// Define how mercenaries will handle various messages
public class MercMessageReceiver : MessageReceiver 
{
	public override void ReceiveMessage (Message msg)
	{
		if (msg.msgType == (int) MessageTypes.MsgType.DealDamage)
		{
			AIRig mercAI = GetComponentInChildren<AIRig>();
			int currentHealth = mercAI.AI.WorkingMemory.GetItem<int>("Health");

			// Decrement our health by the amount of damage contained in the message
			mercAI.AI.WorkingMemory.SetItem<int>("Health", (currentHealth - (int)msg.info));
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.MoveTo)
		{
			AIRig mercAI = GetComponentInChildren<AIRig>();

			mercAI.AI.WorkingMemory.SetItem<Vector3>("Location", (Vector3)msg.info);
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.GiveCommand)
		{
			AIRig mercAI = GetComponentInChildren<AIRig>();

			mercAI.AI.WorkingMemory.SetItem<string>("Command", (string)msg.info);
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.ActivateEntity)
		{
			EntityRig mercEnt = GetComponentInChildren<EntityRig>();

			mercEnt.Entity.GetAspect ("Good").IsActive = true;
		}
	}
}
