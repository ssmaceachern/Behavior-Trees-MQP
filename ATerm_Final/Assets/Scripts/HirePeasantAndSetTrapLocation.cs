using UnityEngine;
using System.Collections;
using RAIN.Core;

public class HirePeasantAndSetTrapLocation : MonoBehaviour 
{
	private bool trapSet = false;	// Whether we have been ordered to lay a trap

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.GetMouseButtonDown(0) && trapSet)
		{
			//.Log ("Clicking");

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 200.0f) != false)
			{
				GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<Vector3>("trapPlacement", hit.point);
				trapSet = false;
			}
		}

	}

	void OnSelect(string command)
	{
		GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("trapToLay", command);
		trapSet = true;
	}
}
