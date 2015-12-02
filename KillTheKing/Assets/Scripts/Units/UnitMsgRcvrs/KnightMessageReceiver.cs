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
		if (msg.msgType == (int)MessageTypes.MsgType.DealDamage)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();

			int oldHealth = knightAI.AI.WorkingMemory.GetItem<int>("Health");

			knightAI.AI.WorkingMemory.SetItem<int>("Health", oldHealth - (int)msg.info);
		}
		if (msg.msgType == (int)MessageTypes.MsgType.GetSpooked)
		{
			AIRig knightAI = GetComponentInChildren<AIRig>();

			int oldParanoia = knightAI.AI.WorkingMemory.GetItem<int>("Paranoia");

			knightAI.AI.WorkingMemory.SetItem<int>("Paranoia", oldParanoia + (int)msg.info);
		}
	}
}
