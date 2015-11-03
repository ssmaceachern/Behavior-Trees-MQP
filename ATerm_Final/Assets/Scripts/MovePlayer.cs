using UnityEngine;
using System.Collections;

// Move the player using the arrow keys
public class MovePlayer : MonoBehaviour 
{
	public float moveForce = 100f;
	private float horizontal;
	private float vertical;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponentInParent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
		rb.AddForce(movement * moveForce);
	}
}
