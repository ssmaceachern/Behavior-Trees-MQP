using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private float walkSpeed;
    private float curSpeed;

    // Use this for initialization
    void Start () {
        walkSpeed = 10.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        curSpeed = walkSpeed;

        // Move senteces
        GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f), 0,
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
    }
}
