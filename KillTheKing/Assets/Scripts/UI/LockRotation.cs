using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Transform))]
public class LockRotation : MonoBehaviour {

	Quaternion rotation;
	void Awake()
	{
		rotation = transform.rotation;
	}
	void LateUpdate()
	{
		transform.rotation = rotation;
	}
}
