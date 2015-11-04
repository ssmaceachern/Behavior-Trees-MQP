﻿using UnityEngine;
using System.Collections;
using RAIN.Core;

public class KingMessageReceiver : MessageReceiver 
{
	public override void ReceiveMessage (Message msg)
	{
		// Subtract from the king's health by some amount
		if (msg.msgType == (int) MessageTypes.MsgType.DealDamage)
		{
			AIRig kingAI = GetComponentInChildren<AIRig>();

			int currentHealth = kingAI.AI.WorkingMemory.GetItem<int>("Health");
			kingAI.AI.WorkingMemory.SetItem<int>("Health", (currentHealth - ((int) msg.info)));

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
	}
}
