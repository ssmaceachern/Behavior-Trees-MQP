using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Transform))]
public class LockRotation : MonoBehaviour {

	Quaternion rotation;
    Vector3 position;

	void Awake()
	{
        rotation = transform.rotation;
        position = transform.localPosition;
	}

	void LateUpdate()
	{
		transform.rotation = rotation;
        transform.position = new Vector3(transform.parent.position.x,
                                         1.0f,
                                         transform.parent.position.z - (position.z * 4.5f));
	}
}
