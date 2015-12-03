using UnityEngine;
using System.Collections;

public class ReceiveTestMessage : MessageReceiver 
{
	public override void ReceiveMessage (Message msg)
	{
		if (msg.msgType == (int) MessageTypes.MsgType.MoveTo)
		{
			Vector3 oldPos = transform.position;

			Vector3 newPos = new Vector3(oldPos.x + (float)msg.info, oldPos.y, oldPos.z);

			transform.position = newPos;

			Debug.Log ("Moving to " + newPos);
		}
	}	
}
