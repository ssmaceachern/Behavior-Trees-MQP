using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		// Set the rotation to always look towards the camera.
		Quaternion lookAt = Quaternion.LookRotation(Camera.main.transform.forward);
		transform.rotation = lookAt;
	}
}
