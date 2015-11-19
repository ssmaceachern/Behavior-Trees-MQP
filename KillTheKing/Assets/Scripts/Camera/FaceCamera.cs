using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		// Set the rotation to always look towards the camera.
		Quaternion lookAt = Quaternion.LookRotation(Camera.main.transform.forward);
		Vector3 euler = lookAt.eulerAngles;
		euler = new Vector3(euler.x, 0.0f, euler.z);

		transform.localEulerAngles = euler;
	}
}
