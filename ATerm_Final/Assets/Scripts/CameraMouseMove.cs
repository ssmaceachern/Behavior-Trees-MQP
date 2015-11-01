using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using RAIN.Core;

public class CameraMouseMove : MonoBehaviour {
    public int Boundary = 100; // distance from edge scrolling starts
    public int speed = 50;
    public int mouseScrollSpeed = 200;
	public string[] selectableTags;

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
        KnightUI.SetActive(false);
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

            if (followTarget.name == "Knight")
            {
                //Debug.Log("Activate GUI");
                KnightUI.SetActive(true);

                AIRig tRig = followTarget.GetComponentInChildren<AIRig>();

                if (tRig != null)
                {
                    int knightHealth = (int)tRig.AI.WorkingMemory.GetItem("Health");
                    int knightLoyalty = (int)tRig.AI.WorkingMemory.GetItem("Loyalty");
                    int knightHunger = (int)tRig.AI.WorkingMemory.GetItem("Hunger");

                    Slider healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
                    Slider loyaltySlider = GameObject.Find("LoyaltySlider").GetComponent<Slider>();
                    Slider hungerSlider = GameObject.Find("HungerSlider").GetComponent<Slider>();

                    healthSlider.value = knightHealth;
                    loyaltySlider.value = knightLoyalty;
                    hungerSlider.value = knightHunger;
                }
            }
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
                        KnightUI.SetActive(false);
                    }
				}
            } else
            {
                isFollowing = false;
            }
        }
    }

    
}
