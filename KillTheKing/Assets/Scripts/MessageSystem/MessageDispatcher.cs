using UnityEngine;
using System.Collections;

/* A class that must be put onto a game object in order to send messages to objects with a MessageReceiver inherited class */
public class MessageDispatcher : MonoBehaviour 
{
	// Send a message to the receiver
	public void SendMsg(float delay,
	               			GameObject sender,
	                        GameObject receiver,
	                        int msgType,
	                        object info)
	{
		// Build the message to send
		Message newMsg = new Message();

		// Fill the new message with the appropriate information
		newMsg.dispatchDelay = delay;
		newMsg.sender = sender;
		newMsg.receiver = receiver;
		newMsg.msgType = msgType;
		newMsg.info = info;

		// Ensure the receiver is capable of receiving a message
		MessageReceiver rcvr = receiver.GetComponent<MessageReceiver> ();
		if (rcvr != null)
		{
			rcvr.ReceiveMessage(newMsg);
		}
	}
	
}
