using UnityEngine;
using System.Collections;

/* Defines a game object for messages to be sent between game objects */
public class Message 
{
	public GameObject sender;		// The sender of this message
	public GameObject receiver;		// The receiver of this message
	public int msgType;				// An enumerated value stored in "MessageTypes.cs" representing the type of message sent
	public float dispatchDelay;		// The amount of time to wait to send this message
	/* objects can contain any value of variable, so they must be casted to the appropriate type when the message is received */
	public object info;				// Any additional information needed for the message
}
