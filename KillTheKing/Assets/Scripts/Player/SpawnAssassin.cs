using UnityEngine;
using System.Collections;

public class SpawnAssassin : MonoBehaviour 
{
	public bool buttonClicked = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (buttonClicked)
		{
			if (Input.GetMouseButtonDown (0))
			{
				RaycastHit hit;
				Ray placement = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast(placement, out hit, 100.0f) != false)
				{
					// If we hit the trigger around the king
					if (hit.collider.gameObject.tag == "King")
					{
						Debug.Log ("Cannot place assassin here");
						buttonClicked = false;
						return;
					}
					else
					{
						GameObject newAssassin = (GameObject) GameObject.Instantiate (Resources.Load ("Assassin"));

						newAssassin.transform.position = hit.point;

						GameObject chars = GameObject.FindGameObjectWithTag ("Characters");

						newAssassin.transform.parent = chars.transform;

						this.enabled = false;
					}
				}
			}
		}
	}

	public void ButtonClicked()
	{
		buttonClicked = true;
	}
}
