using UnityEngine;
using System.Collections;

public class CameraMouseMove : MonoBehaviour {
    public int Boundary = 100; // distance from edge scrolling starts
    public int speed = 50;
    public int mouseScrollSpeed = 200;

    private int ScreenWidth;
    private int ScreenHeight;

    private bool isFollowing = false;
    private Transform followTarget;
    private Vector3 velocity = Vector3.zero;

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
            }
        } else



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
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + (mouseScrollSpeed * Time.deltaTime),
                transform.position.z);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + (-mouseScrollSpeed * Time.deltaTime),
                transform.position.z);
        }

        //Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
        //Debug.Log(ScreenWidth + ", " + ScreenHeight);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f) != false)
            {
                isFollowing = true;
                followTarget = hit.transform;
                Debug.Log("You selected the " + hit.transform); // ensure you picked right object
            } else
            {
                isFollowing = false;
            }
        }
    }

    
}
