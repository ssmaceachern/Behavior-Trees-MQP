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
	}
}
