using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using RAIN.Core;

public class CameraMouseMove : MonoBehaviour {
    public int Boundary = 100; // distance from edge scrolling starts
    public int speed = 50;
    public int mouseScrollSpeed = 200;
	public string[] selectableTags;
	public float xBoundary;	// When to stop the camera from moving along the x-axis
	public float zBoundary;	// When to stop the camera from moving along the y-axis
	public float yBoundary;	// When to stop the camera from moving along the y-axis

    private int ScreenWidth;
    private int ScreenHeight;

    private bool isFollowing = false;
    private Transform followTarget;
    private Vector3 velocity = Vector3.zero;

    public GameObject KnightUI;

    // Use this for initialization
    void Start () {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(horizontal * speed, 0.0f, vertical * speed);

		Vector3 newPos = transform.position + movement;

		if (newPos.x >= xBoundary || newPos.x <= -xBoundary)
			newPos.x = transform.position.x;
		if (newPos.z >= zBoundary || newPos.z <= -zBoundary)
			newPos.z = transform.position.z;

		transform.position = newPos;

        /*
         *  Mouse Scroll Wheel
         */
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
			newPos = new Vector3(
				transform.position.x,
				transform.position.y + (mouseScrollSpeed * Time.deltaTime),
				transform.position.z);

			if (newPos.y >= yBoundary)
				newPos.y = transform.position.y;

			transform.position = newPos;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
			newPos = new Vector3(
				transform.position.x,
				transform.position.y + (-mouseScrollSpeed * Time.deltaTime),
				transform.position.z);

			if (newPos.y <= 8.0f)
				newPos.y = transform.position.y;

			transform.position = newPos;
        }

        //Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
        //Debug.Log(ScreenWidth + ", " + ScreenHeight);

    }

    
}
