using UnityEngine;
using System.Collections;
using RAIN.Core;

// Hire a peasant to do the selected task
public class HirePeasant : MonoBehaviour 
{
	private GameObject peasant;
	private bool specifyLoc = false;		// Whether we need to specify a location for a given action
	private TextMesh[] texts = new TextMesh[3];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for mouse input
		if (Input.GetMouseButtonDown (0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;

			// Raycast to see if we hit an interactable character
			if (Physics.Raycast (mouseRay, out hit, 20.0f))
			{
				if (specifyLoc == true)
				{
					// If we hit a valid point
					if (hit.collider != null)
					{
						// Set the action location to be that point
						peasant.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<Vector3>("actionLocation", hit.point);
						specifyLoc = false;
						return;
					}
					else
						return;
				}

				if (hit.collider.tag == "Peasant")
				{
					// If we haven't clicked on a peasant yet
					if (peasant == null)
					{
						peasant = hit.collider.gameObject;
					}

					// If we've clicked on a peasant, but haven't assigned a task yet.
					else if (peasant.name != hit.collider.gameObject.name)
					{
						// Turn off all of the actions for the current peasant selected
						for (int i = 0; i < texts.Length; i++)
						{
							texts[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
							texts[i].gameObject.GetComponent<BoxCollider>().enabled = false;
						}

						// And select the new peasant
						peasant = hit.collider.gameObject;
					}

					// Turn on all of the options of the peasant clicked on
					texts = hit.collider.gameObject.GetComponentsInChildren<TextMesh>();

					for (int i = 0; i < texts.Length; i++)
					{
						texts[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
						texts[i].gameObject.GetComponent<BoxCollider>().enabled = true;
					}
				}
				// If we select a command
				else if (hit.collider.tag == "Command")
				{
					AIRig peasantAI = peasant.GetComponentInChildren<AIRig>();

					// The id of the command to issue to the peasant
					int commandID = int.Parse (hit.collider.gameObject.name);

					// Check to see if we need to specify a location as well
					if (commandID == 1)
						specifyLoc = true;

					peasantAI.AI.WorkingMemory.SetItem<int>("actionToDo", commandID);

					// Once we have selected the command, turn off all the actions
					for (int i = 0; i < texts.Length; i++)
					{
						texts[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
						texts[i].gameObject.GetComponent<BoxCollider>().enabled = false;
					}

					texts.Initialize();
				}
			}
		}
	}
}
