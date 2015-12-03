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
        if (isFollowing == false)
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
            if (Input.mousePosition.x > ScreenWidth - Boundary)
            {
                transform.position = new Vector3(
                    transform.position.x + (speed * Time.deltaTime),
                    transform.position.y,
                    transform.position.z); // move on +X axis
            }
            if (Input.mousePosition.x < 0 + Boundary)
            {
                transform.position = new Vector3(
                    transform.position.x + (-speed * Time.deltaTime),
                    transform.position.y,
                    transform.position.z); // move on -X axis
            }
            if (Input.mousePosition.y > ScreenHeight - Boundary)
            {
                //Debug.Log("North");
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + (speed * Time.deltaTime)); // move on +Z axis
            }
            if (Input.mousePosition.y < 0 + Boundary)
            {
                //Debug.Log("South");
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + (-speed * Time.deltaTime)); // move on -Z axis
            } */
        } 
		else
        {
            Vector3 goalPos = followTarget.position;
            goalPos.y = transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, 0.03f);

        }

        /*
         *  Mouse Scroll Wheel
         */
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
			Vector3 newPos = new Vector3(
				transform.position.x,
				transform.position.y + (mouseScrollSpeed * Time.deltaTime),
				transform.position.z);

			if (newPos.y >= yBoundary)
				newPos.y = transform.position.y;

			transform.position = newPos;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
			Vector3 newPos = new Vector3(
				transform.position.x,
				transform.position.y + (-mouseScrollSpeed * Time.deltaTime),
				transform.position.z);

			if (newPos.y <= 8.0f)
				newPos.y = transform.position.y;

			transform.position = newPos;
        }

        //Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
        //Debug.Log(ScreenWidth + ", " + ScreenHeight);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f) != false)
            {
				for (int i = 0; i < selectableTags.Length; i++)
				{
					if (hit.collider.tag == selectableTags[i])
					{
						isFollowing = true;
						followTarget = hit.transform;
						//Debug.Log("You selected the " + hit.transform); // debug ensure you picked right object
						break;
					}
					else
					{
						isFollowing = false;
                        
                    }
				}
            } else
            {
                isFollowing = false;
            }
        }
    }

    
}
