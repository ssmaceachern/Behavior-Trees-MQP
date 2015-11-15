using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Navigation.Waypoints;
using RAIN.Navigation.Graph;

public class BlowUp : MonoBehaviour 
{
	public GameObject king;	// To tell to reset AI
	public WaypointRig wpr;

	public void BlowBridge()
	{
		MessageDispatcher dispatch = GetComponent<MessageDispatcher> ();
				

		Debug.Log ("Removing connection");
		wpr.WaypointSet.RemoveWaypointAt (5);

		dispatch.SendMsg (0.0f,
		                  this.gameObject,
		                  king,
		                  (int)MessageTypes.MsgType.ResetAI,
		                  null);
		
		Destroy (this.gameObject);
	}

}
