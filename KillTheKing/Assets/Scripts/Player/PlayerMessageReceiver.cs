using UnityEngine;
using System.Collections;

public class PlayerMessageReceiver : MessageReceiver 
{
	public override void ReceiveMessage (Message msg)
	{
		if (msg.msgType == (int)MessageTypes.MsgType.ChangeGold)
		{
			GetComponent<GoldTracker>().ChangeGold ((int) msg.info);
		}
	}
}
