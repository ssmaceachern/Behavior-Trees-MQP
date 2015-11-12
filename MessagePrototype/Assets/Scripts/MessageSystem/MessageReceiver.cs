using UnityEngine;
using System.Collections;

// Defines how game objects will handle receiving messages from senders
public abstract class MessageReceiver : MonoBehaviour
{
	// Receive a message from a sender
	public abstract void ReceiveMessage (Message msg);
}
