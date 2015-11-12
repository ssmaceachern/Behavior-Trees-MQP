using UnityEngine;
using System.Collections;

public class MoveTo : MessageReceiver
{
	public override void ReceiveMessage(Message msg)
	{
		if (msg.msgType == (int)MessageTypes.MsgType.MoveTo)
			transform.position = (Vector3)msg.info;
	}
}
