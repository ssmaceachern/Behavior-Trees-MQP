using UnityEngine;
using System.Collections;

// Move the camera to center on a given point
public class CenterCameraOnPoint : MonoBehaviour 
{
	public float centerSpeed = 0.1f;	// The speed at which the camera will move to the point
	public float goodEnough = 0.02f;	// The distance at which we will simply snap the camera in place

	private Vector3 centerPoint;		// The point at which we are centering the camera. Null if not centering
	private Vector3 junkValue;			// A junk value to use as a placeholder when not focusing the camera
	private Vector3 returnPoint;		// The point the camera was at before centering so we can return to it

	// Use this for initialization
	void Start () 
	{
		junkValue = new Vector3 (-666f, -666f, -666f);
		centerPoint = junkValue;
		returnPoint = junkValue;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (centerPoint != junkValue)
		{
			float xPos = Mathf.Lerp (Camera.main.transform.position.x, centerPoint.x, centerSpeed);
			float zPos = Mathf.Lerp (Camera.main.transform.position.z, centerPoint.z, centerSpeed);

			Vector3 newPos = new Vector3(xPos, Camera.main.transform.position.y, zPos);

			// Lastly, set the camera to the new position
			Camera.main.transform.position = newPos;
		}
		else if (returnPoint != junkValue)
		{
			float xPos = Mathf.Lerp (Camera.main.transform.position.x, returnPoint.x, centerSpeed);
			float zPos = Mathf.Lerp (Camera.main.transform.position.z, returnPoint.z, centerSpeed);

			Vector3 newPos = new Vector3(xPos, Camera.main.transform.position.y, zPos);

			// Set the camera to the new position
			Camera.main.transform.position = newPos;

			// When we are close enough to the return point, relinquish control back to the player.
			if (Vector3.Distance (newPos, returnPoint) <= goodEnough)
			{
				returnPoint = junkValue;
			}
		}
	}

	// Tell the camera to center on a point
	public void SetPoint(Vector3 newPoint)
	{
		centerPoint = newPoint;
		returnPoint = Camera.main.transform.position;
	}

	// Allow the camera to move freely
	public void FreeCamera()
	{
		centerPoint = junkValue;
	}
}
