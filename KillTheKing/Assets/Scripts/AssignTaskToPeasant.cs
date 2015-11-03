using UnityEngine;
using System.Collections;
using RAIN.Core;

// Assign a task to a peasant after clicking on them
public class AssignTaskToPeasant : MonoBehaviour 
{
	private GameObject peasant;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for mouse input
		if (Input.GetMouseButtonDown (0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;

			TextMesh[] texts = new TextMesh[3];

			if (Physics.Raycast (mouseRay, out hit, 20.0f))
			{
				if (hit.collider.gameObject.tag == "Peasant")
				{
					if (peasant == null)
					{
						peasant = hit.collider.gameObject;
					}
					else if (peasant.name != hit.collider.gameObject.name)
					{
						peasant = hit.collider.gameObject;

						for (int i = 0; i < texts.Length; i++)
						{
							texts[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
							texts[i].gameObject.GetComponent<BoxCollider>().enabled = false;
						}
					}

					texts = hit.collider.gameObject.GetComponentsInChildren<TextMesh> ();

					Debug.Log (texts.Length);
					for (int i = 0; i < texts.Length; i++)
					{
						texts[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
						texts[i].gameObject.GetComponent<BoxCollider>().enabled = true;
					}
				}
				else if (hit.collider.gameObject.tag == "Command")
				{
					AIRig peasantAI = peasant.GetComponentInChildren<AIRig>();

					TextMesh command = hit.collider.GetComponent<TextMesh>();

					peasantAI.AI.WorkingMemory.SetItem<string>("trapToLay", command.text);

					texts = hit.collider.gameObject.GetComponentsInChildren<TextMesh>();

					Debug.Log (texts.Length);

					for (int i = 0; i < texts.Length; i++)
					{
						texts[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
						texts[i].gameObject.GetComponent<BoxCollider>().enabled = false;
					}
				}
			}
		}
	}
}
