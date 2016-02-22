using UnityEngine;
using System.Collections;

// Lock a game object's rotation to be independent of its parent
public class FixRotation : MonoBehaviour
{
    private Quaternion initRot;     // The initial rotation of the game object to freeze at

	// Use this for initialization
	void Start ()
    {
        // Save our initial rotation
        initRot = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        // Fix our rotation
        transform.rotation = initRot;
	}
}
