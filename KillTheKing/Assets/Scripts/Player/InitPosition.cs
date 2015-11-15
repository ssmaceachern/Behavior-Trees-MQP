using UnityEngine;
using System.Collections;
using RAIN.Core;

public class InitPosition : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		AIRig ai = GetComponentInChildren <AIRig> ();

		ai.AI.WorkingMemory.SetItem<Vector3> ("InitPos", transform.position);
		ai.AI.WorkingMemory.SetItem<Vector3> ("Location", transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
