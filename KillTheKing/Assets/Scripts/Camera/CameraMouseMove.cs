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

    public float minX, minY, minZ;
    public float maxX, maxY, maxZ;
    private Vector3 topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner;
    private PauseGame pg;

    // Use this for initialization
    void Start () {
        //ScreenWidth = Screen.width;
        //ScreenHeight = Screen.height;

        //
        bottomLeftCorner = new Vector3(minX, yBoundary, minZ);
        bottomRightCorner = new Vector3(maxX, yBoundary, minZ);
        topLeftCorner = new Vector3(minX, yBoundary, maxZ);
        topRightCorner = new Vector3(maxX, yBoundary, maxZ);

        pg = GameObject.FindGameObjectWithTag("Player").GetComponent<PauseGame>();
    }

    // Update is called once per frame
    void Update()
    {
        // Don't move the camera when the game is paused
        if (pg.isPaused())
            return;

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

    public void OnDrawGizmos()
    {
        if(xBoundary > 0 && yBoundary > 0 && zBoundary > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(xBoundary, 5f, zBoundary), new Vector3(xBoundary, 5f, -zBoundary));
            Gizmos.DrawLine(new Vector3(-xBoundary, 5f, zBoundary), new Vector3(xBoundary, 5f, zBoundary));

            Gizmos.DrawLine(new Vector3(-xBoundary, 5f, -zBoundary), new Vector3(xBoundary, 5f, -zBoundary));
            Gizmos.DrawLine(new Vector3(-xBoundary, 5f, -zBoundary), new Vector3(-xBoundary, 5f, zBoundary));
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(bottomLeftCorner, bottomRightCorner);
            Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
            Gizmos.DrawLine(topRightCorner, bottomRightCorner);
            Gizmos.DrawLine(topRightCorner, topLeftCorner);
        }

        
    }

    
}
