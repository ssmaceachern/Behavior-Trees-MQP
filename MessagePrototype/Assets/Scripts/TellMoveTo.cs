using UnityEngine;
using System.Collections;

public class TellMoveTo : MonoBehaviour 
{
	public GameObject toSend;
	private MessageDispatcher dispatch;
	private bool flip = false;

	// Use this for initialization
	void Start () 
	{
		dispatch = GetComponent<MessageDispatcher> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newPos;
		if (Input.GetKeyDown ("space"))
		{
			if (!flip)
			{
				newPos = new Vector3 (2.0f, 0.5f, -2.15f);
				dispatch.SendMsg (0.0f, 
			    	              this.gameObject, 
			        	          toSend, 
			            	      (int)MessageTypes.MsgType.MoveTo, 
			                	  newPos);
				flip = true;
			}
			else
			{
				newPos = new Vector3(0.0f, 0.5f, -2.15f);
				dispatch.SendMsg (0.0f, 
				                  this.gameObject,
				                  toSend,
				                  (int)MessageTypes.MsgType.MoveTo,
				                  newPos);
				flip = false;
			}
		}
	}
}
