using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Navigation.Waypoints;

// Create a path plane between each of the connected waypoints on the attached waypoint network
public class DrawPath : MonoBehaviour 
{
	// The scale of the path on the x-axis
	[Range(0.1f, 0.5f)]
	public float pathWidth = 0.5f;
	public float pathLength = 1.0f;

	private WaypointRig wpr;
	private Object pathPart;

	// Use this for initialization
	void Start () 
	{
		wpr = GetComponent<WaypointRig> ();

		// If there is no attached waypoint rig, disable this behaviour
		if (wpr == null)
		{
			Debug.LogError ("No waypoint rig on attached object.");
			this.enabled = false;
		}

		pathPart = Resources.Load ("PathPart");

		ConstructPath ();
	}

	public void ConstructPath()
	{
		WaypointSet wps = wpr.WaypointSet;

		for (int i = 0; i < wps.Waypoints.Count - 1; i++)
		{
			for (int j = 1; j < wps.Waypoints.Count; j++)
			{
				if (wps.HasConnection (i, j))
				{
					DrawPartOfPath(i, j);
				}
			}
		}
	}

	public void DrawPartOfPath(int waypointIndex1, int waypointIndex2)
	{
		WaypointSet wps = wpr.WaypointSet;

		float distBetweenPoints = Vector3.Distance (wps.Waypoints [waypointIndex1].Position, 
		                                            wps.Waypoints [waypointIndex2].Position);
	
		Vector3 midpointBetweenPoints = Midpoint (wps.Waypoints [waypointIndex1].Position, 
		                                          wps.Waypoints [waypointIndex2].Position);
		midpointBetweenPoints.y = 0.01f;

		GameObject newPart = (GameObject)Instantiate (pathPart);

		newPart.transform.position = midpointBetweenPoints;

		newPart.transform.LookAt (wps.Waypoints [waypointIndex2].Position);
		Vector3 eulerRot = newPart.transform.rotation.eulerAngles;
		eulerRot.x = 0.0f;
		eulerRot.z = 0.0f;
		newPart.transform.rotation = Quaternion.Euler (eulerRot);

		newPart.transform.localScale = new Vector3 (pathWidth, 1.0f, distBetweenPoints * pathLength);
	}

	public Vector3 Midpoint(Vector3 point1, Vector3 point2)
	{
		Vector3 midpoint = new Vector3();

		midpoint.x = (point1.x + point2.x) / 2;
		midpoint.y = (point1.y + point2.y) / 2;
		midpoint.z = (point1.z + point2.z) / 2;

		return midpoint;
	}
}
