using UnityEngine;
using System.Collections;
using RAIN.Core;

public class PeasantMessageReceiver : MessageReceiver 
{
	public override void ReceiveMessage (Message msg)
	{
		if (msg.msgType == (int)MessageTypes.MsgType.FollowMe)
		{
			AIRig pAI = GetComponentInChildren<AIRig>();

			pAI.AI.WorkingMemory.SetItem<GameObject>("FollowTarget", msg.sender);
		}
		else if (msg.msgType == (int)MessageTypes.MsgType.Saved)
		{
			Destroy (this.gameObject);
		}
	}
}
