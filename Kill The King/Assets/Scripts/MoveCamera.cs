using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public float moveFactor;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (horizontal * moveFactor, 0.0f, vertical * moveFactor);

		transform.position += movement;
	}
}
