using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;
using RAIN.BehaviorTrees;

public class HireUnitSetLocation : MonoBehaviour {

	private bool givePosition = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.GetMouseButtonDown(0) && givePosition)
		{
			//.Log ("Clicking");
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit, 200.0f) != false)
			{
				GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<Vector3>("Location", hit.point);
				givePosition = false;
			}

			EntityRig pEnt = GetComponentInChildren<AIRig>().AI.Body.GetComponentInChildren<EntityRig> ();

			if(pEnt!=null)
			{
				pEnt.Entity.GetAspect("Good").IsActive=true;
				//pEnt.Entity.ActivateEntity();
			}
		}
	}
	
	void OnSelect(string command)
	{
		if (!givePosition) {
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("Command", command);
			givePosition = true;
		}
	}
}
