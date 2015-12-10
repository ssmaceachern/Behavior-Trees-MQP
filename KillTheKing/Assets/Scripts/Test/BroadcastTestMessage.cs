using UnityEngine;
using System.Collections;

public class BroadcastTestMessage : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		// Tell everyone around to move over 3 units
		GetComponent<MessageDispatcher> ().BroadcastMsg (0.0f,
		                                                this.gameObject,
		                                                this.transform.position,
		                                                10.0f,
		                                                (int)MessageTypes.MsgType.MoveTo,
		                                                3.0f);
	}

}
