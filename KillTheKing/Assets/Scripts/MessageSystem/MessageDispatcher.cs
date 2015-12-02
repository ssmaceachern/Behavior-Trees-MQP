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

	// Send a message to every available object within a given radius from a given point
	public void BroadcastMsg(float delay,
	                         GameObject sender,
	                         Vector3 broadcastSource,
	                         float radius,
	                         int msgType,
	                         object info)
	{
		GameObject characters = GameObject.FindGameObjectWithTag ("Characters");

		// As we can only send messaages to objects with message receivers, get a list of all the message receivers
		MessageReceiver[] rcvrs = characters.GetComponentsInChildren<MessageReceiver> ();

		// Now check to see if the parent game object of each receiver lies within the given radius from the broadcast point
		for (int i = 0; i < rcvrs.Length; i++)
		{
			// If the receiver does lie within the effective range of the broadcast, send a message to the object
			if (Vector3.Distance (broadcastSource, rcvrs[i].gameObject.transform.position) <= radius)
			{
				SendMsg (delay,
				         this.gameObject,
				         rcvrs[i].gameObject,
				         msgType,
				         info);
			}
		}
	}
}
