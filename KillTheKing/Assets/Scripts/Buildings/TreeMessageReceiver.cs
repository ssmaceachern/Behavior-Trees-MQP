using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Navigation.Waypoints;

public class TreeMessageReceiver : MessageReceiver 
{
	public Vector3 felledLoc;	// The position the tree will fall down to
	public Vector3 felledRot;	// The rotation of the tree when fallen down
	public int waypointIndex;	// The index of the waypoint to remove
	public WaypointRig wpr;		// The network we will be deleting nodes from

	public override void ReceiveMessage(Message msg)
	{
		// Move the tree to its felled position
		if (msg.msgType == (int)MessageTypes.MsgType.DestroyBuilding)
		{
			transform.position = felledLoc;
			transform.rotation = Quaternion.Euler (felledRot);

			WaypointTracker tracker = transform.parent.gameObject.GetComponent<WaypointTracker>();

			int modifier = tracker.waypointsModifier (waypointIndex);

			tracker.addWaypoint (waypointIndex);

			waypointIndex -= modifier;

			// Remove the associated waypoint
			wpr.WaypointSet.RemoveWaypointAt (waypointIndex);

			// Let the king know the route has changed
			GameObject king = GameObject.FindGameObjectWithTag("King");

			if (king != null)
			{
				GetComponent<MessageDispatcher>().SendMsg (0.0f,
				                                           this.gameObject,
				                                           king,
				                                           (int)MessageTypes.MsgType.ResetAI,
				                                           null);
			}
		}

	}
}
